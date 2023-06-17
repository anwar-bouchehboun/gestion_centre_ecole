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
    public partial class GestioGroup : Form
    {
        public GestioGroup()
        {
            InitializeComponent();
        }
        DataSet ds ;
        BindingSource bs;
        DataTable dt;
        SqlCommandBuilder cb ;
        private void GestioGroup_Load(object sender, EventArgs e)
        {
            loadTables();

            LoadData();
            //DATAbiding();

        }

        private void loadTables()
        {
            ds = new DataSet();
            bs = new BindingSource();
            cb = new SqlCommandBuilder();
            //niveau
            Program.da2 = new SqlDataAdapter("select * from niveau", Program.cn);
            Program.da2.Fill(ds, "niveau");
            cmbNiveau.DataSource = ds.Tables["niveau"];
            cmbNiveau.DisplayMember = "intitule";
            cmbNiveau.ValueMember = "id_nv";
            // branche
            Program.da3 = new SqlDataAdapter("select * from branche", Program.cn);
            Program.da3.Fill(ds, "branche");
            cmbBranche.DataSource = ds.Tables["branche"];
            cmbBranche.DisplayMember = "intitule";
            cmbBranche.ValueMember = "id_br";
            //annescolaire
            Program.da4 = new SqlDataAdapter("select * from Annescolaire", Program.cn);
            Program.da4.Fill(ds, "Annescolaire");
            cmbAnneeScolaire.DataSource = ds.Tables["Annescolaire"];
            cmbAnneeScolaire.DisplayMember = "lebale";
            cmbAnneeScolaire.ValueMember = "id_As";
            //groupe
            Program.da = new SqlDataAdapter("select * from groupe", Program.cn);
            Program.da.Fill(ds, "groupe");
            dt = ds.Tables["groupe"];
            cb = new SqlCommandBuilder(Program.da);
            bs.DataSource = dt;

            //data relation niveau-groupe
            DataColumn[] cols_nv = { ds.Tables["niveau"].Columns["id_nv"] };
            ds.Tables["niveau"].PrimaryKey = cols_nv;

            DataColumn[] cols_gr = { ds.Tables["groupe"].Columns["id_gp"] };
            ds.Tables["groupe"].PrimaryKey = cols_gr;

            DataRelation drel = new DataRelation("niveau-groupe", ds.Tables["niveau"].Columns["id_nv"],
        ds.Tables["groupe"].Columns["id_nv"]);
            ds.Relations.Add(drel);

            //data relation Annescolaire-groupe
            DataColumn[] cols_A = { ds.Tables["Annescolaire"].Columns["id_As"] };
            ds.Tables["Annescolaire"].PrimaryKey = cols_A;

            DataRelation drel2 = new DataRelation("Annescolaire-groupe", ds.Tables["Annescolaire"].Columns["id_As"],
        ds.Tables["groupe"].Columns["id_As"]);
            ds.Relations.Add(drel2);

            DataColumn[] cols_B = { ds.Tables["branche"].Columns["id_br"] };
            ds.Tables["branche"].PrimaryKey = cols_B;
            //data relation branche-groupe

            DataRelation drel3 = new DataRelation("branche-groupe", ds.Tables["branche"].Columns["id_br"],
      ds.Tables["groupe"].Columns["id_br"]);
            ds.Relations.Add(drel3);
        }

        //private void DATAbiding()
        //{
        //    textBox1.DataBindings.Add("Text", dt, "id_gp");
        //    textBox2.DataBindings.Add("Text", dt, "intitule");
        //    cmbNiveau.DataBindings.Add("SelectedValue", dt, "id_nv");
        //    cmbBranche.DataBindings.Add("SelectedValue", dt, "id_br");
        //    cmbAnneeScolaire.DataBindings.Add("SelectedValue", dt, "id_As");
            
        //}

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow drgroupe in dt.Rows)
            {
                //drgroupe[0],
                dataGridView1.Rows.Add( drgroupe[1], drgroupe.GetParentRow("niveau-groupe")[1].ToString(),
                     drgroupe.GetParentRow("branche-groupe")[1].ToString(), drgroupe.GetParentRow("Annescolaire-groupe")[1].ToString());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Nouveau")
            {
                //textBox1.Text = "";
                textBox2.Text = "";
                cmbNiveau.SelectedValue = -1;
                cmbBranche.SelectedValue = -1;
                cmbAnneeScolaire.SelectedValue = -1;

                button2.Text = "Ajouter";
            }
            else
            {
                
                DataRow dr = dt.NewRow();
                dr[0] = 0;
                dr[1] = textBox2.Text;
                dr[2] = cmbAnneeScolaire.SelectedValue;                

                dr[3] = cmbBranche.SelectedValue;
                dr[4] = cmbNiveau.SelectedValue;
                
                //dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text);
                dt.Rows.Add(dr);
                cb.GetInsertCommand();
                MessageBox.Show("Groupe ajouté !");
                LoadData();
                button2.Text = "Nouveau";
            }
        }
        DataRow[] dr = null;
        private void button1_Click(object sender, EventArgs e)
        {

           dr= dt.Select($@"intitule = '{textBox2.Text}'");
           
            cmbNiveau.SelectedValue= dr[0][4].ToString();
            cmbBranche.SelectedValue = dr[0][3].ToString();
            cmbAnneeScolaire.SelectedValue = dr[0][2].ToString();
        }
        public void vider()
        {
            
            textBox2.Text = "";
            cmbNiveau.SelectedValue = -1;
            cmbBranche.SelectedValue = -1;
            cmbAnneeScolaire.SelectedValue = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Voulez vous Vraiment Modifier Cet Enregistrement ?",
              "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (dr != null)
                {
                  
                    dr[0][1] = textBox2.Text;
                    dr[0][4] = cmbNiveau.SelectedValue;
                    dr[0][3] = cmbBranche.SelectedValue;
                    dr[0][2] = cmbAnneeScolaire.SelectedValue;
                    cb.GetUpdateCommand();
                    MessageBox.Show("Modifier ligne ");
                    vider();
                }
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            Program.da.Update(dt);
            LoadData();
            MessageBox.Show("Donnes Savgarder");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmGestionBranche f = new frmGestionBranche();
            f.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmGestionAnneeScolaire f = new frmGestionAnneeScolaire();
            f.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbNiveau_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
