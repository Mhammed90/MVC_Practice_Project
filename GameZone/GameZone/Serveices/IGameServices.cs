namespace GameZone.Serveices
{
    public interface IGameServices
    {
        Task CreateGame(CreateGameVeiwModle model);
        Game? GetById(int id);
        bool Delete(int id); 
        Task<Game?> Edit(EditGameViewModle modle);

        IEnumerable<Game> GetAll();
    }
}
