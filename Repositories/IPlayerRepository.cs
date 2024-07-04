namespace WebApplication3.Repositories
{
    public interface IPlayerRepository
    {
        public Task<List<Player>> ToListAsync();

        public Task<Player> GetByIdAsync(Guid id);

        public Task AddAsync(Player player);

        public Task<Player> UpdateAsync(Guid id, Player player);


        public Task Remove(Player player);






    }
}
