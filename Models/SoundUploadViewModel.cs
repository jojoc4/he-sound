
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HESound.Models
{
    public class SoundUploadViewModel : Sound
    {
        [Display(Name = "Son")]
        public IFormFile FormFile { get; set; }
    }
}
