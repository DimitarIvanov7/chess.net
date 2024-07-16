
namespace WebApplication3.Model.Domain
{
    public class Thread
    {

        public Guid Id { get; set; }

        public Player PlayerOne { get; set; }
        
        public Player PlayerTwo { get; set; }


        public ICollection<Message> Messages { get; set; }




    }
}
