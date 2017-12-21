using System;
using System.IO;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FPIOM Testing Application - enter 0 to stop");
            Console.WriteLine("1. PROD Adapter - GetDataForEnsurees()");
            Console.WriteLine("2. PROD Adapter - GetDataForRetired()");
            Console.WriteLine("3. PROD Adapter - GetInsuredStatus()");
            Console.WriteLine("4. PROD Adapter - GetRetiredStatus()");
            Console.WriteLine("5. PROD Adapter - GetYWExpXML()");
            Console.WriteLine("6. TEST Adapter - GetDataForEnsurees()");
            Console.WriteLine("7. TEST Adapter - GetDataForRetired()");
            Console.WriteLine("8. TEST Adapter - GetInsuredStatus()");
            Console.WriteLine("9. TEST Adapter - GetRetiredStatus()");
            Console.WriteLine("10. TEST Adapter - GetYWExpXML()");

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
                                var productionClient = new DataForEnsurers.DataForEnsurersClient();
                                var data = productionClient.GetDataForEnsurees("0206986499501");

                                Console.WriteLine("Call to PROD service DataForEnsurers. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataForEnsurees(). {0}", ex.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                var productionClient = new DataForRetired.DataForRetiredClient();
                                var data = productionClient.GetDataForRetired("0606949469013");

                                Console.WriteLine("Call to PROD service DataForRetired. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataForRetired(). {0}", ex.Message);
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                var productionClient = new InsuredStatus.InsuredStatusClient();
                                var data = productionClient.GetInsuredStatus("0206986499501");

                                Console.WriteLine("Call to PROD service InsuredStatus. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataForRetired(). {0}", ex.Message);
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                var productionClient = new RetiredStatus.RetiredStatusClient();
                                var data = productionClient.GetRetiredStatus("0606949469013");

                                Console.WriteLine("Call to PROD service RetiredStatus. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataForRetired(). {0}", ex.Message);
                            }
                            break;
                        }
                    case 5:
                        {
                            try
                            {
                                var productionClient = new YearsOfWorkExperience.YearsOfWorkExperienceClient();
                                var data = productionClient.GetYWExpXML("0606949469013");

                                Console.WriteLine("Call to PROD service YearsOfWorkExperience. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataForRetired(). {0}", ex.Message);
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                var testClient = new DataForEnsurersTest.DataForEnsurersClient();
                                var data = testClient.GetDataForEnsurees("0206986499501");
                                Console.WriteLine("Call to TEST service DataForEnsurers. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataForEnsurees() fault message error. {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 7:
                        {
                            try
                            {
                                var testClient = new DataForRetiredTest.DataForRetiredClient();
                                var data = testClient.GetDataForRetired("0606949469013");

                                Console.WriteLine("Call to TEST service DataForRetired. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataForRetired() fault message error. {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 8:
                        {
                            try
                            {
                                var testClient = new InsuredStatusTest.InsuredStatusClient();
                                var data = testClient.GetInsuredStatus("0206986499501");

                                Console.WriteLine("Call to TEST service InsuredStatus. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataForRetired() fault message error. {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 9:
                        {
                            try
                            {
                                var testClient = new RetiredStatusTest.RetiredStatusClient();
                                var data = testClient.GetRetiredStatus("0606949469013");

                                Console.WriteLine("Call to TEST service RetiredStatus. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataForRetired() fault message error. {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 10:
                        {
                            try
                            {
                                var testClient = new YearsOfWorkExperienceTest.YearsOfWorkExperienceClient();
                                YearsOfWorkExperienceTest.YearsOfWorkExperienceOutput data = testClient.GetYWExpXML("0606949469013");


                                //Way of fetching data in FPIOM API controller
                                //string resultDataXml;

                                //var serializer = new XmlSerializer(typeof(YearsOfWorkExperienceTest.YearsOfWorkExperienceOutput));
                                //var sww = new StringWriter();
                                //using (XmlWriter writer = XmlWriter.Create(sww))
                                //{
                                //    serializer.Serialize(writer, data);
                                //    resultDataXml = sww.ToString(); 
                                //}

                                //var serializerToDto = new XmlSerializer(typeof(YearsOfWorkExperienceDTO));
                                //var rezult = (YearsOfWorkExperienceDTO)serializerToDto.Deserialize(new StringReader(resultDataXml));

                                Console.WriteLine("Call to TEST service YearsOfWorkExperience. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataForRetired() fault message error. {0}", ex.Message);
                                Console.ReadLine();
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
