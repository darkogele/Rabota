using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Interfaces.IDataForEDB;
using Contracts.Models.DataFor_EDB_EMB;
using Helpers;
using IBM.Data.Informix;

namespace Implementations.Implementations.DataForEDB
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class DataForEDB : IDataForEDB
    {
        readonly string _connectionString = ConfigurationManager.AppSettings.Get("connectionStringEdbEmb");
        public List<EDB_EMB_Output> GetEDB(string emb)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(emb))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'EMБ' не смее да биде празен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (emb.Length != 7)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Должината на параметарот 'EMБ' не е точна!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region GettingDataFromInstitutionDatabase

                bool firstDr = false;
                bool secondDr = false;

                var output = new List<EDB_EMB_Output>();

                var conn = new IfxConnection {ConnectionString = _connectionString};
                conn.Open();

                var cmd = new IfxCommand("select * from do_dpl_v1 where mb = ?", conn);
                cmd.Parameters.Add(new IfxParameter("mb", emb));
                var cmd1 = new IfxCommand("select * from do_vd_v1 where mb = ?", conn);
                cmd1.Parameters.Add(new IfxParameter("mb", emb));

                IfxDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    firstDr = true;
                }
                else
                {
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
                }

                dr.Close();
                IfxDataReader dr1 = cmd1.ExecuteReader();

                if (!dr1.HasRows)
                {
                    secondDr = true;
                }
                else
                {
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
                }

                dr1.Close();
                conn.Close();

                if (firstDr && secondDr)//output.Count == 0
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постојат податоци за внесените параметри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                return output;
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (IfxException ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при земањето податоци од базата на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (Exception ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при повикување на сервисот на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
        }
    }
}
