using System;
using System.Collections.Generic;
using System.Linq;
using election.Exceptions;
using election.Interfaces;
using election.Models;

namespace election.DAO
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgresqlContext _context;
        public DataAccessProvider(PostgresqlContext context)
        {
            _context = context;
        }

        
        #region Candiate
        void IDataAccessProvider.AddCandidate(CandidateModel candidate)
        {
            _context.candidate.Add(candidate);
            _context.SaveChanges();
        }

        void IDataAccessProvider.DeleteCandidate(int id)
        {
            var entity = _context.candidate.FirstOrDefault(t => t.id == id);
            _context.candidate.Remove(entity);
            _context.SaveChanges();
        }

        List<CandidateModel> IDataAccessProvider.GetAllCandidates()
        {
            return _context.candidate.ToList();
        }

        CandidateModel IDataAccessProvider.GetCandidateById(int id)
        {
            return _context.candidate.FirstOrDefault(t => t.id == id);
        }

        CandidateModel IDataAccessProvider.UpdateCandidate(int candidateId, CandidateModel candidate)
        {
            _context.Update(candidate);
            _context.SaveChanges();

            return _context.candidate.FirstOrDefault(t => t.id == candidate.id);

        }
        #endregion

        #region Vote
        void IDataAccessProvider.Vote(VoteModel vote)
        {
            var voter = _context.votes.FirstOrDefault(t => t.nationalId == vote.nationalId);
            if (voter != null)
                throw new AlreadyVoteException();

            var candidate = _context.candidate.FirstOrDefault(t => t.id == vote.candidateId);
            
            candidate.voteCount++;
            _context.candidate.Update(candidate);
            _context.SaveChanges();

            _context.votes.Add(vote);
            _context.SaveChanges();
        }

        public VoteModel GetVoteByNationalId(string nationalId)
        {
            return _context.votes.FirstOrDefault(t => t.nationalId == nationalId);
        }
        #endregion

    }
}
