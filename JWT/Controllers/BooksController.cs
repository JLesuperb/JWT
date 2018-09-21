using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var CurrentUser = HttpContext.User;
            if(CurrentUser.FindFirst("UserName")!=null)
            {
                String UserName = CurrentUser.FindFirst("UserName").Value;
                var resultBookList = new Book[]
                {
                    new Book { Author = "Ray Bradbury",Title = "Fahrenheit 451",UserName=UserName },
                    new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude",UserName=UserName },
                    new Book { Author = "George Orwell", Title = "1984",UserName=UserName },
                    new Book { Author = "Anais Nin", Title = "Delta of Venus" ,UserName=UserName}
                };
                return Ok(resultBookList);
            }
            else
            {
                return Unauthorized();
            }
        }

        public class Book
        {
            public String Author { get; set; }
            public String Title { get; set; }
            public bool AgeRestriction { get; set; }
            public String UserName { get; set; }
        }
    }
}