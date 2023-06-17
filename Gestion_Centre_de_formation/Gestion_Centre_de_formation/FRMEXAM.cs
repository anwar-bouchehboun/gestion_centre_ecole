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
    public partial class FRMEXAM : Form
    {
        public FRMEXAM()
        {
            InitializeComponent();
        }
        Gestion_EcoleEntities4 data = new Gestion_EcoleEntities4();
        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var res = data.Exam.FirstOrDefault(m => m.id_Exam == id);
            if (res != null)
            {
                textBox2.Text =  res.Intitule;

            }
            else MessageBox.Show("n'est pas exsit");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Nouveau")
            {
                textBox1.Text = "";
                textBox2.Text = "";
              


                button2.Text = "Ajouter";
            }
            else
            {
                int id = int.Parse(textBox1.Text);
                var res = data.Exam.Where(o => o.id_Exam == id).Count();
                if (res == 0)
                {

                    Exam m = new Exam();

                    m.id_Exam = int.Parse(textBox1.Text);
                 
                 
                    m.Intitule = textBox2.Text;
                    data.Exam.Add(m);

                    MessageBox.Show("Exam ajouté !");
                }





                button2.Text = "Nouveau";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            data.SaveChanges();
            MessageBox.Show("save  !");
            affiche();
        }
        private void affiche()
        {
            dataGridView1.DataSource = data.Exam.Select(o => new
            {
                ID_Exam = o.id_Exam,
                INTITULE = o.Intitule,
              
            }).ToList();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var res = data.Exam.Where(o => o.id_Exam == id).First();
            if (res != null)
            {


                data.Exam.Remove(res);

                MessageBox.Show("Exam Supprimer !");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var res = data.Exam.Where(o => o.id_Exam == id).First();
            if (res != null)
            {

            
                res.Intitule = textBox2.Text;
               

                MessageBox.Show("Exam Modifier !");
            }
        }

        private void FRMEXAM_Load(object sender, EventArgs e)
        {
            affiche();
        }
    }
}
