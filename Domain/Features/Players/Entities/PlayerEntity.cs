

using System.ComponentModel.DataAnnotations;
using WebApplication3.Domain.Database;

namespace WebApplication3.Domain.Features.Players.Entities
{
    public class PlayerEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public int Elo { get; set; } = 100;
        public string Username { get; }
        public string Email { get; }
    }

}
