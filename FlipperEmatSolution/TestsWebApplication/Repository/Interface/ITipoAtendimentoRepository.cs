using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface ITipoAtendimentoRepository
    {
        int Add(TipoAtendimento tipoAtendimento);
        int Delete(int id);
        int Edit(TipoAtendimento tipoAtendimento);
        TipoAtendimento Get(int id);
        List<TipoAtendimento> GetAll();
    }
}