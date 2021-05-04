using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using RegisterDataAccess;


namespace VotingApp.Controllers
{

    public class LoginController : ApiController
    {
     

        public String Post(RegisterUser val)
        {
            var result = "test";
            using (RegisterDataAccess.votingdbEntities entities = new RegisterDataAccess.votingdbEntities())
            {
                RegisterDataAccess.RegisterUser registerUser = new RegisterDataAccess.RegisterUser();
                var x = entities.RegisterUsers.ToList();
                foreach (RegisterDataAccess.RegisterUser u in x)
                {
                    if (val.Email == u.Email && val.User_Password == u.User_Password)
                    {
                        result = "Success";
                     
                        if (u.Voting_Status == true)
                        {
                                result = "Voted";
                            break;
                        }
                        

                    }
                 

                }


                   
            }
            return result;
        }
    }
}
