using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace 作業客戶管理.Models.Attribute
{
    public class 電話號碼Attribute : DataTypeAttribute
    {
        public 電話號碼Attribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(Object value)
        {
            bool result = Regex.IsMatch(value.ToString(), @"\d{4}-\d{6}");
            if (result == false)
            {
                this.ErrorMessage = "請輸入手機號碼格式09XX-XXXXXX";
            }
            return result;
        }
    }
}