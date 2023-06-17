using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Centre_de_formation
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            flowLayoutMenu.Hide();
            flowLayoutgroup.Hide();
            flowLayoutevl.Hide();
            flowLayoutstagaire.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GestioGroup f = new GestioGroup();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (flowLayoutstagaire.Visible)
            {
                flowLayoutstagaire.Hide();
            }
            else
            {
                flowLayoutstagaire.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (flowLayoutevl.Visible)
            {
                flowLayoutevl.Hide();
            }
            else
            {
                flowLayoutevl.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (flowLayoutgroup.Visible)
            {
                flowLayoutgroup.Hide();
            }
            else
            {
                flowLayoutgroup.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmGestionBranche f = new frmGestionBranche();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmGestionAnneeScolaire f = new frmGestionAnneeScolaire();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LA_Liste_De_stagaire f = new LA_Liste_De_stagaire();
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Gestion_Stagire f = new Gestion_Stagire();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            formEval f = new formEval();
            f.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FRMEXAM f = new FRMEXAM();
            f.ShowDialog();
        }

      

        private void button12_Click_1(object sender, EventArgs e)
        {
            frmMat f = new frmMat();
            f.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (flowLayoutMenu.Visible)
            {
                flowLayoutMenu.Hide();
            }
            else
            {
                flowLayoutMenu.Show();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Liste_de_note_stagaire f = new Liste_de_note_stagaire();
            f.ShowDialog();
        }
    }
}
