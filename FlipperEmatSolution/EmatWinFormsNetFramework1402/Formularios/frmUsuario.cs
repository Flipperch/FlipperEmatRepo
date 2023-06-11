using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// Tela responsável pelo cadastro e configuração de usuários no sistema
/// </summary>
namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO:: FORM frmUsuario NÃO CONCLUÍDO... VERIFICAR SE VALE A PENA TERMINAR OU DEIXAR PARA REALIZAR ALTERAÇÕES PELO ASP.NET
    public partial class frmUsuario : Form
    {
        private readonly IEmatriculaSettings _settings;
        List<Modelo.USUARIO> listaUsuarios;
        Modelo.USUARIO usuarioSelecionado;

        public frmUsuario(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            cmbNivel.DataSource = Enum.GetValues(typeof(Enumeradores.NivelAcesso));

            var disciplinas_ = _settings.Context.DISCIPLINA;
            cmbDisciplina.DataSource = disciplinas_.Select(d => d.NOME);

            CarregarUsuarios();

            Cursor.Current = Cursors.Default;

        }

        private void CarregarUsuarios()
        {
            var usuarios_ = _settings.Context.USUARIO.Include("PROFESSOR.DISCIPLINA");
            //var professores_ = Utils.Settings.Context.PROFESSOR.Include("DISCIPLINA").Include("AREA");


            listaUsuarios = usuarios_.ToList();

            dataGridView1.DataSource = (from usuario_
                                   in listaUsuarios
                                        where usuario_.ATIVO
                                        orderby usuario_.NOME
                                        select new
                                        {
                                            Nome = usuario_.NOME,
                                            Usuário = usuario_.NOME_ACESSO,
                                            RG = usuario_.RG,
                                            Nível = (Enumeradores.NivelAcesso)usuario_.NIVEL_ACESSO,
                                            Disciplina = usuario_.PROFESSOR != null ?
                                                usuario_.PROFESSOR.DISCIPLINA.NOME : "NÃO SE APLICA"
                                        }).ToList();



            //var nonNullItems = list.Select(x => x.MyProperty).OfType<T>();
        }

        private void ptbFoto_Click(object sender, EventArgs e)
        {
            //Obter Foto do Usuário do Sistema para a carteirinha

            if (usuarioSelecionado != null)
            {

            }
        }

        private void btnSenha_Click(object sender, EventArgs e)
        {
            //Atribuir senha padrão para o usuário selecionado

            if (usuarioSelecionado != null)
            {

            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            //Salvar

            if (usuarioSelecionado != null)
            {

            }
        }
    }
}
