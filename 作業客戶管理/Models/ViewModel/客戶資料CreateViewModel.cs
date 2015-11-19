using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using 作業客戶管理.Models.Enum;

namespace 作業客戶管理.Models.ViewModel
{
    public class 客戶資料CreateViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }

        [StringLength(8, ErrorMessage = "欄位長度不得大於 8 個字元")]
        [Required]
        public string 統一編號 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 電話 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 傳真 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        //[RegularExpression(@"\d{4}-\d{6}")]
        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        public string Email { get; set; }

        public Enum客戶分類 客戶分類 { get; set; }
    }
}