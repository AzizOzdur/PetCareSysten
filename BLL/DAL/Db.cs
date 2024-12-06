 
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db: DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Species> Species { get; set; }

        public Db(DbContextOptions<Db> options) : base(options) { }
    }
}
