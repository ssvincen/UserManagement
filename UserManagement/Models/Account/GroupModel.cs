using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class GroupModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GroupModelView : GroupModel
    {
        public int Id { get; set; }

    }

}