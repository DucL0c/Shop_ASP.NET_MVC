﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Adv")]
    public class Adv:CommonAstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(550)]
        public string Description { get; set; }
        [StringLength(550)]
        public string Image { get; set; }
        [StringLength(550)]
        public string Link { get; set; }
        public int Type { get; set; }
    }
}