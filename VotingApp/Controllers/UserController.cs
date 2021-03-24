using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserDataAccess;

namespace VotingApp.Controllers
{
    public class UserController : ApiController
    {
        List<VotingApp.Users> data = new List<VotingApp.Users>();
        public IEnumerable<VotingApp.Users> Get()
                {
                    
                    using (userdbEntities entities = new userdbEntities())

                    {
                        var x = entities.Users.ToList();
                        foreach (UserDataAccess.Users u in x)
                        {
                            var fname = u.FirstName;
                            var id = u.UserId;
                            var lname = u.LastName;
                            var dob = u.DOB;
                            var license = u.License;
                            var email = u.Email;
                            var pass = u.User_Password;
                            var img = u.User_Image;
                            var status = u.Voting_Status;
                            VotingApp.Users user = new Users();
                            user.FirstName = fname;
                            user.LastName = lname;
                            user.UserId = id;
                            user.DOB = dob;
                            user.Email = email;
                            user.License = license;
                            user.User_Image = img;
                            user.User_Password = pass;
                            user.Voting_Status = status;
                            data.Add(user);
                            Console.Write(id.ToString(), fname);
                        }
                    }

                    return data;
           
        }

        //     using(userdbEntities entities = new userdbEntities())
        //  {
        //         var x = entities.Users.ToList();
        //   Console.WriteLine(x);
        // return entities.User.ToList();
        //}

        public void Post(Users val)
        {
            using (userdbEntities entities = new userdbEntities())
            {
                UserDataAccess.Users users = new UserDataAccess.Users();
                users.UserId = val.UserId;
                users.FirstName = val.FirstName;
                users.LastName = val.LastName;
                users.Email = val.Email;
                users.User_Password = val.User_Password;
                users.License = val.License;
                users.DOB = val.DOB;
                users.Voting_Status = val.Voting_Status;
                users.User_Image = val.User_Image;
                entities.Users.Add(users);
                entities.SaveChanges();
            }
        }



    }
}
