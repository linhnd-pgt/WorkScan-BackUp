using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApp.Models
{
    [Table("category")]
    public class CategoryModel : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        public ICollection<TaskModel> taskModelList { get; set; }   

    }
}
