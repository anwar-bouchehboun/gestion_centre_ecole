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
    public partial class frmMat : Form
    {
        public frmMat()
        {
            InitializeComponent();
        }
        Gestion_EcoleEntities4 data = new Gestion_EcoleEntities4();

        private void frmMat_Load(object sender, EventArgs e)
        {
            affiche();
        }

        private void affiche()
        {
            dataGridView1.DataSource = data.Matiere.Select(o => new
            {
                ID_Matiere = o.id_mat,
                Matiere = o.intitule,
                Coeftion = o.coef

            }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Nouveau")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
             

                button2.Text = "Ajouter";
            }
            else
            {
                int id = int.Parse(textBox1.Text);
                var res = data.Matiere.Where(o => o.id_mat == id).Count();
                if (res==0)
                {
                   
                    Matiere m = new Matiere();
                  
                    m.id_mat = int.Parse(textBox1.Text);

                    m.intitule = textBox2.Text;
                    m.coef = int.Parse(textBox3.Text);
                    data.Matiere.Add(m);
                   
                    MessageBox.Show("Matiere ajouté !");
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

        private void button1_Click(object sender, EventArgs e)
        {
            int id= int.Parse(textBox1.Text);
            var res = data.Matiere.FirstOrDefault(m => m.id_mat == id);
            if (res != null)
            {
                textBox2.Text = res.intitule;
                textBox3.Text = res.coef.ToString();

            }
            else MessageBox.Show("n'est pas exsit");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var res = data.Matiere.Where(o => o.id_mat == id).First();
            if (res !=null)
            {


                res.intitule = textBox2.Text;
                res.coef= int.Parse(textBox3.Text) ;

                MessageBox.Show("Matiere Modifier !");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var res = data.Matiere.Where(o => o.id_mat == id).First();
            if (res != null)
            {


                data.Matiere.Remove(res);

                MessageBox.Show("Matiere Supprimer !");
            }
        }
    }
}
