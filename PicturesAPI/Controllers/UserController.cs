using Database;
using Microsoft.AspNetCore.Mvc;
using PicturesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DatabaseContext _dbContext;

        public UserController(DatabaseContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        [HttpGet("{email}")]
        public ActionResult<IEnumerable<UserDTO>> GetUser(string email)
        {
            return _dbContext.Users.Where(u => u.email.Equals(email)).Select(e => new UserDTO
            {
                email = e.email,
                Name = e.Name,
                Password = e.Password,
                Id = e.Id
            }).ToList(); 

        }

        [HttpPost]
        public ActionResult<Guid> PostUser([FromBody]UserDTO value)
        {
            Guid id = Guid.NewGuid();

            _dbContext.Users.Add(new Database.User
            {
                Id = id,
                email = value.email,
                Name = value.Name,
                Password=value.Password,

            });
            _dbContext.SaveChanges();
            return id;
        }

        [HttpPut("id")]
        public void PutUser(Guid id, [FromBody] UserDTO value)
        {
            var entity = _dbContext.Users.SingleOrDefault(e => e.Id == id);
            if (entity != null)
            {
                //TODO dopisac mapowanie na tego usera
            }
        }
        
    }
}
