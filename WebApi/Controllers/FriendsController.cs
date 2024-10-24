using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Domain.Features.Friends.Entities;
using WebApplication3.Domain.Features.Friends.Repository;
using WebApplication3.Domain.Abstractions.Data;
using System.Threading.Tasks;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Features.Friends.Dtos;
using WebApplication3.Domain.Features.Players.Dtos;

namespace WebApplication3.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendsController : BaseController
    {
        private readonly FriendsRepositorySql friendsRepository;
        private readonly IUnitOfWork unitOfWork;

        public FriendsController(FriendsRepositorySql friendsRepository, IRepository<PlayerEntity> playersRepository, IUnitOfWork unitOfWork)
            : base(playersRepository)
        {
            this.friendsRepository = friendsRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public async Task<IActionResult> GetAllFriends([FromQuery] FriendFilterDto filterDto)
        {
            var currentPlayer = await GetPlayerFromToken();
            if (currentPlayer == null)
                return NotFound("Player not found");

            var friends = await friendsRepository.GetListByIdAsync(currentPlayer.Id);
            var filteredFriends = friends.Where(f =>
                (f.PlayerOneId == currentPlayer.Id || f.PlayerTwoId == currentPlayer.Id) &&
                (filterDto.Status == null || f.Status == filterDto.Status)
            ).ToList();

            var friendDtos = filteredFriends.Select(f => new FriendResponseDto
            {
                Player = f.PlayerOneId == currentPlayer.Id ? new PlayerResponseDto
                {
                    Id = f.PlayerTwo.Id,
                    Username = f.PlayerTwo.Username,
                    Email = f.PlayerTwo.Email
                } : new PlayerResponseDto
                {
                    Id = f.PlayerOne.Id,
                    Username = f.PlayerOne.Username,
                    Email = f.PlayerOne.Email
                },
                State = f.Status
            }).ToList();

            return Ok(friendDtos);
        }

        [HttpPost]
        [Authorize]
        [Route("send")]
        public async Task<IActionResult> SendFriendRequest([FromBody] CreateFriendRequestDto friendRequestDto)
        {
            var currentPlayer = await GetPlayerFromToken();
            if (currentPlayer == null)
                return NotFound("Player not found");

            var friendRequest = new FriendsEntity
            {
                PlayerOneId = currentPlayer.Id,
                PlayerTwoId = friendRequestDto.ReceiverId,
                Status = FriendStates.Requested
            };

            friendsRepository.Add(friendRequest);
            await unitOfWork.SaveChangesAsync();

            return Ok("Friend request sent");
        }

        [HttpPut]
        [Authorize]
        [Route("change-status")]
        public async Task<IActionResult> ChangeFriendRequestStatus([FromBody] ChangeFriendRequestStatusDto changeStatusDto)
        {
            var currentPlayer = await GetPlayerFromToken();
            if (currentPlayer == null)
                return NotFound("Player not found");

            var friendRequest = await friendsRepository.GetByIdAsync(changeStatusDto.RequestId);
            if (friendRequest == null)
                return NotFound("Friend request not found");

            if (friendRequest.PlayerTwoId != currentPlayer.Id)
                return Forbid("You are not authorized to change the status of this friend request");

            if (changeStatusDto.Status == FriendStates.Rejected)
            {
                friendsRepository.Remove(friendRequest);
                await unitOfWork.SaveChangesAsync();
                return Ok("Friend request rejected and deleted");
            }

            friendRequest.Status = changeStatusDto.Status;
            friendsRepository.Update(friendRequest);
            await unitOfWork.SaveChangesAsync();

            return Ok("Friend request status changed");

        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{requestId:Guid}")]
        public async Task<IActionResult> DeleteFriend([FromRoute] Guid requestId)
        {
            var currentPlayer = await GetPlayerFromToken();
            if (currentPlayer == null)
                return NotFound("Player not found");

            var friendRequest = await friendsRepository.GetByIdAsync(requestId);
            if (friendRequest == null)
                return NotFound("Friend request not found");

            if (friendRequest.PlayerOneId != currentPlayer.Id && friendRequest.PlayerTwoId != currentPlayer.Id)
                return Forbid("You are not authorized to delete this friend request");

            friendsRepository.Remove(friendRequest);
            await unitOfWork.SaveChangesAsync();

            return Ok("Friend request deleted");
        }
    }
}
