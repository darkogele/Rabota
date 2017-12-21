using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Interfaces.IRegExcBonds;
using Contracts.Models.RegExcBonds;
using Helpers;
using IBM.Data.Informix;

namespace Implementations.Implementations.RegExcBonds
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class RegExcBonds : IRegExcBonds
    {
        readonly string _connectionString = ConfigurationManager.AppSettings.Get("connectionStringRegExcBonds");
        public ExciseBondsOutput GetRegExcBonds(ExciseBondsInput input)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors


                if (input.EDB < 1000000 || input.EDB > 10000000000000)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'ЕДБ' не е валиден!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region GettingDataFromInstitutionDatabase

                var output = new ExciseBondsOutput();
                var brojOdobrenie = new List<string>();
                var exciseBonds = new List<ExciseBonds>();
                var exciseGoods = new List<ExciseGoods>();
                var exciseSpaces = new List<ExciseSpaces>();

                var conn = new IfxConnection {ConnectionString = _connectionString};

                IfxCommand cmd = null;
                IfxDataReader dr = null;
                conn.Open();

                if (input.Number != null)
                {
                    cmd = new IfxCommand("select * from akcizi_obvrznici where edb = ? and ru = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("edb", input.EDB.ToString()));
                    cmd.Parameters.Add(new IfxParameter("ru", input.Number));
                }
                else if (input.Number == null)
                {
                    cmd = new IfxCommand("select * from akcizi_obvrznici where edb = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("edb", input.EDB));
                }

                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постојат податоци за внесените параметри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                while (dr.Read())
                {
                    var eb = new ExciseBonds
                    {
                        id = dr.GetString(0),
                        edb = dr.GetString(1),
                        name = dr.GetString(2),
                        vid_odobrenie = dr.GetString(3),
                        broj_odobrenie = dr.GetString(4),
                        datum_odobrenie = dr.GetString(5),
                        prethodno_odobrenie = dr.GetString(6),
                        sledno_odobrenie = dr.GetString(7),
                        embg_odgovorno_lice = dr.GetString(8),
                        naziv_odgovorno_lice = dr.GetString(9),
                        embg_polnomosnik = dr.GetString(10),
                        naziv_polnomosnik = dr.GetString(11)
                    };
                    brojOdobrenie.Add(eb.broj_odobrenie);
                    exciseBonds.Add(eb);
                }
                output.ExciseBonds = exciseBonds;
                dr.Close();

                for (int i = 0; i < brojOdobrenie.Count; i++)
                {
                    cmd = new IfxCommand("select * from akcizi_prostorii where ru = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("ru", brojOdobrenie[i]));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var es = new ExciseSpaces
                        {
                            id = dr.GetString(0),
                            broj_odobrenie = dr.GetString(1),
                            stavka = dr.GetString(2),
                            opstina = dr.GetString(3),
                            naziv_opstina = dr.GetString(4),
                            mesto = dr.GetString(5),
                            naziv_mesto = dr.GetString(6),
                            naziv_ulica = dr.GetString(7),
                            ulica_broj = dr.GetString(8),
                            za_proizvodstvo = dr.GetString(9),
                            za_skladiranje = dr.GetString(10),
                            embg_odgovorno_lice = dr.GetString(11),
                            naziv_odgovorno_lice = dr.GetString(12),
                            zabeleska = dr.GetString(13),
                            status = dr.GetString(14),
                            status_datum = dr.GetString(15)
                        };
                        exciseSpaces.Add(es);
                    }
                    output.ExciseSpaces = exciseSpaces;
                    dr.Close();
                    cmd = new IfxCommand("select * from akcizi_dobra where ru = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("ru", brojOdobrenie[i]));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var eg = new ExciseGoods
                        {
                            id = dr.GetString(0),
                            broj_odobrenie = dr.GetString(1),
                            stavka = dr.GetString(2),
                            vid = dr.GetString(3),
                            proizvod = dr.GetString(4),
                            tarifa = dr.GetString(5),
                            kolicina = dr.GetString(6),
                            opis_merka = dr.GetString(7),
                            za_proizvodstvo = dr.GetString(8),
                            za_skladiranje = dr.GetString(9)
                        };
                        exciseGoods.Add(eg);
                    }
                    output.ExciseGoods = exciseGoods;
                    dr.Close();
                }
                conn.Close();

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
