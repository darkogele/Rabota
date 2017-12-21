using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Interop.CS.Models;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.RepositoryContracts.MIOARecords;
using Microsoft.AspNet.Identity;
using Interop.CS.Models.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Http;
using System.Net;
using Interop.CS.CrossCutting;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Cryptography;
using Interop.CS.Models.Models.MIOARecords;

namespace Interop.CS.Portal.API.Controllers
{
    [RoutePrefix("api/Auth")]
    public class MIOARecordsController : ApiController
    {
        private readonly IAdministrativeRecordRepository _adminRecordRepo;
        private readonly IAdministrativeServiceRepository _adminServiceRepo;
        private readonly IPrincipal _loggedUser;

        public MIOARecordsController(IAdministrativeRecordRepository adminRecordRepo, IAdministrativeServiceRepository adminServiceRepo)
        {
            _adminRecordRepo = adminRecordRepo;
            _adminServiceRepo = adminServiceRepo;
            _loggedUser = HttpContext.Current.User;
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public PagedCollection<AdministrativeRecord> GetAdministrativeRecords(int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            return _adminRecordRepo.GetAdministrativeRecordPaged(pageIndex, itemsPerPage, sortDir, sortCol);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public PagedCollection<AdministrativeService> GetAdministrativeServices(int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            return _adminServiceRepo.GetAdministrativeServicePaged(pageIndex, itemsPerPage, sortDir, sortCol);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public PagedCollection<AdministrativeRecordLog> GetRecordDetails(int id, int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            return _adminRecordRepo.GetRecordDetailsPaged(id, pageIndex, itemsPerPage, sortDir, sortCol);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public PagedCollection<AdministrativeServiceLog> GetServiceDetails(int id, int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            return _adminServiceRepo.GetServiceDetailsPaged(id, pageIndex, itemsPerPage, sortDir, sortCol);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public AdministrativeRecord GetRecord(int id)
        {
            return _adminRecordRepo.GetRecord(id);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public AdministrativeService GetService(int id)
        {
            return _adminServiceRepo.GetService(id);
        }

        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpPost]
        public void UpdateRecord(AdministrativeRecord rec)
        {
            var log = new AdministrativeRecordLog();
            var oldRecord = _adminRecordRepo.GetRecord(rec.Id);

            log.ChangeDate = DateTime.Now;
            log.UserName = _loggedUser.Identity.Name;
            log.PerformedActivity = "";
            log.AdministrativeRecordId = rec.Id;
            if (rec.ItemNumber != oldRecord.ItemNumber)
                log.PerformedActivity = log.PerformedActivity + oldRecord.ItemNumber.ToString()+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.DateReceived != oldRecord.DateReceived)
                log.PerformedActivity = log.PerformedActivity + oldRecord.DateReceived.ToString()+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.ApplicantNameAddress != oldRecord.ApplicantNameAddress)
                log.PerformedActivity = log.PerformedActivity +oldRecord.ApplicantNameAddress+ ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.ElectronicDBName != oldRecord.ElectronicDBName)
                log.PerformedActivity = log.PerformedActivity + oldRecord.ElectronicDBName+ ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.ElectronicDBTypeVersion != oldRecord.ElectronicDBTypeVersion)
                log.PerformedActivity = log.PerformedActivity + oldRecord.ElectronicDBTypeVersion+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.DataType != oldRecord.DataType)
                log.PerformedActivity = log.PerformedActivity + oldRecord.DataType+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.LegislationData != oldRecord.LegislationData)
                log.PerformedActivity = log.PerformedActivity + oldRecord.LegislationData+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.AuthorizedPersonData != oldRecord.AuthorizedPersonData)
                log.PerformedActivity = log.PerformedActivity + oldRecord.AuthorizedPersonData+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.Note != oldRecord.Note)
                log.PerformedActivity = log.PerformedActivity + oldRecord.Note+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.OptionalField1 != oldRecord.OptionalField1)
                log.PerformedActivity = log.PerformedActivity + oldRecord.OptionalField1+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (rec.OptionalField2 != oldRecord.OptionalField2)
                log.PerformedActivity = log.PerformedActivity + oldRecord.OptionalField2+",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            rec.DateReceived = rec.DateReceived.AddDays(1);
            _adminRecordRepo.EditRecord(rec, log);

        }

        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpPost]
        public void UpdateService(AdministrativeService ser)
        {
            var log = new AdministrativeServiceLog();
            var oldService = _adminServiceRepo.GetService(ser.Id);

            log.ChangeDate = DateTime.Now;
            log.UserName = _loggedUser.Identity.Name;
            log.PerformedActivity = "";
            log.AdministrativeServiceId = ser.Id;
            if (ser.ItemNumber != oldService.ItemNumber)
                log.PerformedActivity = log.PerformedActivity + oldService.ItemNumber.ToString() + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.DateReceived != oldService.DateReceived)
                log.PerformedActivity = log.PerformedActivity + oldService.DateReceived.ToString() + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.ApplicantNameAddress != oldService.ApplicantNameAddress)
                log.PerformedActivity = log.PerformedActivity + oldService.ApplicantNameAddress + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.AdministrativeServiceName != oldService.AdministrativeServiceName)
                log.PerformedActivity = log.PerformedActivity + oldService.AdministrativeServiceName + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.AdministrativeServiceDesc != oldService.AdministrativeServiceDesc)
                log.PerformedActivity = log.PerformedActivity + oldService.AdministrativeServiceDesc + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.LegislationForAdminService != oldService.LegislationForAdminService)
                log.PerformedActivity = log.PerformedActivity + oldService.LegislationForAdminService + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.LegislationForAdminServicePayment != oldService.LegislationForAdminServicePayment)
                log.PerformedActivity = log.PerformedActivity + oldService.LegislationForAdminServicePayment + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.AdminServicePaymentAmount != oldService.AdminServicePaymentAmount)
                log.PerformedActivity = log.PerformedActivity + oldService.AdminServicePaymentAmount + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.AdminServiceSubmissionLeagalDeadline != oldService.AdminServiceSubmissionLeagalDeadline)
                log.PerformedActivity = log.PerformedActivity + oldService.AdminServiceSubmissionLeagalDeadline + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.DeliveringSpecialFormData != oldService.DeliveringSpecialFormData)
                log.PerformedActivity = log.PerformedActivity + oldService.DeliveringSpecialFormData + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.PreviousSubmittedDocDependencyData != oldService.PreviousSubmittedDocDependencyData)
                log.PerformedActivity = log.PerformedActivity + oldService.PreviousSubmittedDocDependencyData + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.ElectronicDBTypeVersion != oldService.ElectronicDBTypeVersion)
                log.PerformedActivity = log.PerformedActivity + oldService.ElectronicDBTypeVersion + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.AuthorizedPersonData != oldService.AuthorizedPersonData)
                log.PerformedActivity = log.PerformedActivity + oldService.AuthorizedPersonData + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.Note != oldService.Note)
                log.PerformedActivity = log.PerformedActivity + oldService.Note + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.OptionalField1 != oldService.OptionalField1)
                log.PerformedActivity = log.PerformedActivity + oldService.OptionalField1 + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            if (ser.OptionalField2 != oldService.OptionalField2)
                log.PerformedActivity = log.PerformedActivity + oldService.OptionalField2 + ",";
            else
                log.PerformedActivity = log.PerformedActivity + "Нема промена" + ",";
            ser.DateReceived = ser.DateReceived.AddDays(1);
            _adminServiceRepo.EditService(ser, log);

        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public void CreateRecord(AdministrativeRecord rec)
        {
            rec.DateEntered = DateTime.Now;
            rec.DateReceived = rec.DateReceived.AddDays(1);
            _adminRecordRepo.CreateRecord(rec);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public void CreateService(AdministrativeService ser)
        {
            ser.DateEntered = DateTime.Now;
            ser.DateReceived = ser.DateReceived.AddDays(1);
            _adminServiceRepo.CreateService(ser);
        }
    }
}