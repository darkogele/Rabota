using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.UJP.Interfaces;
using IBM.Data.Informix;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class DataForAnnualRevenues : IDataForAnnualRevenues
    {
        string ConnectionString = "Host=" + "10.9.15.15" + "; " +
                                  "Service=" + "suslugi" + "; " +
                                  "Server=" + "etax20ujp" + "; " +
                                  "Database=" + "public_ujp" + "; " +
                                  "User Id=" + "mioa" + "; " +
                                  "Password=" + "MInt@ujp" + "; ";
        public AnnualRevenuesMTSPOutput GetAnnualRevenuesMTSP(string EDB, string year)
        {
            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = ConnectionString;
            AnnualRevenuesMTSPOutput output = new AnnualRevenuesMTSPOutput();
            try
            {
                IfxCommand cmd = null;
                IfxDataReader dr = null;
                conn.Open();
                if (EDB != "" && year != "")
                {
                    cmd = new IfxCommand("select * from servis_gdp_test where edb = ? and godina = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("edb", EDB));
                    cmd.Parameters.Add(new IfxParameter("godina", year));
                }
                else 
                    throw new System.ArgumentException("Parameter cannot be null", "EDB, year");

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    output.EDB = dr.GetString(0);
                    output.FirstName = dr.GetString(1);
                    output.LastName = dr.GetString(2);
                    output.Year = dr.GetString(3);
                    output.Plata_3m_Bruto = dr.GetString(4);
                    output.Plata_3m_Neto = dr.GetString(5);
                    output.Licniprim_Bruto = dr.GetString(6);
                    output.Licniprim_Neto = dr.GetString(7);
                    output.Prihodi_Svd = dr.GetString(8);
                    output.Prihodi_Zem = dr.GetString(9);
                    output.Prihodi_Imot = dr.GetString(10);
                    output.Prihodi_Avtor = dr.GetString(11);
                    output.Prihodi_Kapital = dr.GetString(12);
                    output.Prihodi_Kapdob = dr.GetString(13);
                    output.Prihodi_Igri = dr.GetString(14);
                    output.Prihodi_Drugo = dr.GetString(15);
                    output.Zabeleska = dr.GetString(28);
                }
                dr.Close();
                conn.Close();
            }
            catch (IfxException ex)
            {
                Console.WriteLine("Problem with connection attempt: " + ex.Message);
            }
            return output;
        }
        public AnnualRevenuesMONOutput GetAnnualRevenuesMON(string EDB, string year)
        {
            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = ConnectionString;
            AnnualRevenuesMONOutput output = new AnnualRevenuesMONOutput();
            try
            {
                IfxCommand cmd = null;
                IfxDataReader dr = null;
                conn.Open();
                if (EDB != "" && year != "")
                {
                    cmd = new IfxCommand("select * from servis_gdp_test where edb = ? and godina = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("edb", EDB));
                    cmd.Parameters.Add(new IfxParameter("godina", year));
                }
                else
                    throw new System.ArgumentException("Parameter cannot be null", "EDB, year");

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    output.EDB = dr.GetString(0);
                    output.FirstName = dr.GetString(1);
                    output.LastName = dr.GetString(2);
                    output.Year = dr.GetString(3);
                    output.Licniprim_Bruto = dr.GetString(6);
                    output.Licniprim_Neto = dr.GetString(7);
                    output.Prihodi_Svd = dr.GetString(8);
                    output.Prihodi_Zem = dr.GetString(9);
                    output.Prihodi_Imot = dr.GetString(10);
                    output.Prihodi_Avtor = dr.GetString(11);
                    output.Prihodi_Kapital = dr.GetString(12);
                    output.Prihodi_Kapdob = dr.GetString(13);
                    output.Prihodi_Igri = dr.GetString(14);
                    output.Prihodi_Drugo = dr.GetString(15);
                    output.Zabeleska = dr.GetString(28);
                }
                dr.Close();
                conn.Close();
            }
            catch (IfxException ex)
            {
                Console.WriteLine("Problem with connection attempt: " + ex.Message);
            }
            return output;
        }
        public AnnualRevenuesFZOOutput GetAnnualRevenuesFZO(string EDB, string year)
        {
            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = ConnectionString;
            AnnualRevenuesFZOOutput output = new AnnualRevenuesFZOOutput();
            try
            {
                IfxCommand cmd = null;
                IfxDataReader dr = null;
                conn.Open();
                if (EDB != "" && year != "")
                {
                    cmd = new IfxCommand("select * from servis_gdp_test where edb = ? and godina = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("edb", EDB));
                    cmd.Parameters.Add(new IfxParameter("godina", year));
                }
                else
                    throw new System.ArgumentException("Parameter cannot be null", "EDB, year");

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    output.EDB = dr.GetString(0);
                    output.FirstName = dr.GetString(1);
                    output.LastName = dr.GetString(2);
                    output.Year = dr.GetString(3);
                    output.FZO_Bruto = dr.GetString(16);
                    output.FZO_Neto = dr.GetString(17);
                    output.Zabeleska = dr.GetString(28);
                }
                dr.Close();
                conn.Close();
            }
            catch (IfxException ex)
            {
                Console.WriteLine("Problem with connection attempt: " + ex.Message);
            }
            return output;
        }
        public AnnualRevenuesKKKSOutput GetAnnualRevenuesKKKS(string EDB, string year)
        {
            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = ConnectionString;
            AnnualRevenuesKKKSOutput output = new AnnualRevenuesKKKSOutput();
            try
            {
                IfxCommand cmd = null;
                IfxDataReader dr = null;
                conn.Open();
                if (EDB != "" && year != "")
                {
                    cmd = new IfxCommand("select * from servis_gdp_test where edb = ? and godina = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("edb", EDB));
                    cmd.Parameters.Add(new IfxParameter("godina", year));
                }
                else
                    throw new System.ArgumentException("Parameter cannot be null", "EDB, year");

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    output.EDB = dr.GetString(0);
                    output.FirstName = dr.GetString(1);
                    output.LastName = dr.GetString(2);
                    output.Year = dr.GetString(3);
                    output.Pospl_mmgg = dr.GetString(18);
                    output.Pospl_Bruto = dr.GetString(19);
                    output.Pospl_Neto = dr.GetString(20);
                    output.Mes6_Broj = dr.GetString(21);
                    output.Mes6_Bruto = dr.GetString(22);
                    output.Mes6_Neto = dr.GetString(23);
                    output.Vk_Bruto = dr.GetString(24);
                    output.Vk_Neto = dr.GetString(25);
                    output.Drugo_Bruto = dr.GetString(26);
                    output.Drugo_Neto = dr.GetString(27);
                    output.Zabeleska = dr.GetString(28);
                }
                dr.Close();
                conn.Close();
            }
            catch (IfxException ex)
            {
                Console.WriteLine("Problem with connection attempt: " + ex.Message);
            }
            return output;
        }
    }
}
