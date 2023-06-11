using System;
using System.Windows.Forms;



namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frCrystalReport : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
