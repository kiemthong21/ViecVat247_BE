using BusinessObject;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ControllerDAO
{
    public class StaffDAO
    {
        public static PaginatedList<User> GetStaffs(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var staff = new List<User>();
            int count = 0;
            IQueryable<User> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Users
                        .Include(c => c.TypeManagerUsers)
                        .ThenInclude(c => c.TypeManager)
                        .Where(s => s.Role == 1);

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.Uid.ToString().Equals(searchValue)
                        || e.Uemail.Contains(searchValue)
                        || e.Username.Contains(searchValue)
                        || e.PhoneNumber.Contains(searchValue)
                        || e.Address.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.Uid);
                                break;
                            case "email":
                                query = query.OrderBy(e => e.Uemail);
                                break;
                            case "email desc":
                                query = query.OrderByDescending(e => e.Uemail);
                                break;
                            case "username":
                                query = query.OrderBy(e => e.Username);
                                break;
                            case "username desc":
                                query = query.OrderByDescending(e => e.Username);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.Uid);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    staff = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<User>(staff, count, pageIndex, pageSize);
        }

        public static List<User> GetStaffss(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var staff = new List<User>();
            int count = 0;
            IQueryable<User> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Users
                        .Include(c => c.TypeManagerUsers)
                        .ThenInclude(c => c.TypeManager)
                        .Where(s => s.Role == 1);

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.Uid.ToString().Equals(searchValue)
                        || e.Uemail.Contains(searchValue)
                        || e.Username.Contains(searchValue)
                        || e.PhoneNumber.Contains(searchValue)
                        || e.Address.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.Uid);
                                break;
                            case "email":
                                query = query.OrderBy(e => e.Uemail);
                                break;
                            case "email desc":
                                query = query.OrderByDescending(e => e.Uemail);
                                break;
                            case "username":
                                query = query.OrderBy(e => e.Username);
                                break;
                            case "username desc":
                                query = query.OrderByDescending(e => e.Username);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.Uid);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    staff = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return staff;
        }

        public static TypeManager GettypeManager(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    TypeManager TypeManager = context.TypeManager
                        .SingleOrDefault(x => x.TypeManagerId == id);
                    return TypeManager;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<TypeManager> GetAllTypeManager()
        {
            List<TypeManager> typeManagerList = new List<TypeManager>();
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    typeManagerList = context.TypeManager.Where(js => js.Status == 1).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return typeManagerList;
        }

        public static void DeleteTypeManagerUserbyUserId(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var TypeManagerUserToDelete = context.TypeManagerUser.Where(js => js.Uid == id).ToList();
                    context.TypeManagerUser.RemoveRange(TypeManagerUserToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void CreateTypeManagerUser(string[]? typeManagers, int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    if (typeManagers == null || typeManagers.Length == 0)
                    {
                        return;
                    }
                    foreach (string typeManager in typeManagers)
                    {
                        TypeManagerUser tm = new TypeManagerUser();
                        tm.TypeManagerId = int.Parse(typeManager);
                        tm.Uid = id;
                        context.TypeManagerUser.Add(tm);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static User GetStaffByEmailAndPhone(string? uemail, string? phoneNumber)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    User user = context.Users
                        .Include(c => c.TypeManagerUsers)
                        .ThenInclude(c => c.TypeManager)
                        .FirstOrDefault(x => x.Uemail.Equals(uemail) || x.PhoneNumber.Equals(phoneNumber));
                    return user;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User CreateStaff(User user)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User GetStaffById(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    User staff = context.Users
                        .Include(c => c.TypeManagerUsers)
                        .ThenInclude(c => c.TypeManager)
                        .SingleOrDefault(x => x.Uid == id);
                    return staff;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateStaff(User staff)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<User>(staff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User GetStaffByTest(int id)
        {
            try
            {
                string sql = "EXEC SelectStaffById @Id";
                List<SqlParameter> parms = new List<SqlParameter>
        {
        // Create parameter(s)    
        new SqlParameter { ParameterName = "@Id", Value = id }
         };
                using (var context = new Viecvat247DBcontext())
                {

                    User staff = context.Users.FromSqlRaw<User>(sql, parms.ToArray()).AsEnumerable().SingleOrDefault();
                    return staff;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
