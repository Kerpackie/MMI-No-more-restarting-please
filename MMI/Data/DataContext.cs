using Microsoft.EntityFrameworkCore;
using MMI.Models;

namespace MMI.Data
{
	public class DataContext : DbContext
	{
        public DataContext()
        {

        }

		public DataContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite("Data source=./Database.db");
			}
		}

		public DbSet<Quotation> Quotations { get; set; }
	}
}