namespace BussinessObject.DTO
{
    public class AccountLoginDTO
    {
        public int AccID { get; set; }

        public string? Name { get; set; }

        public string? Avatar { get; set; }

        public int Role { get; set; }

        public int? ProfileStatus { get; set; }

        public virtual List<TypeManagerDTO>? TypeManagers { get; set; }

    }
}
