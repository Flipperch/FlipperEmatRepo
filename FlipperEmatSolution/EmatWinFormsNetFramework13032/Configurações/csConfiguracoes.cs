using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Configurações
{
    public class csConfiguracoes
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public string caminho_app = @"C:\E-mat\configuracoes.txt";

        public bool verifica_arquivo_conf()
        {
            bool a = false;

            //Verificar se conf.txt existe
            if(File.Exists(caminho_app))
            {
                a = true;
            }          

            return a;
        }

        public void update_arquivo_conf(string item, string valor)
        {
            List<string> list_ = new List<string>();

            string item_valor = item + ">" + valor;
            
            //adciona linhas na list_
            string line;

            StreamReader read = new StreamReader(caminho_app);
            while((line = read.ReadLine()) != null)
            {
                list_.Add(line);
            }

            read.Close();    

            for (int i = 0; i < list_.Count; i ++ )
            {
                if (list_[i].Contains(item))
                {
                    list_.Remove(list_[i]);
                    break;
                }
            }
            
            list_.Add(item_valor);

            File.WriteAllLines(caminho_app, list_);

                    
        }

        public string busca_valor_conf(string item)
        {
            string a = null;

            //fotos>D:\Arquivos\Documents\FOTOS\
            //webconf>USB2.0 HD UVC WebCam>
            //modo_conf>
            //end_cid_mais_usado>
            //end_uf_mais_usado>
            //
            //

            int counter = 0;
            string line;
            if (File.Exists(caminho_app))
            {
                StreamReader read = new StreamReader(caminho_app);
                while ((line = read.ReadLine()) != null)
                {
                    if (line.Contains(item))
                    {
                        string[] caminho = line.Split('>');
                        a = caminho[1];
                    }
                    counter++;
                }
                
                read.Close();
            } 

            return a;

        }

        public void criar_conf()
        {
            //Criar arquivo conf
            Directory.CreateDirectory(Path.GetDirectoryName(caminho_app));

            using (FileStream fs = new FileStream(caminho_app, FileMode.Create))
            {
                fs.Close();
            }
        }

        public void buscar_escola_ativa()
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ESCOLAS WHERE ATIVO=1";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while(reader.Read())
                {
                    csEscola.id_escola = Convert.ToInt16(reader["ID_ESCOLA"]);
                    csEscola.cidade = reader["CIDADE"].ToString();
                    csEscola.escola = reader["ESCOLA"].ToString();
                    csEscola.sigla_estado = reader["SIGLA_ESTADO"].ToString();                    
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                
                sql_conn.Close();
            }
        }

        public bool conexao_bd_ok()
        {
            bool ok = false;

            sql_conn.Close();
            
            try
            {
                sql_conn.Open();

                ok = true;
            }
            catch
            {

            }
            finally
            {
                sql_conn.Close();
            }

            return ok;
        }
    }
}
