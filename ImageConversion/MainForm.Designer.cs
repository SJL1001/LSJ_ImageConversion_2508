namespace ImageConversion
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textImagePath = new System.Windows.Forms.ToolStripTextBox();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measureTool = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationTool = new System.Windows.Forms.ToolStripMenuItem();
            this.blobTool = new System.Windows.Forms.ToolStripMenuItem();
            this.mainContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainMenuStrip.SuspendLayout();
            this.mainContainer.TopToolStripPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.textImagePath,
            this.toolToolStripMenuItem});
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(800, 27);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // textImagePath
            // 
            this.textImagePath.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.textImagePath.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.textImagePath.Name = "textImagePath";
            this.textImagePath.ReadOnly = true;
            this.textImagePath.Size = new System.Drawing.Size(400, 23);
            this.textImagePath.Text = "파일 경로 : ";
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.measureTool,
            this.calibrationTool,
            this.blobTool});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(42, 23);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // measureTool
            // 
            this.measureTool.Name = "measureTool";
            this.measureTool.Size = new System.Drawing.Size(132, 22);
            this.measureTool.Text = "Measure";
            this.measureTool.Click += new System.EventHandler(this.measureTool_Click);
            // 
            // calibrationTool
            // 
            this.calibrationTool.Name = "calibrationTool";
            this.calibrationTool.Size = new System.Drawing.Size(132, 22);
            this.calibrationTool.Text = "Calibration";
            this.calibrationTool.Click += new System.EventHandler(this.calibrationTool_Click);
            // 
            // blobTool
            // 
            this.blobTool.Name = "blobTool";
            this.blobTool.Size = new System.Drawing.Size(132, 22);
            this.blobTool.Text = "Blob";
            this.blobTool.Click += new System.EventHandler(this.blobTool_Click);
            // 
            // mainContainer
            // 
            // 
            // mainContainer.ContentPanel
            // 
            this.mainContainer.ContentPanel.Size = new System.Drawing.Size(800, 423);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(800, 450);
            this.mainContainer.TabIndex = 2;
            this.mainContainer.Text = "toolStripContainer1";
            // 
            // mainContainer.TopToolStripPanel
            // 
            this.mainContainer.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainContainer);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Mainform";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainContainer.TopToolStripPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer mainContainer;
        private System.Windows.Forms.ToolStripTextBox textImagePath;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measureTool;
        private System.Windows.Forms.ToolStripMenuItem calibrationTool;
        private System.Windows.Forms.ToolStripMenuItem blobTool;
    }
}