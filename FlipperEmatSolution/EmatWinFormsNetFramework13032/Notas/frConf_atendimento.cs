﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frConf_atendimento : Form
    {
        csDisciplinas cs_disciplinas = new csDisciplinas();
        csTipo_Atendimento cs_tipodisciplina = new csTipo_Atendimento();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();

        int id_disciplina_selecionado = 0;
        int id_ensino_selecionado = 0;


        string item_selecionado = "";


        public frConf_atendimento()
        {
            InitializeComponent();
        }

        private void frConf_atendimento_Load(object sender, EventArgs e)
        {
            cmbEnsino.DataSource = cs_disciplinas.list_ensinos();

            DataTable dt = cs_disciplinas.buscarDisciplinas();
            for(int i = 0; i< dt.Rows.Count; i++ )
            {
                cmbDisciplina.Items.Add(cs_disciplinas.tab_disciplinas.Rows[i][1].ToString());
            }
            if(cs_usuarios.troca_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_disciplina_logado) != null)
            {                
                cmbDisciplina.SelectedText = cs_usuarios.troca_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_disciplina_logado);
                txtFormula.Text = cs_disciplinas.sel_formula_media(id_disciplina_selecionado, id_ensino_selecionado);
                update_ltb();
            }

            
        }

        private void btAdd_Tipodisciplina_Click(object sender, EventArgs e)
        {
            if (btAdd_Tipodisciplina.Text == "Adicionar")
            {
                #region Cod-adicionar
                if (cmbDisciplina.Text != "")
                {
                    if (txtTipo_Atendimento.Text != "")
                    {
                        //Verifica se tem nome igual na listview
                        bool add = true;
                        foreach (ListViewItem item in ltvTipos_Atendimento.Items)
                        {
                            if (item.Text == txtTipo_Atendimento.Text)
                            {
                                add = false;
                            }

                        }
                        //Grava se não houver
                        if (add == true)
                        {
                            if (ckbTem_Nota.Checked)
                            {
                                cs_tipodisciplina.adicionarTipo_Atendimento(txtTipo_Atendimento.Text, (float)nudNota.Value, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));
                                update_ltb();
                                //Limpar campos e volta botão para adicionar
                                btAdd_Tipodisciplina.Text = "Adicionar";
                                txtTipo_Atendimento.Text = "";
                                nudNota.Value = 0;
                                ckbTem_Nota.Checked = false;
                            }
                            else
                            {                              
                                cs_tipodisciplina.adicionarTipo_Atendimento_semnota(txtTipo_Atendimento.Text, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));
                                update_ltb();
                                //Limpar campos e volta botão para adicionar
                                btAdd_Tipodisciplina.Text = "Adicionar";
                                txtTipo_Atendimento.Text = "";
                                nudNota.Value = 0;
                                ckbTem_Nota.Checked = false;
                            }
                        }
                        else
                        {
                            txtTipo_Atendimento.BackColor = Color.IndianRed;
                            MessageBox.Show("Tipo de Atendimento já existe. Favor usar um nome diferente.");
                        }
                    }
                    else
                    {
                        txtTipo_Atendimento.BackColor = Color.IndianRed;
                        MessageBox.Show("Favor informar o novo tipo de atendimento.");
                    }
                }
                else
                {
                    cmbDisciplina.BackColor = Color.IndianRed;
                    MessageBox.Show("Favor selecionar a disciplina.");
                }
                #endregion Cod-adicionar
            }
            else
            {
                #region Cod-modificar
                if (cmbDisciplina.Text != "")
                {
                    if (txtTipo_Atendimento.Text != "")
                    {
                        
                       
                            if (ckbTem_Nota.Checked)
                            {
                                cs_tipodisciplina.modificaTipo_Atendimento(cs_tipodisciplina.troca_nome_tipoatend_id(item_selecionado,cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text)), txtTipo_Atendimento.Text, (float)nudNota.Value, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));
                               
                                update_ltb();
                                //Limpar campos e volta botão para adicionar
                                btAdd_Tipodisciplina.Text = "Adicionar";
                                txtTipo_Atendimento.Text = "";
                                nudNota.Value = 0;
                                ckbTem_Nota.Checked = false;
                            }
                            else
                            {
                                cs_tipodisciplina.modificaTipo_Atendimento_semnota(cs_tipodisciplina.troca_nome_tipoatend_id(item_selecionado, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text)), txtTipo_Atendimento.Text, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));
          
                                update_ltb();
                                //Limpar campos e volta botão para adicionar
                                btAdd_Tipodisciplina.Text = "Adicionar";
                                txtTipo_Atendimento.Text = "";
                                nudNota.Value = 0;
                                ckbTem_Nota.Checked = false;
                            }
                        
                        
                    }
                    else
                    {
                        txtTipo_Atendimento.BackColor = Color.IndianRed;
                        MessageBox.Show("Favor informar o novo tipo de atendimento.");
                    }
                }
                else
                {
                    cmbDisciplina.BackColor = Color.IndianRed;
                    MessageBox.Show("Favor selecionar a disciplina.");
                }
                #endregion Cod-adicionar                
            }


                           
        }

        private void ckbTem_Nota_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbTem_Nota.Checked)
            {
                nudNota.Enabled = true;

            }
            else
            {
                nudNota.Enabled = false;
                nudNota.Value = 0;
            }
        }

        private void cmbDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDisciplina.Text != null)
            {
                id_disciplina_selecionado = cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text);

                update_ltb();



            }



            btAdd_Tipodisciplina.Enabled = true;
        }

        public void update_ltb()
        {
            ltvTipos_Atendimento.Items.Clear();

            DataTable table = new DataTable();

            table = cs_tipodisciplina.buscarTipo_Atendimento_ind(cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));

            for(int i = 0; i < table.Rows.Count; i++)
            {
                ltvTipos_Atendimento.Items.Add(table.Rows[i][1].ToString());
                
                string a;

                if((string)table.Rows[i][2]!="")
                {
                   a = (string)table.Rows[i][2];
                }
                else
                {
                    a = "NÃO";
                }

                ltvTipos_Atendimento.Items[i].SubItems.Add(a);

                
                
            }
           
        }

        private void cmbDisciplina_DropDown(object sender, EventArgs e)
        {
            cmbDisciplina.BackColor = Color.White;
        }

        private void ltvTipos_Atendimento_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ltvTipos_Atendimento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in ltvTipos_Atendimento.SelectedItems)
            {
                if (ltvTipos_Atendimento.SelectedItems[0].Text != "ENCERRADO" || ltvTipos_Atendimento.SelectedItems[0].Text != "MÉDIA FINAL")
                {
                    if (ltvTipos_Atendimento.SelectedItems[0].Text != "ORIENTAÇÃO INICIAL")
                    {
                        txtTipo_Atendimento.Text = ltvTipos_Atendimento.SelectedItems[0].Text;
                        item_selecionado = ltvTipos_Atendimento.SelectedItems[0].Text;

                        if (item.SubItems[1].Text != "NÃO")
                        {
                            decimal dec = decimal.Parse(item.SubItems[1].Text);
                            nudNota.Value = dec;
                            ckbTem_Nota.Checked = true;
                        }
                        else
                        {
                            nudNota.Value = 0;
                            ckbTem_Nota.Checked = false;
                        }

                        btAdd_Tipodisciplina.Text = "Atualizar";
                        btNovo.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Não é possível alterar um atendimento padrão.", "Alteração");
                    }
                }
                else
                {
                    MessageBox.Show("Não é possível alterar um atendimento padrão.","Alteração");
                }                
            }
        }

        private void txtTipo_Atendimento_TextChanged(object sender, EventArgs e)
        {
            btAdd_Tipodisciplina.Enabled = true;
            
        }

        private void nudNota_ValueChanged(object sender, EventArgs e)
        {
            btAdd_Tipodisciplina.Enabled = true;
            
        }

        private void txtTipo_Atendimento_Enter(object sender, EventArgs e)
        {
            txtTipo_Atendimento.BackColor = Color.White;
        }

        private void btNovo_Click(object sender, EventArgs e)
        {

            txtTipo_Atendimento.Text = "";
            ckbTem_Nota.Checked = false;
            nudNota.Value = 0;
            nudNota.Enabled = false;
            btAdd_Tipodisciplina.Text = "Adicionar";
            btAdd_Tipodisciplina.Enabled = false;
            btNovo.Enabled = true;        
            
        }

        private void ltvTipos_Atendimento_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Gostaria de exlucir este tipo de atendimento ?", "Excluir tipo de Atendimento", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cs_tipodisciplina.excluirTipo_Atendimento(cs_tipodisciplina.troca_nome_tipoatend_id(item_selecionado, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text)));
                    update_ltb();
                }
                
            }

        }

        private void ltvTipos_Atendimento_ItemActivate(object sender, EventArgs e)
        {

        }

        private void ltvTipos_Atendimento_MouseUp(object sender, MouseEventArgs e)
        {
            if (ltvTipos_Atendimento.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in ltvTipos_Atendimento.SelectedItems)
                {
                    item_selecionado = item.Text;
                }
            }
        }

        private void ltvTipos_Atendimento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
