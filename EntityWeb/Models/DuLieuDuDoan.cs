using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Entity.Models
{
    [Table("DuLieuDuDoan")]
    public class DuLieuDuDoan
    {
        public int ID { get; set; }
        public double capacity_db { get; set; }
        public double ghi_db { get; set; }
        public double envtemp_db { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime time_db { get; set; }
    }
    
}