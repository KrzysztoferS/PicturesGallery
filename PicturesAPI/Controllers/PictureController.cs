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
    public class PictureController:ControllerBase
    {
        DatabaseContext _dbContext;

        public PictureController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[HttpGet("{userEmail}")]
        //public ActionResult<IEnumerable<Picture>> GetUserPictures(string userEmail)
        //{
        //    User user=  _dbContext.Users.Where(e => e.email == userEmail).FirstOrDefault();
        //
        //    
        //}

        [HttpPost("{userEmail}")]
        public ActionResult<Guid> PostPicture([FromBody] PictureDTO pictureDTO, string userEmail)
        {
            Guid id = Guid.NewGuid();

            User user = _dbContext.Users.Where(c => c.email == userEmail).FirstOrDefault();
            if (user != null)
            {
                Picture picture = new Picture
                {
                    Url = pictureDTO.Url,
                    Description = pictureDTO.Description,
                    Tags = pictureDTO.Tags,
                    Title = pictureDTO.Title,
                    DateAdded = DateTime.Now
                };

                picture.OwnerId = user.Id;

                if (user.Pictures == null) user.Pictures = new List<Picture>();
                user.Pictures.Add(picture);
                _dbContext.SaveChanges();

                return id;
            }
            return null;
        }
        
        [HttpGet("{userEmail}")]
        public ActionResult<IEnumerable<Picture>> GetUserPictures(string userEmail)
        {
            List<Picture> picList = new List<Picture>();
            User user = _dbContext.Users.Where(c => c.email == userEmail).FirstOrDefault();

            picList = _dbContext.Pictures.Where(e => e.OwnerId == user.Id).ToList();
            
            return picList;
        }
    }
}