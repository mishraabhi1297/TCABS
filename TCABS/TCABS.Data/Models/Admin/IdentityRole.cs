using System.ComponentModel.DataAnnotations;

namespace TCABS.Data.Models.Entities
{
    public class IdentityRole
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "A name for the role is needed.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}
