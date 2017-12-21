using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Security.Principal;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using System.Linq.Dynamic;
using Interop.CS.Models.RepositoryContracts.MIOARecords;
using Interop.CS.Models.Models.MIOARecords;
using System.Data.Entity;

namespace Interop.CS.Models.Repository.MIOARecords
{
    public class AdministrativeServiceRepository : IAdministrativeServiceRepository
    {
        private readonly IUnitOfWork _uow;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdministrativeServiceRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new InteropContext()));
        }
        public IEnumerable<AdministrativeService> GetAdministrativeService()
        {
            return _uow.Context.AdministrativeServices.ToList();
        }

        public PagedCollection<AdministrativeService> GetAdministrativeServicePaged(int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            var items =
               _uow.Context.AdministrativeServices.ToList();


            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "ItemNumber";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }

            if (sortDir == "asc")
            {
                items = items.OrderBy(sortCol).ToList();
            }
            else if (sortDir == "desc")
            {
                items = items.OrderBy(sortCol + " descending").ToList();
            }
            else
            {
                items = items.OrderBy(x => x.ItemNumber.ToString()).ToList();
            }
            var pagedItems = items.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            var totalSize = items.Count();
            return new PagedCollection<AdministrativeService>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }
        public PagedCollection<AdministrativeServiceLog> GetServiceDetailsPaged(int id, int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            var items =
               _uow.Context.AdministrativeServices.Where(x => x.Id == id).FirstOrDefault().AdministrativeServiceLogs;

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "ItemNumber";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }

            if (sortDir == "asc")
            {
                items = items.OrderBy(sortCol).ToList();
            }
            else if (sortDir == "desc")
            {
                items = items.OrderBy(sortCol + " descending").ToList();
            }
            else
            {
                items = items.OrderBy(x => x.Id.ToString()).ToList();
            }
            var pagedItems = items.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            var totalSize = items.Count();

            return new PagedCollection<AdministrativeServiceLog>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }
        public AdministrativeService GetService(int id)
        {
            return _uow.Context.AdministrativeServices.Where(x => x.Id == id).FirstOrDefault();
        }

        public void EditService(AdministrativeService serviceNew, AdministrativeServiceLog log)
        {
            var service = _uow.Context.AdministrativeServices.Find(serviceNew.Id);

            if (service == null)
            {
                throw new Exception("Не постои таков запис");
            }
            service.ItemNumber = serviceNew.ItemNumber;
            service.DateReceived = serviceNew.DateReceived;
            service.ApplicantNameAddress = serviceNew.ApplicantNameAddress;
            service.AdministrativeServiceName = serviceNew.AdministrativeServiceName;
            service.AdministrativeServiceDesc = serviceNew.AdministrativeServiceDesc;
            service.LegislationForAdminService = serviceNew.LegislationForAdminService;
            service.LegislationForAdminServicePayment = serviceNew.LegislationForAdminServicePayment;
            service.AdminServicePaymentAmount = serviceNew.AdminServicePaymentAmount;
            service.AdminServiceSubmissionLeagalDeadline = serviceNew.AdminServiceSubmissionLeagalDeadline;
            service.DeliveringSpecialFormData = serviceNew.DeliveringSpecialFormData;
            service.PreviousSubmittedDocDependencyData = serviceNew.PreviousSubmittedDocDependencyData;
            service.ElectronicDBTypeVersion = serviceNew.ElectronicDBTypeVersion;
            service.AuthorizedPersonData = serviceNew.AuthorizedPersonData;
            service.Note = serviceNew.Note;
            service.OptionalField1 = serviceNew.OptionalField1;
            service.OptionalField2 = serviceNew.OptionalField2;
            service.AdministrativeServiceLogs.Add(log);
            try
            {
                _uow.Context.Entry(service).State = EntityState.Modified;
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }
        public void CreateService(AdministrativeService serviceNew)
        {
            var service = _uow.Context.AdministrativeServices.Find(serviceNew.Id);
            if (service != null)
            {
                throw new Exception("Записот веќе постои во база!");
            }
            try
            {
                _uow.Context.AdministrativeServices.Add(serviceNew);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
