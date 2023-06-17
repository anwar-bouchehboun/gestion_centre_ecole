using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Gestion_Centre_de_formation
{
    public partial class Gestion_Stagire : Form
    {
        public Gestion_Stagire()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        DataTable dt;
        SqlCommandBuilder cb = new SqlCommandBuilder();
        private void Gestion_Stagire_Load(object sender, EventArgs e)
        {   //Stagaire
            Program.da = new SqlDataAdapter("select * from Stagaire", Program.cn);
            Program.da.Fill(ds, "Stagaire");
            dt = ds.Tables["Stagaire"];
            cb = new SqlCommandBuilder(Program.da);
            //groupe charge combo
            Program.da2 = new SqlDataAdapter("select * from groupe", Program.cn);
            Program.da2.Fill(ds, "groupe");
           
            cmbxgrp.DataSource = ds.Tables["groupe"];
            cmbxgrp.DisplayMember = "intitule";
            cmbxgrp.ValueMember = "id_gp";
            //data relation
            //stagaire
            DataColumn[] clos_S = { ds.Tables["stagaire"].Columns["n_ins"] };
            ds.Tables["stagaire"].PrimaryKey = clos_S;
            //groupe
            DataColumn[] clos_g = { ds.Tables["groupe"].Columns["id_gp"] };
            ds.Tables["groupe"].PrimaryKey = clos_g;
            //data relation g/s
            DataRelation drl4 = new DataRelation("groupe-stagaire", ds.Tables["groupe"].Columns["id_gp"],
                                                                    ds.Tables["stagaire"].Columns["id_gp"]);
            ds.Relations.Add(drl4);
            laoddatagrid();
        }

        private void laoddatagrid()
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow drstagaire in ds.Tables["stagaire"].Rows)
            {
                dataGridView1.Rows.Add(drstagaire[0], drstagaire[1], drstagaire[2], drstagaire[3],
                    drstagaire[4], drstagaire[5], drstagaire[6], drstagaire[7],
                    drstagaire[8], drstagaire[9], drstagaire.GetParentRow("groupe-stagaire")[1].ToString());
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Nouveau")
            {
                txtins.Text = txtnom.Text = txtprenom.Text = txtnomAR.Text = txtprenomAR.Text = txtcin.Text = txtniveu.Text = string.Empty;
                txtadrres.Text = txttele.Text = string.Empty;
                dateTimePicker1.Value = DateTime.Today;
                cmbxgrp.SelectedValue = -1;


                button2.Text = "Ajouter";
            }
            else
            {


                DataRow dr = dt.NewRow();
                dr[0] = txtins.Text;
                dr[1] = txtnom.Text;
                dr[2] = txtprenom.Text;
                dr[3] = dateTimePicker1.Value;
                dr[4] = txtnomAR.Text;
                dr[5] = txtprenomAR.Text;
                dr[6] = txtcin.Text;
                dr[7] = txtniveu.Text;
                dr[8] = txtadrres.Text;
                dr[9] = txttele.Text;
                dr[10] = cmbxgrp.SelectedValue;

                //dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text);
                dt.Rows.Add(dr);
                cb.GetInsertCommand();
                MessageBox.Show("Livre ajouté !");
                
                button2.Text = "Nouveau";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.da.Update(dt);
            MessageBox.Show("save");
            laoddatagrid();
        }
        DataRow[] dr = null;
        private void button1_Click(object sender, EventArgs e)
        {
            dr = dt.Select($@"n_ins = {txtins.Text}");
            txtnom.Text = dr[0][1].ToString();

           
            txtprenom.Text = dr[0][2].ToString();
            dateTimePicker1.Value = DateTime.Parse( dr[0][3].ToString());
            txtnomAR.Text = dr[0][4].ToString();
            txtprenomAR.Text = dr[0][5].ToString();
            txtcin.Text = dr[0][6].ToString();
            txtniveu.Text = dr[0][7].ToString();
            txtadrres.Text = dr[0][8].ToString();
            txttele.Text = dr[0][9].ToString();
            cmbxgrp.SelectedValue = dr[0][10];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Voulez vous Vraiment Modifier Cet Enregistrement ?",
              "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (dr != null)
                {

                    dr[0][1] = txtnom.Text;
                    dr[0][2] = txtprenom.Text;
                    dr[0][3] = dateTimePicker1.Value;
                    dr[0][4] = txtnomAR.Text;
                    dr[0][5] = txtprenomAR.Text;
                    dr[0][6] = txtcin.Text;
                    dr[0][7] = txtniveu.Text;
                    dr[0][8] = txtadrres.Text;
                    dr[0][9] = txttele.Text;
                    dr[0][10] = cmbxgrp.SelectedValue;
                    cb.GetUpdateCommand();
               
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
            

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
