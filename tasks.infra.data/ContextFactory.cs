using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace tasks.infra.data
{
    public class ContextFactory : IDesignTimeDbContextFactory<TarefaContext>
    {
        public TarefaContext CreateDbContext()
        {
            return CreateDbContext(null);
        }
        
        public TarefaContext CreateDbContext(string[] args)
        {
            var settingPath = Path.GetFullPath(Path.Combine(@"../tasks.service/appsettings.json"));
            var builderConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingPath);
            var configuration = builderConfiguration.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<TarefaContext>();
            builder.UseSqlServer(connectionString);

            return new TarefaContext(builder.Options);
        }
    }
}