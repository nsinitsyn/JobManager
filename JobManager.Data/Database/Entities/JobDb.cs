using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Database.Entities
{
    [Table("JM_JOBS")]
    public class JobDb : BaseEntity
    {
        public string ClassName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Data { get; set; }

        [InverseProperty("Job")]
        public virtual IList<WorkerDb> Workers { get; set; }

        [InverseProperty("Job")]
        public virtual IList<TriggerDb> Triggers { get; set; }
    }
}
