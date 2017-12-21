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
    public class AdministrativeRecordRepository : IAdministrativeRecordRepository
    {
        private readonly IUnitOfWork _uow;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdministrativeRecordRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new InteropContext()));
        }
        public IEnumerable<AdministrativeRecord> GetAdministrativeRecord()
        {
            return _uow.Context.AdministrativeRecords.ToList();
        }
        public PagedCollection<AdministrativeRecord> GetAdministrativeRecordPaged(int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            var items =
               _uow.Context.AdministrativeRecords.ToList();

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
 
            return new PagedCollection<AdministrativeRecord>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }
        public PagedCollection<AdministrativeRecordLog> GetRecordDetailsPaged(int id, int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            var items =
               _uow.Context.AdministrativeRecords.Where(x => x.Id == id).FirstOrDefault().AdministrativeRecordLogs ;
            
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
            return new PagedCollection<AdministrativeRecordLog>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }

        public AdministrativeRecord GetRecord(int id)
        {
            return _uow.Context.AdministrativeRecords.Where(x => x.Id == id).FirstOrDefault();
        }
        public void EditRecord(AdministrativeRecord recordNew, AdministrativeRecordLog log)
        {
            var record = _uow.Context.AdministrativeRecords.Find(recordNew.Id);

            if (record == null)
            {
                throw new Exception("Не постои таков запис");
            }
            record.ItemNumber = recordNew.ItemNumber;
            record.DateReceived = recordNew.DateReceived;
            record.ApplicantNameAddress = recordNew.ApplicantNameAddress;
            record.ElectronicDBName = recordNew.ElectronicDBName;
            record.ElectronicDBTypeVersion = recordNew.ElectronicDBTypeVersion;
            record.DataType = recordNew.DataType;
            record.LegislationData = recordNew.LegislationData;
            record.AuthorizedPersonData = recordNew.AuthorizedPersonData;
            record.Note = recordNew.Note;
            record.OptionalField1 = recordNew.OptionalField1;
            record.OptionalField2 = recordNew.OptionalField2;
            record.AdministrativeRecordLogs.Add(log);
            try
            {
                _uow.Context.Entry(record).State = EntityState.Modified;
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }
        public void CreateRecord(AdministrativeRecord recordNew)
        {
            var record = _uow.Context.AdministrativeRecords.Find(recordNew.Id);
            if (record != null)
            {
                throw new Exception("Записот веќе постои во база!");
            }
            try
            {
                _uow.Context.AdministrativeRecords.Add(recordNew);
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
