using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using...
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using EmatWinFormsNetFramework13032.Properties;


using EmatWinFormsNetFramework13032.Error_Log;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public class csAlunos
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();

        #region Atributos Alunos
        //Info - Pessoal 
        public string n_mat { get; set; }
        public string nome { get; set; }
        public string nome_social { get; set; }
        public string rg { get; set; }
        public string uf_rg { get; set; }
        public string orgao { get; set; }
        public string dat_rg { get; set; }
        public string ra { get; set; }
        public string dat_nasc { get; set; }
        public string nasc_cidade { get; set; }
        public string nasc_uf { get; set; }
        public string nasc_pais { get; set; }
        public string sexo { get; set; }
        public string nome_mae { get; set; }
        public string nome_pai { get; set; }
        public bool port_nec { get; set; }
        public string nec { get; set; }
        public string estado_civil { get; set; }
        public int id_raca { get; set; }
        public string cpf { get; set; }
        //Info -Situação
        public DateTime dat_mat { get; set; }
        public int id_ensino_atual { get; set; }
        public string nome_ensino_atual { get; set; }
        public int id_disciplina_atual { get; set; }
        public string dt_ent_disciplina { get; set; }
        public int ativo { get; set; }
        public int certidao { get; set; }
        public int historico { get; set; }
        public string rematriculas { get; set; }
        public List<DateTime> lista_rematriculas { get; set; }
        public int concluido { get; set; }
        public string obs_passaporte { get; set; }
        public string termo_mat { get; set; }
        public int id_usuario_cad { get; set; }
        public int id_usuario_mod { get; set; }
        public int id_cartao { get; set; }
        //Info - Endereço
        public string res_endereco {get; set; }
        public string res_numero {get; set; }
        public string res_bairro {get; set; }
        public string res_cidade {get; set; }    
        public string res_uf {get; set; }
        public string res_complemento {get; set; }
        public string res_cep {get; set; }
        //Info - Contato
        public string res_telefone {get; set; }
        public string res_celular {get; set; }
        public string e_mail { get; set; }
        //Info - Trabalho
        public string trabalho { get; set; }
        public string trab_local { get; set; }
        public string trab_endereco { get; set; }
        public string trab_bairro { get; set; }
        public string trab_cidade { get; set; }
        public string trab_estado { get; set; }
        public string trab_cep { get; set; }
        public string trab_telefone { get; set; }
        #endregion

        #region Métodos

        public void salvar_aluno(csAlunos obj_aluno)
        {            
            if(sel_list_alunos(obj_aluno.n_mat).Count > 0)
            {
                upd_aluno(obj_aluno);
            }
            else
            {
                add_aluno(obj_aluno);
            }
        }

        private void add_aluno(csAlunos obj_aluno)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO ALUNOS (N_MAT,
                                                         ENSINO,
                                                         DAT_MAT,
                                                         ID_USUARIO_CAD,
                                                         RG,
                                                         UF_RG,
                                                         ORGAO, 
                                                         RA, 
                                                         ALUNO,
                                                         DAT_NASC,
                                                         NASC_CIDADE,
                                                         NASC_ESTADO, 
                                                         NASC_PAIS, 
                                                         SEXO,
                                                         NOME_MAE, 
                                                         PORT_NEC, 
                                                         NEC, 
                                                         ESTADO_CIVIL, 
                                                         ID_RACA, 
                                                         RES_ENDERECO, 
                                                         RES_NUMERO, 
                                                         RES_BAIRRO, 
                                                         RES_CIDADE,
                                                         RES_ESTADO, 
                                                         RES_COMPLEMENTO,
                                                         RES_CEP,
                                                         RES_TELEFONE,
                                                         RES_CELULAR, 
                                                         TERMO_MAT,
                                                         E_MAIL,
                                                         CONCLUIDO, 
                                                         OBS_PASSAPORTE, 
                                                         CERTIDAO, 
                                                         HISTORICO,
                                                         NOME_SOCIAL,
                                                         NOME_PAI, 
                                                         TRABALHO,
                                                         TRAB_CIDADE,
                                                         TRAB_TELEFONE,
                                                         EXP_RG,
                                                         CPF,
                                                         TRAB_ESTADO)
                                                         VALUES (
                                                         @nmat,
                                                         @ensino,
                                                         @dat_mat,
                                                         @id_usuario_cad,
                                                         @rg,
                                                         @uf_rg,
                                                         @orgao,
                                                         @ra,
                                                         @aluno,
                                                         @dat_nasc,
                                                         @nasc_cidade,
                                                         @nasc_estado,
                                                         @nasc_pais,
                                                         @sexo,
                                                         @nome_mae,
                                                         @port_nec,
                                                         @nec,
                                                         @estado_civil,
                                                         @id_raca,
                                                         @res_endereco,
                                                         @res_numero,
                                                         @res_bairro,
                                                         @res_cidade,
                                                         @res_estado,
                                                         @res_complemento,
                                                         @res_cep,
                                                         @res_telefone,
                                                         @res_celular,
                                                         @termo_mat,
                                                         @email, 
                                                         @concluido,
                                                         @obs_passaporte, 
                                                         @certidao,
                                                         @historico,
                                                         @nome_social,
                                                         @nome_pai,
                                                         @trabalho, 
                                                         @trab_cidade,
                                                         @trab_telefone,
                                                         @exp_rg,
                                                         @cpf,
                                                         @trab_estado)";

            sql_comm.Parameters.AddWithValue("@nmat", obj_aluno.n_mat);
            
            #region ensino
            if (obj_aluno.nome_ensino_atual != "") sql_comm.Parameters.AddWithValue("@ensino", obj_aluno.nome_ensino_atual);            
            else sql_comm.Parameters.AddWithValue("@ensino", DBNull.Value);            
            #endregion

            #region dat_mat
            if(obj_aluno.dat_mat != null) sql_comm.Parameters.AddWithValue("@dat_mat", obj_aluno.dat_mat);
            else sql_comm.Parameters.AddWithValue("@dat_mat", DBNull.Value);
            #endregion

            #region id_usuario_cad
            sql_comm.Parameters.AddWithValue("@id_usuario_cad", Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
            #endregion

            #region rg
            if (obj_aluno.rg != "") sql_comm.Parameters.AddWithValue("@rg", obj_aluno.rg);
            else sql_comm.Parameters.AddWithValue("@rg", DBNull.Value);
            #endregion

            #region uf_rg
            if (obj_aluno.uf_rg != "") sql_comm.Parameters.AddWithValue("@uf_rg", obj_aluno.uf_rg);
            else sql_comm.Parameters.AddWithValue("@uf_rg", DBNull.Value);
            #endregion

            #region orgao
            if (obj_aluno.orgao != "") sql_comm.Parameters.AddWithValue("@orgao", obj_aluno.orgao);
            else sql_comm.Parameters.AddWithValue("@orgao", DBNull.Value);
            #endregion

            #region ra
            if (obj_aluno.ra != "") sql_comm.Parameters.AddWithValue("@ra", obj_aluno.ra);
            else sql_comm.Parameters.AddWithValue("@ra", DBNull.Value);
            #endregion

            #region aluno
            if (obj_aluno.nome != "") sql_comm.Parameters.AddWithValue("@aluno", obj_aluno.nome);
            else sql_comm.Parameters.AddWithValue("@aluno", DBNull.Value);
            #endregion

            #region dat_nasc
            sql_comm.Parameters.AddWithValue("@dat_nasc", obj_aluno.dat_nasc);
            #endregion

            #region nasc_cidade
            if (obj_aluno.nasc_cidade != "") sql_comm.Parameters.AddWithValue("@nasc_cidade", obj_aluno.nasc_cidade);
            else sql_comm.Parameters.AddWithValue("@nasc_cidade", DBNull.Value);
            #endregion

            #region nasc_estado
            if (obj_aluno.nasc_uf != "") sql_comm.Parameters.AddWithValue("@nasc_estado", obj_aluno.nasc_pais);
            else sql_comm.Parameters.AddWithValue("@nasc_estado", DBNull.Value);
            #endregion

            #region nasc_pais
            if (obj_aluno.nasc_pais != "") sql_comm.Parameters.AddWithValue("@nasc_pais", obj_aluno.nasc_pais);            
            else sql_comm.Parameters.AddWithValue("@nasc_pais", DBNull.Value);            
            #endregion

            #region sexo
            if (obj_aluno.sexo != "") sql_comm.Parameters.AddWithValue("@sexo", obj_aluno.sexo);            
            else sql_comm.Parameters.AddWithValue("@sexo", DBNull.Value);             
            #endregion

            #region nome_mae
            if (obj_aluno.nome_mae != "") sql_comm.Parameters.AddWithValue("@nome_mae", obj_aluno.nome_mae);            
            else sql_comm.Parameters.AddWithValue("@nome_mae", DBNull.Value);            
            #endregion

            #region port_nec
            if (obj_aluno.port_nec) sql_comm.Parameters.AddWithValue("@port_nec", "SIM");
            else sql_comm.Parameters.AddWithValue("@port_nec", "NÃO");
            #endregion

            #region nec
            if (obj_aluno.nec != "") sql_comm.Parameters.AddWithValue("@nec", obj_aluno.nec);            
            else sql_comm.Parameters.AddWithValue("@nec", DBNull.Value);            
            #endregion

            #region estado_civil
            if (obj_aluno.estado_civil != "") sql_comm.Parameters.AddWithValue("@estado_civil", obj_aluno.estado_civil);            
            else sql_comm.Parameters.AddWithValue("@estado_civil", DBNull.Value);            
            #endregion

            #region raca
            if (obj_aluno.id_raca > 0) sql_comm.Parameters.AddWithValue("@id_raca", obj_aluno.id_raca);            
            else sql_comm.Parameters.AddWithValue("@id_raca", DBNull.Value);            
            #endregion

            #region res_endereco
            if (obj_aluno.res_endereco != "") sql_comm.Parameters.AddWithValue("@res_endereco", obj_aluno.res_endereco);            
            else sql_comm.Parameters.AddWithValue("@res_endereco", DBNull.Value);            
            #endregion

            #region res_numero
            if (obj_aluno.res_numero != "") sql_comm.Parameters.AddWithValue("@res_numero", obj_aluno.res_numero);            
            else sql_comm.Parameters.AddWithValue("@res_numero", DBNull.Value);            
            #endregion

            #region res_bairro
            if (obj_aluno.res_bairro != "") sql_comm.Parameters.AddWithValue("@res_bairro", obj_aluno.res_bairro);            
            else sql_comm.Parameters.AddWithValue("@res_bairro", DBNull.Value);            
            #endregion

            #region res_cidade
            if (obj_aluno.res_bairro != "") sql_comm.Parameters.AddWithValue("@res_cidade", obj_aluno.res_bairro);            
            else sql_comm.Parameters.AddWithValue("@res_cidade", DBNull.Value);            
            #endregion

            #region res_uf
            if (obj_aluno.res_uf != "") sql_comm.Parameters.AddWithValue("@res_estado", obj_aluno.res_uf);
            else sql_comm.Parameters.AddWithValue("@res_estado", DBNull.Value);            
            #endregion

            #region res_complemento
            if (obj_aluno.res_complemento != "") sql_comm.Parameters.AddWithValue("@res_complemento", obj_aluno.res_complemento);            
            else sql_comm.Parameters.AddWithValue("@res_complemento", DBNull.Value);            
            #endregion

            #region res_cep
            if (obj_aluno.res_cep != "") sql_comm.Parameters.AddWithValue("@res_cep", obj_aluno.res_cep);            
            else sql_comm.Parameters.AddWithValue("@res_cep", DBNull.Value);            
            #endregion

            #region res_telefone
            if (obj_aluno.res_telefone != "") sql_comm.Parameters.AddWithValue("@res_telefone", obj_aluno.res_telefone);            
            else sql_comm.Parameters.AddWithValue("@res_telefone", DBNull.Value);            
            #endregion

            #region res_celular
            if (obj_aluno.res_celular != "") sql_comm.Parameters.AddWithValue("@res_celular", obj_aluno.res_celular);            
            else sql_comm.Parameters.AddWithValue("@res_celular", DBNull.Value);            
            #endregion

            #region termo_mat
            if (obj_aluno.termo_mat != "") sql_comm.Parameters.AddWithValue("@termo_mat", obj_aluno.termo_mat);            
            else sql_comm.Parameters.AddWithValue("@termo_mat", DBNull.Value);            
            #endregion

            #region email
            if (obj_aluno.e_mail != "") sql_comm.Parameters.AddWithValue("@email", obj_aluno.e_mail);            
            else sql_comm.Parameters.AddWithValue("@email", DBNull.Value);            
            #endregion

            #region Concluido
            sql_comm.Parameters.AddWithValue("@concluido", "0");
            #endregion

            #region Obs_passaporte
            if (obj_aluno.obs_passaporte != "") sql_comm.Parameters.AddWithValue("@obs_passaporte", obj_aluno.obs_passaporte);
            else sql_comm.Parameters.AddWithValue("@obs_passaporte", DBNull.Value);
            #endregion

            #region certidao
            sql_comm.Parameters.AddWithValue("@certidao", 1);
            #endregion

            #region historico
            sql_comm.Parameters.AddWithValue("@historico", 1);
            #endregion

            #region nome_social
            if (obj_aluno.nome_social != "") sql_comm.Parameters.AddWithValue("@nome_social", obj_aluno.nome_social);            
            else sql_comm.Parameters.AddWithValue("@nome_social", DBNull.Value);
            #endregion

            #region nome_pai
            if (obj_aluno.nome_pai != "") sql_comm.Parameters.AddWithValue("@nome_pai", obj_aluno.nome_pai);
            else sql_comm.Parameters.AddWithValue("@nome_pai", DBNull.Value);
            #endregion

            #region trabalho
            if (obj_aluno.trabalho != "") sql_comm.Parameters.AddWithValue("@trabalho", obj_aluno.trabalho);
            else sql_comm.Parameters.AddWithValue("@trabalho", DBNull.Value);
            #endregion

            #region trab_cidade
            if (obj_aluno.trab_cidade != "") sql_comm.Parameters.AddWithValue("@trab_cidade", obj_aluno.trab_cidade);
            else sql_comm.Parameters.AddWithValue("@trab_cidade", DBNull.Value);
            #endregion

            #region trab_telefone
            if (obj_aluno.trab_telefone != "") sql_comm.Parameters.AddWithValue("@trab_telefone", obj_aluno.trab_telefone);            
            else sql_comm.Parameters.AddWithValue("@trab_telefone", DBNull.Value);
            #endregion

            #region exp_rg
            string exp_rg_sem_separador = obj_aluno.dat_rg.Replace("/", "");
            exp_rg_sem_separador = exp_rg_sem_separador.Replace(" ", "");
            if (exp_rg_sem_separador != string.Empty) sql_comm.Parameters.AddWithValue("@exp_rg", exp_rg_sem_separador);            
            else sql_comm.Parameters.AddWithValue("@exp_rg", DBNull.Value);
            #endregion

            #region cpf
            if (obj_aluno.cpf != "") sql_comm.Parameters.AddWithValue("@cpf", obj_aluno.cpf);
            else sql_comm.Parameters.AddWithValue("@cpf", DBNull.Value);            
            #endregion

            #region trab_estado
            if (obj_aluno.trab_estado != "") sql_comm.Parameters.AddWithValue("@trab_estado", obj_aluno.trab_estado);
            else sql_comm.Parameters.AddWithValue("@trab_estado", DBNull.Value);            
            #endregion

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

        private void upd_aluno(csAlunos obj_aluno)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE ALUNOS SET 
                                     ENSINO=@ensino,
                                     RG=@rg,
                                     UF_RG=@uf_rg,
                                     ORGAO=@orgao,
                                     RA=@ra, ALUNO=@aluno,
                                     DAT_NASC=@dat_nasc,
                                     NASC_CIDADE=@nasc_cidade,
                                     NASC_ESTADO=@nasc_estado,
                                     NASC_PAIS=@nasc_pais,
                                     SEXO=@sexo,
                                     NOME_MAE=@nome_mae,
                                     PORT_NEC=@port_nec,
                                     NEC=@nec,
                                     ESTADO_CIVIL=@estado_civil,
                                     ID_RACA=@id_raca,
                                     RES_ENDERECO=@res_endereco,
                                     RES_NUMERO=@res_numero,
                                     RES_BAIRRO=@res_bairro,
                                     RES_CIDADE=@res_cidade,
                                     RES_ESTADO=@res_estado,
                                     RES_COMPLEMENTO=@res_complemento,
                                     RES_CEP=@res_cep, RES_TELEFONE=@res_telefone,
                                     RES_CELULAR=@res_celular, TERMO_MAT=@termo_mat,
                                     ID_USUARIO_MOD=@id_usuario_mod,
                                     DAT_MAT=@dat_mat,
                                     DAT_MOD=@dat_mod,
                                     ID_DISCIPLINA_ATUAL=@id_disciplina_atual,
                                     DT_ENT_DISCIPLINA=@dt_ent_disciplina,
                                     OBS_PASSAPORTE=@obs_passaporte,
                                     CERTIDAO=@certidao,
                                     HISTORICO=@historico,
                                     NOME_SOCIAL=@nome_social,
                                     NOME_PAI=@nome_pai,
                                     TRABALHO=@trabalho,                
                                     TRAB_CIDADE=@trab_cidade,
                                     TRAB_TELEFONE=@trab_telefone,
                                     EXP_RG=@exp_rg,
                                     CPF=@cpf,                
                                     TRAB_ESTADO=@trab_estado
                                     WHERE N_MAT=@nmat";

            #region ensino
            if (obj_aluno.nome_ensino_atual != "") sql_comm.Parameters.AddWithValue("@ensino", nome_ensino_atual);
            else sql_comm.Parameters.AddWithValue("@ensino", DBNull.Value);
            #endregion

            #region dat_mat
            if (obj_aluno.dat_mat != null) sql_comm.Parameters.AddWithValue("@dat_mat", obj_aluno.dat_mat);
            else sql_comm.Parameters.AddWithValue("@ensino", DBNull.Value);
            #endregion

            #region id_usuario_cad
            sql_comm.Parameters.AddWithValue("@id_usuario_cad", Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
            #endregion

            #region id_usuario_mod
            sql_comm.Parameters.AddWithValue("@id_usuario_mod", Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
            #endregion

            #region dat_mod
            sql_comm.Parameters.AddWithValue("@dat_mod", DateTime.Now);
            #endregion

            #region rg
            if (obj_aluno.rg != "") sql_comm.Parameters.AddWithValue("@rg", obj_aluno.rg);
            else sql_comm.Parameters.AddWithValue("@rg", DBNull.Value);
            #endregion

            #region uf_rg
            if (obj_aluno.uf_rg != "") sql_comm.Parameters.AddWithValue("@uf_rg", obj_aluno.uf_rg);
            else sql_comm.Parameters.AddWithValue("@uf_rg", DBNull.Value);
            #endregion

            #region orgao
            if (obj_aluno.orgao != "") sql_comm.Parameters.AddWithValue("@orgao", obj_aluno.orgao);
            else sql_comm.Parameters.AddWithValue("@orgao", DBNull.Value);
            #endregion

            #region ra
            if (obj_aluno.ra != "") sql_comm.Parameters.AddWithValue("@ra", obj_aluno.ra);
            else sql_comm.Parameters.AddWithValue("@ra", DBNull.Value);
            #endregion

            #region aluno
            if (obj_aluno.nome != "") sql_comm.Parameters.AddWithValue("@aluno", obj_aluno.nome);
            else sql_comm.Parameters.AddWithValue("@aluno", DBNull.Value);
            #endregion

            #region dat_nasc
            sql_comm.Parameters.AddWithValue("@dat_nasc", obj_aluno.dat_nasc);
            #endregion

            #region nasc_cidade
            if (obj_aluno.nasc_cidade != "") sql_comm.Parameters.AddWithValue("@nasc_cidade", obj_aluno.nasc_cidade);
            else sql_comm.Parameters.AddWithValue("@nasc_cidade", DBNull.Value);
            #endregion

            #region nasc_estado
            if (obj_aluno.nasc_uf != "") sql_comm.Parameters.AddWithValue("@nasc_estado", obj_aluno.nasc_pais);
            else sql_comm.Parameters.AddWithValue("@nasc_estado", DBNull.Value);
            #endregion

            #region nasc_pais
            if (obj_aluno.nasc_pais != "") sql_comm.Parameters.AddWithValue("@nasc_pais", obj_aluno.nasc_pais);
            else sql_comm.Parameters.AddWithValue("@nasc_pais", DBNull.Value);
            #endregion

            #region sexo
            if (obj_aluno.sexo != "") sql_comm.Parameters.AddWithValue("@sexo", obj_aluno.sexo);
            else sql_comm.Parameters.AddWithValue("@sexo", DBNull.Value);
            #endregion

            #region nome_mae
            if (obj_aluno.nome_mae != "") sql_comm.Parameters.AddWithValue("@nome_mae", obj_aluno.nome_mae);
            else sql_comm.Parameters.AddWithValue("@nome_mae", DBNull.Value);
            #endregion

            #region port_nec
            if (obj_aluno.port_nec) sql_comm.Parameters.AddWithValue("@port_nec", "SIM");
            else sql_comm.Parameters.AddWithValue("@port_nec", "NÃO");
            #endregion

            #region nec
            if (obj_aluno.nec != "") sql_comm.Parameters.AddWithValue("@nec", obj_aluno.nec);
            else sql_comm.Parameters.AddWithValue("@nec", DBNull.Value);
            #endregion

            #region estado_civil
            if (obj_aluno.estado_civil != "") sql_comm.Parameters.AddWithValue("@estado_civil", obj_aluno.estado_civil);
            else sql_comm.Parameters.AddWithValue("@estado_civil", DBNull.Value);
            #endregion

            #region raca
            if (obj_aluno.id_raca > 0) sql_comm.Parameters.AddWithValue("@id_raca", obj_aluno.id_raca);
            else sql_comm.Parameters.AddWithValue("@id_raca", DBNull.Value);
            #endregion

            #region res_endereco
            if (obj_aluno.res_endereco != "") sql_comm.Parameters.AddWithValue("@res_endereco", obj_aluno.res_endereco);
            else sql_comm.Parameters.AddWithValue("@res_endereco", DBNull.Value);
            #endregion

            #region res_numero
            if (obj_aluno.res_numero != "") sql_comm.Parameters.AddWithValue("@res_numero", obj_aluno.res_numero);
            else sql_comm.Parameters.AddWithValue("@res_numero", DBNull.Value);
            #endregion

            #region res_bairro
            if (obj_aluno.res_bairro != "") sql_comm.Parameters.AddWithValue("@res_bairro", obj_aluno.res_bairro);
            else sql_comm.Parameters.AddWithValue("@res_bairro", DBNull.Value);
            #endregion

            #region res_cidade
            if (obj_aluno.res_bairro != "") sql_comm.Parameters.AddWithValue("@res_cidade", obj_aluno.res_bairro);
            else sql_comm.Parameters.AddWithValue("@res_cidade", DBNull.Value);
            #endregion

            #region res_uf
            if (obj_aluno.res_uf != "") sql_comm.Parameters.AddWithValue("@res_estado", obj_aluno.res_uf);
            else sql_comm.Parameters.AddWithValue("@res_estado", DBNull.Value);
            #endregion

            #region res_complemento
            if (obj_aluno.res_complemento != "") sql_comm.Parameters.AddWithValue("@res_complemento", obj_aluno.res_complemento);
            else sql_comm.Parameters.AddWithValue("@res_complemento", DBNull.Value);
            #endregion

            #region res_cep
            if (obj_aluno.res_cep != "") sql_comm.Parameters.AddWithValue("@res_cep", obj_aluno.res_cep);
            else sql_comm.Parameters.AddWithValue("@res_cep", DBNull.Value);
            #endregion

            #region res_telefone
            if (obj_aluno.res_telefone != "") sql_comm.Parameters.AddWithValue("@res_telefone", obj_aluno.res_telefone);
            else sql_comm.Parameters.AddWithValue("@res_telefone", DBNull.Value);
            #endregion

            #region res_celular
            if (obj_aluno.res_celular != "") sql_comm.Parameters.AddWithValue("@res_celular", obj_aluno.res_celular);
            else sql_comm.Parameters.AddWithValue("@res_celular", DBNull.Value);
            #endregion

            #region termo_mat
            if (obj_aluno.termo_mat != "") sql_comm.Parameters.AddWithValue("@termo_mat", obj_aluno.termo_mat);
            else sql_comm.Parameters.AddWithValue("@termo_mat", DBNull.Value);
            #endregion

            #region email
            if (obj_aluno.e_mail != "") sql_comm.Parameters.AddWithValue("@email", obj_aluno.e_mail);
            else sql_comm.Parameters.AddWithValue("@email", DBNull.Value);
            #endregion

            #region Concluido
            sql_comm.Parameters.AddWithValue("@concluido", "0");
            #endregion

            #region Obs_passaporte
            if (obj_aluno.obs_passaporte != "") sql_comm.Parameters.AddWithValue("@obs_passaporte", obj_aluno.obs_passaporte);
            else sql_comm.Parameters.AddWithValue("@obs_passaporte", DBNull.Value);
            #endregion

            #region certidao
            sql_comm.Parameters.AddWithValue("@certidao", 1);
            #endregion

            #region historico
            sql_comm.Parameters.AddWithValue("@historico", 1);
            #endregion

            #region nome_social
            if (obj_aluno.nome_social != "") sql_comm.Parameters.AddWithValue("@nome_social", obj_aluno.nome_social);
            else sql_comm.Parameters.AddWithValue("@nome_social", DBNull.Value);
            #endregion

            #region nome_pai
            if (obj_aluno.nome_pai != "") sql_comm.Parameters.AddWithValue("@nome_pai", obj_aluno.nome_pai);
            else sql_comm.Parameters.AddWithValue("@nome_pai", DBNull.Value);
            #endregion

            #region trabalho
            if (obj_aluno.trabalho != "") sql_comm.Parameters.AddWithValue("@trabalho", obj_aluno.trabalho);
            else sql_comm.Parameters.AddWithValue("@trabalho", DBNull.Value);
            #endregion

            #region trab_cidade
            if (obj_aluno.trab_cidade != "") sql_comm.Parameters.AddWithValue("@trab_cidade", obj_aluno.trab_cidade);
            else sql_comm.Parameters.AddWithValue("@trab_cidade", DBNull.Value);
            #endregion

            #region trab_telefone
            if (obj_aluno.trab_telefone != "") sql_comm.Parameters.AddWithValue("@trab_telefone", obj_aluno.trab_telefone);
            else sql_comm.Parameters.AddWithValue("@trab_telefone", DBNull.Value);
            #endregion

            #region exp_rg
            string exp_rg_sem_separador = obj_aluno.dat_rg.Replace("/", "");
            exp_rg_sem_separador = exp_rg_sem_separador.Replace(" ", "");
            if (exp_rg_sem_separador != string.Empty) sql_comm.Parameters.AddWithValue("@exp_rg", exp_rg_sem_separador);
            else sql_comm.Parameters.AddWithValue("@exp_rg", DBNull.Value);
            #endregion

            #region id_disciplina_atual
            if (obj_aluno.id_disciplina_atual > 0) sql_comm.Parameters.AddWithValue("@id_disciplina_atual", obj_aluno.id_disciplina_atual);
            else sql_comm.Parameters.AddWithValue("@id_disciplina_atual", DBNull.Value);
            #endregion

            #region dt_ent_disciplina
            if (obj_aluno.dt_ent_disciplina != "") sql_comm.Parameters.AddWithValue("@dt_ent_disciplina", obj_aluno.dt_ent_disciplina);
            else sql_comm.Parameters.AddWithValue("@dt_ent_disciplina", DBNull.Value);
            #endregion

            #region cpf
            if (obj_aluno.cpf != "") sql_comm.Parameters.AddWithValue("@cpf", obj_aluno.cpf);
            else sql_comm.Parameters.AddWithValue("@cpf", DBNull.Value);
            #endregion

            #region trab_estado
            if (obj_aluno.trab_estado != "") sql_comm.Parameters.AddWithValue("@trab_estado", obj_aluno.trab_estado);
            else sql_comm.Parameters.AddWithValue("@trab_estado", DBNull.Value);
            #endregion

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

        public List<csAlunos> sel_list_alunos(string n_mat_pesquisa="", string rg_pesquisa="")
        {
            List<csAlunos> list_ = new List<csAlunos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ALUNOS ";

            if(n_mat_pesquisa != string.Empty)
            {
                sql_comm.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";
                sql_comm.Parameters.AddWithValue("@n_mat", n_mat_pesquisa);
            }
            if (rg_pesquisa != string.Empty)
            {
                sql_comm.CommandText = @"SELECT * FROM ALUNOS WHERE RG=@rg";
                sql_comm.Parameters.AddWithValue("@rg", rg_pesquisa);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csAlunos csaluno_ = new csAlunos();
                    //Info - Pessoal
                    csaluno_.n_mat = reader["N_MAT"].ToString();
                    csaluno_.nome = reader["ALUNO"].ToString();
                    csaluno_.nome_social = reader["NOME_SOCIAL"].ToString();
                    csaluno_.rg = reader["RG"].ToString();
                    csaluno_.uf_rg = reader["UF_RG"].ToString();
                    csaluno_.orgao = reader["ORGAO"].ToString();
                    csaluno_.dat_rg = reader["EXP_RG"].ToString();
                    csaluno_.ra = reader["RA"].ToString();
                    csaluno_.dat_nasc = reader["DAT_NASC"].ToString();
                    csaluno_.nasc_cidade = reader["NASC_CIDADE"].ToString();
                    csaluno_.nasc_uf = reader["NASC_ESTADO"].ToString();
                    csaluno_.nasc_pais = reader["NASC_PAIS"].ToString();
                    csaluno_.sexo = reader["SEXO"].ToString();
                    csaluno_.nome_mae = reader["NOME_MAE"].ToString();
                    csaluno_.nome_pai = reader["NOME_PAI"].ToString();
                    if (reader["PORT_NEC"].ToString() == "SIM") csaluno_.port_nec = true; else csaluno_.port_nec = false;
                    csaluno_.nec = reader["NEC"].ToString();
                    csaluno_.estado_civil = reader["ESTADO_CIVIL"].ToString();
                    if (reader["ID_RACA"] != DBNull.Value) csaluno_.id_raca = Convert.ToInt16(reader["ID_RACA"].ToString());
                    csaluno_.cpf = reader["CPF"].ToString();
                    //Info -Situação
                    csaluno_.dat_mat = DateTime.Parse(reader["DAT_MAT"].ToString());                    
                    csaluno_.nome_ensino_atual = reader["ENSINO"].ToString();
                    csaluno_.id_ensino_atual = cs_disciplinas.troca_ensino_nome_por_id(nome_ensino_atual);
                    if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value) csaluno_.id_disciplina_atual = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"].ToString());
                    csaluno_.dt_ent_disciplina = reader["DT_ENT_DISCIPLINA"].ToString();
                    if (reader["ATIVO"] != DBNull.Value) csaluno_.ativo = Convert.ToInt16(reader["ATIVO"].ToString());
                    if (reader["CERTIDAO"] != DBNull.Value) csaluno_.certidao = Convert.ToInt16(reader["CERTIDAO"].ToString());
                    if (reader["HISTORICO"] != DBNull.Value) csaluno_.historico = Convert.ToInt16(reader["HISTORICO"].ToString());

                    if (reader["REMATRICULAS"] != DBNull.Value)
                    {
                        csaluno_.lista_rematriculas = reader["REMATRICULAS"].ToString().Split('|').Select(DateTime.Parse).ToList();
                    }

                    if (reader["CONCLUIDO"] != DBNull.Value) csaluno_.concluido = Convert.ToInt16(reader["CONCLUIDO"].ToString());
                    csaluno_.obs_passaporte = reader["OBS_PASSAPORTE"].ToString();
                    csaluno_.termo_mat = reader["TERMO_MAT"].ToString();
                    if (reader["ID_USUARIO_CAD"] != DBNull.Value) csaluno_.id_usuario_cad = Convert.ToInt16(reader["ID_USUARIO_CAD"].ToString());
                    if (reader["ID_USUARIO_MOD"] != DBNull.Value) csaluno_.id_usuario_mod = Convert.ToInt16(reader["ID_USUARIO_MOD"].ToString());
                    //Info - Endereço
                    csaluno_.res_endereco = reader["RES_ENDERECO"].ToString();
                    csaluno_.res_numero = reader["RES_NUMERO"].ToString();
                    csaluno_.res_bairro = reader["RES_BAIRRO"].ToString();
                    csaluno_.res_cidade = reader["RES_CIDADE"].ToString();
                    csaluno_.res_uf = reader["RES_ESTADO"].ToString();
                    csaluno_.res_complemento = reader["RES_COMPLEMENTO"].ToString();
                    csaluno_.res_cep = reader["RES_CEP"].ToString();
                    //Info - Contato
                    csaluno_.res_telefone = reader["RES_TELEFONE"].ToString();
                    csaluno_.res_celular = reader["RES_CELULAR"].ToString();
                    csaluno_.e_mail = reader["E_MAIL"].ToString();
                    //Info - Trabalho
                    csaluno_.trabalho = reader["TRABALHO"].ToString();
                    csaluno_.trab_cidade = reader["TRAB_CIDADE"].ToString();
                    csaluno_.trab_estado = reader["TRAB_ESTADO"].ToString();
                    csaluno_.trab_telefone = reader["TRAB_TELEFONE"].ToString();

                    list_.Add(csaluno_);
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return list_;
        }

        public void sel_aluno(string n_mat_pesquisa = "", string rg_pesquisa = "")
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ALUNOS ";

            if (n_mat_pesquisa != string.Empty)
            {
                sql_comm.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";
                sql_comm.Parameters.AddWithValue("@n_mat", n_mat_pesquisa);
            }
            if (rg_pesquisa != string.Empty)
            {
                sql_comm.CommandText = @"SELECT * FROM ALUNOS WHERE RG=@rg";
                sql_comm.Parameters.AddWithValue("@rg", rg_pesquisa);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {                
                    n_mat = reader["N_MAT"].ToString();
                    nome = reader["ALUNO"].ToString();
                    nome_social = reader["NOME_SOCIAL"].ToString();
                    rg = reader["RG"].ToString();
                    uf_rg = reader["UF_RG"].ToString();
                    orgao = reader["ORGAO"].ToString();
                    dat_rg = reader["EXP_RG"].ToString();
                    ra = reader["RA"].ToString();
                    dat_nasc = reader["DAT_NASC"].ToString();
                    nasc_cidade = reader["NASC_CIDADE"].ToString();
                    nasc_uf = reader["NASC_ESTADO"].ToString();
                    nasc_pais = reader["NASC_PAIS"].ToString();
                    sexo = reader["SEXO"].ToString();
                    nome_mae = reader["NOME_MAE"].ToString();
                    nome_pai = reader["NOME_PAI"].ToString();
                    if (reader["PORT_NEC"].ToString() == "SIM") port_nec = true; else port_nec = false;
                    nec = reader["NEC"].ToString();
                    estado_civil = reader["ESTADO_CIVIL"].ToString();
                    if (reader["ID_RACA"] != DBNull.Value) id_raca = Convert.ToInt16(reader["ID_RACA"].ToString());
                    cpf = reader["CPF"].ToString();
                    //Info -Situação
                    dat_mat = DateTime.Parse(reader["DAT_MAT"].ToString());
                    nome_ensino_atual = reader["ENSINO"].ToString();
                    id_ensino_atual = cs_disciplinas.troca_ensino_nome_por_id(nome_ensino_atual);
                    if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value) id_disciplina_atual = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"].ToString());
                    dt_ent_disciplina = reader["DT_ENT_DISCIPLINA"].ToString();
                    if (reader["ATIVO"] != DBNull.Value) ativo = Convert.ToInt16(reader["ATIVO"].ToString());
                    if (reader["CERTIDAO"] != DBNull.Value) certidao = Convert.ToInt16(reader["CERTIDAO"].ToString());
                    if (reader["HISTORICO"] != DBNull.Value) historico = Convert.ToInt16(reader["HISTORICO"].ToString());

                    if (reader["REMATRICULAS"] != DBNull.Value)
                    {
                        lista_rematriculas = reader["REMATRICULAS"].ToString().Split('|').Select(DateTime.Parse).ToList();
                    }

                    if (reader["CONCLUIDO"] != DBNull.Value) concluido = Convert.ToInt16(reader["CONCLUIDO"].ToString());
                    obs_passaporte = reader["OBS_PASSAPORTE"].ToString();
                    termo_mat = reader["TERMO_MAT"].ToString();
                    if (reader["ID_USUARIO_CAD"] != DBNull.Value) id_usuario_cad = Convert.ToInt16(reader["ID_USUARIO_CAD"].ToString());
                    if (reader["ID_USUARIO_MOD"] != DBNull.Value) id_usuario_mod = Convert.ToInt16(reader["ID_USUARIO_MOD"].ToString());
                    //Info - Endereço
                    res_endereco = reader["RES_ENDERECO"].ToString();
                    res_numero = reader["RES_NUMERO"].ToString();
                    res_bairro = reader["RES_BAIRRO"].ToString();
                    res_cidade = reader["RES_CIDADE"].ToString();
                    res_uf = reader["RES_ESTADO"].ToString();
                    res_complemento = reader["RES_COMPLEMENTO"].ToString();
                    res_cep = reader["RES_CEP"].ToString();
                    //Info - Contato
                    res_telefone = reader["RES_TELEFONE"].ToString();
                    res_celular = reader["RES_CELULAR"].ToString();
                    e_mail = reader["E_MAIL"].ToString();
                    //Info - Trabalho
                    trabalho = reader["TRABALHO"].ToString();
                    trab_cidade = reader["TRAB_CIDADE"].ToString();
                    trab_estado = reader["TRAB_ESTADO"].ToString();
                    trab_telefone = reader["TRAB_TELEFONE"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        private List<int> sel_aluno_catraca(string cartao)
        {
            List<int> list_ = new List<int>();

            return list_;
        }


   

        public void add_aluno_catraca(string cartao)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO ACESSO_CATRACA (ID_CARTAO, ESTADO)
                                     VALUES (@idcartao, @estado)";

            sql_comm.Parameters.AddWithValue("@idcartao", cartao);
            sql_comm.Parameters.AddWithValue("@estado", 0);
            
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

        public void upd_aluno_catraca(string cartao, string novo_cartao)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE ACESSO_CATRACA SET ID_CARTAO=@novo_cartao WHERE ID_CARTAO=@idcartao";

            sql_comm.Parameters.AddWithValue("@idcartao", cartao);
            sql_comm.Parameters.AddWithValue("@novo_cartao", novo_cartao);

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

        #endregion










        public DateTime ultima_presenca { get; set; }

        public List<csAlunos> lista_alunos(bool todos=true, int ativo_pesquisa=0, int concluido_pesquisa=0)
        {
            List<csAlunos> list_ = new List<csAlunos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ALUNO, DAT_MAT, REMATRICULAS FROM ALUNOS ";

            if (!todos)
            {
                sql_comm.CommandText += "WHERE ATIVO=@ativo_pesquisa ";
                sql_comm.Parameters.AddWithValue("@ativo_pesquisa", ativo_pesquisa);

                sql_comm.CommandText += "AND CONCLUIDO=@concluido_pesquisa";
                sql_comm.Parameters.AddWithValue("@concluido_pesquisa", concluido_pesquisa);
            }
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csAlunos csaluno_ = new csAlunos();
                    csaluno_.nome = reader["ALUNO"].ToString();
                    csaluno_.dat_mat = DateTime.Parse(reader["DAT_MAT"].ToString());
                    if (reader["REMATRICULAS"] != DBNull.Value)
                    {
                        csaluno_.lista_rematriculas = reader["REMATRICULAS"].ToString().Split('|').Select(DateTime.Parse).ToList();
                    }                    
                    list_.Add(csaluno_);
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return list_;
            
        }

        public List<csAlunos> lista_alunos_com_ultima_presenca(DateTime dat_ini_pesquisa_, DateTime dat_fim_pesquisa_)
        {
            List<csAlunos> list_ = new List<csAlunos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * 
                                     FROM(
                                          SELECT ALUNOS.N_MAT, ALUNOS.ALUNO, ALUNOS.RG, ALUNOS.ENSINO, ALUNOS.ID_DISCIPLINA_ATUAL, MAX(TAB.DATA_ATENDIMENTO) AS ULTIMA 
                                          FROM ALUNOS LEFT JOIN ATENDIMENTOS AS TAB
                                          ON ALUNOS.N_MAT = TAB.N_MAT
                                          WHERE TAB.DATA_ATENDIMENTO IS NOT NULL and ALUNOS.ativo = 1
                                          GROUP BY ALUNOS.N_MAT, ALUNOS.ALUNO, ALUNOS.RG, ALUNOS.ENSINO, ALUNOS.ID_DISCIPLINA_ATUAL
			                              ) AS PESQUISA
                                     WHERE PESQUISA.ULTIMA between @dat_ini_pesquisa_ AND @dat_fim_pesquisa_
                                     ORDER BY PESQUISA.ULTIMA DESC ";

            sql_comm.Parameters.AddWithValue("@dat_ini_pesquisa_",dat_ini_pesquisa_.ToString("dd/MM/yyyy"));
            sql_comm.Parameters.AddWithValue("@dat_fim_pesquisa_", dat_fim_pesquisa_.AddDays(1).ToString("dd/MM/yyyy"));

            int cont = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csAlunos csaluno_ = new csAlunos();
                    csaluno_.n_mat = reader["N_MAT"].ToString();
                    csaluno_.nome = reader["ALUNO"].ToString();
                    csaluno_.rg = reader["RG"].ToString();
                    if(reader["ENSINO"] != DBNull.Value) csaluno_.id_ensino_atual = cs_disciplinas.troca_ensino_nome_por_id(reader["ENSINO"].ToString());
                    if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value) csaluno_.id_disciplina_atual = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"]);
                    csaluno_.ultima_presenca = DateTime.Parse(reader["ULTIMA"].ToString());
                    list_.Add(csaluno_);
                    cont++;
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return list_;            
        }

        public List<csAlunos> dados_aluno (string n_mat)
        {
            List<csAlunos> list_ = new List<csAlunos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csAlunos csaluno_ = new csAlunos();
                    //Info - Pessoal
                    csaluno_.n_mat = reader["N_MAT"].ToString();
                    csaluno_.nome = reader["ALUNO"].ToString();
                    csaluno_.nome_social = reader["NOME_SOCIAL"].ToString();
                    csaluno_.rg = reader["RG"].ToString();
                    csaluno_.uf_rg = reader["UF_RG"].ToString();
                    csaluno_.orgao = reader["ORGAO"].ToString();
                    csaluno_.dat_rg = reader["EXP_RG"].ToString();
                    csaluno_.ra = reader["RA"].ToString();
                    csaluno_.dat_nasc = reader["DAT_NASC"].ToString();
                    csaluno_.nasc_cidade = reader["NASC_CIDADE"].ToString();
                    csaluno_.nasc_uf = reader["NASC_ESTADO"].ToString();
                    csaluno_.nasc_pais = reader["NASC_PAIS"].ToString();
                    csaluno_.sexo = reader["SEXO"].ToString();
                    csaluno_.nome_mae = reader["NOME_MAE"].ToString();
                    csaluno_.nome_pai = reader["NOME_PAI"].ToString(); 
                    if (reader["PORT_NEC"].ToString() == "SIM") csaluno_.port_nec = true; else csaluno_.port_nec = false;
                    csaluno_.nec = reader["NEC"].ToString();
                    csaluno_.estado_civil = reader["ESTADO_CIVIL"].ToString();
                    if (reader["ID_RACA"] != DBNull.Value) csaluno_.id_raca = Convert.ToInt16(reader["ID_RACA"].ToString());
                    csaluno_.cpf = reader["CPF"].ToString();
                    //Info -Situação
                    csaluno_.dat_mat = DateTime.Parse(reader["DAT_MAT"].ToString());
                    csaluno_.id_ensino_atual = cs_disciplinas.troca_ensino_nome_por_id(reader["ENSINO"].ToString());
                    if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value) csaluno_.id_disciplina_atual = Convert.ToInt16(reader["ID_DISCIPLINA_ATUAL"].ToString());
                    csaluno_.dt_ent_disciplina = reader["DT_ENT_DISCIPLINA"].ToString();
                    if (reader["ATIVO"] != DBNull.Value) csaluno_.ativo = Convert.ToInt16(reader["ATIVO"].ToString());
                    if (reader["CERTIDAO"] != DBNull.Value) csaluno_.certidao = Convert.ToInt16(reader["CERTIDAO"].ToString());
                    if (reader["HISTORICO"] != DBNull.Value) csaluno_.historico = Convert.ToInt16(reader["HISTORICO"].ToString());

                    if (reader["REMATRICULAS"] != DBNull.Value)
                    {
                        csaluno_.lista_rematriculas = reader["REMATRICULAS"].ToString().Split('|').Select(DateTime.Parse).ToList();
                    }

                    if (reader["CONCLUIDO"] != DBNull.Value) csaluno_.concluido = Convert.ToInt16(reader["CONCLUIDO"].ToString());
                    csaluno_.obs_passaporte = reader["OBS_PASSAPORTE"].ToString();
                    csaluno_.termo_mat = reader["TERMO_MAT"].ToString();
                    if (reader["ID_USUARIO_CAD"] != DBNull.Value) csaluno_.id_usuario_cad = Convert.ToInt16(reader["ID_USUARIO_CAD"].ToString());
                    if (reader["ID_USUARIO_MOD"] != DBNull.Value) csaluno_.id_usuario_mod = Convert.ToInt16(reader["ID_USUARIO_MOD"].ToString());                    
                    //Info - Endereço
                    csaluno_.res_endereco = reader["RES_ENDERECO"].ToString();
                    csaluno_.res_numero = reader["RES_NUMERO"].ToString();
                    csaluno_.res_bairro = reader["RES_BAIRRO"].ToString();
                    csaluno_.res_cidade = reader["RES_CIDADE"].ToString();
                    csaluno_.res_uf = reader["RES_ESTADO"].ToString();
                    csaluno_.res_complemento = reader["RES_COMPLEMENTO"].ToString();
                    csaluno_.res_cep = reader["RES_CEP"].ToString();
                    //Info - Contato
                    csaluno_.res_telefone = reader["RES_TELEFONE"].ToString();
                    csaluno_.res_celular = reader["RES_CELULAR"].ToString();
                    csaluno_.e_mail = reader["E_MAIL"].ToString();
                    //Info - Trabalho
                    csaluno_.trabalho = reader["TRABALHO"].ToString();                   
                    csaluno_.trab_cidade = reader["TRAB_CIDADE"].ToString();
                    csaluno_.trab_estado = reader["TRAB_ESTADO"].ToString();                
                    csaluno_.trab_telefone = reader["TRAB_TELEFONE"].ToString();

                    list_.Add(csaluno_);
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return list_;
        }

        public int situacao_atual_na_lista;
        
        public bool existe_nmat(string n_mat)
        {
            bool a = false;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_MAT FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(reader["N_MAT"] !=  DBNull.Value)
                    {
                        a = true;
                    }              
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public bool existe_rg(string rg_pesquisa)
        {
            bool a = false;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT RG FROM ALUNOS WHERE RG=@rg_pesquisa";

            sql_comm.Parameters.AddWithValue("@rg_pesquisa", rg_pesquisa);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["RG"] != DBNull.Value)
                    {
                        a = true;
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
            return a;
        }
        
        public string troca_n_mat_nome(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ALUNO FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {
                    a = reader["ALUNO"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        //data_matricula
        public DateTime dat_mat__(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT DAT_MAT FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            DateTime a = DateTime.Now;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = DateTime.Parse(reader["DAT_MAT"].ToString());
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }
        //id_user_cad - matricula
        public int id_user_cad(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_USUARIO_CAD FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = Convert.ToInt32(reader["ID_USUARIO_CAD"]);
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        

        //Fotos
        public Image foto_aluno(string n_mat="")
        {
            Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            conf.get_configuracoes();

            Image foto = Resources.sem_foto;

            if (n_mat != "")
            {

                if (File.Exists(conf.caminho_fotos + n_mat + ".png"))
                {
                    System.Threading.Thread.Sleep(1 * 1000);      
                    FileStream stream = new FileStream(conf.caminho_fotos + n_mat + ".png", FileMode.Open, FileAccess.Read);
                    foto = Image.FromStream(stream);

                    stream.Close();


                    

                }
                else if (File.Exists(conf.caminho_fotos + n_mat + ".jpg"))
                {
                    System.Threading.Thread.Sleep(1 * 1000);
                    FileStream stream = new FileStream(conf.caminho_fotos + n_mat + ".jpg", FileMode.Open, FileAccess.Read);
                    foto = Image.FromStream(stream);

                    stream.Close();

          
                }
            }

            return foto;
        }

        //Notas
        public int id_disciplina_atual__(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_DISCIPLINA_ATUAL FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            int a=0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value)
                    {
                        a = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"]);
                    }                    
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string termo(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT TERMO_MAT FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["TERMO_MAT"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string ensino(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ENSINO FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["ENSINO"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string rg__(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT RG FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["RG"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string numat_por_rg(string rg)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_MAT FROM ALUNOS WHERE RG=@rg";

            sql_comm.Parameters.AddWithValue("@rg", rg);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["N_MAT"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        //Cor/Raca
        public List<string> racas()
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM RACAS";

            List<string> a = new List<string>();
            a.Add("");

            try
            {               
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {                    
                    a.Add(reader["RACA"].ToString());
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public int troca_id_raca(string raca)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM RACAS WHERE RACA=@raca";

            sql_comm.Parameters.AddWithValue("@raca", raca);

            int a = 0;         

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = Convert.ToInt32(reader["ID_RACA"]);
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string troca_raca_id(int id_raca)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM RACAS WHERE ID_RACA=@id_raca";

            sql_comm.Parameters.AddWithValue("@id_raca", id_raca);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["RACA"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        //Tabela - CommandText editavel
        public DataTable tab_alunos(string cmd_text)
        {
            //@"SELECT ___ FROM ALUNOS WHERE ___ ORDER BY ___ ";

            sql_comm.Connection = sql_conn;
            
            sql_comm.CommandText = cmd_text;

            //Determinar campos

            int part_A = cmd_text.IndexOf("SELECT") + "SELECT".Length + 1;

            int part_B = cmd_text.IndexOf("FROM");

            string a = cmd_text.Substring(part_A,part_B-part_A);
            a = a.Replace(" ","");a = a.Replace("ALUNOS.", "");
            a = a.Replace("ATENDIMENTOS.", "");
            a = a.Replace("HISTORICOS.", "");
                         
            List<string> lista_campos = a.Split(',').ToList();

            //Criação Tabela
            DataTable table = new DataTable();

            for (int z = 0; z < lista_campos.Count; z++)
            {
                table.Columns.Add(lista_campos[z]);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    table.Rows.Add();
                    for (int z = 0; z < lista_campos.Count; z++)
                    {
                        table.Rows[i][z] = reader[z].ToString();                  
                    }
                    i++;                   
                }
            }
            catch (Exception ex)
            {
                 csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return table;
        }

        #region Rematrícula

        public void add_rematricula(int n_mat, string rematriculas)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE ALUNOS SET REMATRICULAS=@rematriculas WHERE N_MAT='" + n_mat + "'";


            sql_comm.Parameters.AddWithValue("@rematriculas", rematriculas);
       

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public string sequencia_rematriculas(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT REMATRICULAS FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            string sequencia = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(reader["REMATRICULAS"] != DBNull.Value)
                    {
                        sequencia += reader["REMATRICULAS"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return sequencia;            
        }        

        public List<string> list_rematriculas(string n_mat)
        {
            

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT REMATRICULAS FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            List<string> list_ = new List<string>();

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(!(reader["REMATRICULAS"] is DBNull))
                    {
                        list_ = reader["REMATRICULAS"].ToString().Split('|').ToList<string>();;
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
         
            return list_;
        }

        #endregion

        #region Pesquisa de Alunos - Form

        public DataTable tab_alunos_nao_concluentes(string aluno, string rg, string n_mat)
        {
            sql_comm.Connection = sql_conn;
            //sql_comm.CommandText = @"SELECT ALUNO, RG, N_MAT FROM ALUNOS";
            sql_comm.CommandText = @"SELECT Aluno, RG, N_MAT FROM ALUNOS WHERE ALUNO LIKE @aluno AND RG LIKE @rg AND n_mat LIKE @n_mat AND CONCLUIDO=0 ORDER BY ALUNO ";

            sql_comm.Parameters.AddWithValue("@aluno", "%" + aluno + "%");
            sql_comm.Parameters.AddWithValue("@rg", rg + "%");
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat + "%");

            DataTable table = new DataTable();

            table.Columns.Add("nome");
            table.Columns.Add("rg");
            table.Columns.Add("n_mat");

            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["ALUNO"].ToString();
                    table.Rows[i][1] = reader["RG"].ToString();
                    table.Rows[i][2] = reader["N_MAT"].ToString();
                    i++;
                }
            }
            catch (Exception ex) 
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return table;
        }

        public DataTable tab_alunos_concluentes(string aluno, string rg, string n_mat)
        {
            sql_comm.Connection = sql_conn;
            //sql_comm.CommandText = @"SELECT ALUNO, RG, N_MAT FROM ALUNOS";
            sql_comm.CommandText = @"SELECT Aluno, RG, N_MAT FROM ALUNOS WHERE ALUNO LIKE @aluno AND RG LIKE @rg AND n_mat LIKE @n_mat AND CONCLUIDO=1 ORDER BY ALUNO ";

            sql_comm.Parameters.AddWithValue("@aluno", "%" + aluno + "%");
            sql_comm.Parameters.AddWithValue("@rg", rg + "%");
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat + "%");

            DataTable table = new DataTable();

            table.Columns.Add("nome");
            table.Columns.Add("rg");
            table.Columns.Add("n_mat");

            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["ALUNO"].ToString();
                    table.Rows[i][1] = reader["RG"].ToString();
                    table.Rows[i][2] = reader["N_MAT"].ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return table;
        }

        #endregion

        #region Conclusão

        public void concluir_aluno(int n_mat)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE ALUNOS SET ATIVO=0, CONCLUIDO=1 WHERE N_MAT=@n_mat";

            //where
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public bool esta_concluido(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT CONCLUIDO FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            bool a = false;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["CONCLUIDO"].ToString() == "1")
                    {
                        a = true;
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }



        #endregion

        #region ATIVAR-INATIVAR

        public void ativa_inativa_aluno(string n_mat, int opc, int id_user_mod)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE ALUNOS SET ATIVO=@opc, ID_USUARIO_MOD=@id_user_mod, DAT_MOD=@dat_mod WHERE N_MAT=@n_mat";

            //opção - 0 - Inativar/ 1 - Ativar
            sql_comm.Parameters.AddWithValue("@opc", opc);
            sql_comm.Parameters.AddWithValue("@id_user_mod", id_user_mod);
            sql_comm.Parameters.AddWithValue("@dat_mod", DateTime.Now);

            //where
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();                
            }
        }

        public bool esta_ativo(string n_mat)
        {
            bool a = false;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ATIVO FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ATIVO"].ToString() == "1")
                    {
                        a = true;
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        #endregion

        #region Lista Inativados | Ativados

        public DataTable tab_alterecao_ativar_inativar(int situacao, string dat_ini = "0", string dat_fin = "0")
        {
            DataTable a = new DataTable();

            a.Columns.Add("n_mat");         //0
            a.Columns.Add("data");          //1
            a.Columns.Add("id_user");       //2
            a.Columns.Add("motivo_origem"); //3
            

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_MAT, DATA_ALTERACAO, ID_USER_ALTERACAO, MOTIVO_ORIGEM FROM LISTA_ALTERACAO 
                                         WHERE SITUACAO=@situacao";

            if (dat_ini != "0")
                sql_comm.CommandText = @"SELECT N_MAT, DATA_ALTERACAO, ID_USER_ALTERACAO, MOTIVO_ORIGEM FROM LISTA_ALTERACAO 
                                         WHERE SITUACAO=@situacao AND DATA_ALTERACAO BETWEEN '" + dat_ini + " 07:00:00' AND '" + dat_ini + " 23:00:00'";

            if (dat_fin != "0")
                sql_comm.CommandText = @"SELECT N_MAT, DATA_ALTERACAO, ID_USER_ALTERACAO, MOTIVO_ORIGEM FROM LISTA_ALTERACAO 
                                         WHERE SITUACAO=@situacao AND DATA_ALTERACAO BETWEEN '" + dat_ini + " 07:00:00' AND '" + dat_fin + " 23:00:00'";

            sql_comm.Parameters.AddWithValue("@situacao", situacao);
            
            int i =0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Rows.Add();
                    a.Rows[i][0] = reader["N_MAT"].ToString();
                    a.Rows[i][1] = reader["DATA_ALTERACAO"].ToString();
                    a.Rows[i][2] = reader["ID_USER_ALTERACAO"].ToString();
                    a.Rows[i][3] = reader["MOTIVO_ORIGEM"].ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public void add_mod_lista_ativa_inativa(string n_mat, int opc, int id_user_alteracao, string motivo_origem)
        {
            //verifica se já esta na lista de alteracaoes
            if (verifica_alteracao(n_mat))
            {
                if (opc == 0)
                {
                    //mod na lista para inativar.
                    mod_lista_ativa_inativa(n_mat, 0, id_user_alteracao, motivo_origem);
                }
                else
                {
                    //mod na lista para ativar.
                    mod_lista_ativa_inativa(n_mat, 1, id_user_alteracao, motivo_origem);
                }
            }
            else
            {
                if (opc == 0)
                {
                    //add na lista para inativar.
                    add_lista_ativa_inativa(n_mat, 0, id_user_alteracao, motivo_origem);
                }
                else
                {
                    //add na lista para ativar.
                    add_lista_ativa_inativa(n_mat, 1, id_user_alteracao, motivo_origem);
                }
            }
        }

        public void add_lista_ativa_inativa(string n_mat, int situacao, int id_user_alteracao, string motivo_origem)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"INSERT INTO LISTA_ALTERACAO (N_MAT, SITUACAO, DATA_ALTERACAO, ID_USER_ALTERACAO, MOTIVO_ORIGEM) 
                                               VALUES (@n_mat, @situacao, @data_alteracao, @id_user_alteracao, @motivo_origem)";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@situacao", situacao);
            sql_comm.Parameters.AddWithValue("@data_alteracao", DateTime.Now.ToShortDateString());
            sql_comm.Parameters.AddWithValue("@id_user_alteracao", id_user_alteracao);
            sql_comm.Parameters.AddWithValue("@motivo_origem", motivo_origem);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {

                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public void mod_lista_ativa_inativa(string n_mat, int situacao, int id_user_alteracao, string motivo_origem)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE LISTA_ALTERACAO SET SITUACAO=@situacao, 
                                                                ID_USER_ALTERACAO=@id_user_alteracao, 
                                                                MOTIVO_ORIGEM=@motivo_origem, 
                                                                DATA_ALTERACAO=@data_alteracao  
                                                                WHERE N_MAT=@n_mat";
                                                                
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@situacao", situacao);
            sql_comm.Parameters.AddWithValue("@data_alteracao", DateTime.Now.ToShortDateString());
            sql_comm.Parameters.AddWithValue("@id_user_alteracao", id_user_alteracao);
            sql_comm.Parameters.AddWithValue("@motivo_origem", motivo_origem);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {

                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public void remove_lista_alteracao(int n_mat)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"DELETE FROM LISTA_ALTERACAO WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public bool verifica_alteracao(string n_mat)
        {
            bool a = false;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_MAT, SITUACAO FROM LISTA_ALTERACAO WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);


            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = true;

                    situacao_atual_na_lista = Convert.ToInt32(reader["SITUACAO"]);
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string data_ativado(int n_mat)
        {
            string a = "";

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT DATA_ALTERACAO FROM LISTA_ALTERACAO WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);


            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["DATA_ALTERACAO"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        #endregion

        #region OBS_passaporte

        public string obs_passaporte__(string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT OBS_PASSAPORTE FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["OBS_PASSAPORTE"].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;           
        }

        public void upt_obs_passporte(string n_mat, string obs)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE ALUNOS SET OBS_PASSAPORTE=@obs WHERE N_MAT=@n_mat";

           
            sql_comm.Parameters.AddWithValue("@obs", obs);

            //where
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();                  
            }
        }

        #endregion

        #region Reparacao   

        public DataTable tab_novo_antigo()
        {
            DataTable tb = new DataTable();

            tb.Columns.Add("1");
            tb.Columns.Add("2");

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM TAB_REPARACAO";

            //sql_comm.Parameters.AddWithValue("@n_antigo",n_antigo);

            //string a = "";
            int i = 0; 

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    tb.Rows.Add();
                    tb.Rows[i][0] = reader["ATENDIMENTOS_ANTIGO"].ToString();
                    tb.Rows[i][1] = reader["ATENDIMENTOS_NOVO"].ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return tb;
         
        }

        public void repar(List<string> lista ,string tabela_saida, string tabela_entrada, string campo)
        {

            Notas.csNotas cs_notas = new Notas.csNotas();

            #region Novo   

            for (int i = 0; i < lista.Count; i++)
            {
                string valor_1 = consulta_hist_m(lista[i], tabela_saida, campo);
                

                if(valor_1 != "") //Caso o valor exista na tabela hist_m
                {
                    string valor_2 = consulta_hist_aluno_m(lista[i], tabela_entrada, campo);
                    if(valor_2 == "") //Caso o valor NÃO exista na tabela hist_aluno_m
                    {
                        update_hist(lista[i], tabela_entrada, valor_1, campo);
                    }
                }
            }


            #endregion


            #region Usado

            //DataTable tab = tab_novo_antigo();

            //for (int i = 0; i < tab.Rows.Count; i++ )
            //{
            //    sql_comm.Connection = sql_conn;

            //    sql_comm.CommandText = @"UPDATE NOTAS SET ID_ATENDIMENTO=@atend_novo WHERE ID_ATENDIMENTO=@atend_antigo";

            //    //novo
            //    sql_comm.Parameters.AddWithValue("@atend_novo", tab.Rows[i][1]);

            //    //where - antigo
            //    sql_comm.Parameters.AddWithValue("@atend_antigo", tab.Rows[i][0]);

            //    try
            //    {
            //        sql_conn.Open();
            //        sql_comm.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //    finally
            //    {
            //        sql_comm.Parameters.Clear();
            //        sql_conn.Close();
            //    }
            //}

            #endregion




        }

        public string consulta_hist_m(string n,string tabela_saida, string campo)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT " + campo + " FROM " + tabela_saida + " WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {                    
                    a = reader[campo].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string consulta_hist_aluno_m(string n,string tabela_entrada, string campo)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT " + campo + " FROM " + tabela_entrada + " WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader[campo].ToString();
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public void update_hist(string n, string tabela_entrada, string valor, string campo)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE " + tabela_entrada + " SET " + campo + "=@valor WHERE N_MAT=@n_mat";

           
            sql_comm.Parameters.AddWithValue("@valor", valor);
            //where
            sql_comm.Parameters.AddWithValue("@n_mat", n);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        #endregion

        public int qtd_alunos_disciplinas(int id_disciplina_pesquisa)
        {
            int i = 0;            
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT count(*) as QTD from ALUNOS WHERE ID_DISCIPLINA_ATUAL=@id_disciplina_pesquisa";
            sql_comm.Parameters.AddWithValue("@id_disciplina_pesquisa", id_disciplina_pesquisa);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["QTD"] != DBNull.Value)
                    {
                        i = Convert.ToInt32(reader["QTD"]);
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
            return i;
        }

        public int qtd_matriculas(string dat_ini, string dat_fin, string periodo="TODOS")
        {
            int i = 0;
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"select COUNT(*) AS QTD from alunos where dat_mat between '";
            
            if (periodo=="TODOS")
            {
                sql_comm.CommandText += dat_ini + " 08:00:00' and '" + dat_fin + " 23:00:00'";
            }
            else if (periodo == "MANHÃ")
            {
                sql_comm.CommandText += dat_ini + " 08:00:00' and '" + dat_fin + " 12:00:00'";
            }
            else if (periodo == "TARDE")
            {
                sql_comm.CommandText += dat_ini + " 12:00:00' and '" + dat_fin + " 18:00:00'";
            }
            else
            {
                sql_comm.CommandText += dat_ini + " 18:00:00' and '" + dat_fin + " 23:00:00'";
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["QTD"] != DBNull.Value)
                    {
                        i = Convert.ToInt32(reader["QTD"]);
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
            return i;
        }

        public int qtd_alunos_ativos(int ativo = 1, int concluido=0)
        {
            int i = 0;
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"select COUNT(*) AS QTD from alunos where ATIVO = @ativo and CONCLUIDO=@concluido";

            sql_comm.Parameters.AddWithValue("ativo", ativo);
            sql_comm.Parameters.AddWithValue("concluido", concluido);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["QTD"] != DBNull.Value)
                    {
                        i = Convert.ToInt32(reader["QTD"]);
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
            return i;
        }

        public List<int> list_ids_users_matriculas(string dat_ini, string dat_fin, string periodo = "TODOS")
        {
            List<int> list_ = new List<int>();
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_USUARIO_CAD, COUNT(*) AS QTD FROM ALUNOS where DAT_MAT BETWEEN '";

            if (periodo == "TODOS")
            {
                sql_comm.CommandText += dat_ini + " 08:00:00' and '" + dat_fin + " 23:00:00' GROUP BY ID_USUARIO_CAD";
            }                                       
            else if (periodo == "MANHÃ")            
            {                                       
                sql_comm.CommandText += dat_ini + " 08:00:00' and '" + dat_fin + " 12:00:00' GROUP BY ID_USUARIO_CAD";
            }                                       
            else if (periodo == "TARDE")            
            {                                       
                sql_comm.CommandText += dat_ini + " 12:00:00' and '" + dat_fin + " 18:00:00' GROUP BY ID_USUARIO_CAD";
            }                                       
            else                                    
            {                                       
                sql_comm.CommandText += dat_ini + " 18:00:00' and '" + dat_fin + " 23:00:00' GROUP BY ID_USUARIO_CAD";
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID_USUARIO_CAD"] != DBNull.Value)
                    {
                        list_.Add(Convert.ToInt32(reader["ID_USUARIO_CAD"]));
                        list_.Add(Convert.ToInt32(reader["QTD"]));
                    }
                    else
                    {
                        list_.Add(0);
                        list_.Add(Convert.ToInt32(reader["QTD"]));
                    }
                }
            }
            catch (Exception ex)
            {
                csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
            return list_;
        }

        public void atrib_disciplina_aluno(string n_mat_, int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE ALUNOS SET ID_DISCIPLINA_ATUAL=@id_disciplina_atual, DT_ENT_DISCIPLINA=@dt_ent_disciplina WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat_);
            sql_comm.Parameters.AddWithValue("@id_disciplina_atual", id_disciplina);
            sql_comm.Parameters.AddWithValue("@dt_ent_disciplina", DateTime.Now);
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

        //Numeros n_mat anteriores
        public class n_mat_antigo
        {
            public string n_mat{ get; set; }
            public string n_mat_antigo_{get;set;}
        }

        public List<n_mat_antigo> lista_n_mat_antigos(string n_mat_pesquisa="")
        {
            List<n_mat_antigo> list_ = new List<n_mat_antigo>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_MAT, N_MAT_ANTIGO FROM TAB_N_MAT_ANTIGOS ";

            if (n_mat_pesquisa != string.Empty) sql_comm.CommandText += " WHERE N_MAT='" + n_mat_pesquisa + "'";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {
                    n_mat_antigo n_mat_antigo__ = new n_mat_antigo();
                    n_mat_antigo__.n_mat = reader["N_MAT"].ToString();
                    n_mat_antigo__.n_mat_antigo_ = reader["N_MAT_ANTIGO"].ToString();
                    list_.Add(n_mat_antigo__);
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

            return list_;
        }

    }
}
