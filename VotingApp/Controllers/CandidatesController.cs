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
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        /*
        List<Candidate> candidate = new List<Candidate>();
        // GET: api/Candidates
        public CandidatesController()
        {
            candidate.Add(new Candidate { CandidateId = 1, CandidateName = "Doug Ford" });
            candidate.Add(new Candidate { CandidateId = 2, CandidateName = "Patrick Brown" });
        }

        public List<Candidate> Get()
        {
            return candidate;
        }
        */
    
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
            data.Add(val);
           
           
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
