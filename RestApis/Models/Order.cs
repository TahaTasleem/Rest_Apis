using RestApis.Repository;

namespace RestApis.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
    }
}
