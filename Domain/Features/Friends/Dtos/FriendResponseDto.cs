using WebApplication3.Domain.Features.Players.Dtos;
using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.Friends.Dtos
{
    public class FriendResponseDto
    {

        public PlayerResponseDto Player { get; set; }

        public FriendStates State { get; set; }


    }
}

public class ChangeFriendRequestStatusDto
{
    public Guid RequestId { get; set; }
    public FriendStates Status { get; set; }
}


public class CreateFriendRequestDto
{
    public Guid ReceiverId { get; set; }
}

public class FriendFilterDto
{
    public FriendStates? Status { get; set; }
}