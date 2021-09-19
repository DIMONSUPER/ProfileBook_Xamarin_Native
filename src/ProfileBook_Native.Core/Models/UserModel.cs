using SQLite;

namespace ProfileBook_Native.Core.Models
{
    [Table(Constants.USERS_TABLE_NAME)]
    public class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(16)]
        public string Login { get; set; }
        [MaxLength(16)]
        public string Password { get; set; }
    }
}
