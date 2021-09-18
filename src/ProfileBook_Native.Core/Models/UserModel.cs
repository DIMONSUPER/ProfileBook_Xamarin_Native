using SQLite;

namespace ProfileBook_Native.Core.Models
{
    [Table("Users")]
    public class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(16)]
        public string Login { get; set; }
    }
}
