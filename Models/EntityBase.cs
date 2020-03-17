using System;
using System.ComponentModel.DataAnnotations;


namespace myFirtsAzureWebApp.Models
{
    public class EntityBase : IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
