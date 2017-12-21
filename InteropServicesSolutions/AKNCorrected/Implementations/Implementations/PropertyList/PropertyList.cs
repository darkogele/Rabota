using System;
using Contracts.DataAccessLibrary;
using Contracts.DTO_s.AKNService;
using System.Collections.Generic;
using System.ServiceModel;
using Contracts.AKNPropertyListAndCadastrialParcelProduction;
using Contracts.Interfaces.IPropertyList;
using Helpers;

namespace Implementations.Implementations.PropertyList
{
    public class PropertyList : IPropertyList
    {
        public Contracts.DTO_s.AKNService.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (String.IsNullOrEmpty(username))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'корисничко име' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (String.IsNullOrEmpty(password))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'лозинка' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (String.IsNullOrEmpty(opstina))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'општина' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (String.IsNullOrEmpty(katastarskaOpstina))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'катастарска општина' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (String.IsNullOrEmpty(brImotenList))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'број на имотен лист' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region CallingInstitutionService

                System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
                var aknClient = new Service_MACEDONIAN_CADASTRESoapClient();
                var output = aknClient.ReturnImotenList_3(username, password, opstina, katastarskaOpstina, brImotenList);

                #endregion

                #region LogicAfterCallingInstitutionService

                if (output.nizobj == null && output.nizpar == null && output.nizsop == null && output.niztov == null)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постојат податоци за внесените параметри. " + output.message);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (output.nizsop != null && (output.nizpar != null && (output.nizobj != null && (output.nizobj.Count == 0 && output.nizpar.Count == 0 && output.nizsop.Count == 0 && output.niztov.Count == 0))))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", output.message);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                var loads = new List<Loads>();
                var objects = new List<Objects>();
                var owners = new List<Owner>();
                var parcels = new List<Parcel>();
                if (output.niztov != null)
                {
                    foreach (var tovar in output.niztov)
                    {
                        var tov = new Loads { text = tovar.text };
                        loads.Add(tov);
                    }
                }
                if (output.nizobj != null)
                {
                    foreach (var obj in output.nizobj)
                    {
                        var objectItem = new Objects
                        {
                            broj = obj.broj,
                            objekt = obj.objekt,
                            vlez = obj.vlez,
                            kat = obj.kat,
                            stan = obj.stan,
                            namena = obj.namena,
                            mesto = obj.mesto,
                            povrsina = obj.povrsina,
                            godinagradba = obj.godinagradba,
                            osnov = obj.osnov,
                            pravo = obj.pravo
                        };
                        objects.Add(objectItem);
                    }
                }
                if (output.nizsop != null)
                {
                    foreach (var sopstvenik in output.nizsop)
                    {
                        var owner = new Owner
                        {
                            embg = sopstvenik.embg,
                            ime = sopstvenik.ime,
                            mesto = sopstvenik.mesto,
                            ulica = sopstvenik.ulica,
                            broj = sopstvenik.broj,
                            del = sopstvenik.del
                        };
                        owners.Add(owner);
                    }
                }
                if (output.nizpar != null)
                {
                    foreach (var parcela in output.nizpar)
                    {
                        var parcel = new Parcel
                        {
                            broj_del = parcela.broj_del,
                            objekt = parcela.objekt,
                            mesto = parcela.mesto,
                            kultura = parcela.kultura,
                            klasa = parcela.klasa,
                            povrsina = parcela.povrsina,
                            pravo = parcela.pravo
                        };
                        parcels.Add(parcel);
                    }
                }
                var propertyList = new Contracts.DTO_s.AKNService.dzgr
                {
                    ops = output.ops,
                    kops = output.kops,
                    ilist = output.ilist,
                    niztov = loads,
                    nizobj = objects,
                    nizsop = owners,
                    nizpar = parcels,
                    data = output.data,
                    message = output.message,
                };

                #endregion

                return propertyList;
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (TimeoutException)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до АКН сервисот не може да се воспостави.");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (EndpointNotFoundException)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до АКН сервисот не може да се воспостави.");
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
