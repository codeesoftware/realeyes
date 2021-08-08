using Microsoft.EntityFrameworkCore;
using Realeyes.Domain.Models;

namespace Realeyes.Infrastructure.Data
{
    public class RealeyeDbContext:DbContext
    {
        public DbSet<Survey> Surveys { get; private set; }
        public DbSet<Session> Sessions { get; private set; }
        public RealeyeDbContext(DbContextOptions<RealeyeDbContext> options) : base(options)
        {
        }
    }
}
