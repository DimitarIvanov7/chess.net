

using System.ComponentModel.DataAnnotations;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Features.Games.Entities;

namespace WebApplication3.Domain.Features.Players.Entities
{
    public class PlayerEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public int Elo { get; set; } = 100;
        public string Username { get; set; }
        public string Email { get; set; }

        public virtual ICollection<GameEntity> WhiteGames { get; set; }
        public virtual ICollection<GameEntity> BlackGames { get; set; }
    }

}
