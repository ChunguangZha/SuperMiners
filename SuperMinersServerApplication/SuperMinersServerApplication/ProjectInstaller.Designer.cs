namespace SuperMinersServerApplication
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
            this.superMinersService1 = new SuperMinersServerApplication.SuperMinersService();
            // 
            // superMinersService1
            // 
            this.superMinersService1.ExitCode = 0;
            this.superMinersService1.ServiceName = "SuperMinersService";

        }

        #endregion

        private SuperMinersService superMinersService1;
    }
}