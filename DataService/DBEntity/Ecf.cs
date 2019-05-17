using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class Ecf
    {
        public int Ecfid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int E1 { get; set; }
        public int E2 { get; set; }
        public int E3 { get; set; }
        public int E4 { get; set; }
        public int E5 { get; set; }
        public int E6 { get; set; }
        public int E7 { get; set; }
        public int E8 { get; set; }
        public bool Active { get; set; }

        public virtual ApplicationCharacteristic ApplicationCharacteristic { get; set; }
    }
}
