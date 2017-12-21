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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class RegExcBonds : IRegExcBonds
    {
        string ConnectionString = "Host=" + "10.9.15.15" + "; " +
                                  "Service=" + "suslugi" + "; " +
                                  "Server=" + "etax20ujp" + "; " +
                                  "Database=" + "public_ujp" + "; " +
                                  "User Id=" + "mioa" + "; " +
                                  "Password=" + "MInt@ujp" + "; ";
        public ExciseBondsOutput GetRegExcBonds(ExciseBondsInput input)
        {
            ExciseBondsOutput output = new ExciseBondsOutput();
            try
            {
            if (input.EDB < 1000000 || input.EDB > 10000000000000)
                throw new System.ArgumentException("Parametarot EDB e nevaliden");

            List<string> broj_odobrenie = new List<string>();
            
            List<ExciseBonds> exciseBonds = new List<ExciseBonds>();
            List<ExciseGoods> exciseGoods = new List<ExciseGoods>();
            List<ExciseSpaces> exciseSpaces = new List<ExciseSpaces>();
            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = ConnectionString;
            
                IfxCommand cmd = null;
                IfxDataReader dr = null;
                conn.Open();
                if (input.EDB != null && input.Number != null)
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
                else if (input.EDB == null)
                    throw new System.ArgumentException("Parameter cannot be null", "EDB");
                
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ExciseBonds eb = new ExciseBonds();
                    eb.id = dr.GetString(0);
                    eb.edb = dr.GetString(1);
                    eb.name = dr.GetString(2);
                    eb.vid_odobrenie = dr.GetString(3);
                    eb.broj_odobrenie = dr.GetString(4);
                    eb.datum_odobrenie = dr.GetString(5);
                    eb.prethodno_odobrenie= dr.GetString(6);
                    eb.sledno_odobrenie = dr.GetString(7);
                    eb.embg_odgovorno_lice = dr.GetString(8);
                    eb.naziv_odgovorno_lice = dr.GetString(9);
                    eb.embg_polnomosnik = dr.GetString(10);
                    eb.naziv_polnomosnik = dr.GetString(11);
                    broj_odobrenie.Add(eb.broj_odobrenie);
                    exciseBonds.Add(eb);
                }
                output.ExciseBonds = exciseBonds;
                dr.Close();

                for (int i = 0; i < broj_odobrenie.Count; i++)
                {
                    cmd = new IfxCommand("select * from akcizi_prostorii where ru = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("ru", broj_odobrenie[i]));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ExciseSpaces es = new ExciseSpaces();
                        es.id = dr.GetString(0);
                        es.broj_odobrenie = dr.GetString(1);
                        es.stavka = dr.GetString(2);
                        es.opstina = dr.GetString(3);
                        es.naziv_opstina = dr.GetString(4);
                        es.mesto = dr.GetString(5);
                        es.naziv_mesto= dr.GetString(6);
                        es.naziv_ulica = dr.GetString(7);
                        es.ulica_broj = dr.GetString(8);
                        es.za_proizvodstvo = dr.GetString(9);
                        es.za_skladiranje = dr.GetString(10);
                        es.embg_odgovorno_lice = dr.GetString(11);
                        es.naziv_odgovorno_lice = dr.GetString(12);
                        es.zabeleska = dr.GetString(13);
                        es.status = dr.GetString(14);
                        es.status_datum = dr.GetString(15);
                        exciseSpaces.Add(es);
                    }
                    output.ExciseSpaces = exciseSpaces;
                    dr.Close();
                    cmd = new IfxCommand("select * from akcizi_dobra where ru = ?", conn);
                    cmd.Parameters.Add(new IfxParameter("ru", broj_odobrenie[i]));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ExciseGoods eg = new ExciseGoods();
                        eg.id = dr.GetString(0);
                        eg.broj_odobrenie = dr.GetString(1);
                        eg.stavka = dr.GetString(2);
                        eg.vid = dr.GetString(3);
                        eg.proizvod = dr.GetString(4);
                        eg.tarifa = dr.GetString(5);
                        eg.kolicina = dr.GetString(6);
                        eg.opis_merka = dr.GetString(7);
                        eg.za_proizvodstvo = dr.GetString(8);
                        eg.za_skladiranje = dr.GetString(9);
                        exciseGoods.Add(eg);
                    }
                    output.ExciseGoods = exciseGoods;
                    dr.Close();
                }
                conn.Close();
            }
            catch (IfxException ex)
            {
                Console.WriteLine("Problem with connection attempt: "+ ex.Message);
            }
            return output;
        }
    }
}
