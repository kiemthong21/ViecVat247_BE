using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BussinessObject.Viecvat247Context
{
    public class Viecvat247ContextFactory : IDesignTimeDbContextFactory<Viecvat247DBcontext>
    {
        public Viecvat247DBcontext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("viecvat247DB");
            var optionBuilder = new DbContextOptionsBuilder<Viecvat247DBcontext>();
            optionBuilder.UseSqlServer(connectionString);

            return new Viecvat247DBcontext(optionBuilder.Options);
        }
    }
}
