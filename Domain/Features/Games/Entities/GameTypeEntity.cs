using MediatR.NotificationPublishers;
using WebApplication3.Domain.Constants;

namespace WebApplication3.Model.Domain.Games.Entities
{
    public class GameTypeEntity
    {

        public int Id { get; set; }

        public PlayStateSubTypes Type { get; set; }



    }
}
