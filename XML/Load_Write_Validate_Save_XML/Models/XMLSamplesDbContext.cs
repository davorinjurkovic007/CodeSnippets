using Microsoft.EntityFrameworkCore;

namespace XMLSamples
{
    /// <summary>
    /// Entity Framework DbContext class
    /// </summary>
    public class XMLSamplesDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        #region BuildConnectionString Method
        /// <summary>
        /// Build the connection string using the current directory information
        /// </summary>
        public string BuildConnectionString()
        {
            // Get path to database
            //string path = @"D:\Samples\Database\XMLSamples.mdf";

            // Build connection string
            //return @$"Server=(localdb)\mssqllocaldb;AttachDbFileName={path};Integrated Security=true";
            return @$"Data Source=localhost,1433;Database=XMLSamples;User ID=sa;Password=Password_123#;Connect Timeout=30;Encrypt=False";
        }
        #endregion

        #region Override OnConfiguring Method
        /// <summary>
        /// Set the connection string and other options on the DbContext object
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = BuildConnectionString();

            optionsBuilder.UseSqlServer(connString, builder =>
            {
                builder.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null);
            });
        }
        #endregion

        #region OnModelCreating Method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}