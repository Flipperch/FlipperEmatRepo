using EmatWinFormsApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmatWinFormsApp.Forms.Ajuda
{
    public partial class FrSobre : Form
    {
        public FrSobre()
        {
            InitializeComponent();

            lblVersion.Text = $"Version - {EmatGeneralServices.EmatVersion()}";
        }
    }
}
