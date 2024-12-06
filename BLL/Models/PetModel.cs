
using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class PetModel
    {
        public Pet Record { get; set; }
        public string Name => Record.Name;

        [DisplayName("Gender")]
        public string IsFemale => (bool)Record.IsFemale ? "Female" : "Male";
        
        [DisplayName("Age")]
        public string Age => Record.Age is null ? string.Empty : Record.Age.Value.ToString();

        [DisplayName("Weight")]
        public string Weight => Record.Weight.ToString("N1");

        public string Species => Record.Species?.Name;
        public string User => Record.User?.Name;
        




        
          

    }
}
