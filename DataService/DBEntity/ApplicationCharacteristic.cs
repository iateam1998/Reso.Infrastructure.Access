using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class ApplicationCharacteristic
    {
        public int ApplicationCharacteristicId { get; set; }
        public int ApplicationId { get; set; }
        public int? ActualEfford { get; set; }
        public bool Active { get; set; }

        public virtual Application Application { get; set; }
        public virtual Ecf Ecf { get; set; }
        public virtual Tcf Tcf { get; set; }
        public virtual Uaw Uaw { get; set; }
        public virtual Uucw Uucw { get; set; }
    }
}
