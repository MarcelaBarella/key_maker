using Kramer.Models;
using System;

namespace Kramer.Domain
{
    public class UserRequest
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Username { get; set; }
        public virtual int Status { get; set; } // Isso aqui deixa de ser um bool e vira uma string ligada a uma nova tabela
        public virtual ApplicationUser RequestedBy { get; set; }
        public virtual SaleType SaleType { get; set; }
        public virtual int? SaleTypeId { get; set; }
        public virtual DateTime RequestDate { get; set; }
    }
}