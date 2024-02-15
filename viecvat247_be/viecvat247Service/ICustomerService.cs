using BusinessObject;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int id);

        void UpdateCustomer(Customer customer);

        void SendMailForgotPassword(string mail, string code, int id);

        void ChangeForgotPassword(string email);

        void CreateCustomerSkill(string[]? listSkill, int cusId);

        void DeleteCustomerSkillbyCustomerId(int cusId);

        PaginatedList<Customer> GetCustomers(string searchValue, int pageIndex, int pageSize, string orderBy, string role, string status);

        PaginatedList<Customer> GetCustomersApplyByJob(int jobid, int pageIndex, int pageSize, string orderBy);
        Customer GetInfoById(int cid, string? uid);
    }
}
