using Database;
using Microsoft.AspNetCore.Mvc;
using PicturesAPI.Abstraction;
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
        IFileSaver _fileSaver;

        public PictureController(DatabaseContext dbContext, IFileSaver fileSaver)
        {
            _dbContext = dbContext;
            _fileSaver = fileSaver;
        }

        //[HttpGet("{userEmail}")]
        //public ActionResult<IEnumerable<Picture>> GetUserPictures(string userEmail)
        //{
        //    User user=  _dbContext.Users.Where(e => e.email == userEmail).FirstOrDefault();
        //
        //    
        //}

        //old version of post without physicall file.
       //[HttpPost("{userEmail}")]
       //public ActionResult<Guid> PostPicture([FromBody] PictureDTO pictureDTO, string userEmail)
       //{
       //    Guid id = Guid.NewGuid();
       //
       //    User user = _dbContext.Users.Where(c => c.email == userEmail).FirstOrDefault();
       //    if (user != null)
       //    {
       //        Picture picture = new Picture
       //        {
       //            Url = pictureDTO.Url,
       //            Description = pictureDTO.Description,
       //            Tags = pictureDTO.Tags,
       //            Title = pictureDTO.Title,
       //            DateAdded = DateTime.Now
       //        };
       //
       //        picture.OwnerId = user.Id;
       //
       //        if (user.Pictures == null) user.Pictures = new List<Picture>();
       //        user.Pictures.Add(picture);
       //        _dbContext.SaveChanges();
       //
       //        return id;
       //    }
       //    return null;
       //}

       [HttpPost("{userEmail}")]
       public async Task<IActionResult> PostPicture([FromForm] PictureDTO picture, string userEmail)
       {
            //Sprawdza czy uzytkownik o podanym mailu istnieje
            User user = _dbContext.Users.Where(e => e.email == userEmail).FirstOrDefault();
            if (user == null) return Problem("User not found");

            //Sprawdza czy podany plik nie jest pusty, jesli nie zapisuje go i dodaje link do pliku do bazy
            if (picture.File != null)
            {
                Guid id = Guid.NewGuid();
                string filePath = await _fileSaver.SaveFile(picture.File, id.ToString());
                Picture _picture = new Picture
                {
                    Id = id,
                    Title = picture.Title,
                    Url = filePath,
                    Description = picture.Description,
                    Tags = picture.Tags,
                    DateAdded = DateTime.Now,
                    OwnerId = user.Id
                };

                _dbContext.Pictures.Add(_picture);
                await _dbContext.SaveChangesAsync();

                return Ok(new { filePath });
            }
            else return Problem("No picture file provided");
       }
        
        [HttpGet("{userEmail}")]
        public ActionResult<IEnumerable<Picture>> GetUserPictures(string userEmail)
        {
            List<Picture> picList = new List<Picture>();
            User user = _dbContext.Users.Where(c => c.email == userEmail).FirstOrDefault();

            if (user != null)
            {
                picList = _dbContext.Pictures.Where(e => e.OwnerId == user.Id).ToList();

                return picList;
            }
            else return null;
        }
    }
}