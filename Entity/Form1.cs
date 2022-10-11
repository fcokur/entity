using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entity
{
    public partial class Form1 : Form
    {
        ERPEntities db = new ERPEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            comboBox1.DataSource = db.Sehirler.ToList();
            comboBox1.DisplayMember = "SehirAdi";
            comboBox1.ValueMember = "Id";

            GridViewDoldur();

            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Personeller p = new Personeller();
            p.Ad = textBox1.Text;
            p.Soyad = textBox2.Text;
            p.DogumYeriId = ((Sehirler)comboBox1.SelectedItem).Id;

            db.Personeller.Add(p);
            db.SaveChanges(); // eklemenin veritabanına yansıtılmasını sağlıyor

            GridViewDoldur();


        }

        private void GridViewDoldur()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.Personeller.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            db.Personeller.Remove((Personeller)dataGridView1.CurrentRow.DataBoundItem);
            db.SaveChanges();
            GridViewDoldur();
        }
        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id  = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
           textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
           int sehirId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

            comboBox1.SelectedValue = sehirId;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
          
            Personeller p = db.Personeller.Find(id);
            p.Ad = textBox1.Text;
            p.Soyad = textBox2.Text;
            p.DogumYeriId = ((Sehirler)comboBox1.SelectedItem).Id;

            db.SaveChanges();
            GridViewDoldur();
        }
    }
}
