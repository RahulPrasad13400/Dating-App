using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MembersController(API.Data.AppDbContext context)
        {
            _context = context; 
        }

        // To get all the members
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetAllMembers()
        {
            List<AppUser> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // To get a member based on the Id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMemberById(string id)
        {
            AppUser user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        // To Add a member
        [HttpPost]
        public async Task<ActionResult> AddMember(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { data = user, message = "User Added Successfully"});
        }
    }
}
