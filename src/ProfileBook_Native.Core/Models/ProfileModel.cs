using System;
using SQLite;

namespace ProfileBook_Native.Core.Models
{
    [Table("Profiles")]
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
