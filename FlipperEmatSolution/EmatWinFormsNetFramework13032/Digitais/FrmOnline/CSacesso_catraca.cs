using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{
    public class CSacesso_catraca
    {
        public string id_cartao;
        public string id_buscar;
        public string estado;
        public string ativo;

        public string ultimo_acesso;

        public List<string> cartoes = new List<string>() { };        

        SqlQuery consulta = new SqlQuery(Conexoes.GetSqlConnection());

        //Verificar se numero digitado existe na tabela Acesso_catraca
        public void get_cartao()
        {
            CScard_catraca.presenca = "n_liberar";

            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText =
                @"SELECT * FROM ACESSO_CATRACA  WHERE ID_CARTAO = @idcartao";

            consulta.Command.Parameters.AddWithValue("@idcartao", id_buscar);

            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }

                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();
                while (reader.Read())
                {
                    //string s = reader["ID_CARTAO"].ToString();
                    //s += "|" + reader["ESTADO"].ToString();
                    //cartoes.Add(s);

                    ultimo_acesso = reader["ULTIMO_ACESSO"].ToString();
                    ativo = reader["ATIVO"].ToString();
                    CScard_catraca.estado_retorno = reader["ESTADO"].ToString();

                    CScard_catraca.presenca = "liberar";
                }
            }

            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                consulta.Connection.Close();

            }
        }

        public void altera_estado()
        {

            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText =
                @"UPDATE ACESSO_CATRACA SET
                ESTADO=@estado,
                ULTIMO_ACESSO=@ultimo
                WHERE ID_CARTAO=@id_cartao";

            consulta.Command.Parameters.AddWithValue("@id_cartao", CScard_catraca.id_card);
            consulta.Command.Parameters.AddWithValue("@estado", CScard_catraca.estado);
            consulta.Command.Parameters.AddWithValue("@ultimo", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                consulta.Command.ExecuteNonQuery();                
                consulta.Command.Parameters.Clear();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                
            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        public void registra_passagem_estado()
        {

            

            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText =
                @"INSERT INTO CAT_ENT_SAI (ID_CARTAO, ENT_SAI, DATA) VALUES ( @id_cartao, @ent_sai, @data)";               
           
            consulta.Command.Parameters.AddWithValue("@id_cartao", CScard_catraca.id_card);
            consulta.Command.Parameters.AddWithValue("@data", DateTime.Now);

            if(CScard_catraca.estado == "1")
            {
                consulta.Command.Parameters.AddWithValue("@ent_sai","1");
            }
            else
            {
                consulta.Command.Parameters.AddWithValue("@ent_sai", "0");
            }            

            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                consulta.Command.ExecuteNonQuery();
                consulta.Command.Parameters.Clear();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        public void adciona_catoes()
        {

        }

        public void verifica_presença()
        {
            if (ativo == "1")
            {
                if (ultimo_acesso != "")
                {
                    DateTime Ultima = DateTime.Parse(ultimo_acesso);
                    DateTime hoje = DateTime.Now;

                    TimeSpan TSDiferenca = hoje - Ultima;
                    int DiferencaEmDias = TSDiferenca.Days;

                    if (DiferencaEmDias > 31)
                    {
                        CScard_catraca.presenca = "bloquear";
                    }
                    else
                    {
                        CScard_catraca.presenca = "liberar";
                    }
                }
                else
                {
                    //Para "ULTIMO_ACESSO" vazio, o programa irá reconhecer que o aluno estará tendo o seu ultimo acesso neste momento e começa a contar a partir de agora.
                    CScard_catraca.presenca = "liberar";
                }
            }
            else
            {
                CScard_catraca.presenca = "bloquear";
            }
            
        }
    }
}
