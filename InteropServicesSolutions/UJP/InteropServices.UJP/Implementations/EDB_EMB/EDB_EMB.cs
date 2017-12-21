using System.Configuration;
using System.IO;
using IBM.Data.Informix;
using InteropServices.UJP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class EDB_EMB: IEDB_EMB
    {
        string ConnectionString = "Host=" + "10.9.15.15" + "; " +
                                  "Service=" + "suslugi" + "; " +
                                  "Server=" + "etax20ujp" + "; " +
                                  "Database=" + "public_ujp" + "; " +
                                  "User Id=" + "mioa" + "; " +
                                  "Password=" + "MInt@ujp" + "; ";
        public List<EDB_EMB_Output> GetEDB(string EDB)
        {
           
                List<EDB_EMB_Output> output = new List<EDB_EMB_Output>();
                IfxConnection conn = new IfxConnection();
                conn.ConnectionString = ConnectionString;
                try
                {
                    IfxCommand cmd = null;
                    IfxCommand cmd1 = null;
                    IfxDataReader dr = null;
                    IfxDataReader dr1 = null;
                    conn.Open();
                    
                    if (EDB != "")
                    {
                        if (EDB.Length == 7)
                        {
                            cmd = new IfxCommand("select * from do_dpl_v1 where edb = ?", conn);
                            cmd.Parameters.Add(new IfxParameter("edb", EDB));
                            cmd1 = new IfxCommand("select * from do_vd_v1 where edb = ?", conn);
                            cmd1.Parameters.Add(new IfxParameter("edb", EDB));
                        }
                        else
                            throw new System.ArgumentException("Parameter length is not correct!", "EDB");
                    }
                    else
                        throw new System.ArgumentException("Parameter cannot be null!", "EDB");

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        EDB_EMB_Output temp = new EDB_EMB_Output();
                        temp.Edb = dr.GetString(0);
                        temp.Naziv = dr.GetString(1);
                        temp.Emb = dr.GetString(2);
                        temp.Ziro = dr.GetString(3);
                        temp.BankaZiro = dr.GetString(4);
                        temp.DatumPrijava = dr.GetString(5);
                        temp.PrijavaVid = dr.GetString(6);
                        temp.PrijavaStatus = dr.GetString(7);
                        temp.DejnostNace = dr.GetString(9);
                        temp.SedisteNaziv = dr.GetString(10);
                        temp.SedisteBroj = dr.GetString(11);
                        temp.SedisteUlica = dr.GetString(12);
                        temp.SedisteTelefon = dr.GetString(13);
                        temp.SedisteTelefax = dr.GetString(14);
                        output.Add(temp);
                    }
                    dr.Close();
                    dr1 = cmd1.ExecuteReader();
                    while (dr1.Read())
                    {
                        EDB_EMB_Output temp = new EDB_EMB_Output();
                        temp.Edb = dr1.GetString(0);
                        temp.Naziv = dr1.GetString(1);
                        temp.Emb = dr1.GetString(2);
                        temp.Ziro = dr1.GetString(3);
                        temp.BankaZiro = dr1.GetString(4);
                        temp.DatumPrijava = dr1.GetString(5);
                        temp.PrijavaVid = dr1.GetString(6);
                        temp.PrijavaStatus = dr1.GetString(7);
                        temp.DejnostNace = dr1.GetString(8);
                        temp.SedisteNaziv = dr1.GetString(9);
                        temp.SedisteBroj = dr1.GetString(10);
                        temp.SedisteUlica = dr1.GetString(11);
                        temp.SedisteTelefon = dr1.GetString(12);
                        temp.SedisteTelefax = dr1.GetString(13);
                        output.Add(temp);
                    }
                    dr1.Close();
                    conn.Close();
                }
                catch (IfxException ex)
                {
                    //LogToLocalFile("exception catch: " + ex.Message);
                    throw ex;
                }
                return output;
            
               
            }
        public List<EDB_EMB_Output> GetEMB(string EMB)
        {

            List<EDB_EMB_Output> output = new List<EDB_EMB_Output>();
            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = ConnectionString;
            try
            {
                IfxCommand cmd = null;
                IfxCommand cmd1 = null;
                IfxDataReader dr = null;
                IfxDataReader dr1 = null;
                conn.Open();

                if (EMB != "")
                {
                    if (EMB.Length == 13)
                    {
                        cmd = new IfxCommand("select * from do_dpl_v1 where mb = ?", conn);
                        cmd.Parameters.Add(new IfxParameter("mb", EMB));
                        cmd1 = new IfxCommand("select * from do_vd_v1 where mb = ?", conn);
                        cmd1.Parameters.Add(new IfxParameter("mb", EMB));
                       
                    }
                    else
                        throw new System.ArgumentException("Parameter length is not correct!", "EDB, EMB");
                }
                else
                    throw new System.ArgumentException("Parameter cannot be null!", "EDB, EMB");

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EDB_EMB_Output temp = new EDB_EMB_Output();
                    temp.Edb = dr.GetString(0);
                    temp.Naziv = dr.GetString(1);
                    temp.Emb = dr.GetString(2);
                    temp.Ziro = dr.GetString(3);
                    temp.BankaZiro = dr.GetString(4);
                    temp.DatumPrijava = dr.GetString(5);
                    temp.PrijavaVid = dr.GetString(6);
                    temp.PrijavaStatus = dr.GetString(7);
                    temp.DejnostNace = dr.GetString(9);
                    temp.SedisteNaziv = dr.GetString(10);
                    temp.SedisteBroj = dr.GetString(11);
                    temp.SedisteUlica = dr.GetString(12);
                    temp.SedisteTelefon = dr.GetString(13);
                    temp.SedisteTelefax = dr.GetString(14);
                    output.Add(temp);
                }
                dr.Close();
                dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    EDB_EMB_Output temp = new EDB_EMB_Output();
                    temp.Edb = dr1.GetString(0);
                    temp.Naziv = dr1.GetString(1);
                    temp.Emb = dr1.GetString(2);
                    temp.Ziro = dr1.GetString(3);
                    temp.BankaZiro = dr1.GetString(4);
                    temp.DatumPrijava = dr1.GetString(5);
                    temp.PrijavaVid = dr1.GetString(6);
                    temp.PrijavaStatus = dr1.GetString(7);
                    temp.DejnostNace = dr1.GetString(8);
                    temp.SedisteNaziv = dr1.GetString(9);
                    temp.SedisteBroj = dr1.GetString(10);
                    temp.SedisteUlica = dr1.GetString(11);
                    temp.SedisteTelefon = dr1.GetString(12);
                    temp.SedisteTelefax = dr1.GetString(13);
                    output.Add(temp);
                }
                dr1.Close();
                conn.Close();
            }
            catch (IfxException ex)
            {
                throw ex;
            }
            return output;

        }
        }
       
    }

