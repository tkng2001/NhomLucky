using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NhomLucky.Models
{
    public class Book
    {
        public Object _id { get; set; }
        [Display(Name = "Mã sách")]
        public string code { get; set; }
        [Display(Name ="Tên sách")]
        public string name { get; set; }
        [Display(Name = "Hình ảnh")]
        public string image { get; set; }
        [Display(Name = "Đơn giá")]
        public string dongia { get; set; }
    }
}