
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HESound.Models
{
    public class PersonUploadViewModel : Person
    {
        [Display(Name = "Photo")]
        public IFormFile FormFile { get; set; }
    }
}
