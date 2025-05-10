using Microsoft.EntityFrameworkCore;

namespace ToolAppStudentsServer.Context
{
    public class UserAuthContext:DbContext
    {
        public UserAuthContext(DbContextOptions<UserAuthContext> options):base(options) { }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.CalendarTask> Tasks { get; set; }

    }
}
