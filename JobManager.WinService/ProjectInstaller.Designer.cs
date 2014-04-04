namespace JobManager.WinService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.JobManagerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.JobManagerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // JobManagerProcessInstaller
            // 
            this.JobManagerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.JobManagerProcessInstaller.Password = null;
            this.JobManagerProcessInstaller.Username = null;
            // 
            // JobManagerInstaller
            // 
            this.JobManagerInstaller.ServiceName = "JobManager";
            this.JobManagerInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.JobManagerProcessInstaller,
            this.JobManagerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller JobManagerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller JobManagerInstaller;
    }
}