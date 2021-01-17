using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Yms.Data.Context
{
    public class YmsDbContextFactory : IDesignTimeDbContextFactory<YmsDbContext>
    {
        public YmsDbContext CreateDbContext(string[] args)
        {
            var cs = "Server =.;Database=YmsDb;User Id=ymsadmin;Password=123qwe!";
            var builder = new DbContextOptionsBuilder().UseSqlServer(cs);
            return new YmsDbContext(builder.Options);
        }
    }
}