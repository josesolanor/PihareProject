using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihareii.Models
{
    public class DepartmentClientViewModel
    {
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public int DepartmentCode { get; set; }       
        public int NumberFloor { get; set; }       
        public int NumberBedrooms { get; set; }      
        public decimal DeparmentPrice { get; set; }
        public int DepartmentTypeId { get; set; }
        public int DepartmentStateId { get; set; }
        public string DepartmentState { get; set; }
        public string DepartmentType { get; set; }
    }
}
