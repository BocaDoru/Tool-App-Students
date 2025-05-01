using Microsoft.EntityFrameworkCore;

namespace ToolAppStudentsServer.Context
{
    public class UserAuthContext:DbContext
    {
        public UserAuthContext(DbContextOptions<UserAuthContext> options):base(options) { }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.CalendarTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=studentsdb;user=root;password=123456789");
        }
    }
}
