using System;
using SQLite;

namespace ProfileBook_Native.Core.Models
{
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public byte[] ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
