using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using System.Security.Cryptography;
using System.Text;

namespace viecvat247Service.Service
{
    public class OtherService : IOtherService
    {
        private readonly string key = "2giotoitaigoccayda";

        public string Decrypt(string toDecrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string GenerateRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder randomString = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characters.Length);
                randomString.Append(characters[index]);
            }

            return randomString.ToString();
        }

        public PaginatedList<Skill> GetAllSkills(string searchValue, string cate, string orderBy)
        => SkillDAO.GetAllSkills(searchValue, cate, orderBy);

        public List<ChartDTO> GetChart(int year)
        => DashboardDAO.GetChart(year);

        public PaginatedList<JobsCategory> GetJobCategory(string searchValue, string orderBy)
        => CategoryJobsDAO.GetAllJobCategory(searchValue, orderBy);

        public PaginatedList<SkillCategory> GetSkillCategories(string searchValue, string orderBy)
        => SkillDAO.GetAllSkillCategory(searchValue, orderBy);

        public DashboardDTO GetStatisticsDaskboard(int year)
        => DashboardDAO.GetStatisticsDaskboard(year);
    }
}
