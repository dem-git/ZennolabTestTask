using CaptchaApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptchaApp.Server.Configuration
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class CaptchaContext : DbContext
    {
        public CaptchaContext(DbContextOptions<CaptchaContext> options) : base(options)
        {
        }

        /// <summary>
        /// Наборы данных.
        /// </summary>
        public DbSet<CaptchaDataSet> DataSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaptchaDataSet>(entity => { entity.HasKey(val => val.Id); });
        }
    }
}
