using rentSys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.Entitys
{
    public class userEntity : BaseEntity
    {
        public int id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

    }
}
