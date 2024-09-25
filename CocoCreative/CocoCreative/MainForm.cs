using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocoCreative
{
    public partial class MainForm : Form
    {
        private FormCluster childForm;
        public MainForm()
        {
            InitializeComponent();
        }

        //to show subform in mainform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


        private void btnCluster_Click(object sender, EventArgs e)
        {
            openChildForm(new FormCluster());
        }

        private void btnKaryawan_Click(object sender, EventArgs e)
        {
            openChildForm(new FormKaryawan());
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            openChildForm(new FormBarang());
        }


    }
}
