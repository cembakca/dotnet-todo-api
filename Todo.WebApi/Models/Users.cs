using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Models
{
    public class Users : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli mail adresi giriniz.")]
        public string Email { get; set; }

        public virtual List<Todo> Todos { get; set; }
    }
}