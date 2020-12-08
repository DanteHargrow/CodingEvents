using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventCategoryViewModel
    {
        [Required(ErrorMessage ="Category name is required!")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Category name needs to be between 2 and 20 characters!")]
        public string Name { get; set; }
    }
}
