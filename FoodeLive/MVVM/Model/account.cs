//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodeLive.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class account
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string type { get; set; }
        public string provider { get; set; }
        public string provider_account_id { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
        public Nullable<int> expires_at { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string id_token { get; set; }
        public string session_state { get; set; }
    }
}
