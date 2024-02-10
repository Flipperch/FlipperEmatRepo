using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface IAreaRepository
    {
        int Add(Area area);
        int Delete(int id);
        int Edit(Area area);
        Area Get(int id);
        List<Area> GetAll();
        List<Area> GetAreasDisciplinas();
    }
}