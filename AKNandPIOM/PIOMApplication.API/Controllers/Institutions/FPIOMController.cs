using PIOMApplication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PIOMApplication.API.AKNPropertyListCadastralParcel;

namespace PIOMApplication.API.Controllers.Institutions
{
    [Authorize] 
    public class FPIOMController : ApiController
    {

        [HttpPost]
        public DataForPropertyListDTO GetPropertyList(string username, string password, string municipality, string cadastralMunicipality, string noPropertyList)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
             ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var AKNClient = new AKNServiceClient();
           

            
             var dzgrObject = AKNClient.GetPropertyList(username, password, municipality, cadastralMunicipality, noPropertyList);
           


            List<Loads> loads = new List<Loads>();
            foreach (var tovar in dzgrObject.niztovField)
            {
                var load = new Loads { Text = tovar.textField };
                loads.Add(load);
            }

            List<Objects> objects = new List<Objects>();
            foreach (var objekt in dzgrObject.nizobjField)
            {
                var objectItem = new Objects
                {
                    Apartment = objekt.stanField,
                    Grounds = objekt.povrsinaField,
                    Entry = objekt.vlezField,
                    Floor = objekt.katField,
                    Location = objekt.mestoField,
                    Number = objekt.brojField,
                    Object = objekt.objektField,
                    Pravo = objekt.pravoField,
                    Purpose = objekt.namenaField
                };
                objects.Add(objectItem);
            }

            List<Owner> owners = new List<Owner>();
            foreach (var sopstvenik in dzgrObject.nizsopField)
            {
                var owner = new Owner()
                {
                    Name = sopstvenik.imeField,
                    Location = sopstvenik.mestoField,
                    Number = sopstvenik.brojField,
                    Street = sopstvenik.ulicaField,
                    Part = sopstvenik.delField
                };
                owners.Add(owner);
            }

            List<Parcel> parcels = new List<Parcel>();
            foreach (var parcela in dzgrObject.nizparField)
            {
                var parcel = new Parcel
                {
                    PartNumber = parcela.broj_delField,
                    Culture = parcela.kulturaField,
                    Grounds = parcela.povrsinaField,
                    Location = parcela.mestoField,
                    ObjectParcel = parcela.objektField,
                    Pravo = parcela.pravoField
                };
                parcels.Add(parcel);
            }

            var propertyList = new DataForPropertyListDTO
            {
                Municipality = municipality,
                CadastralMunicipality = cadastralMunicipality,
                PropertyList = dzgrObject.ilistField,
                LoadsList = loads,
                ObjectsList = objects,
                OwnersList = owners,
                ParcelsList = parcels,
                Date = Convert.ToString(DateTime.Now),
                Message = dzgrObject.messageField,
            };

            return propertyList;
        }

        [HttpPost]
        public CadastralParcelDTO GetCadastralParcel(string username, string password, string municipality, string cadastralMunicipality, string noCadastralParcel)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var AKNClient = new AKNServiceClient();
            var cadastralParcel = new ATRparceli();


            cadastralParcel = AKNClient.GetCadastrialParcel(username, password, municipality, cadastralMunicipality, noCadastralParcel);
               
           


            List<ParcelAttributes> attributes = new List<ParcelAttributes>();
            foreach (var parcel in cadastralParcel.nizparField)
            {
                var attribute = new ParcelAttributes()
                {
                    Area = parcel.povrsinaField,
                    Location = parcel.mestoField,
                    Culture = parcel.kulturaField,
                    Object = parcel.objektField,
                    PartNumber = parcel.broj_delField,
                    PropertyList = parcel.ilistField,
                    Pravo = parcel.pravoField
                };
                attributes.Add(attribute);
            }

            var cadastralParcelDto = new CadastralParcelDTO
            {
                AttributesList = attributes,
                Message = cadastralParcel.messageField
            };

            return cadastralParcelDto;
        }
    }
}