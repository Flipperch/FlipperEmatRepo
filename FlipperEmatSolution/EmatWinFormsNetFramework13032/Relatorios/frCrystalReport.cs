using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using CrystalDecisions.CrystalReports.Engine;



namespace EmatWinFormsNetFramework13032.Relatorios
{
    public partial class frCrystalReport : Form
    {
        public string n_mat_pesquisa { get; set; }

        public object cr_report;
        

        public frCrystalReport()
        {
            InitializeComponent();
        }

        private void frCrystalReport_Load(object sender, EventArgs e)
        {
            carrega_report();
        }

        public void carrega_report()
        {
            //List<object> cr_list = new List<object>();

            object cr = cr_report;

            //cr_list.Add(cr);

            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
        }

        

        
    }
}
