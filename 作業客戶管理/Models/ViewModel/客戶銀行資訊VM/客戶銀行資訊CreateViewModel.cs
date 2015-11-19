using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 作業客戶管理.Models.ViewModel.客戶銀行資訊VM
{
    public class 客戶銀行資訊CreateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }

        [Required]
        public int 銀行代碼 { get; set; }
        public Nullable<int> 分行代碼 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }

        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        [Required]
        public string 帳戶號碼 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}