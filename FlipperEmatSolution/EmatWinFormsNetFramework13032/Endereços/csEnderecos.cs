using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.Data.OleDb;

using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Endereços
{
    class csEnderecos
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        SqlConnection conn = Conexoes.GetSqlConnection();

        DataTable dt1 = new DataTable();

        public DataTable dt_todos_enderecos = new DataTable();

        //classe configurações
        Configurações.csConfiguracoes cs_configuracoes = new Configurações.csConfiguracoes();
        
        
        //Selecionados
        public string estadoselecionado { get; set; }
        
        public string paisselecionado { get; set; }
        //Estados
        public List<string> list_estados = new List<string>();
        public List<string> list_estados_res = new List<string>();
        public BindingSource bsestados = new BindingSource();        
        //Cidades
        public List<string> cidades = new List<string>();
        public BindingSource bscidades = new BindingSource();
        //Países
        public List<string> list_paises = new List<string>(); 
        public BindingSource bspaises = new BindingSource();
        public string pais_selec;

        //Estados Res
        public string estadoselecionadores { get; set; }
        public List<string> estadosres = new List<string>();
        public BindingSource bsestadosres = new BindingSource();
        //Cidades Res
        public List<string> cidadesres = new List<string>();
        public BindingSource bscidadesres = new BindingSource();
        
        public string siglaestado = "";
        public string siglaestadores = "";
        public string siglapais = "";

        public string estado_sel = "";
        

        public string cidade_sel = "";
        public string cidade_nova = "";
        public string pais_novo = "";
        public string estado_novo = "";

        #region-configurações
        public void importarcidades()
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            try
            {
                string dirbd = "I:\\Projetos\\E-Matricula\\BD.mdb";

                string accessConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", dirbd);

                conn = new OleDbConnection(accessConn);

                conn.Open();

                string dbcommand = "SELECT * FROM CIDADES";

                comm = new OleDbCommand(dbcommand, conn);

                OleDbDataReader reader = comm.ExecuteReader();

                DataTable dt = new DataTable();
                DataColumn dc;
                dc = new DataColumn();
                dc.ColumnName = "SIGLA";
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "CIDADE";
                dt.Columns.Add(dc);



                while (reader.Read())
                {
                    DataRow row = dt.NewRow();

                    row[0] = reader["SIGLA"].ToString();
                    row[1] = reader["CIDADE"].ToString();

                    dt.Rows.Add(row);
                }

                dt1 = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
        }

        public void gravarcidades()
        {
            int qtd = 0;

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=I:\Projetos\E-Matricula\E-Matricula\E-Matricula\BD\BDMatricula.mdf;Integrated Security=True;Connect Timeout=30");

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            try
            {
                conn.Open();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO CIDADES (SIGLA_ESTADO, CIDADE ) VALUES (@SIGLA, @CIDADE )";
                    //.....................Parametros..............

                    cmd.Parameters.AddWithValue("@SIGLA", dt1.Rows[i].ItemArray[0]);
                    cmd.Parameters.AddWithValue("@CIDADE", dt1.Rows[i].ItemArray[1]);

                    cmd.ExecuteNonQuery();

                    qtd++;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
                MessageBox.Show("Importado " + qtd + " registros com Sucesso!");
            }
        }

        public void deletartudo()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=I:\Projetos\E-Matricula\E-Matricula\E-Matricula\BD\BDMatricula.mdf;Integrated Security=True;Connect Timeout=30");

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandText = "DELETE CIDADES";

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Limpo!");


        }
        #endregion

        #region Nascimento

        public void buscarpaises()
        {

            string comm = "Select * From PAISES ";
            comm += "ORDER BY PAIS";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bspaises.DataSource = list_paises;
                list_paises.Add("");
                while (reader.Read())
                {
                    list_paises.Add(reader["PAIS"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void buscarestados()
        {
            string comm = "Select * From ESTADOS ";
            comm += "WHERE SIGLA_PAIS='";
            comm += siglapais;
            comm += "' ORDER BY ESTADO";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bsestados.DataSource = list_estados;
                list_estados.Add("");
                while (reader.Read())
                {
                    list_estados.Add(reader["ESTADO"].ToString());
                }
                            

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void buscarcidades()
        {
            cidades.Clear();
            string comm = "Select * From CIDADES ";
            comm += "where ESTADO='";
            comm += estado_sel;
            comm += "' ORDER BY CIDADE";                      

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bscidades.DataSource = cidades;
                cidades.Add("");
                while (reader.Read())
                {
                    cidades.Add(reader["CIDADE"].ToString());
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

                ordena_cidades();
            }
        }

        #endregion

        #region Residencia

        

        public void buscarcidadesres()
        {
            cidadesres.Clear();
            

            string comm = "Select * From CIDADES ";
            comm += "where SIGLA_ESTADO='";
            comm += siglaestadores;
            comm += "' ORDER BY CIDADE";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bscidadesres.DataSource = cidadesres;
                cidadesres.Add("");
                while (reader.Read())
                {
                    cidadesres.Add(reader["CIDADE"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region Siglas

        public void definisiglaestadores()
        {
            

            string comm = "Select * From ESTADOS ";
            comm += "where ESTADO='";
            comm += estadoselecionadores + "'";
            

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bsestadosres.DataSource = estadosres;
                while (reader.Read())
                {
                    siglaestadores = reader["SIGLA_ESTADO"].ToString();
                }                             

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public void definisiglaestado()
        {
            

            string comm = "Select * From ESTADOS ";
            comm += "where ESTADO='";
            comm += estadoselecionado + "'";


            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bsestados.DataSource = list_estados;
                while (reader.Read())
                {
                    siglaestado = reader["SIGLA_ESTADO"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public void definisiglapais()
        {
            

            string comm = "Select * From PAISES ";
            comm += "where PAIS='";
            comm += paisselecionado + "'";


            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                bsestados.DataSource = list_estados;
                while (reader.Read())
                {
                    siglapais = reader["SIGLA_PAIS"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        //public void definisiglaestado_sel()
        //{
            

        //    string comm = "Select * From ESTADOS ";
        //    comm += "where ESTADO='";
        //    comm += estado_sel + "'";


        //    SqlCommand command = new SqlCommand(comm, conn);

        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        //bsestados.DataSource = estados;
        //        while (reader.Read())
        //        {
        //            siglaestado_sel = reader["SIGLA_ESTADO"].ToString();
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //}



        #endregion  

        #region Obter Endereços

        public void obterendereços()
        {                       
            obter_cidades();
            obter_estados();
            obter_paises();
        }

        // Início Corretos

        public void obter_paises()
        {          

            string comm = "Select * From PAISES ORDER BY PAIS";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                list_paises.Add("");

                while (reader.Read())
                {
                    list_paises.Add(reader["PAIS"].ToString());
                    
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void obter_estados()
        {
            list_estados.Clear();
            string comm = "Select * From ESTADOS WHERE PAIS='" + pais_selec + "' ORDER BY ESTADO";


            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                list_estados.Add("");

                while (reader.Read())
                {
                    list_estados.Add(reader["ESTADO"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

                ordena_estados();
            }
        }

        public void obter_estados_res()
        {


            string comm = "Select * From ESTADOS ";
            comm += "WHERE PAIS='";
            comm += "BRASIL";
            comm += "' ORDER BY ESTADO";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                list_estados_res.Add("");
                while (reader.Read())
                {
                    list_estados_res.Add(reader["ESTADO"].ToString());
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Fim - Corretos

        public void obter_cidades()
        {
            

            DataTable dt = new DataTable();

            DataColumn dc;

            dc = new DataColumn();
            dc.ColumnName = "País";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Estado";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Cidade";
            dt.Columns.Add(dc);

            

            string comm = "";

            if (estado_sel == "")
            {
                comm = "Select * From CIDADES ";

            }
            else
            {
                comm = "Select * From CIDADES ";
                comm += "where ESTADO='";
                comm += estado_sel + "'";

            }            


            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = reader["SIGLA_ESTADO"].ToString();
                    dr[2] = reader["CIDADE"].ToString();
                    dt.Rows.Add(dr);
                }

                dt_todos_enderecos = dt;

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        #endregion

        #region - Exclusão

        public void exlcuir_endereço()
        {
            

            string comm = "DELETE From CIDADES ";
            comm += "where CIDADE='";
            comm += cidade_sel + "'";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region - Inclusão

        public void verifica_repete_cidade()
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM CIDADES WHERE CIDADE=@cidade AND ESTADO=@estado";

            sql_comm.Parameters.AddWithValue("@cidade",cidade_nova);
            sql_comm.Parameters.AddWithValue("@estado",estado_sel);
            
            int incluir = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();               
                if (!reader.HasRows)
                {
                    incluir = 1;
                }
                else
                {
                    MessageBox.Show("Cidade já Cadastrada.", "Nova Cidade");
                }
                reader.Close();
            }
            
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }

            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
                
                if(incluir == 1)
                {
                    incluir_endereço();
                }                
            }           
        }

        public void incluir_endereço()
        {
            string sigla_estado = troca_estado_nome_sigla(estado_sel);

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO CIDADES (CIDADE, SIGLA_ESTADO, ESTADO) VALUES (@cidade_nova, @sigla_estado, @estado_sel)";

            sql_comm.Parameters.AddWithValue("@cidade_nova", cidade_nova);
            sql_comm.Parameters.AddWithValue("@sigla_estado", sigla_estado);
            sql_comm.Parameters.AddWithValue("@estado_sel", estado_sel);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public void incluir_pais()
        {


            string comm = "INSERT INTO PAISES ";
            comm += "(PAIS ) ";
            comm += "VALUES ";
            comm += "('" + pais_novo + "')";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void incluir_estado()
        {


            string comm = "INSERT INTO ESTADOS ";
            comm += "(ESTADO, PAIS ) ";
            comm += "VALUES ";
            comm += "('" + estado_novo + "', '" + pais_selec + "')";

            SqlCommand command = new SqlCommand(comm, conn);

            try
            {
                conn.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion


        //Novo Modelo

        public List<string> lista_estados(string pais_pesquisa = "BRASIL")
        {
            List<string> list_ = new List<string>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ESTADOS WHERE PAIS=@pais";

            sql_comm.Parameters.AddWithValue("@pais", pais_pesquisa);

            list_.Add("");

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while (reader.Read())
                {
                    list_.Add(reader["ESTADO"].ToString());
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return list_;
        }

        public List<string> lista_uf_brasil()
        {
            List<string> a = new List<string>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT SIGLA_ESTADO FROM ESTADOS WHERE PAIS='BRASIL'";

            a.Add("");

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Add(reader["SIGLA_ESTADO"].ToString());
                }
            }
            catch
            {

            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }


            return a;
        }

        public List<string> lista_cidades(string uf="")
        {
            List<string> a = new List<string>();

            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"SELECT CIDADE FROM CIDADES ";

            if (uf != string.Empty)
            {
                sql_comm.CommandText += " WHERE SIGLA_ESTADO=@uf ";
                sql_comm.Parameters.AddWithValue("@uf", uf);
            }

            sql_comm.CommandText += " ORDER BY CIDADE";

            a.Add("");

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Add(reader["CIDADE"].ToString());
                }
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string troca_estado_nome_sigla(string nome)
        {
            string a = "";

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ESTADOS WHERE ESTADO=@nome";

            sql_comm.Parameters.AddWithValue("@nome", nome);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["SIGLA_ESTADO"].ToString();
                }
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }


        ///favoritos

        //end_cid_mais_usado>
        //end_uf_mais_usado>

        public void grava_end_cid_favorito(string valor)
        {
            List<string> list_ = new List<string>();

            string valor_add = null;

            //Buscar valor já gravado se houver.
            if(cs_configuracoes.busca_valor_conf("end_cid_mais_usado") != null)
            {
                list_ = cs_configuracoes.busca_valor_conf("end_cid_mais_usado").Split('|').ToList<string>();
            }           
            

            if(list_.Count >2 )
            {
                list_.Remove(list_[0]);
            }

            if(!list_.Contains(valor))
            {
                list_.Add(valor);                
            }

            for (int i = 0; i < list_.Count; i++ )
            {
                valor_add += list_[i] + "|";
            }

            valor_add = valor_add.Remove((valor_add.Length)-1);
            
            //Gravação
            cs_configuracoes.update_arquivo_conf("end_cid_mais_usado", valor_add);
        }

        public void grava_end_est_favorito(string valor)
        {
            List<string> list_ = new List<string>();

            string valor_add = null;

            //Buscar valor já gravado se houver.
            if (cs_configuracoes.busca_valor_conf("end_est_mais_usado") != null)
            {
                list_ = cs_configuracoes.busca_valor_conf("end_est_mais_usado").Split('|').ToList<string>();
            }


            if (list_.Count > 2)
            {
                list_.Remove(list_[0]);
            }

            if (!list_.Contains(valor))
            {
                list_.Add(valor);
            }

            for (int i = 0; i < list_.Count; i++)
            {
                valor_add += list_[i] + "|";
            }

            valor_add = valor_add.Remove((valor_add.Length) - 1);

            //Gravação
            cs_configuracoes.update_arquivo_conf("end_est_mais_usado", valor_add);
        }

        public List<string> list_end_cid_favorito()
        {
            List<string> list_ = new List<string>();

            //Buscar valor já gravado se houver.
            if (cs_configuracoes.busca_valor_conf("end_cid_mais_usado") != null)
            {
                list_ = cs_configuracoes.busca_valor_conf("end_cid_mais_usado").Split('|').ToList<string>();
            }

            return list_;
        }

        public List<string> list_end_est_favorito()
        {
            List<string> list_ = new List<string>();

            //Buscar valor já gravado se houver.
            if (cs_configuracoes.busca_valor_conf("end_est_mais_usado") != null)
            {
                list_ = cs_configuracoes.busca_valor_conf("end_est_mais_usado").Split('|').ToList<string>();
            }

            return list_;
        }

        public void ordena_cidades()
        {
            List<string> list = list_end_cid_favorito();

            for(int z =0; z < list.Count; z++)
            {
                if (cidades.Contains(list[z]))
                {
                    cidades.Remove(list[z]);

                    cidades.Insert(0, list[z]);
                }
            }            
        }

        public void ordena_estados()
        {
            List<string> list = list_end_est_favorito();

            for (int z = 0; z < list.Count; z++)
            {
                if (list_estados.Contains(list[z]))
                {
                    list_estados.Remove(list[z]);

                    list_estados.Insert(0, list[z]);
                }
            }
        }
    }
}
