using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface IUsuarioRepository
    {
        int Add(Usuario usuario);
        int Delete(int id);
        int Edit(Usuario usuario);
        Usuario Get(int id);
        List<Usuario> GetAll();
    }
}
