using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPihare.Entities;

namespace WebPihare.Models
{
    public class RegisterViewModel
    {
        public Client Client { get; set; }
        public Department Department { get; set; }
        public Commisioner Commisioner { get; set; }
        public Visitregistration VisitRegistration { get; set; }

    }
}
