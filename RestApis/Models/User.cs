using RestApis.Repository;

namespace RestApis.Models
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
