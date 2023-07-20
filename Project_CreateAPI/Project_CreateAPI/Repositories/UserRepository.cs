using Microsoft.EntityFrameworkCore;
using Project_CreateAPI.Models;

namespace Project_CreateAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CMSContext _context;
        public UserRepository(CMSContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(u => u.CourseEnrollments).Include(u => u.QuizAttendances).Include(u => u.StudentAssignments).Include(u => u.Courses).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
