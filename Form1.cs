using BLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulAppSube2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                OgrenciBL obl = new OgrenciBL();
                MessageBox.Show(obl.OgrenciEkle(new Ogrenci { Ad = txtAd.Text, Soyad = txtSoyad.Text, Numara = txtNumara.Text, Sinifid = 1, Tckimlik = txtTcKimlik.Text }) ? "Ekleme Başarılı" : "Ekleme Başarısız");
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException)
            {
                MessageBox.Show("Veritabanı Hatası");
            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen Hata!");
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            OgrenciBL obl = new OgrenciBL();
            Ogrenci ogr = obl.OgrenciGetir(txtBul.Text.Trim());

            if (ogr==null)
            {
                MessageBox.Show("Öğrenci Bulunamadı!");
            }
            else
            {
                txtAd.Text = ogr.Ad;
                txtSoyad.Text = ogr.Soyad;
                txtNumara.Text = ogr.Numara;
                txtTcKimlik.Text = ogr.Tckimlik;
                cmbSiniflar.SelectedValue = ogr.Sinifid;
            }
        }


        //Garbage Collector
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cstr"].ConnectionString))
                {
                    if (cn != null)
                    {
                        cn.Open();
                    }
                    SqlCommand cmd = new SqlCommand($"Select SinifId,SinifAd,Kontenjan from tblSiniflar ", cn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    List<Sinif> siniflar = new List<Sinif>();


                    ; while (dr.Read())
                    {
                        siniflar.Add(new Sinif { Kontenjan = Convert.ToInt32(dr["Kontenjan"]), Sinifad = dr["SinifAd"].ToString(), Sinifid = Convert.ToInt32(dr["SinifId"]) });
                    }
                    dr.Close();
                    cmbSiniflar.DisplayMember = "Sinifad";
                    cmbSiniflar.ValueMember = "Sinifid";
                    cmbSiniflar.DataSource = siniflar;
                }





            }
            catch (Exception)
            {

            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)//Null Check
                {
                    cn.Close();
                    cn.Dispose();
                }
            }

        }
    }
}
//DRY: Don't Repeat Yourself