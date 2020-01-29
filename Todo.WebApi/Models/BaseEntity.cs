using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } // ? nullable
    }
}