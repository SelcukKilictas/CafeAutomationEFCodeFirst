using CafeAutomationCodeFirst.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeAutomationCodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private FrmMenu frmMenu;
        private FrmFloorSetting frmFloorSetting;
        private FrmCafe frmCafe;
        private FrmReport frmReport;

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (frmMenu == null || frmMenu.IsDisposed)
            {
                frmMenu = new FrmMenu();
            }
            //frmMenu.MdiParent = this;
            frmMenu.Show();
            frmMenu.WindowState = FormWindowState.Maximized;
        }

        private void btnFloorTable_Click(object sender, EventArgs e)
        {
            if (frmFloorSetting == null || frmFloorSetting.IsDisposed)
            {
                frmFloorSetting = new FrmFloorSetting();
            }
            frmFloorSetting.Show();
            frmFloorSetting.WindowState = FormWindowState.Maximized;
        }

        private void btnCafe_Click(object sender, EventArgs e)
        {
            if (frmCafe == null || frmCafe.IsDisposed)
            {
                frmCafe = new FrmCafe();
            }
            frmCafe.Show();
            frmCafe.WindowState = FormWindowState.Maximized;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (frmReport == null || frmReport.IsDisposed)
            {
                frmReport = new FrmReport();
            }
            frmReport.Show();
            frmReport.WindowState = FormWindowState.Maximized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Programı kapatmak istiyor musunuz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
    }
}
