using WebApplication3.Model.Domain;

namespace WebApplication3.Model.DTO.MessageDto
{
    public class MessageResponseDto
    {

        public Player Sender { get; set; }

        public DateTime DateTime { get; set; }  

        public string Message { get; set; } 



    }
}
