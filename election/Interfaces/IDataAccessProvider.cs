using System;
using System.Collections.Generic;
using election.Models;

namespace election.Interfaces
{
    public interface IDataAccessProvider
    {
        public void AddCandidate(CandidateModel candidate);
        public CandidateModel UpdateCandidate(int candidateId, CandidateModel candidate);
        public void DeleteCandidate(int id);
        public CandidateModel GetCandidateById(int id);
        public List<CandidateModel> GetAllCandidates();

        public void Vote(VoteModel vote);
        public VoteModel GetVoteByNationalId(string nationalId);
    }
}
