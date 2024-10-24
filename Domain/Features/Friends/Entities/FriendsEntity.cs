using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Friends.Entities
{
    public class FriendsEntity: IEntity
    {

        public Guid Id { get; set; }

        public Guid PlayerOneId { get; set; }

        public Guid PlayerTwoId { get; set; }

        public FriendStates Status { get; set; }

        public PlayerEntity PlayerOne { get; set; }

        public PlayerEntity PlayerTwo { get; set; }
    }
}
