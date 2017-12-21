using System;
using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.DTO_s.AKNService;
using Contracts.Interfaces.ICadastrialParcel;
using Contracts.AKNPropertyListAndCadastrialParcelProduction;
using Helpers;

namespace Implementations.Test.Implementations.CadastrialParcel
{
    public class CadastrialParcelTest : ICadastrialParcel
    {
        public Contracts.DTO_s.AKNService.ATRparceli GetCParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela)
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
                if (String.IsNullOrEmpty(brParcela))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'број на катастарска парцела' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region CallingInstitutionService

                var aknClient = new Service_MACEDONIAN_CADASTRESoapClient();//System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
                var output = aknClient.ReturnParcela_7(username, password, opstina, katastarskaOpstina, brParcela);

                #endregion

                #region LogicAfterCallingInstitutionService

                if (output.nizpar == null)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постојат податоци за внесените параметри. " + output.message);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (output.nizpar.Count == 0)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", output.message);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
               

                var attributes = new List<ParcelAtr>();
                foreach (var parcel in output.nizpar)
                {
                    var attribute = new ParcelAtr
                    {
                        ops = parcel.ops,
                        kops = parcel.kops,
                        ilist = parcel.ilist,
                        broj_del = parcel.broj_del,
                        objekt = parcel.objekt,
                        mesto = parcel.mesto,
                        kultura = parcel.kultura,
                        povrsina = parcel.povrsina,
                        pravo = parcel.pravo
                    };
                    attributes.Add(attribute);
                }

                var cadastralParcelDto = new Contracts.DTO_s.AKNService.ATRparceli
                {
                    nizpar = attributes,
                    message = output.message
                };

                return cadastralParcelDto;

                #endregion
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
