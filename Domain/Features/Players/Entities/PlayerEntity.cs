

using Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Domain.Features.Players.Entities
{
    public class PlayerEntity : Entity
    {
        public PlayerEntity(Guid id, string username, string email, string password) : base(id)
        {
            Username = username;
            Email = email;
            Password = password;
        }



        public string Password { get; set; }
        public int Elo { get; set; } = 100;
        public string Username { get; }
        public string Email { get; }
    }

}
