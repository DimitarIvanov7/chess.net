using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Features.Players.Entities;

public class BaseController : Controller
{
    IRepository<PlayerEntity> playersRepository;
    public BaseController(IRepository<PlayerEntity> playersRepository)
    {
        this.playersRepository = playersRepository;
    }
    protected async Task<PlayerEntity?> GetPlayerFromToken()
    {
        var username = User.FindFirstValue(ClaimTypes.Name);
        if (username == null)
            throw new UnauthorizedAccessException("Player not found in token");


        var sender = await playersRepository.GetByPropertyAsync(x => x.Username, username);
        if (sender == null)  return null;

        return sender;
    }
}