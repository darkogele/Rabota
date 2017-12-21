// Decompiled with JetBrains decompiler
// Type: WebApplicationInterop.LOGDAL
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using interop;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebApplicationInterop
{
  public class LOGDAL
  {
    public LOG GetByID(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      try
      {
        return Queryable.Single<LOG>((IQueryable<LOG>) classes1DataContext.LOGs, (Expression<Func<LOG, bool>>) (p => p.ID == (long) id));
      }
      catch
      {
        return (LOG) null;
      }
    }

    public List<HelpClassLogs> GetByDate(DateTime DateFrom, DateTime DateTo)
    {
      IQueryable<LOG> queryable = Queryable.Where<LOG>((IQueryable<LOG>) new DataClasses1DataContext().LOGs, (Expression<Func<LOG, bool>>) (p => p.DateTime >= DateFrom && p.DateTime <= DateTo));
      List<HelpClassLogs> list = new List<HelpClassLogs>();
      foreach (LOG log in (IEnumerable<LOG>) queryable)
      {
        HelpClassLogs helpClassLogs = new HelpClassLogs();
        helpClassLogs.ID = log.ID;
        helpClassLogs.idTable = (long) log.Id_Table;
        helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
        helpClassLogs.idUser = log.Id_User.ToString();
        helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
        helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
        helpClassLogs.idItem = log.Id_Item;
        helpClassLogs.Date = log.DateTime;
        helpClassLogs.ActiveType = log.ActiveType;
        helpClassLogs.Old = log.Old;
        helpClassLogs.New = log.New;
        switch (helpClassLogs.ActiveType)
        {
          case 1:
            helpClassLogs.ActiveTypeName = "insert";
            break;
          case 2:
            helpClassLogs.ActiveTypeName = "update";
            break;
          case 3:
            helpClassLogs.ActiveTypeName = "delete";
            break;
          default:
            helpClassLogs.ActiveTypeName = "unknown";
            break;
        }
        list.Add(helpClassLogs);
      }
      return list;
    }

    public List<HelpClassLogs> GetTop50()
    {
      IQueryable<LOG> queryable = Queryable.Take<LOG>(Queryable.Select<LOG, LOG>((IQueryable<LOG>) new DataClasses1DataContext().LOGs, (Expression<Func<LOG, LOG>>) (p => p)), 50);
      List<HelpClassLogs> list = new List<HelpClassLogs>();
      foreach (LOG log in (IEnumerable<LOG>) queryable)
      {
        HelpClassLogs helpClassLogs = new HelpClassLogs();
        helpClassLogs.ID = log.ID;
        helpClassLogs.idTable = (long) log.Id_Table;
        helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
        helpClassLogs.idUser = log.Id_User.ToString();
        helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
        helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
        helpClassLogs.idItem = log.Id_Item;
        helpClassLogs.Date = log.DateTime;
        helpClassLogs.ActiveType = log.ActiveType;
        helpClassLogs.Old = log.Old;
        helpClassLogs.New = log.New;
        switch (helpClassLogs.ActiveType)
        {
          case 1:
            helpClassLogs.ActiveTypeName = "insert";
            break;
          case 2:
            helpClassLogs.ActiveTypeName = "update";
            break;
          case 3:
            helpClassLogs.ActiveTypeName = "delete";
            break;
          default:
            helpClassLogs.ActiveTypeName = "unknown";
            break;
        }
        list.Add(helpClassLogs);
      }
      return list;
    }

    public List<HelpClassLogs> GetAll()
    {
      IQueryable<LOG> queryable = Queryable.Select<LOG, LOG>((IQueryable<LOG>) new DataClasses1DataContext().LOGs, (Expression<Func<LOG, LOG>>) (p => p));
      List<HelpClassLogs> list = new List<HelpClassLogs>();
      foreach (LOG log in (IEnumerable<LOG>) queryable)
      {
        HelpClassLogs helpClassLogs = new HelpClassLogs();
        helpClassLogs.ID = log.ID;
        helpClassLogs.idTable = (long) log.Id_Table;
        helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
        helpClassLogs.idUser = log.Id_User.ToString();
        helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
        helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
        helpClassLogs.idItem = log.Id_Item;
        helpClassLogs.Date = log.DateTime;
        helpClassLogs.ActiveType = log.ActiveType;
        helpClassLogs.Old = log.Old;
        helpClassLogs.New = log.New;
        switch (helpClassLogs.ActiveType)
        {
          case 1:
            helpClassLogs.ActiveTypeName = "insert";
            break;
          case 2:
            helpClassLogs.ActiveTypeName = "update";
            break;
          case 3:
            helpClassLogs.ActiveTypeName = "delete";
            break;
          default:
            helpClassLogs.ActiveTypeName = "unknown";
            break;
        }
        list.Add(helpClassLogs);
      }
      return list;
    }

    public List<HelpClassLogs> GetAllByUser(USER user)
    {
      IQueryable<LOG> queryable = Queryable.Where<LOG>((IQueryable<LOG>) new DataClasses1DataContext().LOGs, (Expression<Func<LOG, bool>>) (p => p.Id_User == user.ID));
      List<HelpClassLogs> list = new List<HelpClassLogs>();
      foreach (LOG log in (IEnumerable<LOG>) queryable)
      {
        HelpClassLogs helpClassLogs = new HelpClassLogs();
        helpClassLogs.ID = log.ID;
        helpClassLogs.idTable = (long) log.Id_Table;
        helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
        helpClassLogs.idUser = log.Id_User.ToString();
        helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
        helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
        helpClassLogs.idItem = log.Id_Item;
        helpClassLogs.Date = log.DateTime;
        helpClassLogs.ActiveType = log.ActiveType;
        helpClassLogs.Old = log.Old;
        helpClassLogs.New = log.New;
        switch (helpClassLogs.ActiveType)
        {
          case 1:
            helpClassLogs.ActiveTypeName = "insert";
            break;
          case 2:
            helpClassLogs.ActiveTypeName = "update";
            break;
          case 3:
            helpClassLogs.ActiveTypeName = "delete";
            break;
          default:
            helpClassLogs.ActiveTypeName = "unknown";
            break;
        }
        list.Add(helpClassLogs);
      }
      return list;
    }

    	public List<HelpClassLogs> GetAllByAdminInstitution(USER user)
		{
			DataClasses1DataContext pom = new DataClasses1DataContext();
			INSTITUTION queryInst = (
				from i in pom.INSTITUTIONs
				from u in pom.USERs
				from p in pom.PERMISSIONs
				where i.ID == p.ID_INST && p.ID_USER == u.ID && p.Usage == 1 && u.ID == user.ID
				select i).First<INSTITUTION>();
			IQueryable<LOG> queryable = 
				from l in pom.LOGs
				join u in pom.USERs on l.Id_User equals user.ID
				join p in pom.PERMISSIONs on u.ID equals p.ID_USER
				join i in pom.INSTITUTIONs on p.ID_INST equals i.ID
				where p.Usage == 1 && i.ID == queryInst.ID
				select l;
			List<HelpClassLogs> list = new List<HelpClassLogs>();
			foreach (LOG current in queryable)
			{
				HelpClassLogs helpClassLogs = new HelpClassLogs();
				helpClassLogs.ID = current.ID;
				helpClassLogs.idTable = (long)current.Id_Table;
				helpClassLogs.TittleTable = InteropDAL.TablesDictionary[current.Id_Table];
				helpClassLogs.idUser = current.Id_User.ToString();
				helpClassLogs.objUser = new UsersDAL().GetByID(current.Id_User);
				helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
				helpClassLogs.idItem = current.Id_Item;
				helpClassLogs.Date = current.DateTime;
				helpClassLogs.ActiveType = current.ActiveType;
				helpClassLogs.Old = current.Old;
				helpClassLogs.New = current.New;
				switch (helpClassLogs.ActiveType)
				{
				case 1:
					helpClassLogs.ActiveTypeName = "insert";
					break;
				case 2:
					helpClassLogs.ActiveTypeName = "update";
					break;
				case 3:
					helpClassLogs.ActiveTypeName = "delete";
					break;
				default:
					helpClassLogs.ActiveTypeName = "unknown";
					break;
				}
				list.Add(helpClassLogs);
			}
			return list;
		}


    	public List<HelpClassLogs> GetTop50ByAdminInstitution(USER user)
		{
			List<HelpClassLogs> result;
			try
			{
				DataClasses1DataContext pom = new DataClasses1DataContext();
			//	USER u;
				INSTITUTION queryInst = (
					from i in pom.INSTITUTIONs
					from u in pom.USERs
					from p in pom.PERMISSIONs
					where i.ID == p.ID_INST && p.ID_USER == u.ID && p.Usage == 1 && u.ID == user.ID
					select i).First<INSTITUTION>();
				IQueryable<USER> queryable = (
					from u in pom.USERs
					join p in pom.PERMISSIONs on u.ID equals p.ID_USER
					join i in pom.INSTITUTIONs on p.ID_INST equals i.ID
					where i.ID == queryInst.ID && u.Active == true && p.Active == true
					select u).Distinct<USER>().Take(50);
				List<HelpClassLogs> list = new List<HelpClassLogs>();
				foreach (USER u in queryable)
				{
					IQueryable<LOG> queryable2 = 
						from l in pom.LOGs
						where l.Id_User == u.ID && u.Type != 1
						select l;
					foreach (LOG current in queryable2)
					{
						HelpClassLogs helpClassLogs = new HelpClassLogs();
						HelpClassLogs helpClassLogs2 = new HelpClassLogs();
						helpClassLogs2.ID = current.ID;
						helpClassLogs2.idTable = (long)current.Id_Table;
						helpClassLogs2.TittleTable = InteropDAL.TablesDictionary[current.Id_Table];
						helpClassLogs2.idUser = current.Id_User.ToString();
						helpClassLogs2.objUser = new UsersDAL().GetByID(current.Id_User);
						helpClassLogs2.NameSurnameUser = helpClassLogs2.objUser.Name + " " + helpClassLogs2.objUser.Surname;
						helpClassLogs2.idItem = current.Id_Item;
						helpClassLogs2.Date = current.DateTime;
						helpClassLogs2.ActiveType = current.ActiveType;
						helpClassLogs2.Old = current.Old;
						helpClassLogs2.New = current.New;
						switch (helpClassLogs2.ActiveType)
						{
						case 1:
							helpClassLogs2.ActiveTypeName = "insert";
							break;
						case 2:
							helpClassLogs2.ActiveTypeName = "update";
							break;
						case 3:
							helpClassLogs2.ActiveTypeName = "delete";
							break;
						default:
							helpClassLogs2.ActiveTypeName = "unknown";
							break;
						}
						list.Add(helpClassLogs2);
					}
				}
				result = list;
			}
			catch (Exception var_8_C1C)
			{
				result = null;
			}
			return result;
		}

        public List<HelpClassLogs> GetAllByAdminInstitutionForDate(USER user, DateTime DateFrom, DateTime DateTo)
        {
            List<HelpClassLogs> result;
            try
            {
                DataClasses1DataContext pom = new DataClasses1DataContext();
               // USER u;
                INSTITUTION queryInst = (
                    from i in pom.INSTITUTIONs
                    from u in pom.USERs
                    from p in pom.PERMISSIONs
                    where i.ID == p.ID_INST && p.ID_USER == u.ID && p.Usage == 1 && u.ID == user.ID
                    select i).First<INSTITUTION>();
                IQueryable<USER> queryable = (
                    from u in pom.USERs
                    join p in pom.PERMISSIONs on u.ID equals p.ID_USER
                    join i in pom.INSTITUTIONs on p.ID_INST equals i.ID
                    where i.ID == queryInst.ID && u.Active == true && p.Active == true
                    select u).Distinct<USER>();
                List<HelpClassLogs> list = new List<HelpClassLogs>();
                foreach (USER u in queryable)
                {
                    IQueryable<LOG> queryable2 =
                        from l in pom.LOGs
                        where l.Id_User == u.ID && l.DateTime >= DateFrom && l.DateTime <= DateTo && u.Type != 1
                        select l;
                    foreach (LOG current in queryable2)
                    {
                        HelpClassLogs helpClassLogs = new HelpClassLogs();
                        HelpClassLogs helpClassLogs2 = new HelpClassLogs();
                        helpClassLogs2.ID = current.ID;
                        helpClassLogs2.idTable = (long)current.Id_Table;
                        helpClassLogs2.TittleTable = InteropDAL.TablesDictionary[current.Id_Table];
                        helpClassLogs2.idUser = current.Id_User.ToString();
                        helpClassLogs2.objUser = new UsersDAL().GetByID(current.Id_User);
                        helpClassLogs2.NameSurnameUser = helpClassLogs2.objUser.Name + " " + helpClassLogs2.objUser.Surname;
                        helpClassLogs2.idItem = current.Id_Item;
                        helpClassLogs2.Date = current.DateTime;
                        helpClassLogs2.ActiveType = current.ActiveType;
                        helpClassLogs2.Old = current.Old;
                        helpClassLogs2.New = current.New;
                        switch (helpClassLogs2.ActiveType)
                        {
                            case 1:
                                helpClassLogs2.ActiveTypeName = "insert";
                                break;
                            case 2:
                                helpClassLogs2.ActiveTypeName = "update";
                                break;
                            case 3:
                                helpClassLogs2.ActiveTypeName = "delete";
                                break;
                            default:
                                helpClassLogs2.ActiveTypeName = "unknown";
                                break;
                        }
                        list.Add(helpClassLogs2);
                    }
                }
                result = list;
            }
            catch (Exception var_8_CB1)
            {
                result = null;
            }
            return result;
        }

    public List<HelpClassLogs> GetTop50ByUser(USER user)
    {
      try
      {
        IQueryable<LOG> queryable = Queryable.Take<LOG>(Queryable.Where<LOG>((IQueryable<LOG>) new DataClasses1DataContext().LOGs, (Expression<Func<LOG, bool>>) (p => p.Id_User == user.ID)), 50);
        List<HelpClassLogs> list = new List<HelpClassLogs>();
        foreach (LOG log in (IEnumerable<LOG>) queryable)
        {
          HelpClassLogs helpClassLogs = new HelpClassLogs();
          helpClassLogs.ID = log.ID;
          helpClassLogs.idTable = (long) log.Id_Table;
          helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
          helpClassLogs.idUser = log.Id_User.ToString();
          helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
          helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
          helpClassLogs.idItem = log.Id_Item;
          helpClassLogs.Date = log.DateTime;
          helpClassLogs.ActiveType = log.ActiveType;
          helpClassLogs.Old = log.Old;
          helpClassLogs.New = log.New;
          switch (helpClassLogs.ActiveType)
          {
            case 1:
              helpClassLogs.ActiveTypeName = "insert";
              break;
            case 2:
              helpClassLogs.ActiveTypeName = "update";
              break;
            case 3:
              helpClassLogs.ActiveTypeName = "delete";
              break;
            default:
              helpClassLogs.ActiveTypeName = "unknown";
              break;
          }
          list.Add(helpClassLogs);
        }
        return list;
      }
      catch (Exception ex)
      {
        return (List<HelpClassLogs>) null;
      }
    }

    public List<HelpClassLogs> GetByUserForToday(USER user)
    {
      try
      {
        DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
        DateTime DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        DateTime DateTo = DateFrom.AddHours(24.0);
        IQueryable<LOG> queryable = Queryable.Where<LOG>((IQueryable<LOG>) classes1DataContext.LOGs, (Expression<Func<LOG, bool>>) (p => p.Id_User == user.ID && p.DateTime >= DateFrom && p.DateTime <= DateTo));
        List<HelpClassLogs> list = new List<HelpClassLogs>();
        foreach (LOG log in (IEnumerable<LOG>) queryable)
        {
          HelpClassLogs helpClassLogs = new HelpClassLogs();
          helpClassLogs.ID = log.ID;
          helpClassLogs.idTable = (long) log.Id_Table;
          helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
          helpClassLogs.idUser = log.Id_User.ToString();
          helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
          helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
          helpClassLogs.idItem = log.Id_Item;
          helpClassLogs.Date = log.DateTime;
          helpClassLogs.ActiveType = log.ActiveType;
          helpClassLogs.Old = log.Old;
          helpClassLogs.New = log.New;
          switch (helpClassLogs.ActiveType)
          {
            case 1:
              helpClassLogs.ActiveTypeName = "insert";
              break;
            case 2:
              helpClassLogs.ActiveTypeName = "update";
              break;
            case 3:
              helpClassLogs.ActiveTypeName = "delete";
              break;
            default:
              helpClassLogs.ActiveTypeName = "unknown";
              break;
          }
          list.Add(helpClassLogs);
        }
        return list;
      }
      catch (Exception ex)
      {
        return (List<HelpClassLogs>) null;
      }
    }

    public List<HelpClassLogs> GetByUserForDate(USER user, DateTime DateFrom, DateTime DateTo)
    {
      try
      {
        DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
        DateTo = DateTo.AddHours(24.0);
        IQueryable<LOG> queryable = Queryable.Where<LOG>((IQueryable<LOG>) classes1DataContext.LOGs, (Expression<Func<LOG, bool>>) (p => p.Id_User == user.ID && p.DateTime >= DateFrom && p.DateTime <= DateTo));
        List<HelpClassLogs> list = new List<HelpClassLogs>();
        foreach (LOG log in (IEnumerable<LOG>) queryable)
        {
          HelpClassLogs helpClassLogs = new HelpClassLogs();
          helpClassLogs.ID = log.ID;
          helpClassLogs.idTable = (long) log.Id_Table;
          helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
          helpClassLogs.idUser = log.Id_User.ToString();
          helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
          helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
          helpClassLogs.idItem = log.Id_Item;
          helpClassLogs.Date = log.DateTime;
          helpClassLogs.ActiveType = log.ActiveType;
          helpClassLogs.Old = log.Old;
          helpClassLogs.New = log.New;
          switch (helpClassLogs.ActiveType)
          {
            case 1:
              helpClassLogs.ActiveTypeName = "insert";
              break;
            case 2:
              helpClassLogs.ActiveTypeName = "update";
              break;
            case 3:
              helpClassLogs.ActiveTypeName = "delete";
              break;
            default:
              helpClassLogs.ActiveTypeName = "unknown";
              break;
          }
          list.Add(helpClassLogs);
        }
        return list;
      }
      catch (Exception ex)
      {
        return (List<HelpClassLogs>) null;
      }
    }

    public List<HelpClassLogs> GetAllByLocalAdmin(USER user)
    {
      try
      {
        IQueryable<LOG> queryable = Queryable.Where<LOG>((IQueryable<LOG>) new DataClasses1DataContext().LOGs, (Expression<Func<LOG, bool>>) (p => p.Id_User == user.ID));
        List<HelpClassLogs> list = new List<HelpClassLogs>();
        foreach (LOG log in (IEnumerable<LOG>) queryable)
        {
          HelpClassLogs helpClassLogs = new HelpClassLogs();
          helpClassLogs.ID = log.ID;
          helpClassLogs.idTable = (long) log.Id_Table;
          helpClassLogs.TittleTable = InteropDAL.TablesDictionary[log.Id_Table];
          helpClassLogs.idUser = log.Id_User.ToString();
          helpClassLogs.objUser = new UsersDAL().GetByID(log.Id_User);
          helpClassLogs.NameSurnameUser = helpClassLogs.objUser.Name + " " + helpClassLogs.objUser.Surname;
          helpClassLogs.idItem = log.Id_Item;
          helpClassLogs.Date = log.DateTime;
          helpClassLogs.ActiveType = log.ActiveType;
          helpClassLogs.Old = log.Old;
          helpClassLogs.New = log.New;
          switch (helpClassLogs.ActiveType)
          {
            case 1:
              helpClassLogs.ActiveTypeName = "insert";
              break;
            case 2:
              helpClassLogs.ActiveTypeName = "update";
              break;
            case 3:
              helpClassLogs.ActiveTypeName = "delete";
              break;
            default:
              helpClassLogs.ActiveTypeName = "unknown";
              break;
          }
          list.Add(helpClassLogs);
        }
        return list;
      }
      catch (Exception ex)
      {
        return (List<HelpClassLogs>) null;
      }
    }

    public void Insert(int idTable, USER user, string idItem, int activetype, DateTime time, string old, string newone)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      classes1DataContext.LOGs.InsertOnSubmit(new LOG()
      {
        Id_Table = idTable,
        Id_User = user.ID,
        Id_Item = idItem,
        ActiveType = activetype,
        DateTime = time,
        Old = old,
        New = newone
      });
      classes1DataContext.SubmitChanges();
    }

    public void Update(int? idTable, USER user, string idItem, int? activetype, DateTime? time, int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      LOG log = Queryable.Single<LOG>((IQueryable<LOG>) classes1DataContext.LOGs, (Expression<Func<LOG, bool>>) (p => p.ID == (long) id));
      if (idTable.HasValue)
        log.Id_Table = Convert.ToInt32((object) idTable);
      if (user != null)
        log.Id_User = user.ID;
      if (idItem != "")
        log.Id_Item = idItem;
      if (activetype.HasValue)
        log.ActiveType = Convert.ToInt32((object) activetype);
      if (time.HasValue)
        log.DateTime = Convert.ToDateTime((object) time);
      classes1DataContext.SubmitChanges();
    }

    public void Delete(int id)
    {
      DataClasses1DataContext classes1DataContext = new DataClasses1DataContext();
      Queryable.Single<LOG>((IQueryable<LOG>) classes1DataContext.LOGs, (Expression<Func<LOG, bool>>) (p => p.ID == (long) id));
      classes1DataContext.SubmitChanges();
    }
  }
}
