using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class Tcf
    {
        public int Tcfid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int T1 { get; set; }
        public int T2 { get; set; }
        public int T3 { get; set; }
        public int T4 { get; set; }
        public int T5 { get; set; }
        public int T6 { get; set; }
        public int T7 { get; set; }
        public int T8 { get; set; }
        public int T9 { get; set; }
        public int T10 { get; set; }
        public int T11 { get; set; }
        public int T12 { get; set; }
        public int T13 { get; set; }
        public bool Active { get; set; }

        public virtual ApplicationCharacteristic ApplicationCharacteristic { get; set; }
    }
}
