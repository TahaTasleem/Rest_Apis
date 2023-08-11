using RestApis.Repository;

namespace RestApis.Models
{
    public class User: IEntity
    {
        [StoredProcedureParameter]
        public int Id { get; set; }
        [StoredProcedureParameter]
        public string Username { get; set; }
        [StoredProcedureParameter]
        public string Email { get; set; }
        [StoredProcedureParameter]

        public string Password { get; set; }
    }
}
