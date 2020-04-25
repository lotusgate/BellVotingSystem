using System.ComponentModel.DataAnnotations;

namespace BellVotingSystem.WEB.Models.Entry
{
    public class EntryCreateViewModel
    {
        [Required(ErrorMessage= "Song title is required" )]
        [StringLength(30, ErrorMessage = "Song must be 30 characters long")]
        public string Song { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        [StringLength(30, ErrorMessage = "Artist name must be 30 characters long")]
        public string Artist { get; set; }
    }
}
