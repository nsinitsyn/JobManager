using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Database.Entities
{
    [Table("JM_TRIGGERS")]
    public class TriggerDb : BaseEntity
    {
        public virtual JobDb Job { get; set; }
        public string Cron { get; set; }
    }
}
