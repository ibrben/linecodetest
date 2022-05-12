using System;
using election.Interfaces;

namespace election.DAO
{
    public class ElectionState : IElectionState
    {
        private bool electionState;
        public ElectionState()
        {
        }

        bool IElectionState.GetElectionState()
        {
            return electionState;
        }

        bool IElectionState.ToggleElection(bool enable)
        {
            electionState = enable;

            return electionState;
        }
    }
}
