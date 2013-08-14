using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsSystem.Models;

namespace NewsSystem.Api.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int ArticleCount { get; set; }

        public int UserId { get; set; }

        public string Password { get; set; }
    }

    public class UserDetailsModel : UserModel
    {

        public ICollection<Article> Articles { get; set; }
    }

    public class UserLoginModel
    { 
        
    }
}