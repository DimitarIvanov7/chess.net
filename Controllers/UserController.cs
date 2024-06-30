using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly ChessDbContext dbContext;

        public UserController(ChessDbContext dbContext)
   
        {
            this.dbContext = dbContext; 

            
        }



        [HttpGet]
        public IActionResult GetAllUsers()
        {
        
            var users = this.dbContext.Players.ToList();

            return Ok(users);
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Players>> GetPlayer(int id)
        {
            try
            {
                var result = await dbContext.FindAsync<Players>(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Players>> CreatePlayer(Players player)
        {
            try
            {
                if (player == null)
                    return BadRequest();

                var createdPlayer = await dbContext.AddAsync<Players>(player);

                return CreatedAtAction(nameof(GetAllUsers),
                    new { id = createdPlayer.Entity.Id}, createdPlayer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Player record");
            }
        }
    }
}
