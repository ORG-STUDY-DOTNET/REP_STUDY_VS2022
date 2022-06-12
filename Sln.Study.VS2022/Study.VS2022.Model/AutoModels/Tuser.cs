using System;
using System.Collections.Generic;

namespace Study.VS2022.Model.AutoModels
{
    public partial class TUser
    {
        public Guid TU_GUID { get; set; }
        public string? TU_Account { get; set; }
        public string? TU_Password { get; set; }
        public string? TU_RealName { get; set; }
    }
}
