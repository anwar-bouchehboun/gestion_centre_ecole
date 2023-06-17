using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gestion_Centre_de_formation
{
    public partial class rapport : Form
    {
        public rapport()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        private void rapport_Load(object sender, EventArgs e)
        {
      
           
        }
    CrystalReport1 cr = new CrystalReport1();
     

        private void button2_Click(object sender, EventArgs e)
        {

          

            crystalReportViewer1.ReportSource = cr;
            
            crystalReportViewer1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrystalReport2 cr2 = new CrystalReport2();
            cr2.SetParameterValue("idg", textBox1.Text);
           
            crystalReportViewer1.ReportSource = cr2;
            crystalReportViewer1.Refresh();
        }
    }
}
