using RestApis.Repository;

namespace RestApis.Models
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
    }
}
