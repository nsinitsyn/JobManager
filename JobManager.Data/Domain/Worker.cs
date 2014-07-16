﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Business;

namespace JobManager.Data.Domain
{
    public class Worker
    {
        public Guid Id { get; set; }
        public Job Job { get; set; }
        public JobWorkerBase Instance { get; set; }
        public OperationContext OperationContext { get; set; }
        public bool Completed { get; set; }
    }
}
