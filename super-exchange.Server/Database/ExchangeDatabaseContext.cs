using Microsoft.EntityFrameworkCore;
using super_exchange.Server.Entity;

namespace super_exchange.Server.Database;

public class ExchangeDatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<RateEntity> RateEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RateEntity>().
            HasKey(k => new { k.EffectiveDate, k.Code });
    }
}
