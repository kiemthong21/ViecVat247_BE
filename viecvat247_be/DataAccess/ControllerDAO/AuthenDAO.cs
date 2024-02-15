using BussinessObject.DTO;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.ControllerDAO
{
    public class AuthenDAO
    {
        //Login by Customer
        public static Customer GetCustomerByLogin(LoginDTO login)
        {
            var cus = new Customer();

            try
            {
                //Convert the string into an array of bytes.
                byte[] passEncoding = Encoding.UTF8.GetBytes(login.Password);

                //Create the hash value from the array of bytes.
                byte[] compareHashValue = SHA256.HashData(passEncoding);
                using (var context = new Viecvat247DBcontext())
                {
                    cus = context.Customers.SingleOrDefault(c => c.Cemail.Equals(login.Email));
                    if (cus != null)
                    {
                        bool checkPass = Convert.FromHexString(cus.Password).SequenceEqual(compareHashValue);
                        if (checkPass)
                        {
                            return cus;
                        }
                        else
                        {
                            cus = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cus;
        }

        //Login by Staff
        public static User GetUserByLogin(LoginDTO login)
        {
            var user = new User();
            try
            {
                //Convert the string into an array of bytes.
                byte[] passEncoding = Encoding.UTF8.GetBytes(login.Password);

                //Create the hash value from the array of bytes.
                byte[] compareHashValue = SHA256.HashData(passEncoding);
                using (var context = new Viecvat247DBcontext())
                {
                    user = context.Users
                        .Include(c => c.TypeManagerUsers)
                        .ThenInclude(c => c.TypeManager)
                        .SingleOrDefault(c => c.Uemail.Equals(login.Email));
                    if (user != null)
                    {
                        bool checkPass = Convert.FromHexString(user.Password).SequenceEqual(compareHashValue);
                        if (checkPass)
                        {
                            return user;
                        }
                        else
                        {
                            user = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        public static Customer CreateAccount(Customer cus)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Customers.Add(cus);
                    context.SaveChanges();
                    return cus;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Customer GetCustomerByEmail(string email)
        {
            var user = new Customer();

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    user = context.Customers.SingleOrDefault(c => c.Cemail.Equals(email));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        public static Customer GetCustomerConfirmMail(string code, int id)
        {
            var cus = new Customer();

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    cus = context.Customers.SingleOrDefault(c => c.Cid == id && c.VerifyCode.Equals(code));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cus;
        }

        public static void ConfirmMail(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var cus = context.Customers.SingleOrDefault(c => c.Cid == id);
                    cus.Status = 1;
                    context.Entry<Customer>(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
