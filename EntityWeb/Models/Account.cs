
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    [Table("Account")]
    [Serializable]
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string IdentifyCode { get; set; }
        public string SaltPassword { get; set; }
        public string Role { get; set; }
    }

}