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

        public string decrypt(string encrypt_string)
        {
            var result = "";
            // byte[] bytes = Encoding.ASCII.GetBytes(encrypt_string);

            EncClass encryptionClass = new EncClass();
            result = encryptionClass.Decrypt(encrypt_string, "Password");

            return result;
        }

        public String Post(RegisterUser val)
        {
            var result = "Failure";


         //   DateTime startdate = new DateTime(2021, 05, 01, 12, 00, 00);
         //   DateTime enddate = new DateTime(2021, 05, 15, 11, 00, 00);

            DateTime currentdate = DateTime.Now;

            using (ElectionStartEndDataAccess.startenddbEntities startenddbEntities = new ElectionStartEndDataAccess.startenddbEntities())
            {
                ElectionStartEndDataAccess.ElectionStartEnd startEnd = new ElectionStartEndDataAccess.ElectionStartEnd();
                var y = startenddbEntities.ElectionStartEnds.ToList();
                foreach (ElectionStartEndDataAccess.ElectionStartEnd a in y)
                {
                    if (currentdate > a.EndDate)
                    {
                        result = "election_over";
                    }
                    else
                    {
                        result = "check_later";
                    }
                    //        }
                    //           }


                    if (currentdate >= a.StartDate && currentdate <= a.EndDate)
                    {

                        using (RegisterDataAccess.votingdbEntities entities = new RegisterDataAccess.votingdbEntities())
                        {
                            RegisterDataAccess.RegisterUser registerUser = new RegisterDataAccess.RegisterUser();
                            var x = entities.RegisterUsers.ToList();
                            foreach (RegisterDataAccess.RegisterUser u in x)
                            {
                                if (decrypt(val.Email) == u.Email && val.User_Password == u.User_Password && decrypt(val.DeviceId) == u.DeviceId)
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
                    }
                }
            }

            /*
            else
            {
                if(currentdate >= enddate)
                {
                    result = "election_over";
                }
                else
                {
                    result = "check_later";
                }
            }

            */


            return result;
        }
    }
}
