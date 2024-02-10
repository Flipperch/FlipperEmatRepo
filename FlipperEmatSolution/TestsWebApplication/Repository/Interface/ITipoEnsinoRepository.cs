using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface ITipoEnsinoRepository
    {
        int Add(TipoEnsino tipoEnsino);
        int Delete(int id);
        int Edit(TipoEnsino tipoEnsino);
        TipoEnsino Get(int id);
        List<TipoEnsino> GetAll();
    }
}