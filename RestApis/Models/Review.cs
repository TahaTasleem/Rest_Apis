using RestApis.Repository;

namespace RestApis.Models
{
    public class Review : IEntity
    {
        [StoredProcedureParameter]
        public int Id { get; set; }
        [StoredProcedureParameter]
        public string Content { get; set; }
        [StoredProcedureParameter]
        public int Rating { get; set; }
        [StoredProcedureParameter]
        public int ProductId { get; set; }
    }
}
