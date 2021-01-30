using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HESound.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        [Display(Name="Nom")]
        public string Name { get; set; }
        public byte[] Photo { get; set; }

        public List<Sound> Sounds { get; } = new List<Sound>();
    }
}
