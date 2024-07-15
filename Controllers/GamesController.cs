using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Model.DTO.Player;
using WebApplication3.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IPlayerRepository GameRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PlayerController> logger;

        public GamesController(IPlayerRepository gameRepository, IMapper mapper, ILogger<PlayerController> logger)

        {
            this.GameRepository = gameRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var playersDomain = await playerRepository.GetListAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            var playersDto = mapper.Map<List<PlayerResponseDto>>(playersDomain);

            return Ok(playersDto);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        }
    }
}
