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
    public partial class Liste_de_note_stagaire : Form
    {
        public Liste_de_note_stagaire()
        {
            InitializeComponent();
        }
        Gestion_EcoleEntities4 data = new Gestion_EcoleEntities4();
        private void Liste_de_note_stagaire_Load(object sender, EventArgs e)
        {
            affiche();
            combostagaire();
            a++;
        }
        private void combostagaire()
        {
            var res = data.Stagaire.ToList();
            comboBox4.DataSource = res;
            comboBox4.DisplayMember = "Cin";
            comboBox4.ValueMember = "n_ins";
            comboBox4.SelectedValue = -1;
        }
        private void affiche()
        {
         

           
            var cof = data.Matiere.Sum(m => m.coef);
            textBox4.Text = cof.ToString();
           

        }
        int a = 0;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            float s = 0;
            if (a != 0)
            {

                int id = int.Parse(comboBox4.SelectedValue.ToString());
                var res = data.Evaluation.Where(x => x.n_ins == id).Select(o => new
                {
                    Cin_Stagaire = o.Stagaire.Cin,
                    Nom_Stagaire = o.Stagaire.nom,
                    Prenom_Stagaire = o.Stagaire.prenom,
                    Obervation = o.observation,
                    Date_Evaliation = o.dateEv,
                    Exam = o.Exam.Intitule,
                    Matiere = o.Matiere.intitule,
                    Note = o.note,
                    Coefficient = o.Matiere.coef,
                    Moyen_NC = o.note * o.Matiere.coef



                }).ToList();
                dataGridView1.DataSource = res;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    s += float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString());
                }

                textBox3.Text = s.ToString();
                var cof = data.Matiere.Sum(m => m.coef);
                textBox4.Text = cof.ToString();
                textBox5.Text = (float.Parse(textBox3.Text) / int.Parse(textBox4.Text)).ToString();

              
            }
        }
    }
}
