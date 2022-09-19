using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DomainEntity
    {
        public int ID { get; set; }
        public String ReceivedID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsRemove { get; set; }
        public bool Flag { get; set; }
        public string NormalPath { get; set; }
        public string? BigPath { get; set; }
        public string? TinyPath { get; set; }



    }
}
