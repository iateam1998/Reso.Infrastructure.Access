using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class Uaw
    {
        public int Uawid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int Simple { get; set; }
        public int Average { get; set; }
        public int Complex { get; set; }
        public bool Active { get; set; }

        public virtual ApplicationCharacteristic ApplicationCharacteristic { get; set; }
    }
}
