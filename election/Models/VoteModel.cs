using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using election.Models.Request;

namespace election.Models
{
    public class VoteModel
    {
        public VoteModel()
        {
        }
        public VoteModel(VoteRequest request)
        {
            this.candidateId = request.candidateId;
            this.nationalId = request.nationalId;
        }

        [Key]
        [Column("nationalid")]
        public string nationalId { get; set; }
        [Column("candidateid")]
        public int candidateId { get; set; }
    }
}
