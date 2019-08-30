using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPihare.Entities
{
    public partial class Commisioner
    {
        public Commisioner()
        {
            Client = new HashSet<Client>();
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int CommisionerId { get; set; }
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Apellido P.")]
        public string LastName { get; set; }
        [DisplayName("Apellido M.")]
        public string SecondLastName { get; set; }
        [DisplayName("Nickname")]
        public string Nic { get; set; }
        [DisplayName("No de Contrato")]
        public int ContractNumber { get; set; }
        [DisplayName("Correo Electrónico")]
        public string Email { get; set; }
        [DisplayName("Teléfono")]
        public int Telefono { get; set; }
        [DisplayName("Contraseña")]
        public string CommisionerPassword { get; set; }
        [DisplayName("Rol")]
        public int RoleId { get; set; }
        [DisplayName("Rol")]
        public Role Role { get; set; }

        [DisplayName("Nombre Comisionista")]
        public string FullName{ get {

                string FullNameCommisioner = $"{FirstName} {LastName} {SecondLastName}";
                return FullNameCommisioner;
            } }
        public ICollection<Client> Client { get; set; }
        public ICollection<Visitregistration> Visitregistration { get; set; }
        public ICollection<Chat> Chat { get; set; }
    }
}
