using BussinessObject.DTO;
using BussinessObject.Models;
namespace viecvat247Service
{
    public interface IAuthenService
    {
        Customer GetCustomerByLogin(LoginDTO login);

        User GetUserByLogin(LoginDTO login);

        Customer Register(Customer cus);

        Customer GetCustomerToConfirmMail(int id, string code);

        void ConfirmMailDone(int id);

        void SendMail(int id, string mail, string code);

        Customer GetCustomerByEmail(string email);

    }
}
