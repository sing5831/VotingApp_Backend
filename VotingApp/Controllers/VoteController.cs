using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RegisterDataAccess;
using VoteDataAccess;

namespace VotingApp.Controllers
{
    public class VoteController : ApiController
    {
        List<Vote> data = new List<Vote>();

        public IEnumerable<Vote> Get()
        {
            using (votedbEntities entities = new votedbEntities())

            {
                var x = entities.Votes.ToList();
                foreach (VoteDataAccess.Vote v in x)
                {
                    var vid = v.VoteId;
                    var cname = v.CandidateName;
                    var cid = v.CandidateId;
                    var vo = v.Votes;

                    Vote vote = new Vote();
                    vote.CandidateId = cid;
                    vote.CandidateName = cname;
                    vote.VoteId = vid;
                    vote.Votes = vo;
                    data.Add(vote);
                    Console.Write(vo.ToString(), vo);
                }
            }

            return data;

        }

        public void Post(CastedVote val)
        {
          
            using (votedbEntities entities = new votedbEntities())
            {
                var x = entities.Votes.ToList();
                Vote vote = new Vote();
                vote.CandidateId = val.CandidateId;
                var cname = decrypt(val.CandidateName);
                vote.CandidateName = cname;
                foreach (VoteDataAccess.Vote c in x)
                {
                    if (val.CandidateId == c.CandidateId)
                    {
                            var currentVotes = c.Votes;
                            currentVotes += 1; // Updating for adding a new vote
                            c.Votes = currentVotes;
                   
                    }
                }
                using (RegisterDataAccess.votingdbEntities uentities = new RegisterDataAccess.votingdbEntities())
                {
                    var y = uentities.RegisterUsers.ToList();
                    RegisterDataAccess.RegisterUser users = new RegisterDataAccess.RegisterUser();
                    foreach (RegisterDataAccess.RegisterUser u in y)
                    {
                        if (val.UserEmail == u.Email)
                        {
                            u.Voting_Status = true;                          
                        }
                    }
                    uentities.SaveChanges();
                    SendEmail(val.UserEmail);
                }
                

                    //  entities.Votes.Add(vote);
                    entities.SaveChanges();
                
            }

        }

        public void SendEmail(string email)
        {
           
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Voting App", "capstonevotingproject2021@gmail.com"));
            mailMessage.To.Add(new MailboxAddress("Voter", email));
            mailMessage.Subject = "Voting Confirmation";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Dear Voter,            " +
                "                                                          "+
                "  This email is a confirmation that your vote has been recorded through the mobile voting app.              " +
                "  Thank You for Voting.              " +
                "                                                   "+
                "  Regards,                                         " +
                "  Capstone Project 2021       " +
                "  Sheridan College            "
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate("capstonevotingproject2021@gmail.com", "votingapp2021");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }

        public string decrypt(string encrypt_string)
        {
            var result = "";
            // byte[] bytes = Encoding.ASCII.GetBytes(encrypt_string);

            EncClass encryptionClass = new EncClass();
            result = encryptionClass.Decrypt(encrypt_string, "Password");

            return result;
        }

    }

    public class CastedVote
    {
        public int VoteId { get; set; }
        public Nullable<int> CandidateId { get; set; }
        public string CandidateName { get; set; }
        public Nullable<int> Votes { get; set; }

        public int UserId { get; set; }
        public String UserEmail { get; set; }

    }
}
