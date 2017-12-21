using System;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace CRRMSystemToSystemFromListProviders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Opshtini - GetCMunicipalities(param)");
            Console.WriteLine("2. Opshtini - GetCMunicipalities");
            Console.WriteLine("3. Imoten list");
            Console.WriteLine("4. Parcela");
            Console.WriteLine("5. Dokument - Imoten list");
            Console.WriteLine("6. Kopija od katastarski plan");
            Console.WriteLine("7. Podatoci za infrastrukturni objekti");

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
                                Console.WriteLine("Call to TEST service AKNMunicipality Test - GetCMunicipalities(param)");
                                var client = new CRRMOpshtiniTest.AKNMunicipalityClient();
                                var data = client.GetCMunicipalities("1");
                                foreach (var municipalityDto in data)
                                {
                                    Console.WriteLine(municipalityDto.Name);
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
                    case 2:
                        {
                            try
                            {
                                Console.WriteLine("Call to TEST service AKNMunicipality Test - GetCMunicipalities()");
                                var client = new CRRMOpshtiniTest.AKNMunicipalityClient();
                                var data = client.GetMunicipalities();
                                foreach (var municipalityDto in data)
                                {
                                    Console.WriteLine(municipalityDto.Name);
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
                    case 3:
                    {
                        try
                        {
                            Console.WriteLine("Call to TEST service PropertyListTest - GetPropertyList");
                            var client = new CRRMImotenListTest.PropertyListClient();
                            var data = client.GetPropertyList("mio", "katastarservis", "1", "1", "1");
                            Console.WriteLine("Finished." + data.message);
                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            var filename = "C:\\FaultImotenListTest.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("FaultException when calling service: {0}", ex.Message);
                            Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            var filename = "C:\\ErrorImotenListTest.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("Finished. Error occured:");
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                    case 4:
                    {
                        try
                        {
                            Console.WriteLine("Call to TEST service CadastrialParcelTest - GetCadastrialParcel");
                            var client = new CRRMParcelaTest.CadastrialParcelClient();
                            var data = client.GetCParcel("mio", "katastarservis", "1", "1", "2");
                            Console.WriteLine("Finished." + data.message);
                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            var filename = "C:\\FaultParcelaTest.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("FaultException when calling service: {0}", ex.Message);
                            Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            var filename = "C:\\ErrorParcelaTest.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("Finished. Error occured:");
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                    case 5:
                    {
                        try
                        {
                            Console.WriteLine("Call to TEST service AKNPListDocTest - GetPListDoc");

                            var client = new CRRMDokumentImotenListTest.AKNPListDocClient();
                            //var data = client.GetPListDoc("25", "997", "", "1057", false);
                            //var data = client.GetPListDoc("25", "83", "67403", "", false);
                            var data = client.GetPListDoc("25", "997", "1024", "", false);

                            File.WriteAllBytes("C:\\TempPListDocTest.pdf", data.Document);

                            Console.WriteLine("Service result: {0}", data.Message);
                            File.WriteAllText("C:\\TempPListDocTestMessage.txt", data.Message);

                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine("Finished. Error occured:");
                            File.WriteAllText("C:\\FaultErrorPListDocTest.txt", ex.Message, Encoding.Unicode);
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Finished. Error occured:");
                            File.WriteAllText("C:\\ErrorTempPListDocTest.txt", ex.Message, Encoding.Unicode);
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                    case 6:
                    {
                        try
                        {
                            var client = new CRRMKopijaKatastarskiPlan.AKNCPlanDocClient();
                            //var data = client.GetCPlanDoc("1", "1", "", "2", false);
                            //var data = client.GetCPlanDoc("25", "997", "", "1057", false);
                            var data = client.GetCPlanDoc("25", "997", "", "1057", false);
                            File.WriteAllBytes("C:\\TempKPDocTest.pdf", data.Document);

                            Console.WriteLine("Service result: {0}", data.Message);
                            File.WriteAllText("C:\\TempKPDocTestMessage.txt", data.Message);

                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine("Finished. Error occured:");
                            File.WriteAllText("C:\\FaultErrorTempKPDocTest.txt", ex.Message, Encoding.Unicode);
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Finished. Error occured:");
                            File.WriteAllText("C:\\ErrorTempKPDocTest.txt", ex.Message, Encoding.Unicode);
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                    case 7:
                    {
                        try
                        {
                            Console.WriteLine("Call to TEST service AKNDataForIFDocTest - GetDataForIFDoc");

                            var client = new CRRMPodatociInfrastrukturniObjekti.AKNDataForIFDocClient();
                            var data = client.GetDataForIFDoc("25", "167", "55736", "", false);

                            File.WriteAllBytes("C:\\TempIFTest.pdf", data.Document);

                            File.WriteAllText("C:\\TempIFTestMessage.txt", data.Message, Encoding.Unicode);
                            Console.WriteLine("Service result: {0}", data.Message);
                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine("Finished. Error occured:");
                            File.WriteAllText("C:\\FaultErrorTempIFTest.txt", ex.Message, Encoding.Unicode);
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Finished. Error occured:");
                            File.WriteAllText("C:\\ErrorTempIFTest.txt", ex.Message, Encoding.Unicode);
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
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
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

    }
}
