using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.Model.RequestModel
{
    public class ApplicationCharacteristicRequestModel
    {

    }
    public class ApplicationCharacteristicUpdateModel
    {

        [Required(ErrorMessage = "Application Id must be define")]
        public int ApplicationId { get; set; }

        public int? ActualEfford { get; set; } = null;

        public bool Active { get; set; } = true;

        //Familiarity with development process used
        [Required]
        [Range(1, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E1 { get; set; }

        //Application experience
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E2 { get; set; }

        //Object-oriented experience of team
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E3 { get; set; }

        //Lead analyst capability
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E4 { get; set; }

        //Motivation of the team
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E5 { get; set; }

        //Stability of requirements
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E6 { get; set; }

        //Part-time staff
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E7 { get; set; }

        //Difficult programming language
        [Required]
        [Range(0, 10, ErrorMessage = "Team experience level should be between 1 and 10")]
        public int E8 { get; set; }

        //Distributed system
        [Required]
        [Range(0, 10, ErrorMessage = "Value T1 should be between 1 and 10")]
        public int T1 { get; set; }

        //Response time/performance objectives
        [Required]
        [Range(0, 10, ErrorMessage = "Value T2 should be between 1 and 10")]
        public int T2 { get; set; }

        //End-user efficiency
        [Required]
        [Range(0, 10, ErrorMessage = "Value T3 should be between 1 and 10")]
        public int T3 { get; set; }

        //Internal processing complexity	
        [Required]
        [Range(0, 10, ErrorMessage = "Value T4 should be between 1 and 10")]
        public int T4 { get; set; }

        //Code reusability
        [Required]
        [Range(0, 10, ErrorMessage = "Value T5 should be between 1 and 10")]
        public int T5 { get; set; }

        //Easy to install
        [Required]
        [Range(0, 10, ErrorMessage = "Value T6 should be between 1 and 10")]
        public int T6 { get; set; }

        //Easy to use
        [Required]
        [Range(0, 10, ErrorMessage = "Value T7 should be between 1 and 10")]
        public int T7 { get; set; }

        //Portability to other platforms
        [Required]
        [Range(0, 10, ErrorMessage = "Value T8 should be between 1 and 10")]
        public int T8 { get; set; }

        //System maintenance
        [Required]
        [Range(0, 10, ErrorMessage = "Value T9 should be between 1 and 10")]
        public int T9 { get; set; }

        //Concurrent/parallel processing
        [Required]
        [Range(0, 10, ErrorMessage = "Value T10 should be between 1 and 10")]
        public int T10 { get; set; }

        //Security features
        [Required]
        [Range(0, 10, ErrorMessage = "Value T11 should be between 1 and 10")]
        public int T11 { get; set; }

        //Access for third parties
        [Required]
        [Range(0, 10, ErrorMessage = "Value T12 should be between 1 and 10")]
        public int T12 { get; set; }

        //End user training
        [Required]
        [Range(0, 10, ErrorMessage = "Value T13 should be between 1 and 10")]
        public int T13 { get; set; }
    }
}