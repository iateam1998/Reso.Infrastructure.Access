using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.Model.RequestModel
{
    public class ApplicationCharacteristicRequestModel
    {

    }
    public class ApplicationCharacteristicCreateModel
    {
        
        [Required(ErrorMessage = "Application Id must be define")]
        public int ApplicationId { get; set; }

        public int? ActualEfford { get; set; } = null;

        public bool Active { get; set; } = true;

        [Required]
        public virtual EcfCreateModel Ecf { get; set; }

        [Required]
        public virtual TcfCreateModel Tcf { get; set; }

        [Required]
        public virtual UawCreateModel Uaw { get; set; }

        [Required]
        public virtual UucwCreateModel Uucw { get; set; }
    }

    /// <summary>
    /// Environmental Complexity Factor - ECF
    /// Để tính toán ECF, mỗi yếu tố môi trường được gán một giá trị dựa trên cấp độ kinh nghiệm của nhóm.
    /// </summary>
    public class EcfCreateModel
    {
        [Required]
        public int ApplicationCharacteristicId { get; set; }

        //Familiarity with development process used
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E1 { get; set; }

        //Application experience
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E2 { get; set; }

        //Object-oriented experience of team
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E3 { get; set; }

        //Lead analyst capability
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E4 { get; set; }

        //Motivation of the team
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E5 { get; set; }

        //Stability of requirements
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E6 { get; set; }

        //Part-time staff
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E7 { get; set; }

        //Difficult programming language
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E8 { get; set; }

        public bool Active { get; set; } = true;
    }
    /// <summary>
    /// TCF - Technical Complexity Factor. 
    /// Để tính toán TCF, mỗi yếu tố kỹ thuật được gán một giá trị dựa trên mức độ thiết yếu của khía cạnh kỹ thuật đối với hệ thống đang được phát triển.
    /// </summary>
    public class TcfCreateModel
    {
        [Required]
        public int ApplicationCharacteristicId { get; set; }

        //Distributed system
        [Required]
        [Range(1, 10, ErrorMessage = "Value T1 should be between 1 and 10")]
        public int T1 { get; set; }

        //Response time/performance objectives
        [Required]
        [Range(1, 10, ErrorMessage = "Value T2 should be between 1 and 10")]
        public int T2 { get; set; }

        //End-user efficiency
        [Required]
        [Range(1, 10, ErrorMessage = "Value T3 should be between 1 and 10")]
        public int T3 { get; set; }

        //Internal processing complexity	
        [Required]
        [Range(1, 10, ErrorMessage = "Value T4 should be between 1 and 10")]
        public int T4 { get; set; }

        //Code reusability
        [Required]
        [Range(1, 10, ErrorMessage = "Value T5 should be between 1 and 10")]
        public int T5 { get; set; }

        //Easy to install
        [Required]
        [Range(1, 10, ErrorMessage = "Value T6 should be between 1 and 10")]
        public int T6 { get; set; }

        //Easy to use
        [Required]
        [Range(1, 10, ErrorMessage = "Value T7 should be between 1 and 10")]
        public int T7 { get; set; }

        //Portability to other platforms
        [Required]
        [Range(1, 10, ErrorMessage = "Value T8 should be between 1 and 10")]
        public int T8 { get; set; }

        //System maintenance
        [Required]
        [Range(1, 10, ErrorMessage = "Value T9 should be between 1 and 10")]
        public int T9 { get; set; }

        //Concurrent/parallel processing
        [Required]
        [Range(1, 10, ErrorMessage = "Value T10 should be between 1 and 10")]
        public int T10 { get; set; }

        //Security features
        [Required]
        [Range(1, 10, ErrorMessage = "Value T11 should be between 1 and 10")]
        public int T11 { get; set; }

        //Access for third parties
        [Required]
        [Range(1, 10, ErrorMessage = "Value T12 should be between 1 and 10")]
        public int T12 { get; set; }

        //End user training
        [Required]
        [Range(1, 10, ErrorMessage = "Value T13 should be between 1 and 10")]
        public int T13 { get; set; }

        public bool Active { get; set; }
    }

    /// <summary>
    /// UAW - Unadjusted Actor Weight
    /// UAW được tính toán dựa trên số lượng và độ phức tạp của các tác nhân cho hệ thống.
    /// </summary>
    public class UawCreateModel
    {
        [Required]
        public int ApplicationCharacteristicId { get; set; }

        [Required]
        public int Simple { get; set; }

        [Required]
        public int Average { get; set; }

        [Required]
        public int Complex { get; set; }

        public bool Active { get; set; } = true;
    }

    /// <summary>
    /// Unadjusted Use Case Weight - UUCW
    /// UUCW được tính toán dựa trên số lượng và độ phức tạp của các trường hợp sử dụng cho hệ thống.
    /// </summary>
    public class UucwCreateModel
    {
        [Required]
        public int ApplicationCharacteristicId { get; set; }

        [Required]
        public int Simple { get; set; }

        [Required]
        public int Average { get; set; }

        [Required]
        public int Complex { get; set; }

        public bool Active { get; set; } = true;
    }
}