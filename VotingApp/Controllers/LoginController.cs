using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using UserDataAccess;

namespace VotingApp.Controllers
{

    public class LoginController : ApiController
    {
     

        public String Post(Users val)
        {
            var result = "test";
            using (userdbEntities entities = new userdbEntities())
            {
                UserDataAccess.Users users = new UserDataAccess.Users();
                var x = entities.Users.ToList();
                foreach (UserDataAccess.Users u in x)
                {
                    if (u.Email == val.Email && u.User_Password == val.User_Password)
                    {

                        result = "Success";
                        break;
                    }
                    else
                    {
                        result = "Failure";
                    }
                }
                   
            }
            return result;
        }
    }
}
