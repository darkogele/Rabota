using PIOMApplicationMVC.AKNServiceReference;
using PIOMApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PIOMApplicationMVC.Controllers
{
    public class PIOMController : Controller
    {
        public ActionResult CadastralParcel()
        {
            return View();
        }

        public ActionResult DataForPropertyList()
        {
            return View();
        }
        // GET: PIOM
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: PIOM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Details1(int id)
        {
            return View();
        }

        // GET: PIOM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PIOM/Create
        [HttpPost]
        public ActionResult Create(Input model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //// TODO: Add insert logic here
                    var client = new PIOMApplicationMVC.AKNServiceReference.AKNServiceClient();
                    var output = client.GetCadastrialParcel(model.UserName, model.Password, model.Municipality, model.CadastralMunicipality, model.PropertyList);
                    CadastralParcelDTO parcela = new CadastralParcelDTO();
                    parcela.Message = output.messageField;
                    List<ParcelAttributes> attributes = new List<ParcelAttributes>();
                    foreach (var parcel in output.nizparField)
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
                    parcela.AttributesList = attributes;
                    var k = parcela;

                    return View("Details", parcela);
                }
                else return View("Create");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        // GET: PIOM/Create
        public ActionResult Create1()
        {
            return View();
        }

        // POST: PIOM/Create
        [HttpPost]
        public ActionResult Create1(Input model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //// TODO: Add insert logic here
                    var client = new PIOMApplicationMVC.AKNServiceReference.AKNServiceClient();
                    var dzgrObject = client.GetPropertyList(model.UserName, model.Password, model.Municipality, model.CadastralMunicipality, model.PropertyList);
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
                        PropertyList = dzgrObject.ilistField,
                        LoadsList = loads,
                        ObjectsList = objects,
                        OwnersList = owners,
                        ParcelsList = parcels,
                        Date = Convert.ToString(DateTime.Now),
                        Message = dzgrObject.messageField,
                    };

                    return View("Details1", propertyList);
                }
                else return View("Create1");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        // GET: PIOM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PIOM/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PIOM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PIOM/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
