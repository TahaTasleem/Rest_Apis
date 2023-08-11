using RestApis.Repository;

namespace RestApis.Models
{
    public class Order : IEntity
    {
        [StoredProcedureParameter]
        public int Id { get; set; }
        [StoredProcedureParameter]
        public DateTime OrderDate { get; set; }
        [StoredProcedureParameter]
        public int UserId { get; set; }
    }
}
