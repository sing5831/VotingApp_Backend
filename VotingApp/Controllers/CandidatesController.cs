using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CandidateDataAccess;
using System.Data.SqlClient;

namespace VotingApp.Controllers
{
    public class CandidatesController : ApiController
    {
   
    
        List<Candidates> data = new List<Candidates>();

        public IEnumerable<Candidates> Get()
        {
            using (candidatedbEntities entities = new candidatedbEntities())

            {
                var x = entities.Candidates.ToList();
                foreach (CandidateDataAccess.Candidates c in x)
                {
                    var cname = c.CandidateName;
                    var id = c.CandidateId;
                
                    Candidates candidates = new Candidates();
                    candidates.CandidateId = id;
                    candidates.CandidateName = cname;
                    data.Add(candidates);
                    Console.Write(id.ToString(), cname);
                }
            }

            return data;

        }

        // GET: api/Candidates/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Candidates
        public void Post(Candidates val)
        {
            // _conn = new SqlConnection("Server=tcp:mobilevoting.database.windows.net,1433;Initial Catalog=votingdb;Persist Security Info=False;User ID=navneet;Password=Voting@24;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            // var query = "INSERT INTO Cndidates (CandidateId,CandidateName) ";
            //  data.Add(val);
            using (candidatedbEntities entities = new candidatedbEntities())
            {
                var x = entities.Candidates.ToList();
                Candidates candidates = new Candidates();
                candidates.CandidateId = val.CandidateId;
                foreach (CandidateDataAccess.Candidates c in x)
                {
                     if(val.CandidateId == c.CandidateId)
                    {
                    //    var currentVotes = c.Vote;
                    //    currentVotes += 1; // Updating for adding a new vote
                   //     c.Vote = currentVotes;
                    }
                }


                candidates.CandidateName = val.CandidateName;
                entities.Candidates.Add(candidates);
                entities.SaveChanges();
            }

        }

        // PUT: api/Candidates/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Candidates/5
        public void Delete(int id)
        {
        }
    }
}
