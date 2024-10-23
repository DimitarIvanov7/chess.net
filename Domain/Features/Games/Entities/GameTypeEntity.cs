using MediatR.NotificationPublishers;
using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Database;

namespace WebApplication3.Model.Domain.Games.Entities
{
    public class GameTypeEntity : IEntity
    {

        public Guid Id { get; set; }

        public PlayStateSubTypes Type { get; set; }



    }
}
