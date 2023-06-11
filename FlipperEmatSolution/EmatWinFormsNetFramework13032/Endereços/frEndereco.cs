using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Endereços
{
    public partial class frEndereco : Form
    {
        public frEndereco()
        {
            InitializeComponent();
        }

        //public string ReturnValue1 = "";

        Endereços.csEnderecos clenderecos = new Endereços.csEnderecos();

        private void FRenderecos_Load(object sender, EventArgs e)
        {
            clenderecos.list_paises.Clear();
            clenderecos.obter_paises();
            cmbPais.DataSource = clenderecos.list_paises;


            //cmbFiltraestado.Text = clenderecos.estado_sel;

            //clenderecos.buscarestadosres();
            //cmbFiltraestado.DataSource = clenderecos.bsestadosres;

            //clenderecos.obterendereços();
            //dgvEnderecos.DataSource = clenderecos.dt_todos_enderecos;

        }

        private void cmbFiltraestado_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizar_dgv();
            //txtestado.Text = cmbFiltraestado.Text;
            //txtestado.Enabled = false;
            
        }        

        private void dgvEnderecos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvEnderecos.RowCount > 0)
            //{
            //    int n = dgvEnderecos.SelectedCells[0].RowIndex;

            //    //txtpais.Text = dgvEnderecos.Rows[n].Cells[0].Value.ToString();
            //    //txtestado.Text = dgvEnderecos.Rows[n].Cells[1].Value.ToString();
            //    //txtcidade.Text = dgvEnderecos.Rows[n].Cells[2].Value.ToString();

            //}

        }

        private void btExcluir_Click(object sender, EventArgs e)
        {            
                if(txtcidade.Text != "")
                {
                    DialogResult result = MessageBox.Show("Deseja Excluir a Cidade Selecionada ?", "Confirmar Exlusão", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        clenderecos.cidade_sel = txtcidade.Text;
                        clenderecos.exlcuir_endereço();
                        atualizar_dgv();
                    }
                    else if (result == DialogResult.No)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("É necessario selecionar a cidade a ser excluida.");
                }                  
            
        }

        public void atualizar_dgv()
        {
            //clenderecos.dt_todos_enderecos.Clear();            
            //clenderecos.estado_sel = cmbFiltraestado.Text;
            //clenderecos.definisiglaestado_sel();
            //clenderecos.obterendereços();
            ////dgvEnderecos.DataSource = clenderecos.dt_todos_enderecos;
            

        }

        private void btsalvar_Click(object sender, EventArgs e)
        {

            if (txtcidade.Text != "")
            {
                DialogResult result = MessageBox.Show("Deseja Incluir a Cidade " + txtcidade.Text + " ?", "Confirmar Inclusão", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    clenderecos.cidade_nova = txtcidade.Text;
                    clenderecos.estado_sel = cmbEstado.Text;
                    clenderecos.verifica_repete_cidade();
                }
                else if (result == DialogResult.No)
                {
                }
            }
            else
            {
                MessageBox.Show("É necessario preencher os campos para o registro da nova Cidade.", "Nova Cidade");
            }              

        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbPais.Text != "")
            {
                clenderecos.pais_selec = cmbPais.Text;
                clenderecos.obter_estados();
                cmbEstado.DataSource = null;
                cmbEstado.DataSource = clenderecos.list_estados;
                cmbEstado.Enabled = true;
                bt_novoestado.Enabled = true;
            }
            else
            {
                cmbEstado.Text = "";
                cmbEstado.Enabled = false;
                bt_novoestado.Enabled = false;
            }
            
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbEstado.Text != "")
            {
                txtcidade.Enabled = true;
                
            }
            else
            {
                txtcidade.Text = "";
                txtcidade.Enabled = false;
            }
        }

        private void txtcidade_TextChanged(object sender, EventArgs e)
        {
            if(txtcidade.Text != "")
            {
                btsalvar.Enabled = true;
            }
            else
            {
                btsalvar.Enabled = false;
            }
        }

        private void bt_novopais_Click(object sender, EventArgs e)
        {
            using (Frnovo fr = new Frnovo())
            {
                fr.tipo = "pais";
                if (fr.ShowDialog() == DialogResult.OK)
                {

                }

               if(csNovo_endereco.novo_pais != null)
               {
                   if (!cmbPais.Items.Contains(csNovo_endereco.novo_pais))
                   {
                       clenderecos.pais_novo = csNovo_endereco.novo_pais;
                       clenderecos.incluir_pais();

                       cmbPais.DataSource = null;
                       clenderecos.list_paises.Clear();
                       clenderecos.obter_paises();
                       cmbPais.DataSource = clenderecos.list_paises;
                   }
               }
            }  
        }

        private void bt_novoestado_Click(object sender, EventArgs e)
        {
            using (Frnovo fr = new Frnovo())
            {
                fr.tipo = "estado";
                if (fr.ShowDialog() == DialogResult.OK)
                {

                }

                if (csNovo_endereco.novo_estado != null)
                {
                    if (!cmbEstado.Items.Contains(csNovo_endereco.novo_estado))
                    {
                        clenderecos.estado_novo = csNovo_endereco.novo_estado;
                        clenderecos.incluir_estado();

                        cmbEstado.DataSource = null;
                        clenderecos.list_estados.Clear();
                        clenderecos.obter_estados();
                        cmbEstado.DataSource = clenderecos.list_estados;
                    }
                }
            }  

        }

        
    }
}
