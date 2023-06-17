using CrystalDecisions.Windows.Forms;
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
    public partial class LA_Liste_De_stagaire : Form
    {
        public LA_Liste_De_stagaire()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        DataTable dt;



        private void LA_Liste_De_stagaire_Load(object sender, EventArgs e)
        {
            //Stagaire
            Program.da = new SqlDataAdapter("select * from Stagaire", Program.cn);
            Program.da.Fill(ds, "Stagaire");
            dt = ds.Tables["Stagaire"];
            //cb = new SqlCommandBuilder(Program.da);
            //groupe charge combo
            Program.da2 = new SqlDataAdapter("select * from groupe", Program.cn);
            Program.da2.Fill(ds, "groupe");
            //charge combo 
            combins.DataSource = dt;
            combins.DisplayMember = "n_ins";
            combins.ValueMember = "n_ins";
            combcin.DataSource = dt;
            combcin.DisplayMember = "Cin";
            combcin.ValueMember = "Cin";
            combogrp.DataSource = ds.Tables["groupe"];
            combogrp.DisplayMember = "intitule";
            combogrp.ValueMember = "id_gp";
            a++;
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

        private void combins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a == 1)
            {
                dataGridView1.Rows.Clear();
                foreach (DataRow drstagaire in dt.Rows)
                {
                    if (drstagaire[0].ToString() == combins.SelectedValue.ToString())
                    {
                        dataGridView1.Rows.Add(drstagaire[0], drstagaire[1], drstagaire[2], drstagaire[3],
                      drstagaire[4], drstagaire[5], drstagaire[6], drstagaire[7],
                      drstagaire[8], drstagaire[9], drstagaire.GetParentRow("groupe-stagaire")[1].ToString());
                    }

                }

            }

        }
        int a = 0;
        private void combcin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Program.da = new SqlDataAdapter("select s.* ,g.intitule from groupe g, Stagaire s where s.id_gp = g.id_gp and s.Cin='" + combcin.Text + "'", Program.cn);
            //Program.da.Fill(ds, "groupe Stagaire");
            //dataGridView1.DataSource = ds.Tables["groupe Stagaire"];
            if (a == 1)
            {
                dataGridView1.Rows.Clear();
                foreach (DataRow drstagaire in dt.Rows)
                {
                    if (drstagaire[6].ToString() == combcin.SelectedValue.ToString())
                    {
                        dataGridView1.Rows.Add(drstagaire[0], drstagaire[1], drstagaire[2], drstagaire[3],
                      drstagaire[4], drstagaire[5], drstagaire[6], drstagaire[7],
                      drstagaire[8], drstagaire[9], drstagaire.GetParentRow("groupe-stagaire")[1].ToString());
                    }

                }

            }

        }

        private void combogrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a == 1)
            {
                dataGridView1.Rows.Clear();
                foreach (DataRow drstagaire in dt.Rows)
                {
                    if (drstagaire[10].ToString() == combogrp.SelectedValue.ToString())
                    {
                        dataGridView1.Rows.Add(drstagaire[0], drstagaire[1], drstagaire[2], drstagaire[3],
                      drstagaire[4], drstagaire[5], drstagaire[6], drstagaire[7],
                      drstagaire[8], drstagaire[9], drstagaire.GetParentRow("groupe-stagaire")[1].ToString());
                    }

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rapport f = new rapport();
            f.Show();
        }
    }
}
