using BCrypt;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.Encoders;
using System.Diagnostics;
using System.Drawing;
using ToolAppStudentsServer.DTO;
using ToolAppStudentsServer.Models;
namespace ToolAppStudentsServer.Service
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(RegisterDto registerDto);
        Task<User> LoginUserAsync(LoginDto loginDto);
        List<CalendarTask> GetWeekTask(int userId);
        Task<CalendarTask> SetTaskAsync(int userId,TaskDto taskDto);
        Task<User> FindByNameAsync(string username);
    }
    public class UserService:IUserService
    {
        private readonly Context.UserAuthContext _context;
        public UserService(Context.UserAuthContext context)
        {
            _context = context;
        }
        public async Task<User> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) { return null; }
            return user;
        }
        public async Task<User> FindByNameAsync(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null) { return null; }
            return user;
        }
        public List<CalendarTask> GetWeekTask(int userId)
        {
            var tasks = _context.Tasks.Where(t => t.UserId == userId && 
                                                  t.Day >= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek) && 
                                                  t.Day <= DateTime.Today.AddDays(6 - (int)DateTime.Today.DayOfWeek))
                                      .OrderBy(t => t.Day)
                                      .ToList();
            return tasks;
        }
        public async Task<CalendarTask> SetTaskAsync(int userId,TaskDto taskDto)
        {
            var task = new Models.CalendarTask
            {
                UserId = userId,
                Title=taskDto.Name,
                Day=taskDto.Day,
                Start=taskDto.Start,
                End=taskDto.End,
                Duration=taskDto.Duration,
                Notification=taskDto.Notification,
                Description=taskDto.Description,
                Color=Convert.FromBase64String(taskDto.TaskColor)
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

    }
}
