using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EF_02_X1
{
    [Table("category")]
    public class category
    {
        [Key]
        public int CategoryID{set;get;}
        [StringLength(50)]
        [Required]
        public string Name{set;get;}
        [Column(TypeName = "ntext")]
        public string Description{set;get;}
        public List<Product> Products{set;get;}
        

        
    }
}