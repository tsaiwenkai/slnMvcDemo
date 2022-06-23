//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjMvcDemo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tProduct
    {
        public int fId { get; set; }
        [DisplayName("產品名稱")]
        [Required(ErrorMessage = "此欄位為必填")]
        public string fName { get; set; }
     
        [DisplayName("產品成本")]
        public Nullable<decimal> fCost { get; set; }
        
        public Nullable<int> fQty { get; set; }
        [DisplayName("產品售價")]
        [Required(ErrorMessage = "此欄位為必填")]
        public Nullable<decimal> fPrice { get; set; }
    }
}
