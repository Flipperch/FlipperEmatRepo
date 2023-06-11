using System;

using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmSplash : Form
    {
        //TODO:: Implementar splash scream do emat novamente
        private delegate void ProgressDelegate(int progress);
        private ProgressDelegate del;

        public frmSplash()
        {
            InitializeComponent();
            //this.progressBar1.Maximum = 100;
            del = this.UpdateProgressInternal;
        }

        private void UpdateProgressInternal(int progress)
        {
            if (this.Handle == null)
            {
                return;
            }
            //this.progressBar1.Value = progress;
        }
        public void UpdateProgress(int progress)
        {
            this.Invoke(del, progress);
        }
    }
}