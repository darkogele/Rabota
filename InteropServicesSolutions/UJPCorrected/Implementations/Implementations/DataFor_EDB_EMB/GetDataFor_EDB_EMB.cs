using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Interfaces.IDataFor_EDB_EMB;
using Contracts.Models.DataFor_EDB_EMB;
using IBM.Data.Informix;

namespace Implementations.Implementations.DataFor_EDB_EMB
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class GetDataFor_EDB_EMB : IEDB_EMB
    {
        string connectionString = ConfigurationSettings.AppSettings.Get("connectionStringEdbEmb");
        //public List<EDB_EMB_Output> GetEDB(string edb)
        //{

        //    var output = new List<EDB_EMB_Output>();
        //    var conn = new IfxConnection();
        //    conn.ConnectionString = connectionString;
        //    try
        //    {
        //        IfxCommand cmd = null;
        //        IfxCommand cmd1 = null;
        //        IfxDataReader dr = null;
        //        IfxDataReader dr1 = null;
        //        conn.Open();

        //        if (edb != "")
        //        {
        //            if (edb.Length == 7)
        //            {
        //                cmd = new IfxCommand("select * from do_dpl_v1 where edb = ?", conn);
        //                cmd.Parameters.Add(new IfxParameter("edb", edb));
        //                cmd1 = new IfxCommand("select * from do_vd_v1 where edb = ?", conn);
        //                cmd1.Parameters.Add(new IfxParameter("edb", edb));
        //            }
        //            else
        //            {
        //                var ex = new InteropFault
        //                {
        //                    Result = false,
        //                    ErrorMessage = "Сервисот врати грешка.",
        //                    ErrorDetails = "Должината на параметарот 'edb' не е точна!"
        //                };
        //                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
        //            }
        //        }
        //        else
        //        {
        //            var ex = new InteropFault
        //            {
        //                Result = false,
        //                ErrorMessage = "Сервисот врати грешка.",
        //                ErrorDetails = "Параметарот 'edb' не смее да биде празен!"
        //            };
        //            throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
        //        }

        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            var temp = new EDB_EMB_Output
        //            {
        //                Edb = dr.GetString(0),
        //                Naziv = dr.GetString(1),
        //                Emb = dr.GetString(2),
        //                Ziro = dr.GetString(3),
        //                BankaZiro = dr.GetString(4),
        //                DatumPrijava = dr.GetString(5),
        //                PrijavaVid = dr.GetString(6),
        //                PrijavaStatus = dr.GetString(7),
        //                DejnostNace = dr.GetString(9),
        //                SedisteNaziv = dr.GetString(10),
        //                SedisteBroj = dr.GetString(11),
        //                SedisteUlica = dr.GetString(12),
        //                SedisteTelefon = dr.GetString(13),
        //                SedisteTelefax = dr.GetString(14)
        //            };
        //            output.Add(temp);
        //        }
        //        dr.Close();
        //        dr1 = cmd1.ExecuteReader();
        //        while (dr1.Read())
        //        {
        //            var temp = new EDB_EMB_Output
        //            {
        //                Edb = dr1.GetString(0),
        //                Naziv = dr1.GetString(1),
        //                Emb = dr1.GetString(2),
        //                Ziro = dr1.GetString(3),
        //                BankaZiro = dr1.GetString(4),
        //                DatumPrijava = dr1.GetString(5),
        //                PrijavaVid = dr1.GetString(6),
        //                PrijavaStatus = dr1.GetString(7),
        //                DejnostNace = dr1.GetString(8),
        //                SedisteNaziv = dr1.GetString(9),
        //                SedisteBroj = dr1.GetString(10),
        //                SedisteUlica = dr1.GetString(11),
        //                SedisteTelefon = dr1.GetString(12),
        //                SedisteTelefax = dr1.GetString(13)
        //            };
        //            output.Add(temp);
        //        }
        //        dr1.Close();
        //        conn.Close();
        //    }
        //    catch (IfxException ex)
        //    {
        //        //LogToLocalFile("exception catch: " + ex.Message);
        //        throw ex;
        //    }
        //    return output;


        //}
        public List<EDB_EMB_Output> GetEMB(string emb)
        {

            var output = new List<EDB_EMB_Output>();
            var conn = new IfxConnection();
            conn.ConnectionString = connectionString;
            try
            {
                IfxCommand cmd = null;
                IfxCommand cmd1 = null;
                IfxDataReader dr = null;
                IfxDataReader dr1 = null;
                conn.Open();

                if (emb != "")
                {
                    if (emb.Length == 13)
                    {
                        cmd = new IfxCommand("select * from do_dpl_v1 where mb = ?", conn);
                        cmd.Parameters.Add(new IfxParameter("mb", emb));
                        cmd1 = new IfxCommand("select * from do_vd_v1 where mb = ?", conn);
                        cmd1.Parameters.Add(new IfxParameter("mb", emb));

                    }
                    else
                    {
                        var ex = new InteropFault
                        {
                            Result = false,
                            ErrorMessage = "Сервисот врати грешка.",
                            ErrorDetails = "Должината на параметарот 'EMB' не е точна!"
                        };
                        throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                    }
                }
                else
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Сервисот врати грешка.",
                        ErrorDetails = "Параметарот 'EMB' не смее да биде празен!"
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var temp = new EDB_EMB_Output
                    {
                        Edb = dr.GetString(0),
                        Naziv = dr.GetString(1),
                        Emb = dr.GetString(2),
                        Ziro = dr.GetString(3),
                        BankaZiro = dr.GetString(4),
                        DatumPrijava = dr.GetString(5),
                        PrijavaVid = dr.GetString(6),
                        PrijavaStatus = dr.GetString(7),
                        DejnostNace = dr.GetString(9),
                        SedisteNaziv = dr.GetString(10),
                        SedisteBroj = dr.GetString(11),
                        SedisteUlica = dr.GetString(12),
                        SedisteTelefon = dr.GetString(13),
                        SedisteTelefax = dr.GetString(14)
                    };
                    output.Add(temp);
                }
                dr.Close();
                dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    var temp = new EDB_EMB_Output
                    {
                        Edb = dr1.GetString(0),
                        Naziv = dr1.GetString(1),
                        Emb = dr1.GetString(2),
                        Ziro = dr1.GetString(3),
                        BankaZiro = dr1.GetString(4),
                        DatumPrijava = dr1.GetString(5),
                        PrijavaVid = dr1.GetString(6),
                        PrijavaStatus = dr1.GetString(7),
                        DejnostNace = dr1.GetString(8),
                        SedisteNaziv = dr1.GetString(9),
                        SedisteBroj = dr1.GetString(10),
                        SedisteUlica = dr1.GetString(11),
                        SedisteTelefon = dr1.GetString(12),
                        SedisteTelefax = dr1.GetString(13)
                    };
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
