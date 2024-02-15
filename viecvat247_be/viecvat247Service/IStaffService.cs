using BusinessObject;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface IStaffService
    {
        User GetStaffById(int id);

        void UpdateStaff(User user);

        User AddNewStaff(User user);

        void SendMailAddnewStaff(string mail, string password, string fullname);

        PaginatedList<User> GetStaffs(string searchValue, int pageIndex, int pageSize, string orderBy);
        List<User> GetStaffss(string searchValue, int pageIndex, int pageSize, string orderBy);


        User GetStaffByEmailAndPhone(string? uemail, string? phoneNumber);
        void DeleteTypeManagerUserbyUserId(int id);
        void CreateTypeManagerUser(string[]? typeManagers, int id);
        List<TypeManager> GetAllTypeManager();
        TypeManager GettypeManager(int id);
    }
}
