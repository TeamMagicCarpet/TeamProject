
namespace NewsSystem.Api.Models
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ArticleModel Article { get; set; }
    }
}