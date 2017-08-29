using rentSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.Entitys
{
    public class roomEntity : BaseEntity
    {
        public int id { get; set; }

        public string number { get; set; }

        public int floor { get; set; }

        public decimal price { get; set; }

        public string remark { get; set; }

        public string status { get; set; }
    }
}
