namespace Online_Book_Shop
{
    using Online_Book_Shop.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        public string Address { get; set; }

        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        public bool? IsActive { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
