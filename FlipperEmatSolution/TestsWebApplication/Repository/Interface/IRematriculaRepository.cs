using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface IRematriculaRepository
    {
        int Add(Rematricula rematricula);
        int Delete(int id);
        int Edit(Rematricula rematricula);
        Rematricula Get(int id);
        List<Rematricula> GetAll();
    }
}