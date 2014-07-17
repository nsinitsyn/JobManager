using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Database.Entities
{
    [Table("JM_WORKERS")]
    public class WorkerDb : BaseEntity
    {
        public virtual JobDb Job { get; set; }
        public bool Completed { get; set; }
    }
}
