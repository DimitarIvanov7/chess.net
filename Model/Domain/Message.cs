
namespace WebApplication3.Model.Domain
{
    public class Message
    {

        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public Guid ThreadId { get; set; }  
        public string Data { get; set; } 

        public Player Sender { get; set; }
        public Player Receiver { get; set; }

        public Thread Thread { get; set; }


    }
}
