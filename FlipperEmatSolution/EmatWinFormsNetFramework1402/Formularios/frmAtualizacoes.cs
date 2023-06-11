using System;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmAtualizacoes : Form
    {
        public frmAtualizacoes()
        {
            InitializeComponent();
        }

        private void frmAtualizacoes_Load(object sender, EventArgs e)
        {
            label1.Text = "Versão Atual: " + Utils.csControleVersao.versao_sistema();

            rtbLista.Text = lista_atualizações();
        }

        private string lista_atualizações()
        {
            string lista = "";
            lista += "Versão: - 1.4";
            lista += "\nData de lançamento da versão - 25/06/2018";
            lista += "\nAlteração no Icone";
            lista += "\nAlteração na função de Busca pelo CEP (Interno);"; //18/08/2017 - Erro no sistema em 02/10/2017 por conta da dll Correios.Net 
            lista += "\n";
            lista += "\n\n\n";
            //________________________________________________________
            lista += "Versão: - 1.3.2.0";
            lista += "\nData de lançamento da versão - ";
            lista += "\nAlteração na função de Busca pelo CEP (Interno);"; //18/08/2017 - Erro no sistema em 02/10/2017 por conta da dll Correios.Net 
            lista += "\n";
            lista += "\n\n\n";
            //________________________________________________________
            lista += "Versão: - 1.3.1.2";
            lista += "\nData de lançamento da versão - 31/08/2017";          
            lista += "\nTela Cadastro e Edição de Alunos (Alteração no comportamento das caixas de seleção.)"; //18/08/2017 - Reclamação Sorocaba
            lista += "\nTela Passaporte (Atribuido aos grupos a possibilidade de exclusão de atendimentos.)";
            lista += "\n\n\n";
            //Relatório Histórico Sorocaba Alterado (Cabeçalho) Claudio Ceeja Sorocaba
            //________________________________________________________
            lista += "Versão: - 1.3.1.1";
            lista += "\nData de lançamento da versão - 24/08/2017";            
            lista += "\nTela Inativar Alunos (Inclusão da Tela para a listagem e inativação de alunos filtrados pela data da ultima presença.)"; //18/08/2017 - Reclamação Sorocaba
            lista += "\nRelatório Lista de Inativações (Inclusão de lista de Inativação.)";
            lista += "\n\n\n";
            //________________________________________________________
            lista += "Versão: - 1.3.1.0";
            lista += "\nData de lançamento da versão - 18/08/2017";//18/08/2017 - Reclamação Sorocaba
            lista += "\nPassaporte Otimizado (Redução de problemas de lags, Melhora na performance de salvamente)"; //18/08/2017 - Reclamação Sorocaba
            lista += "\nPassaporte Otimizado (Incluída a possibilidade de exclusão pelo 'passaporte completo' pelo admin, dir, sec e ger)"; //18/08/2017 - Reclamação Sorocaba
            lista += "\nHistórico Otimizado (Inclusão de opção para impressão de médias (coluna de caixa de seleção)"; //18/08/2017 - Solicitação de Americana
            lista += "\nHistórico Otimizado (Inclusão de opção de médias extra, #Alteração também no BD)"; //18/08/2017 - Solicitação de Americana
            lista += "\nHistórico Otimizado (Exclusões pelo histórico também apagam atendimentos de média)"; //18/08/2017 - Flipper            
            lista += "\nTela Atualizações  (Inclusão de Tela de Lista de Atualizações!"; //18/08/2017 - Flipper

            return lista;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
