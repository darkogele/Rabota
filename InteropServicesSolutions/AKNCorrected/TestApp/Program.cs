using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Contracts.DataAccessLibrary;


namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AKN Testing Application - enter 0 to stop");
            Console.WriteLine("1. AKNCPlanDocProduction - GetCPlanDoc");
            Console.WriteLine("2. AKNDataForIFDocProduction - GetIFDoc");
            Console.WriteLine("3. AKNMunicipality Production - GetCMunicipalities(param)");
            Console.WriteLine("4. AKNMunicipality Production - GetCMunicipalities()");
            Console.WriteLine("5. AKNPListDocProduction - GetPListDoc");
            Console.WriteLine("6. AKNCadastrialParcelProduction - GetCadastrialParcel");
            Console.WriteLine("7. AKNPopertyListProduction - GetPropertyList");
            Console.WriteLine("8. TSProduction - GetTSProduction");
            Console.WriteLine("-------------------");
            Console.WriteLine("9. AKNCPlanDocTest - GetCPlanDoc");
            Console.WriteLine("10. AKNDataForIFDocTest - GetDataForIFDoc");
            Console.WriteLine("11. AKNPListDocTest - GetPListDoc");
            Console.WriteLine("12. System to system call to test adapter TSAdapterAKNTest - GetTSAKN");
            Console.WriteLine("13. CadastrialParcelTest - GetCadastrialParcel");
            Console.WriteLine("14. PropertyListTest - GetPropertyList");
            Console.WriteLine("15. AKNMunicipality Test - GetCMunicipalities(param)");
            Console.WriteLine("16. AKNMunicipality Test - GetCMunicipalities)");
            Console.WriteLine("17. System to system call to production adapter TSAdapterAKNProd - GetTSAKN");

            while (true)
            {
                Console.Write("Enter value: ");
                int result;
                int.TryParse(Console.ReadLine(), out result);
                var userWantsToExit = false;

                switch (result)
                {
                    case 0:
                        {
                            userWantsToExit = true;
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNCPlanDocProduction - GetCPlanDoc");
                                var client = new AKNCPlanDocProduction.AKNCPlanDocProductionClient();

                                //var data = client.GetCPlanDoc("1", "1", "8689", "2",false);
                                var data = client.GetCPlanDoc("25", "997", "", "1057", false);

                                File.WriteAllBytes("C:\\Zapisi\\TempKPDoc.pdf", data.Document);

                                Console.WriteLine("Service result: {0}", data.Message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                File.WriteAllText("C:\\Zapisi\\TempKPDocError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNDataForIFDocProduction - GetIFDoc");
                                var client = new AKNDataForIFDocProduction.AKNDataForIFDocProductionClient();
                                var data = client.GetIFDoc("opstina", "katastarska opstina", "br imoten list", "br parcela", true);

                                File.WriteAllBytes("C:\\Zapisi\\TempIFProd.pdf", data.Document);

                                Console.WriteLine("Service result: {0}", data.Message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                File.WriteAllText("C:\\Zapisi\\TempIFProdError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNMunicipality Production - GetCMunicipalities(param)");
                                var client = new AKNMunicipalityProduction.AKNMunicipalityClient();
                                var data = client.GetCMunicipalities("municipality Value");
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNMunicipality Production - GetCMunicipalities()");
                                var client = new AKNMunicipalityProduction.AKNMunicipalityClient();
                                var data = client.GetMunicipalities();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 5:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNPListDocProduction - GetPListDoc");
                                var client = new AKNPListDocProduction.AKNPListDocProductionClient();
                                var data = client.GetPListDoc("25", "23", "", "5837", false);

                                File.WriteAllBytes("C:\\Zapisi\\TempPListDocProd.pdf", data.Document);

                                Console.WriteLine("Service result: {0}", data.Message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                File.WriteAllText("C:\\Zapisi\\TempPListDocProdError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNCadastrialParcelProduction - GetCadastrialParcel");
                                //var client = new AKNPopertyListAndCadastrialParcelProduction.AKNServiceClient();
                                var client = new CadastrialParcel.CadastrialParcelClient();
                                var data = client.GetCParcel("mio", "katastarservis", "1", "1", "2");

                                File.WriteAllText("C:\\Zapisi\\TempCadParcelProd.txt", data.message);

                                var tempString = data.nizpar.Count.ToString();
                                File.WriteAllText("C:\\Zapisi\\TempPListProdCount.txt", tempString);

                                for (int i = 0; i < data.nizpar.Count; i++)
                                {
                                    var outputString = data.nizpar[i].broj_del + "/" + data.nizpar[i].ilist + "/" + data.nizpar[i].kops + "/" + data.nizpar[i].kultura + "/" + data.nizpar[i].mesto
                                        + "/" + data.nizpar[i].objekt + "/" + data.nizpar[i].ops + "/" + data.nizpar[i].povrsina + "/" + data.nizpar[i].pravo + "/";

                                    var filename = String.Format("C:\\Zapisi\\CadastrialParcel{0}.txt", i);

                                    File.WriteAllText(filename, outputString);
                                }



                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 7:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNPopertyListProduction - GetPropertyList");
                                //var client = new AKNPopertyListAndCadastrialParcelProduction.AKNServiceClient();
                                var client = new PropertyList.PropertyListClient();
                                var data = client.GetPropertyList("mio", "katastarservis", "1", "1", "1");

                                File.WriteAllText("C:\\Zapisi\\TempPListProd.txt", data.message);

                                var tempString = data.nizobj.Count + "/" + data.nizpar.Count + "/" + data.nizsop.Count +
                                                 "/" + data.nizobj.Count;
                                File.WriteAllText("C:\\Zapisi\\TempPListProdCount.txt", tempString);

                                for (int i = 0; i < data.nizpar.Count; i++)
                                {
                                    var outputString = data.nizpar[i].broj_del + "/" + data.nizpar[i].klasa + "/" + "/" + data.nizpar[i].kultura + "/" + data.nizpar[i].mesto
                                        + "/" + data.nizpar[i].objekt + "/" + "/" + data.nizpar[i].povrsina + "/" + data.nizpar[i].pravo + "/";

                                    var filename = String.Format("C:\\Zapisi\\CadastrialParcel{0}.txt", i);

                                    File.WriteAllText(filename, outputString);
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 8:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service TSProduction - GetTSProduction");
                                var client = new TSProduction.TSProductionClient();
                                var data = client.GetTSProduction("EMBS");
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 9:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNCPlanDocTest - GetCPlanDoc");

                                //komentiraniot del e povik direktno kon servisot na AKN, ne odi preku nasite adapteri
                                //var output = new AKNDocOutput();
                                //var client = new AKNDocsServiceTest.IntegracijaWSImplClient();
                                //var info = client.getPlistInfo("25", "997", "", "1057", "0");
                                //if (info.idPtype != "1014")
                                //{
                                //    output.HasDocument = false;
                                //    output.Message = "Не постои катастарски план за дадените параметри!";
                                //    output.Document = null;
                                //}

                                //var docInfo = client.generateDocument("25", "997", "", "1057", "0", "1014");//1014 kopija od katastarski plan
                                //if (docInfo.errmsg == null)
                                //{
                                //    //using (var sftp = new SftpClient(Host, Port, Username, Password))
                                //    //{
                                //        //sftp.Connect();
                                //        //byte[] arr = sftp.ReadAllBytes(docInfo.filePath + "//" + docInfo.fileName);
                                //        //output.Document = arr;
                                //        //output.HasDocument = true;
                                //        //output.Message = "Успешна операција!";
                                //        //sftp.Disconnect();
                                //    //}
                                //}

                                var client = new AKNCPlanDocTest.AKNCPlanDocClient();
                                //var data = client.GetCPlanDoc("1", "1", "", "2", false);
                                //var data = client.GetCPlanDoc("25", "997", "", "1057", false);
                                var data = client.GetCPlanDoc("25", "997", "", "1057", false);
                                File.WriteAllBytes("C:\\Zapisi\\TempKPDocTest.pdf", data.Document);

                                Console.WriteLine("Service result: {0}", data.Message);
                                //File.WriteAllText("C:\\Users\\developer\\Desktop\\TempKPDocTestMessage.txt", data.Message);

                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                File.WriteAllText("C:\\Zapisi\\TempKPDocTestFaultError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                File.WriteAllText("C:\\Zapisi\\TempKPDocTestError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 10:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNDataForIFDocTest - GetDataForIFDoc");

                                var client = new AKNDataForIFDocTest.AKNDataForIFDocClient();
                                //var data = client.GetDataForIFDoc("1", "1", "8689", "2", false);

                                //var data = client.GetDataForIFDoc("25", "83", "67403", "", false);

                                var data = client.GetDataForIFDoc("25", "167", "55736", "", false);//("25", "167", "55736", "", false)

                                //File.WriteAllBytes("C:\\Users\\developer\\Desktop\\TempIFTest.pdf", data.Document);

                                //File.WriteAllText("C:\\Users\\developer\\Desktop\\TempIFTestMessage.txt", data.Message, Encoding.Unicode);
                                Console.WriteLine("Service result: {0}", data.Message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                File.WriteAllText("C:\\Zapisi\\TempIFTestFaultError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                File.WriteAllText("C:\\Zapisi\\TempIFTestError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 11:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNPListDocTest - GetPListDoc");

                                var client = new AKNPListDocTest.AKNPListDocClient();
                                //var data = client.GetPListDoc("25", "997", "", "1057", false);
                                //var data = client.GetPListDoc("25", "83", "67403", "", false);
                                var data = client.GetPListDoc("25", "997", "1024", "", false);//uspesen povik //("25", "997", "1024", "", false)

                                File.WriteAllBytes("C:\\Zapisi\\TempPListDocTest.pdf", data.Document);

                                Console.WriteLine("Service result: {0}", data.Message);
                                File.WriteAllText("C:\\Zapisi\\TempPListDocTestMessage.txt", data.Message);

                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                File.WriteAllText("C:\\Zapisi\\TempPListDocTestFaultError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                File.WriteAllText("C:\\Zapisi\\TempPListDocTestError.txt", ex.Message, Encoding.Unicode);
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 12:
                        {
                            try
                            {
                                Console.WriteLine("System to system call to test adapter TSAdapterAKNTest - GetTSAKN");

                                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                            ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
                                //var client = new TSTest.TSTestClient();

                                var client = new TSAdapterAKNTest.GetTSAKNClient();
                                var embs = ConfigurationManager.AppSettings["TekovnaSostojbaAKNSystemToSystemEMBS"];
                                var data = client.TekSostojba(embs);//6325270

                                var xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(data);
                                Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);

                                const string filename = "C:\\Zapisi\\ResultTSAKN.txt";
                                File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                const string filename = "C:\\Zapisi\\FaultTSAKN.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("Finished. Fault Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 13:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service CadastrialParcelTest - GetCadastrialParcel");
                                var client = new CadastrialParcelTest.CadastrialParcelClient();
                                var data = client.GetCParcel("mio", "katastarservis", "1", "1", "2");
                                Console.WriteLine("Finished." + data.message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 14:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service PropertyListTest - GetPropertyList");
                                var client = new PropertyListTest.PropertyListClient();
                                var data = client.GetPropertyList("mio", "katastarservis", "1", "1", "1");
                                Console.WriteLine("Finished." + data.message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                var filename = "C:\\Zapisi\\ErrorTSAKN.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 15:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNMunicipality Test - GetCMunicipalities(param)");
                                var client = new AKNMunicipalityTest.AKNMunicipalityClient();
                                var data = client.GetCMunicipalities("1");
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 16:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNMunicipality Test - GetCMunicipalities()");
                                var client = new AKNMunicipalityTest.AKNMunicipalityClient();
                                var data = client.GetMunicipalities();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Finished. Error FaultException occured:");
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Finished. Error occured:");
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 17:
                        try
                        {
                            Console.WriteLine("System to system call to production adapter TSAdapterAKN - TekSostojba");

                            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                                ((sender, certificate, chain, sslPolicyErrors) => true);
                            //sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
                            //var client = new TSTest.TSTestClient();

                            var client = new TSAdapterAKNProd.GetTSAKNClient();
                            var embs = ConfigurationManager.AppSettings["TekovnaSostojbaAKNSystemToSystemEMBS"];
                            var data = client.TekSostojba(embs); //6325270

                            var xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(data);
                            Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);

                            const string filename = "C:\\Zapisi\\ResultTSAKNProd.txt";
                            File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            const string filename = "C:\\Zapisi\\FaultTSAKNProd.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("Finished. Fault Error occured:");
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            const string filename = "C:\\Zapisi\\ExceptionTSAKNProd.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("Finished. Error occured:");
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Wrong input value.");
                            break;
                        }
                }


                if (userWantsToExit)
                {
                    break;
                }
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
