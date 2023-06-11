using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Professor : Usuario
    {
        public Area Area { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
