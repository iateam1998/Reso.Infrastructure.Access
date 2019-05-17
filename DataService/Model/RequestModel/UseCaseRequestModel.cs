using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.Model.RequestModel
{
    public class UseCaseRequestModel
    {
    }
    public class UseCaseCreateModel
    {
        [Required]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "UseCase Name Should be minimum 5 and a maximum is 200")]
        [DataType(DataType.Text)]
        public string UseCaseName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "UseCase Name Should be minimum 5 and a maximum is 200")]
        [DataType(DataType.Text)]
        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; } = null;

        [Required]
        public DateTime Deadline { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [DataType(DataType.Text)]
        public string Goal { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Status should be between 1 and 5")]
        public int Status { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Priority should be between 1 and 4")]
        public int Priority { get; set; }

        [Required]
        public int Complexity { get; set; }

        public bool IsDone { get; set; } = false;

        [DataType(DataType.Text)]
        public string Stakeholder { get; set; }

        [DataType(DataType.Text)]
        public string Note { get; set; }

        public bool Active { get; set; } = true;

        [Required]
        public virtual List<int> UseCaseActor { get; set; }

        public virtual List<int> UseCaseEntity { get; set; }

        public virtual List<string> UseCaseStep { get; set; }
    }
}
