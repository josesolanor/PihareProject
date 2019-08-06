using System;
using System.Collections.Generic;

namespace WebPihare.Entities
{
    public partial class Departmentstate
    {
        public Departmentstate()
        {
            Department = new HashSet<Department>();
        }

        public int DepartmentStateId { get; set; }
        public string DepartmentStateValue { get; set; }
        public string DepartmentStateDescription { get; set; }

        public ICollection<Department> Department { get; set; }
    }
}
