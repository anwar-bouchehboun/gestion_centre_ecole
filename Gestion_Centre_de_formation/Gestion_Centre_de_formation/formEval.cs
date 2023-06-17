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
    public partial class formEval : Form
    {
        public formEval()
        {
            InitializeComponent();
        }
        Gestion_EcoleEntities4 data = new Gestion_EcoleEntities4();
        private void button5_Click(object sender, EventArgs e)
        {
            data.SaveChanges();
            MessageBox.Show("Save");
            affiche();
            
           
        }

        private void formEval_Load(object sender, EventArgs e)
        {
            RECHERCHECOMBO();
            comboMatier();
            comboExam();
            combostagaire();
            affiche();
            a++;
        }

        private void RECHERCHECOMBO()
        {
            var res = data.Stagaire. ToList();
            comboBox4.DataSource = res;
            comboBox4.DisplayMember = "Cin";
            comboBox4.ValueMember = "n_ins";
        }

        private void affiche()
        {
           
            var res = data.Evaluation.Select(o => new
            {
                Cin_Stagaire = o.Stagaire.Cin,
                Nom_Stagaire = o.Stagaire.nom,
                Prenom_Stagaire = o.Stagaire.prenom,
                Obervation = o.observation,
                Date_Evaliation = o.dateEv,
                Exam = o.Exam.Intitule,
                Matiere = o.Matiere.intitule ,
                Note = o.note,
                Coefficient=o.Matiere.coef,
                Moyen_NC=o.note*o.Matiere.coef
             

        }).ToList();
            dataGridView1.DataSource = res;
           

        }
        private void comboMatier()
        {
            var res = data.Matiere.ToList();
            comboBox3.DataSource = res;
            comboBox3.DisplayMember = "intitule";
            comboBox3.ValueMember = "id_mat";
        }

        private void comboExam()
        {
            var res = data.Exam.ToList();
            comboBox2.DataSource = res;
            comboBox2.DisplayMember = "Intitule";
            comboBox2.ValueMember = "id_Exam";
        }

        private void combostagaire()
        {
            var res = data.Stagaire.ToList();
            comboBox1.DataSource = res;
            comboBox1.DisplayMember = "Cin";
            comboBox1.ValueMember = "n_ins";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Nouveau")
            {
                VIDER();

                button2.Text = "Ajouter";
            }
            else
            {
                  Evaluation m = new Evaluation();

                    m.note = float.Parse(textBox1.Text);
                    m.observation= textBox2.Text;
                m.dateEv = dateTimePicker1.Value;
                    m.n_ins =int.Parse( comboBox1.SelectedValue.ToString());
                    m.id_Exam= int.Parse(comboBox2.SelectedValue.ToString());
                    m.id_mat= int.Parse(comboBox3.SelectedValue.ToString());



                data.Evaluation.Add(m);
               
                    MessageBox.Show("Evaluiation ajouté !");
               
                button2.Text = "Nouveau";
            }
        }

        private void VIDER()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedValue = -1;
            comboBox2.SelectedValue = -1;
            comboBox3.SelectedValue = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox4.SelectedValue.ToString());
            var res = data.Evaluation.Where(o => o.n_ins == id).First();
            if (res != null)
            {
                res.note =int.Parse( textBox1.Text);
                res.observation = textBox2.Text;
                res.dateEv = dateTimePicker1.Value;
               
                res.id_Exam = int.Parse(comboBox2.SelectedValue.ToString());
                res.id_mat = int.Parse(comboBox3.SelectedValue.ToString());
                MessageBox.Show("Evaluiation Modifier");
                VIDER();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox4.SelectedValue.ToString());
            var res = data.Evaluation.Where(o => o.n_ins == id).First();
            if (res!=null)
            {
                data.Evaluation.Remove(res);
                MessageBox.Show("Evaluiation Suprimer");
                VIDER();
            }

        }
        int a = 0;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //float s = 0;
            if (a!=0)
            {
                
            int id = int.Parse(comboBox4.SelectedValue.ToString());
                //var res = data.Evaluation.Where(x => x.n_ins == id).Select(o => new
                //{
                //    Cin_Stagaire = o.Stagaire.Cin,
                //    Nom_Stagaire = o.Stagaire.nom,
                //    Prenom_Stagaire = o.Stagaire.prenom,
                //    Obervation = o.observation,
                //    Date_Evaliation = o.dateEv,
                //    Exam = o.Exam.Intitule,
                //    Matiere = o.Matiere.intitule,
                //    Note = o.note,
                //    Coefficient = o.Matiere.coef,
                //    Moyen_NC = o.note * o.Matiere.coef



                //}).ToList();
                //dataGridView1.DataSource = res;
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    s += float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString());
                //}

                //textBox3.Text = s.ToString();
                //var cof = data.Matiere.Sum(m => m.coef);
                //textBox4.Text = cof.ToString();
                //textBox5.Text = (float.Parse(textBox3.Text) / int.Parse(textBox4.Text)).ToString();

                var res = data.Evaluation.FirstOrDefault(s => s.n_ins == id);
                if (res != null)
                {

                    textBox1.Text = res.note.ToString();
                    textBox2.Text = res.observation;
                    dateTimePicker1.Value = DateTime.Parse(res.dateEv.Value.ToString());
                    comboBox1.SelectedValue = res.n_ins;
                    comboBox2.SelectedValue = res.id_Exam;
                    comboBox3.SelectedValue = res.id_mat;
                }
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Gestion_Stagire f = new Gestion_Stagire();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FRMEXAM f = new FRMEXAM();
            f.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmMat f = new frmMat();
            f.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox1.Text = row.Cells[7].Value.ToString();
                      textBox2.Text = row.Cells[3].Value.ToString();
                  dateTimePicker1.Text = row.Cells[4].Value.ToString();
                  comboBox1.Text = row.Cells[0].Value.ToString();
                  comboBox2.Text = row.Cells[5].Value.ToString();
                     comboBox3.Text = row.Cells[6].Value.ToString();
            }
        }
    }
}
