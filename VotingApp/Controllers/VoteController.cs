using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserDataAccess;
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
                vote.CandidateName = val.CandidateName;
                foreach (VoteDataAccess.Vote c in x)
                {
                    if (val.CandidateId == c.CandidateId)
                    {
                            var currentVotes = c.Votes;
                            currentVotes += 1; // Updating for adding a new vote
                            c.Votes = currentVotes;
                   
                    }
                }
                using (userdbEntities uentities = new userdbEntities())
                {
                    var y = uentities.Users.ToList();
                    UserDataAccess.Users users = new UserDataAccess.Users();
                    foreach (UserDataAccess.Users u in y)
                    {
                        if (val.UserEmail == u.Email)
                        {
                            u.Voting_Status = true;

                        }
                    }
                    uentities.SaveChanges();                 
                }
                

                    //  entities.Votes.Add(vote);
                    entities.SaveChanges();
                
            }

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
