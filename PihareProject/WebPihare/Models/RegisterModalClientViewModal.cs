using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int DepartmentIdCommentSelected { get; set; }
        [DisplayName("Comentario")]
        public string DepartmentComment { get; set; }
    }
}
