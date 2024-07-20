using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Product")]
    public class Product:CommonAstract
    {
        public Product() { 
            this.ProductImage = new HashSet<ProductImage>();        
            this.OrderDetail = new HashSet<OrderDetail>();
            this.Reviews = new HashSet<ReviewProduct>();
            this.Wishlists = new HashSet<Wishlist>();
            this.Colors = new HashSet<Colors>();
            this.Sizes = new HashSet<Sizes>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        [StringLength(250)]
        public string ProductCode { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [AllowHtml]
        [StringLength(50000)]    
        public string Detail { get; set; }
        [AllowHtml]
        public string Image { get; set; }
        public string Alias { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public bool IsHome { get; set; }
        public bool isActive { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsSale { get; set; }
        public int Quantity { get; set; }   
        
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(250)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ReviewProduct> Reviews { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<Colors> Colors { get; set; }
        public virtual ICollection<Sizes> Sizes { get; set; }
    }
}