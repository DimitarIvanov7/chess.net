using MediatR.NotificationPublishers;
using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Database;

namespace WebApplication3.Model.Domain.Games.Entities
{
    public class GameTypeEntity : IEntity
    {

        public Guid Id { get; set; }

        public PlayStateTypes Type { get; set; }
        public PlayStateSubTypes SubType { get; set; }



    }
}
