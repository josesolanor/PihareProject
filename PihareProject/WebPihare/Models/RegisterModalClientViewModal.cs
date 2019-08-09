using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPihare.Entities;

namespace WebPihare.Models
{
    public class RegisterModalClientViewModal
    {
        public Client Client { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        public int DepartmentIdSelected { get; set; }
    }
}
