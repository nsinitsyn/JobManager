using JobManager.Data;

namespace JobManager.Business.Events
{
    public delegate void JobManagerEventHandler(object sender, JobManagerEventArgs e);
    public delegate object JobManagerEventSyncHandler(object sender, JobManagerEventArgs e);
}
