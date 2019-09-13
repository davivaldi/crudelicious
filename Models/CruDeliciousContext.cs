using Microsoft.EntityFrameworkCore;


namespace CruDelicious.Models
{
    public class CruDeliciousContext : DbContext
    {

        public CruDeliciousContext(DbContextOptions options) : base(options){}
         public DbSet<Dishes> Dishes {get;set;}
    }

}



