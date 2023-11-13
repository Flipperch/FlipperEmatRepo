using EmatWinFormsApp.Forms.Acesso;
using EmatWinFormsApp.Forms.Ajuda;
using EmatWinFormsApp.Services;
using EmatWinFormsApp.UserControls;
using System.ComponentModel;

namespace EmatWinFormsApp
{
    public partial class FrPrincipal : Form
    {
        // Declare the ContextMenuStrip control.
        private ContextMenuStrip mainContextMenuStrip;

        public FrPrincipal()
        {
            InitializeComponent();

            versionCheck();

            //addMainMenu();

            IdentityUserControl identityUserControl = new IdentityUserControl();

            this.Controls.Add(identityUserControl);
        }

        private void versionCheck()
        {
            this.Text = $"Emat - {EmatGeneralServices.EmatVersion()}";
        }

        private void addMainMenu()
        {
            // Create a new ContextMenuStrip control.
            mainContextMenuStrip = new ContextMenuStrip();

            // Attach an event handler for the
            // ContextMenuStrip control's Opening event.
            mainContextMenuStrip.Opening += new CancelEventHandler(cms_Opening);

            //// Create a new ToolStrip control.
            //ToolStrip ts = new ToolStrip();

            //// Create a ToolStripDropDownButton control and add it
            //// to the ToolStrip control's Items collections.
            //ToolStripDropDownButton mainToolStripDropDownButton = new ToolStripDropDownButton("Fruit", null, null, "Fruit");
            //ts.Items.Add(mainToolStripDropDownButton);

            //// Dock the ToolStrip control to the top of the form.
            //ts.Dock = DockStyle.Top;

            //// Assign the ContextMenuStrip control as the
            //// ToolStripDropDownButton control's DropDown menu.
            //mainToolStripDropDownButton.DropDown = mainContextMenuStrip;

            MenuStrip ms = new MenuStrip();

            ToolStripMenuItem acessoToolStripMenuItem = new ToolStripMenuItem("Acesso", null, null, "Acesso");
            ToolStripMenuItem ajudaToolStripMenuItem = new ToolStripMenuItem("Ajuda");

            ms.Items.Add(acessoToolStripMenuItem);
            ms.Items.Add(ajudaToolStripMenuItem);

            ms.Dock = DockStyle.Top;

            // Assign the MenuStrip control as the
            // ToolStripMenuItem's DropDown menu.
            acessoToolStripMenuItem.DropDown = mainContextMenuStrip;

            ajudaToolStripMenuItem.DropDown = mainContextMenuStrip;

            // Assign the ContextMenuStrip to the form's
            // ContextMenuStrip property.
            this.ContextMenuStrip = mainContextMenuStrip;

            //// Add the ToolStrip control to the Controls collection.
            //this.Controls.Add(ts);

            ////Add a button to the form and assign its ContextMenuStrip.
            //Button b = new Button();
            //b.Location = new System.Drawing.Point(60, 60);
            //this.Controls.Add(b);
            //b.ContextMenuStrip = mainContextMenuStrip;

            // Add the MenuStrip control last.
            // This is important for correct placement in the z-order.
            this.Controls.Add(ms);
        }

        // This event handler is invoked when the ContextMenuStrip
        // control's Opening event is raised. It demonstrates
        // dynamic item addition and dynamic SourceControl
        // determination with reuse.
        void cms_Opening(object sender, CancelEventArgs e)
        {
            // Acquire references to the owning control and item.
            Control c = mainContextMenuStrip.SourceControl as Control;
            ToolStripDropDownItem tsi = mainContextMenuStrip.OwnerItem as ToolStripDropDownItem;

            // Clear the ContextMenuStrip control's Items collection.
            mainContextMenuStrip.Items.Clear();

            // Check the source control first.
            if (c != null)
            {
                // Add custom item (Form)
                mainContextMenuStrip.Items.Add("Source: " + c.GetType().ToString());
            }
            else if (tsi != null)
            {
                // Add custom item (ToolStripDropDownButton or ToolStripMenuItem)
                mainContextMenuStrip.Items.Add("Source: " + tsi.GetType().ToString());
            }

            // Populate the ContextMenuStrip control with its default items.
            mainContextMenuStrip.Items.Add("-");
            mainContextMenuStrip.Items.Add("Perfil", null, new EventHandler(tsiPerfil_Click));
            mainContextMenuStrip.Items.Add("Alterar Senha", null, new EventHandler(tsiAlterarSenha_Click));
            mainContextMenuStrip.Items.Add("Sair", null, new EventHandler(tsiSair_Click));

            // Set Cancel to false.
            // It is optimized to true based on empty entry.
            e.Cancel = false;
        }

        private void tsiSair_Click(object? sender, EventArgs e) => throw new NotImplementedException();

        private void tsiPerfil_Click(object? sender, EventArgs e)
        {
            FrPerfil frPerfil = new FrPerfil();
            frPerfil.ShowDialog();
        }

        private void tsiAlterarSenha_Click(object? sender, EventArgs e)
        {
            FrAlterarSenha frAlterarSenha = new FrAlterarSenha();
            frAlterarSenha.ShowDialog();
        }

        private void tsiSobre_Click(object sender, EventArgs e)
        {
            FrSobre frSobre = new FrSobre();
            frSobre.ShowDialog();
        }
    }
}