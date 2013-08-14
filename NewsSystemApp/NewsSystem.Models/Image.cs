
namespace NewsSystem.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual Article Article { get; set; }
    }
}
