using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface IDisciplinaRepository
    {
        int Add(Disciplina disciplina);
        int Delete(int id);
        int Edit(Disciplina disciplina);
        Disciplina Get(int id);
        List<Disciplina> GetAll();
        List<Disciplina> GetDisciplinasTiposAtendimento();
    }
}