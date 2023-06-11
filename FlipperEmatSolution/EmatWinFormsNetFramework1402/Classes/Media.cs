using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace EmatWinFormsNetFramework1402.Classes
{
    class Media
    {
        public Media()
        {
                        
        }

        public string Valor { get; set; }
        public string DtMedia { get; set; }
        public string DtModificacao { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public Usuario UsuarioDeModificacao { get; set; }
        public string Instituicao { get; set; }
        public Cidade Cidade { get; set; }
        public AtendimentoAluno AtendimentoAluno { get; set; }
    }

    
}
