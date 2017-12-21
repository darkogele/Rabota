using System;
using Contracts.DataAccessLibrary;
using Contracts.DTO_s.AKNService;
using Contracts.Interfaces.IAKNService;
using System.Collections.Generic;
using System.ServiceModel;
using Contracts.AKNPropertyListAndCadastrialParcelProduction;

namespace Implementations.Implementations.AKNService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class AKNService : IAKNService
    {
        public Contracts.DTO_s.AKNService.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList)
        {
            if (String.IsNullOrEmpty(username))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'корисничко име' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(password))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'лозинка' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(opstina))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'општина' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(katastarskaOpstina))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'катастарска општина' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(brImotenList))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'број на имотен лист' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
            var aknClient = new Service_MACEDONIAN_CADASTRESoapClient();
            var output = aknClient.ReturnImotenList_3(username, password, opstina, katastarskaOpstina, brImotenList);
            var _niztov = new List<Loads>();
            if (output.niztov != null)
                foreach (var tovar in output.niztov)
                {
                    var tov = new Loads { text = tovar.text };
                    _niztov.Add(tov);

                }

            var _nizobj = new List<Objects>();
            if (output.nizobj != null)
                foreach (var objekt in output.nizobj)
                {
                    var objectItem = new Objects
                    {
                        broj = objekt.broj,
                        objekt = objekt.objekt,
                        vlez = objekt.vlez,
                        kat = objekt.kat,
                        stan = objekt.stan,
                        namena = objekt.namena,
                        mesto = objekt.mesto,
                        povrsina = objekt.povrsina,
                        godinagradba = objekt.godinagradba,
                        osnov = objekt.osnov,
                        pravo = objekt.pravo
                    };
                    _nizobj.Add(objectItem);
                }

            var _nizsop = new List<Owner>();
            if (output.nizsop != null)
                foreach (var sopstvenik in output.nizsop)
                {
                    var owner = new Owner()
                    {
                        embg = sopstvenik.embg,
                        ime = sopstvenik.ime,
                        mesto = sopstvenik.mesto,
                        ulica = sopstvenik.ulica,
                        broj = sopstvenik.broj,
                        del = sopstvenik.del
                    };
                    _nizsop.Add(owner);
                }

            var _nizpar = new List<Parcel>();
            if (output.nizpar != null)
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
                    _nizpar.Add(parcel);
                }

            var propertyList = new Contracts.DTO_s.AKNService.dzgr
            {
                ops = output.ops,
                kops = output.kops,
                ilist = output.ilist,
                niztov = _niztov,
                nizobj = _nizobj,
                nizsop = _nizsop,
                nizpar = _nizpar,
                data = output.data,
                message = output.message,
            };
            return propertyList;
        }
        public Contracts.DTO_s.AKNService.ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela)
        {
            if (String.IsNullOrEmpty(username))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'корисничко име' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(password))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'лозинка' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(opstina))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'општина' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(katastarskaOpstina))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'катастарска општина' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (String.IsNullOrEmpty(brParcela))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Параметарот 'број на катастарска парцела' е задолжителен!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var aknClient = new Service_MACEDONIAN_CADASTRESoapClient();

            var output = aknClient.ReturnParcela_7(username, password, opstina, katastarskaOpstina, brParcela);
            var attributes = new List<ParcelAtr>();
            foreach (var parcel in output.nizpar)
            {
                var attribute = new ParcelAtr()
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
        }
    }
}
