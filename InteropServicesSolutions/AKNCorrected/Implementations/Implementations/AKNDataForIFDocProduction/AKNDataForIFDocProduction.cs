﻿using System;
using System.ServiceModel;
using Helpers;
using Renci.SshNet;
using Contracts.Interfaces.IAKNDataForIFDocProduction;
using Contracts.DataAccessLibrary;
using Contracts.AKNDocsServiceProduction;

namespace Implementations.Implementations.AKNDataForIFDocProduction
{
    public class AKNDataForIFDocProduction : IAKNDataForIFDocProduction
    {
        private const String Host = "10.177.159.70";
        private const int Port = 22;
        private const String Username = "king";
        private const String Password = "K1NG123";

        public AKNDocOutput GetIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

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

                var output = new AKNDocOutput();
                var client = new IntegracijaWSImplClient();
                var info = client.getPlistInfo(opstina, katastarskaOpstina, brImotenList, brParcela, "0");

                #endregion

                #region LogicAfterCallingInstitutionService

                if (string.IsNullOrEmpty(info.idPtype))//2005
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постои предбележување на градба за дадените параметри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                var show = showEmb ? "1" : "0";
                var docInfo = client.generateDocument(opstina, katastarskaOpstina, brImotenList, brParcela, show, "2005");//2005 лист предбележување
                if (docInfo.errmsg == null)
                {
                    using (var sftp = new SftpClient(Host, Port, Username, Password))
                    {
                        sftp.Connect();
                        if (!sftp.IsConnected)
                        {
                            faultException = FaultExceptionHelper.CreateFaultException("Грешка во FTP клиентот.", "Не може да се воспостави конекција со FTP серверот!");
                            throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                        }
                        string filePath = docInfo.filePath + "//" + docInfo.fileName;
                        if (sftp.Exists(filePath))
                        {
                            byte[] arr = sftp.ReadAllBytes(filePath);
                            output.Document = arr;
                            output.HasDocument = true;
                            output.Message = "Сервисот на институцијата врати порака: Успешна операција!";
                        }
                        else
                        {
                            faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постои таков документ!");
                            throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                        }
                        sftp.Disconnect();
                    }
                }
                else
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата. Не е пронајден таков документ!", docInfo.errmsg);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                return output;
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
