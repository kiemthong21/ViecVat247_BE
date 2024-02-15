using Microsoft.AspNetCore.Mvc;

namespace viecvat247API.Controllers.MigrationData
{
    public class ImportDataController : ControllerBase
    {
        //Viecvat247DBcontext context = new Viecvat247DBcontext();
        //[HttpPost]
        //[Route("Customer")]
        //public async Task<IActionResult> ExportToExcel()
        //{
        //    try
        //    {

        //        string filePath = "D:/MigrationData/Customer.xlsx";
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //        FileInfo file = new FileInfo(filePath);

        //        using (var package = new ExcelPackage(file))
        //        {
        //            // Tạo một bảng trong tệp Excel
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

        //            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
        //            {
        //                Customer customer = new Customer
        //                {
        //                    Cemail = worksheet.Cells[row, 2].Text,
        //                    Password = worksheet.Cells[row, 3].Text,
        //                    Role = int.Parse(worksheet.Cells[row, 4].Text),
        //                    Username = worksheet.Cells[row, 5].Text,
        //                    PhoneNumber = worksheet.Cells[row, 6].Text,
        //                    FullName = worksheet.Cells[row, 7].Text,
        //                    Address = worksheet.Cells[row, 8].Text,
        //                    Avatar = worksheet.Cells[row, 9].Text,
        //                    CreateDate = DateTime.ParseExact(worksheet.Cells[row, 10].Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
        //                    UpdateDate = DateTime.ParseExact(worksheet.Cells[row, 11].Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
        //                    Epoint = long.Parse(worksheet.Cells[row, 12].Text),
        //                    Cv = worksheet.Cells[row, 13].Text,
        //                    Voting = double.Parse(worksheet.Cells[row, 14].Text),
        //                    Type = int.Parse(worksheet.Cells[row, 15].Text),
        //                    VerifyCode = worksheet.Cells[row, 16].Text,
        //                    Otp = worksheet.Cells[row, 17].Text,
        //                    Status = int.Parse(worksheet.Cells[row, 18].Text)

        //                };
        //                Debug.WriteLine(customer);

        //                context.Customers.Add(customer);
        //            }
        //            context.SaveChanges();

        //        }

        //        return Ok("Import Successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error: " + ex.Message);
        //    }
        //}
    }
}
