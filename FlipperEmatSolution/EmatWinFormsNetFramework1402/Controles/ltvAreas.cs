using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Controles
{
    public partial class ltvAreas : ListView
    {
        public Classes.Area areaSelecionada { get; set; }

        public ltvAreas()
        {
            InitializeComponent();

            this.View = View.Details;
        }

        

        
    }
}
