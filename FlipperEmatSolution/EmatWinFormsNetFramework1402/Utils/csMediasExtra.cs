using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework1402.Utils
{
    public class csMediasExtra
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        //private readonly IEmatriculaSettings _settings;

        public int id_media_extra { get; set; }
        public string disciplina { get; set; }
        public string instituicao { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public DateTime data { get; set; }
        public string media { get; set; }
        public string n_mat { get; set; }
        public int id_ensino { get; set; }

        public csMediasExtra sel_mediaextra(string n_mat)
        {
            csMediasExtra retorno = new csMediasExtra();

            retorno.id_media_extra = 1;
            retorno.disciplina = "x";
            retorno.instituicao = "x";
            retorno.cidade = "x";
            retorno.uf = "x";
            retorno.data = DateTime.Parse("x");
            retorno.media = "x";

            return retorno;
        }

        public List<csMediasExtra> sel_list_mediaextra(string n_mat, int id_ensino)
        {
            List<csMediasExtra> retorno = new List<csMediasExtra>();

            //Chama SQL
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand();

            sql_comm.Connection = sqlHelper.ematConn;

            sql_comm.CommandText = "SELECT * FROM MEDIAS_EXTRA WHERE N_MAT = @n_mat AND ID_ENSINO = @id_ensino";

            sql_comm.Parameters.AddWithValue("n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("id_ensino", id_ensino);

            try
            {
                sql_comm.Connection.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while (reader.Read())
                {
                    csMediasExtra cs_mediaextra = new csMediasExtra();
                    cs_mediaextra.id_media_extra = Convert.ToInt32(reader["ID_MEDIA_EXTRA"].ToString());
                    cs_mediaextra.disciplina = reader["DISCIPLINA"].ToString();
                    cs_mediaextra.instituicao = reader["INSTITUICAO"].ToString();
                    cs_mediaextra.cidade = reader["CIDADE"].ToString();
                    cs_mediaextra.uf = reader["UF"].ToString();
                    //Data
                    if(reader["DATA"] != DBNull.Value) cs_mediaextra.data = DateTime.Parse(reader["DATA"].ToString());

                    cs_mediaextra.media = reader["MEDIA"].ToString();
                    retorno.Add(cs_mediaextra);
                }
                reader.Close();
            }

            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();
            }            

            return retorno;
        }

        public bool existe_media_extra(int id_media_extra)
        {
            bool retorno = false;

            //Chama SQL
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand();

            sql_comm.Connection = sqlHelper.ematConn;

            sql_comm.CommandText = "SELECT * FROM MEDIAS_EXTRA WHERE ID_MEDIA_EXTRA = @id_media_extra";

            sql_comm.Parameters.AddWithValue("id_media_extra", id_media_extra);

            try
            {
                sql_comm.Connection.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while (reader.Read())
                {
                    retorno = true;
                }
                reader.Close();
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();
            }

            return retorno;
        }

        public int salvar_media_extra(csMediasExtra objeto)
        {
            int retorno = 0;

            if(existe_media_extra(objeto.id_media_extra))
            {
                retorno = upd_media_extra(objeto);                
            }
            else
            {
                retorno = ins_media_extra(objeto);
            }

            return retorno;
        }

        public int ins_media_extra(csMediasExtra objeto)
        {
            int retorno = 0;

            //Chama SQL
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand();

            sql_comm.Connection = sqlHelper.ematConn;

            sql_comm.CommandText = @"INSERT INTO MEDIAS_EXTRA (DISCIPLINA, INSTITUICAO, CIDADE, UF, DATA, MEDIA, N_MAT, ID_ENSINO) 
                                                OUTPUT Inserted.ID_MEDIA_EXTRA
                                                VALUES(@disciplina, @instituicao, @cidade, @uf, @data, @media, @n_mat, @id_ensino)";
            
            if(objeto.disciplina != null) sql_comm.Parameters.AddWithValue("disciplina", objeto.disciplina);
            else sql_comm.Parameters.AddWithValue("disciplina", DBNull.Value);
            if(objeto.instituicao != null) sql_comm.Parameters.AddWithValue("instituicao", objeto.instituicao);
            else sql_comm.Parameters.AddWithValue("instituicao", DBNull.Value);
            if (objeto.cidade != null) sql_comm.Parameters.AddWithValue("cidade", objeto.cidade);
            else sql_comm.Parameters.AddWithValue("cidade", DBNull.Value);
            if (objeto.uf != null) sql_comm.Parameters.AddWithValue("uf", objeto.uf);
            else sql_comm.Parameters.AddWithValue("uf", DBNull.Value);
            if (objeto.data.ToString("dd/MM/yyyy") != "01/01/0001") sql_comm.Parameters.AddWithValue("data", objeto.data);
            else sql_comm.Parameters.AddWithValue("data", DBNull.Value);
            if (objeto.media != null) sql_comm.Parameters.AddWithValue("media", objeto.media);
            else sql_comm.Parameters.AddWithValue("media", DBNull.Value);

          

            sql_comm.Parameters.AddWithValue("n_mat", objeto.n_mat);
            sql_comm.Parameters.AddWithValue("id_ensino", objeto.id_ensino);


            try
            {
                sql_comm.Connection.Open();
                retorno = (int)sql_comm.ExecuteScalar();
                
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();
            }

            return retorno;
        }

        public int upd_media_extra(csMediasExtra objeto)
        {
            int retorno = 0;

            //Chama SQL
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand();

            sql_comm.Connection = sqlHelper.ematConn;

            sql_comm.CommandText = @"UPDATE MEDIAS_EXTRA SET DISCIPLINA=@disciplina,
                                                             INSTITUICAO=@instituicao,
                                                             CIDADE=@cidade,
                                                             UF=@uf,
                                                             DATA=@data,
                                                             MEDIA=@media,
                                                             ID_ENSINO=@id_ensino
                                                             WHERE ID_MEDIA_EXTRA=@id_media_extra";

            if (objeto.disciplina != null) sql_comm.Parameters.AddWithValue("disciplina", objeto.disciplina);
            else sql_comm.Parameters.AddWithValue("disciplina", DBNull.Value);
            if (objeto.instituicao != null) sql_comm.Parameters.AddWithValue("instituicao", objeto.instituicao);
            else sql_comm.Parameters.AddWithValue("instituicao", DBNull.Value);
            if (objeto.cidade != null) sql_comm.Parameters.AddWithValue("cidade", objeto.cidade);
            else sql_comm.Parameters.AddWithValue("cidade", DBNull.Value);
            if (objeto.uf != null) sql_comm.Parameters.AddWithValue("uf", objeto.uf);
            else sql_comm.Parameters.AddWithValue("uf", DBNull.Value);
            if (objeto.data.ToString("dd/MM/yyyy") != "01/01/0001") sql_comm.Parameters.AddWithValue("data", objeto.data);
            else sql_comm.Parameters.AddWithValue("data", DBNull.Value);
            if (objeto.media != "") sql_comm.Parameters.AddWithValue("media", objeto.media);
            else sql_comm.Parameters.AddWithValue("media", DBNull.Value);

            sql_comm.Parameters.AddWithValue("n_mat", objeto.n_mat);
            sql_comm.Parameters.AddWithValue("id_media_extra", objeto.id_media_extra);
            sql_comm.Parameters.AddWithValue("id_ensino", objeto.id_ensino);


            try
            {
                sql_comm.Connection.Open();
                sql_comm.ExecuteNonQuery();

                retorno = objeto.id_media_extra;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();
            }

            return retorno;
        }

        public int del_media_extra(int id_media_extra)
        {
            int retorno = 0;

            //Chama SQL
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand();

            sql_comm.Connection = sqlHelper.ematConn;

            sql_comm.CommandText = @"DELETE FROM MEDIAS_EXTRA WHERE ID_MEDIA_EXTRA=@id_media_extra";
            
            sql_comm.Parameters.AddWithValue("id_media_extra", id_media_extra);

            try
            {
                sql_comm.Connection.Open();
                sql_comm.ExecuteNonQuery();

                retorno = 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();
            }

            return retorno;
        }

    }
}
