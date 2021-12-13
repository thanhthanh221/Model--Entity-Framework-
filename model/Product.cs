using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_02_X1
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID{set;get;}
        [StringLength(50)]
        [Required]
        [Column(TypeName = "ntext")]
        public string Name {set;get;}
        [StringLength(50)]
        [Column(TypeName = "money")]
        public decimal Price{set;get;}
        [Required]
        [ForeignKey("CateId")]
        [InverseProperty("Products")]
        public category category{set;get;}
        [ForeignKey("CateId2")]
        public category category2{set;get;}
        
        public override string ToString()
        {
            string product =$"{this.ProductID,5}{this.Name,15}{this.Price,15}{this.category.CategoryID,15}";
            return product.ToString();
        }       
    }
}