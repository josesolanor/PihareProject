using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihare.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleValue { get; set; }
        public string RoleDescription { get; set; }

        public ICollection<Commisioner> Commisioner { get; set; }
    }
}
