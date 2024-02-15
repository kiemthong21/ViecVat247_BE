using BusinessObject;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccess.ControllerDAO
{
    public class CustomerDAO
    {
        public static Customer GetCustomerByID(int id)
        {
            var user = new Customer();

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    user = context.Customers
                    .Include(c => c.Jobs)
                    .Include(c => c.CustomerSkills)
                    .ThenInclude(c => c.Skill)
                    .SingleOrDefault(c => c.Cid == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        public static void UpdateCustomer(Customer cus)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<Customer>(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteCustomerSkillbyCustomerId(int cusId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var cusSkillsToDelete = context.CustomerSkills.Where(js => js.CustomerId == cusId).ToList();
                    context.CustomerSkills.RemoveRange(cusSkillsToDelete);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void CreateCustomerSkill(string[]? listSkill, int cusId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    if (listSkill == null || listSkill.Length == 0)
                    {
                        return;
                    }
                    foreach (string skill in listSkill)
                    {
                        CustomerSkill ck = new CustomerSkill();
                        ck.SkillId = int.Parse(skill);
                        ck.CustomerId = cusId;
                        ck.Status = 1;
                        context.CustomerSkills.Add(ck);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PaginatedList<Customer> GetCustomers(string searchValue, int pageIndex, int pageSize, string orderBy, string role, string status)
        {
            var customer = new List<Customer>();
            int count = 0;
            IQueryable<Customer> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Customers;

                    if (!string.IsNullOrWhiteSpace(role))
                    {
                        query = query.Where(e => e.Role.ToString().Equals(role));
                    }
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.Cid.ToString().Equals(searchValue)
                        || e.Cemail.Contains(searchValue)
                        || e.FullName.Contains(searchValue)
                        || e.PhoneNumber.Contains(searchValue)
                        || e.Address.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id_desc":
                                query = query.OrderByDescending(e => e.Cid);
                                break;
                            case "email":
                                query = query.OrderBy(e => e.Cemail);
                                break;
                            case "email_desc":
                                query = query.OrderByDescending(e => e.Cemail);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.FullName);
                                break;
                            case "name_desc":
                                query = query.OrderByDescending(e => e.FullName);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address_desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.Cid);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    customer = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Customer>(customer, count, pageIndex, pageSize);
        }


        public static Customer GetCustomerByID(int cid, string? uid)
        {

            var user = new Customer();

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    user = context.Customers
                    .Include(c => c.Applications)
                    .ThenInclude(a => a.Job)
                    .SingleOrDefault(c => c.Cid == cid && c.Applications.Any(a => a.Job.JobAssignerId.ToString().Equals(uid)));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        public static PaginatedList<Customer> GetCustomersApplyByJob(int jobid, int pageIndex, int pageSize, string orderBy)
        {
            var customer = new List<Customer>();
            int count = 0;
            IQueryable<Customer> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Customers.Include(x => x.Applications).
                        Where(c => c.Applications != null && c.Applications.Any(a => a.JobId == jobid)
                        && c.Status == 1 && c.FrofileStatus == 1);

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "date_desc":
                                query = query.OrderBy(c => c.Applications.First(a => a.JobId == jobid).ApplicationDate);
                                break;
                            case "vote":
                                query = query.OrderBy(e => e.Voting);
                                break;
                            case "vote_desc":
                                query = query.OrderByDescending(e => e.Voting);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.FullName);
                                break;
                            case "name_desc":
                                query = query.OrderByDescending(e => e.FullName);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address_desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(c => c.Applications.First(a => a.JobId == jobid).ApplicationDate);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    customer = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Customer>(customer, count, pageIndex, pageSize);
        }

    }


}
