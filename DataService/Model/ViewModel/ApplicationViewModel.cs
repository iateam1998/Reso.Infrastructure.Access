﻿using DataService.DBEntity;
using System;
using System.Collections.Generic;

namespace DataService.Model.ViewModel
{
    public class ApplicationViewModel : BaseViewModel<Application>
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Category { get; set; }
        public string SourceCodeUrl { get; set; }
        public int? Stage { get; set; }
        public int? Efford { get; set; }
        public string Origin { get; set; }
        public string Type { get; set; }
        public string Technologies { get; set; }
        public int? Team { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public bool? IsDone { get; set; }
        public bool Active { get; set; }

        public virtual ApplicationCharacteristicViewModel ApplicationCharacteristic { get; set; }
        public virtual ICollection<EntityViewModel> Entity { get; set; }
        public virtual ICollection<UseCaseViewModel> UseCase { get; set; }

    }
}