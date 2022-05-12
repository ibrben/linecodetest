using System;
namespace election.Interfaces
{
    public interface IElectionState
    {
        public bool ToggleElection(bool enable);

        public bool GetElectionState();
    }
}
