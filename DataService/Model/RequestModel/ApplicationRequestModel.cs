using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataService.Model.RequestModel
{
    public class ApplicationRequestModel
    {
        public int? ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int? Category { get; set; }
        public string Origin { get; set; }
        public string Type { get; set; }
        public int? Stage { get; set; }
        public bool? IsDone { get; set; }
        public bool? Active { get; set; }

    }
    public class ApplicationCreateModel
    {
        [Required]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Application Name Should be minimum 5 and a maximum is 200")]
        [DataType(DataType.Text)]
        public string ApplicationName { get; set; }

        public DateTime? UpdateTime { get; set; } = null;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } = null;
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public string Note { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Category should be between 1 and 3")]
        public int Category { get; set; }

        public string SourceCodeUrl { get; set; } = null;

        [Required]
        [Range(1,3,ErrorMessage ="Stage should be between 1 and 3")]
        public int? Stage { get; set; } = 1;

        public int? Efford { get; set; } = null;

        //[StringLength(3,MinimumLength =1,ErrorMessage ="Origin should be I, U or S2B")]
        [Required]
        [StringRange(AllowableValues = new[] { "I", "U", "S2B" }, ErrorMessage = "Type should be N, C or E")]
        [DataType(DataType.Text)]
        public string Origin { get; set; }

        [Required]
        [StringRange(AllowableValues = new[] { "N", "C","E" }, ErrorMessage = "Type should be N, C or E")]
        [DataType(DataType.Text)]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Technologies { get; set; }

        public int? Team { get; set; } = null;

        [Required]
        [Range(1, 4, ErrorMessage = "Priority should be between 1 and 4")]
        public int Priority { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Status should be between 1 and 5")]
        public int Status { get; set; }
    }

}
