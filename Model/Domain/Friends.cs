using WebApplication3.Model;

namespace WebApplication3.Model.Domain
{
    public class Friends
    {

        public int Id { get; set; }

        public Guid PlayerOneId { get; set; }

        public Guid PlayerTwoId { get; set; }

        public DomainEnums.FriendStates States { get; set; }

        public Player PlayerOne { get; set; }   

        public Player PlayerTwo { get; set; } 
    }
}
