using System;

namespace JobManager.Transfer.Data.DTO
{
    public class WorkerDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
    }
}
