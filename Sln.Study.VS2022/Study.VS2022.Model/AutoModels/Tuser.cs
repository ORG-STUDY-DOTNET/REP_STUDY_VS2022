using System;
using System.Collections.Generic;

namespace Study.VS2022.Model.AutoModels
{
    public partial class Tuser
    {
        public Guid TuGuid { get; set; }
        public string? TuAccount { get; set; }
        public string? TuPassword { get; set; }
        public string? TuRealName { get; set; }
    }
}
