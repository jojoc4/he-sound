using System.ComponentModel.DataAnnotations;

namespace HESound.Models
{
    public class Sound
    {
        public int SoundId { get; set; }
        [Display(Name = "Titre")]
        public string Title { get; set; }

        public byte[] Content { get; set; }

        [Display(Name = "Id de la personne")]
        public int PersonId { get; set; }

        [Display(Name = "Personne")]
        public Person Person { get; set; }
    }
}
