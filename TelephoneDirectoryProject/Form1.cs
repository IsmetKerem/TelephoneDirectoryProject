using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace TelephoneDirectoryProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=TelephoneDirectoryDb;Integrated Security=True");


        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKISILER",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }


        void temizle()
        {
            txtad.Text = "";
            txtsoyad.Text = "";
            txtid.Text = "";
            txtmail.Text = "";
            msktelno.Text = "";
            txtad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TELEFON,MAIL) values (@p1,@p2,@p3,@p4) ", baglanti);
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2",txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3",msktelno.Text);
            komut.Parameters.AddWithValue("@p4", txtmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi sisteme kaydedildi. ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktelno.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();


        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From TBLKISILER where ID=" + txtid.Text, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi rehberden silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLKISILER set AD=@P1,SOYAD=@P2,TELEFON=@p3,MAIL=@p4 where ID=@p5", baglanti);
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktelno.Text);
            komut.Parameters.AddWithValue("@p4", txtmail.Text);
            komut.Parameters.AddWithValue("@p5", txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Güncelleme İşlemi Başarılı!");
            listele() ;
            temizle() ;

        }
    }
}
