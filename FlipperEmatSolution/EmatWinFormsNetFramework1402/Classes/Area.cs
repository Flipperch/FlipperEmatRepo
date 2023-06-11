using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Formularios;
using System.Data.SqlClient;
using System.Data;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Area
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public List<Disciplina> ListaDisciplina { get; set; }
    }
}
