using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Text.Json;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Features.ChessLogic;
using WebApplication3.Domain.Features.Messages.Entities;
using WebApplication3.Domain.Features.Messages.Repository;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : BaseController
    {
        private readonly MessagesRepositorySql messagesRepository;
        private readonly IRepository<PlayerEntity> playersRepository;
        private readonly IMapper mapper;
        private readonly ILogger<MessagesController> logger;
        private readonly IUnitOfWork unitOfWork;

        public MessagesController(MessagesRepositorySql messagesRepository, IRepository<PlayerEntity> playersRepository, IMapper mapper, ILogger<MessagesController> logger, IUnitOfWork unitOfWork) : base(playersRepository)
        {
            this.messagesRepository = messagesRepository;
            this.playersRepository = playersRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<MessageResponseDto>> GetMessageById([FromRoute] Guid id)
        {
            var message = await messagesRepository.GetByIdAsync(id);
            if (message == null)
                return NotFound("No such message");

            var messageDto = mapper.Map<MessageResponseDto>(message);
            return Ok(messageDto);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<MessageResponseDto>> CreateMessage(CreateMessageDto messageDto)
        {
            if (messageDto == null)
                return BadRequest();

            var player = await GetPlayerFromToken();
            if (player == null)
                return NotFound("Player not found");

            var receiver = await playersRepository.GetByIdAsync(messageDto.ReceiverId);
            if (receiver == null)
                return BadRequest($"No such receiver with ID {messageDto.ReceiverId}");

            var messageEntity = mapper.Map<MessageEntity>(messageDto);

            messageEntity.SenderId = player.Id;
            messagesRepository.Add(messageEntity);
            await unitOfWork.SaveChangesAsync();

            var createdMessageDto = mapper.Map<MessageResponseDto>(messageEntity);
            return CreatedAtAction(nameof(GetMessageById), new { id = createdMessageDto.Id }, createdMessageDto);
        }

        [HttpGet]
        [Route("player/all-messages")]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult<IEnumerable<MessageGroupedBySenderDto>>> GetMessagesByPlayer()
        {
            var player = await GetPlayerFromToken();
            if (player == null)
                return NotFound("Player not found");

            var messages = await messagesRepository.GetListByIdsAsync(filterQuery: player.Id, filterOn: "ReceiverId");
            var groupedMessages = messages.GroupBy(m => m.SenderId)
                                          .Select(g => new MessageGroupedBySenderDto
                                          {
                                              SenderId = g.Key,
                                              Messages = mapper.Map<List<MessageResponseDto>>(g.OrderBy(m => m.SendDate).ToList())
                                          }).ToList();


            return Ok(groupedMessages);
        }

        [HttpGet]
        [Route("sender/{senderName}")]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<IEnumerable<MessageResponseDto>>> GetMessagesBySender([FromRoute] string senderName)
        {
            var currentPlayer = await GetPlayerFromToken();
            if (currentPlayer == null)
                return NotFound("Player not found");

            var sender = await playersRepository.GetByPropertyAsync(p => p.Username, senderName);
            if (sender == null)
                return NotFound($"No player found with username {senderName}");

            var messages = await messagesRepository.GetListByIdsAsync(filterQuery: currentPlayer.Id, filterOn: "ReceiverId");
            var filteredMessages = messages.Where(m => m.SenderId == sender.Id).ToList();

            var messageDtos = mapper.Map<List<MessageResponseDto>>(filteredMessages);
            return Ok(messageDtos);
        }
    }
}