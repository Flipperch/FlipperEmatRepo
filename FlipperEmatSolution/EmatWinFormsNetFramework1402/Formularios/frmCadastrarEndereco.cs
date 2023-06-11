using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmCadastrarEndereco : Form
    {
        List<Classes.Pais> listaPais;
        List<Classes.Uf> listaUfPais;
        List<Classes.Uf> listaUfCidade;
        List<Classes.Cidade> listaCidadeUf;
        int? codigoSelecionado;

        public frmCadastrarEndereco()
        {
            InitializeComponent();
        }

        private void frmCadastrarEndereco_Load(object sender, EventArgs e)
        {
            PreencherPais();
        }

        private void PreencherPais()
        {
            dgvPais.Rows.Clear();

            listaPais = DAO.PaisDAO.ExibirTodos();

            foreach (Classes.Pais item in listaPais)
            {
                dgvPais.Rows.Add();
                dgvPais.Rows[dgvPais.Rows.Count - 1].Cells[0].Value = item.Codigo;
                dgvPais.Rows[dgvPais.Rows.Count - 1].Cells[1].Value = item.Nome;
            }

            cmbPais.DataSource = listaPais;
            cmbPais.DisplayMember = "Nome";
            cmbPais.ValueMember = "Codigo";

            cmbPaisCidade.DataSource = listaPais;
            cmbPaisCidade.DisplayMember = "Nome";
            cmbPaisCidade.ValueMember = "Codigo";
        }

        private void PreencherUf(Classes.Pais pais)
        {
            dgvUf.Rows.Clear();

            listaUfPais = DAO.UfDAO.ExibirTodos(pais); //dbModel.UF.Select(x => x).Where(x => x.COD_PAIS == pais.CODIGO).ToList();

            foreach (Classes.Uf item in listaUfPais)
            {
                dgvUf.Rows.Add();
                dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[0].Value = item.Codigo;
                dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[1].Value = item.Nome;
                dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[2].Value = item.Sigla;
            }

            cmbUf.DataSource = listaUfPais;
            cmbUf.DisplayMember = "Nome";
            cmbUf.ValueMember = "Codigo";
        }

        private void PreencherCidade(Classes.Uf uf)
        {
            dgvCidade.Rows.Clear();

            listaCidadeUf = DAO.CidadeDAO.ExibirTodos(uf); //dbModel.CIDADE.Select(x => x).Where(x => x.COD_UF == uf.CODIGO).ToList();
            foreach (Classes.Cidade item in listaCidadeUf)
            {
                dgvCidade.Rows.Add();
                dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[0].Value = item.Codigo;
                dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[1].Value = item.Nome;
            }
        }

        //TODO:: Verificar exclusão do código a baixo
        //private bool InserirPais(Model.PAIS pais)
        //{
        //    try
        //    {
        //        Model.dbModel dbModel = new Model.dbModel();
        //
        //        dbModel.PAIS.Add(pais);
        //
        //        dbModel.SaveChanges();
        //
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    
        //}

        //private bool InserirUf(Model.UF uf)
        //{
        //    try
        //    {
        //        Model.dbModel dbModel = new Model.dbModel();
        //
        //        dbModel.UF.Add(uf);
        //
        //        dbModel.SaveChanges();
        //
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    
        //}

        //private bool InserirCidade(Model.CIDADE cidade)
        //{
        //    try
        //    {
        //        Model.dbModel dbModel = new Model.dbModel();
        //
        //        dbModel.CIDADE.Add(cidade);
        //
        //        dbModel.SaveChanges();
        //
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    
        //}

        //private void AtualizarPais(Model.PAIS pais)
        //{
        //    try
        //    {
        //        dbModel.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        throw;
        //    }
        //}

        //private void AtualizarUf(Model.UF uf)
        //{
        //    try
        //    {
        //        dbModel.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        throw;
        //    }
        //}

        //private void AtualizarCidade(Model.CIDADE cidade)
        //{
        //    try
        //    {
        //        dbModel.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        throw;
        //    }
        //}



        //TODO:: Verificar método a baixo
        private void ExcluirPais(Modelo.PAIS pais)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        //TODO:: Verificar método a baixo
        private void ExcluirUf(Modelo.UF UF)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        //TODO:: Verificar método a baixo
        private void ExcluirCidade(Modelo.CIDADE cidade)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnAddPais_Click(object sender, EventArgs e)
        {
            if (!listaPais.Exists(x => x.Nome == txtNomePais.Text) && txtNomePais.Text != string.Empty)
            {
                if (codigoSelecionado != null)
                {
                    Classes.Pais pais = new Classes.Pais();
                    pais.Nome = txtNomePais.Text;
                    DAO.PaisDAO.Atualizar(pais);
                }
                else
                {
                    if (MessageBox.Show(
                    string.Format("Deseja realmente incluir o País de nome {0}?", txtNomePais.Text),
                    "Novo País", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Classes.Pais pais = new Classes.Pais();
                        pais.Nome = txtNomePais.Text;
                        if (DAO.PaisDAO.Inserir(pais) > 0)
                        {
                            listaPais.Add(pais);
                            PreencherPais();

                            txtNomePais.Text = string.Empty;
                        };
                    }
                }
            }
            else
            {
                MessageBox.Show("País ja cadastrado.", "Ematrícula");
            }
        }

        private void btnAddUf_Click(object sender, EventArgs e)
        {
            if (codigoSelecionado != null)
            {
                Classes.Uf uf = new Classes.Uf();
                uf.Nome = txtNomeUf.Text;
                uf.Sigla = txtSiglaUf.Text;
                uf.Pais = (Classes.Pais)cmbPais.SelectedItem;
                uf.Nome = txtNomeUf.Text;
                DAO.UfDAO.Atualizar(uf);
            }
            else
            {
                if (!listaUfPais.Exists(x => x.Nome == txtNomeUf.Text) && txtNomeUf.Text != string.Empty)
                {
                    if (MessageBox.Show(
                        string.Format("Deseja realmente incluir a UF de nome {0}?", txtNomeUf.Text),
                        "Nova UF", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Classes.Uf uf = new Classes.Uf();
                        uf.Nome = txtNomeUf.Text;
                        uf.Sigla = txtSiglaUf.Text;
                        uf.Pais = (Classes.Pais)cmbPais.SelectedItem;
                        uf.Nome = txtNomeUf.Text;
                        if (DAO.UfDAO.Inserir(uf) > 0)
                        {
                            dgvUf.Rows.Add();
                            dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[0].Value = uf.Codigo;
                            dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[1].Value = uf.Nome;
                            dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[2].Value = uf.Sigla;

                            txtNomeUf.Text = string.Empty;
                            txtSiglaUf.Text = string.Empty;
                        };
                    }
                }
                else
                {
                    MessageBox.Show("UF ja cadastrado.", "Ematrícula");
                }
            }
        }

        private void btnAddCidade_Click(object sender, EventArgs e)
        {
            if (codigoSelecionado != null)
            {
                Classes.Cidade cidade = new Classes.Cidade();
                cidade.Nome = txtNomeCidade.Text;
                DAO.CidadeDAO.Atualizar(cidade);
            }
            else
            {

                if (!listaCidadeUf.Exists(x => x.Nome == txtNomeCidade.Text) && txtNomeCidade.Text != string.Empty)
                {
                    if (MessageBox.Show(
                        string.Format("Deseja realmente incluir a Cidade de nome {0}?", txtNomeCidade.Text),
                        "Nova Cidade", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Classes.Cidade cidade = new Classes.Cidade();
                        cidade.Nome = txtNomeCidade.Text;
                        cidade.Uf = ((Classes.Uf)cmbUf.SelectedItem);
                        if (DAO.CidadeDAO.Inserir(cidade) > 0)
                        {
                            //dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[0].Value = cidade.Codigo;
                            //dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[1].Value = cidade.Nome;

                            txtNomeCidade.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cidade ja cadastrada.", "Ematrícula");
                }
            }
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPais.SelectedIndex > -1)
            {
                listaUfPais = DAO.UfDAO.ExibirTodos(((Classes.Pais)cmbPais.SelectedItem));
                //Select(x => x).
                //Where(x => x.COD_PAIS == ((Model.PAIS)cmbPais.SelectedItem).CODIGO).ToList();

                dgvUf.Rows.Clear();

                foreach (Classes.Uf item in listaUfPais)
                {
                    dgvUf.Rows.Add();
                    dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[0].Value = item.Codigo;
                    dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[1].Value = item.Nome;
                    dgvUf.Rows[dgvUf.Rows.Count - 1].Cells[2].Value = item.Sigla;
                }
            }
        }

        private void cmbPaisCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaisCidade.SelectedIndex > -1)
            {

                listaUfCidade = DAO.UfDAO.ExibirTodos(((Classes.Pais)cmbPaisCidade.SelectedItem));
                //Select(x => x).
                //Where(x => x.COD_PAIS == ().ToList();

                cmbUf.DataSource = null;

                cmbUf.DataSource = listaUfCidade;
                cmbUf.DisplayMember = "Nome";
                cmbUf.ValueMember = "Codigo";

            }
        }

        private void cmbUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUf.SelectedIndex > -1)
            {
                listaCidadeUf = DAO.CidadeDAO.ExibirTodos((Classes.Uf)cmbUf.SelectedItem);
                //Select(x => x).
                //Where(x => x.COD_UF == .ToList();

                dgvCidade.Rows.Clear();

                foreach (Classes.Cidade item in listaCidadeUf)
                {
                    dgvCidade.Rows.Add();
                    dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[0].Value = item.Codigo;
                    dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[1].Value = item.Nome;
                }
            }
        }

        private void dgvPais_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            codigoSelecionado = Convert.ToInt32(dgvPais.Rows[dgvPais.CurrentRow.Index].Cells[0].Value);
            txtNomePais.Text = dgvPais.Rows[dgvPais.CurrentRow.Index].Cells[1].Value.ToString();
        }

        private void dgvUf_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            codigoSelecionado = Convert.ToInt32(dgvUf.Rows[dgvUf.CurrentRow.Index].Cells[0].Value);
            txtNomeUf.Text = dgvUf.Rows[dgvUf.CurrentRow.Index].Cells[1].Value.ToString();
            txtSiglaUf.Text = dgvUf.Rows[dgvUf.CurrentRow.Index].Cells[2].Value.ToString();
        }

        private void dgvCidade_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            codigoSelecionado = Convert.ToInt32(dgvCidade.Rows[dgvCidade.CurrentRow.Index].Cells[0].Value);
            txtNomeCidade.Text = dgvCidade.Rows[dgvCidade.CurrentRow.Index].Cells[1].Value.ToString();
        }

        private void btnSalvarPais_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvarUf_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvarCidade_Click(object sender, EventArgs e)
        {

        }

        private void txtNomeCidade_TextChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {
            listaCidadeUf = DAO.CidadeDAO.ExibirTodos((Classes.Uf)cmbUf.SelectedItem);


            List<Classes.Cidade> cidadesFiltradas = listaCidadeUf.Where(item => item.Nome.StartsWith(txtNomeCidade.Text)).ToList();

            dgvCidade.Rows.Clear();

            foreach (Classes.Cidade item in cidadesFiltradas)
            {
                dgvCidade.Rows.Add();
                dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[0].Value = item.Codigo;
                dgvCidade.Rows[dgvCidade.Rows.Count - 1].Cells[1].Value = item.Nome;
            }
        }
    }
}
