using RestApis.Repository;

namespace RestApis.Models
{
    public class Category: IEntity
    {
        [StoredProcedureParameter]
        public int Id { get; set; }
        [StoredProcedureParameter]
        public string Name { get; set; }
    }
}
