using DataService.DBEntity;
using System;

namespace DataService.Model.ViewModel
{
    public class TcfViewModel : BaseViewModel<Tcf>
    {
        public int Tcfid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int T1 { get; set; } = 0;
        public int T2 { get; set; } = 0;
        public int T3 { get; set; } = 0;
        public int T4 { get; set; } = 0;
        public int T5 { get; set; } = 0;
        public int T6 { get; set; } = 0;
        public int T7 { get; set; } = 0;
        public int T8 { get; set; } = 0;
        public int T9 { get; set; } = 0;
        public int T10 { get; set; } = 0;
        public int T11 { get; set; } = 0;
        public int T12 { get; set; } = 0;
        public int T13 { get; set; } = 0;
        public bool Active { get; set; }

        private double T1_Weight = 2.0;
        private double T2_Weight = 1.0;
        private double T3_Weight = 1.0;
        private double T4_Weight = 1.0;
        private double T5_Weight = 1.0;
        private double T6_Weight = 0.5;
        private double T7_Weight = 0.5;
        private double T8_Weight = 2.0;
        private double T9_Weight = 1.0;
        private double T10_Weight = 1.0;
        private double T11_Weight = 1.0;
        private double T12_Weight = 1.0;
        private double T13_Weight = 1.0;

        public double TotalTF
        {
            get
            {
                return Math.Round( 
                    (this.T1 * T1_Weight)
                    + (this.T2 * T2_Weight)
                    + (this.T3 * T3_Weight)
                    + (this.T4 * T4_Weight)
                    + (this.T5 * T5_Weight)
                    + (this.T6 * T6_Weight)
                    + (this.T7 * T7_Weight)
                    + (this.T8 * T8_Weight)
                    + (this.T9 * T9_Weight)
                    + (this.T10 * T10_Weight)
                    + (this.T11 * T11_Weight)
                    + (this.T12 * T12_Weight)
                    + (this.T13 * T13_Weight)
                    ,2);
            }
        }
        public double TotalTCF
        {
            get
            {
                return Math.Round(0.6 + (this.TotalTF / 100),2);
            }
        }

        // public virtual ApplicationCharacteristicViewModel ApplicationCharacteristic { get; set; }
    }
}