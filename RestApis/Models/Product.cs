using RestApis.Repository;

namespace RestApis.Models
{
    public class Product : IEntity
    {
        [StoredProcedureParameter]
        public int Id { get; set; }
        [StoredProcedureParameter]
        public string Name { get; set; }
        [StoredProcedureParameter]
        public decimal Price { get; set; }
        [StoredProcedureParameter]
        public string Description { get; set; }
    }
}
