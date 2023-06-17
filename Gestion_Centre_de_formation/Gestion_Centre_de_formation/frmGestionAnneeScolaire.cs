using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Centre_de_formation
{
    public partial class frmGestionAnneeScolaire : Form
    {
        public frmGestionAnneeScolaire()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        DataTable dt;
        SqlCommandBuilder cb = new SqlCommandBuilder();
        private void frmGestionAnneeScolaire_Load(object sender, EventArgs e)
        {
            Program.da = new SqlDataAdapter("select * from Annescolaire", Program.cn);

            Program.da.Fill(ds, "Annescolaire");
            dt = ds.Tables["Annescolaire"];
            cb = new SqlCommandBuilder(Program.da);
            bs.DataSource = ds.Tables["Annescolaire"];
            dataGridView1.DataSource = bs;
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
                DataRow dr = dt.NewRow();
                dr[0] = textBox1.Text;
                dr[1] = textBox2.Text;


                dt.Rows.Add(dr);
                cb.GetInsertCommand();
              
                dataGridView1.DataSource = dt;
                MessageBox.Show("Livre ajouté !");
                button2.Text = "Nouveau";
            }
        }
        DataRow[] dr = null;
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res= MessageBox.Show("Voulez vous Vraiment Modifier Cet Enregistrement ?",
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res==DialogResult.OK)
            {
                if (dr != null)
                {
                    dr[0][0] = textBox1.Text;
                    dr[0][1] = textBox2.Text;


                    cb.GetUpdateCommand();
                    MessageBox.Show("Modifier ligne ");
                    vider();
                }
            }
            
        }
        public void vider()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult reponse = MessageBox.Show("Voulez vous Vraiment Supprimer Cet Enregistrement ?",
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (reponse == DialogResult.OK)
            {
                dr[0].Delete();
                cb.GetDeleteCommand();
                MessageBox.Show("Supprimer ligne ");
                vider();

            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dr = dt.Select($@"id_As = {textBox1.Text}");

            textBox2.Text = dr[0][1].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.da.Update(dt);
            MessageBox.Show("Donnes Savgarder");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
