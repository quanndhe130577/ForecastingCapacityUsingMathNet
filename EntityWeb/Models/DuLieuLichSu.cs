using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace Entity.Models
{
    [Table("DuLieuLichSu")]
    public class DuLieuLichSu
    {
        public int ID { get; set; }
        public double capacity { get; set; }
        public double ghi { get; set; }
        public double envtemp { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime time { get; set; }
    }
}