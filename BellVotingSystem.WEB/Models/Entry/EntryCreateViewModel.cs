using System.ComponentModel.DataAnnotations;

namespace BellVotingSystem.WEB.Models.Entry
{
    public class EntryCreateViewModel
    {
        [Required(ErrorMessage= "Song link is required" )]
        public string Song { get; set; }
    }
}