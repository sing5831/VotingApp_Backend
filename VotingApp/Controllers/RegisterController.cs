using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RegisterDataAccess;

namespace VotingApp.Controllers
{
    public class RegisterController : ApiController
    {
        // GET: api/Register
        List<RegisterUser> data = new List<RegisterUser>();
        public IEnumerable<RegisterUser> Get()
        {

            using (RegisterDataAccess.votingdbEntities entities = new RegisterDataAccess.votingdbEntities())
            {
                var x = entities.RegisterUsers.ToList();
                foreach (RegisterUser u in x)
                {

                    var uid = u.User_Id;
                    var email = u.Email;
                    var pass = u.User_Password;
                    var vid = u.VoterId;
                    var pin = u.PIN;
                    var deviceid = u.DeviceId;
                    var status = u.Voting_Status;
                   
                    RegisterUser user = new RegisterUser();
                    user.User_Id = uid;
                    user.Email = email;
                    user.User_Password = pass;
                    user.VoterId = vid;
                    user.PIN = pin;
                    user.DeviceId = deviceid;
                    user.Voting_Status = status;
                    data.Add(user);
            
                }
            }

            return data;

        }

        public string decrypt(string encrypt_string)
        {
            var result = "";
           // byte[] bytes = Encoding.ASCII.GetBytes(encrypt_string);

            EncClass encryptionClass = new EncClass();
            result = encryptionClass.Decrypt(encrypt_string,"Password");

            return result;
        }

   

        // POST: api/Register
        public string Post(RegisterUser val)
        {
            using (RegisterDataAccess.votingdbEntities entities = new RegisterDataAccess.votingdbEntities())
            {
                RegisterDataAccess.RegisterUser users = new RegisterDataAccess.RegisterUser();
                users.User_Id = val.User_Id;

                var y = entities.RegisterUsers.ToList();
                RegisterDataAccess.RegisterUser registerUser = new RegisterDataAccess.RegisterUser();
                foreach (RegisterDataAccess.RegisterUser u in y)
                {
                    if (val.Email == u.Email && val.PIN == u.PIN)
                    {
                         var pass = decrypt(val.User_Password);
                         u.User_Password = pass;
                      //  u.User_Password = val.User_Password;
                           u.DeviceId = val.DeviceId;

                    }
                }
                entities.SaveChanges();
            }

            return val.Email;

        }

       
    }
}
