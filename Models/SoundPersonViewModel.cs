using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HESound.Models
{
    public class SoundPersonViewModel
    {
        public List<Sound> Sounds { get; set; }
        public SelectList People { get; set; }
        public string SearchString { get; set; }
        public int PersonId { get; set; }
    }
}
