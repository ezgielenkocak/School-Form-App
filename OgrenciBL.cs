using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OgrenciBL
    {
        public bool OgrenciEkle(Ogrenci ogr)
        {
            if (ogr == null)
            {
                throw new NullReferenceException("Ogrenci Eklerken Referans Null Geldi");
            }

            try
            {
                Helper hlp = new Helper();
                int sonuc = hlp.ExecuteNonQuery($"Insert into tblOgrenciler(Ad,Soyad,Numara,TcKimlik,SinifId)values('{ogr.Ad}','{ogr.Soyad}','{ogr.Numara}','{ogr.Tckimlik}',{ogr.Sinifid})");
                return sonuc > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool OgrenciGuncelle(Ogrenci ogr)
        {
            if (ogr == null)
            {
                throw new NullReferenceException("Ogrenci Güncellerken Referans Null Geldi");
            }

            try
            {
                Helper hlp = new Helper();
                int sonuc = hlp.ExecuteNonQuery($"Update tblOgrenciler set Ad='{ogr.Ad}',Soyad='{ogr.Soyad}',Numara='{ogr.Numara}',Sinifid={ogr.Sinifid} where OgrenciId={ogr.Ogrenciid}");
                return sonuc > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool OgrenciSil(int id)
        {
            try
            {
                Helper hlp = new Helper();
                int sonuc = hlp.ExecuteNonQuery($"Delete from tblOgrenciler where Ogrenciid={id}");
                return sonuc > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ogrenci OgrenciGetir(string numara)
        {
            Ogrenci ogr = null;
            Helper hlp = new Helper();
            SqlDataReader dr = hlp.ExecuteReader($"Select * from tblOgrenciler where Numara='{numara}'");
            if (dr.Read())
            {
                ogr = new Ogrenci();
                ogr.Ad = dr["Ad"].ToString();
                ogr.Numara = dr["Numara"].ToString();
                ogr.Soyad = dr["Soyad"].ToString();               
                ogr.Sinifid = Convert.ToInt32(dr["SinifId"]);
                ogr.Ogrenciid = Convert.ToInt32(dr["OgrenciId"]);
            }
            dr.Close();
            return ogr;

        }


    }
}
