namespace ImageConversion
{
    partial class BlobForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvBlobResult = new System.Windows.Forms.DataGridView();
            this.lblStat = new System.Windows.Forms.Label();
            this.numMinArea = new System.Windows.Forms.NumericUpDown();
            this.numMaxArea = new System.Windows.Forms.NumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.btnSendToMain = new System.Windows.Forms.Button();
            this.imageViewCtrl1 = new ImageConversion.ImageViewCtrl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuLabelEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlobResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxArea)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBlobResult
            // 
            this.dgvBlobResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBlobResult.Location = new System.Drawing.Point(416, 223);
            this.dgvBlobResult.Name = "dgvBlobResult";
            this.dgvBlobResult.RowTemplate.Height = 23;
            this.dgvBlobResult.Size = new System.Drawing.Size(240, 150);
            this.dgvBlobResult.TabIndex = 0;
            this.dgvBlobResult.SelectionChanged += new System.EventHandler(this.dgvBlobResult_SelectionChanged);
            this.dgvBlobResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBlobResult_KeyDown);
            this.dgvBlobResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvBlobResult_MouseDown);
            // 
            // lblStat
            // 
            this.lblStat.AutoSize = true;
            this.lblStat.Location = new System.Drawing.Point(414, 138);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(38, 12);
            this.lblStat.TabIndex = 1;
            this.lblStat.Text = "label1";
            // 
            // numMinArea
            // 
            this.numMinArea.Location = new System.Drawing.Point(536, 129);
            this.numMinArea.Name = "numMinArea";
            this.numMinArea.Size = new System.Drawing.Size(120, 21);
            this.numMinArea.TabIndex = 2;
            // 
            // numMaxArea
            // 
            this.numMaxArea.Location = new System.Drawing.Point(536, 165);
            this.numMaxArea.Name = "numMaxArea";
            this.numMaxArea.Size = new System.Drawing.Size(120, 21);
            this.numMaxArea.TabIndex = 3;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(414, 174);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(38, 12);
            this.lblMin.TabIndex = 4;
            this.lblMin.Text = "label1";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(414, 208);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(38, 12);
            this.lblMax.TabIndex = 5;
            this.lblMax.Text = "label2";
            // 
            // btnSendToMain
            // 
            this.btnSendToMain.Location = new System.Drawing.Point(416, 48);
            this.btnSendToMain.Name = "btnSendToMain";
            this.btnSendToMain.Size = new System.Drawing.Size(75, 23);
            this.btnSendToMain.TabIndex = 7;
            this.btnSendToMain.Text = "보내기";
            this.btnSendToMain.UseVisualStyleBackColor = true;
            this.btnSendToMain.Click += new System.EventHandler(this.btnSendToMain_Click);
            // 
            // imageViewCtrl1
            // 
            this.imageViewCtrl1.Location = new System.Drawing.Point(12, 23);
            this.imageViewCtrl1.Name = "imageViewCtrl1";
            this.imageViewCtrl1.Size = new System.Drawing.Size(377, 415);
            this.imageViewCtrl1.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLabelEdit,
            this.menuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // menuLabelEdit
            // 
            this.menuLabelEdit.Name = "menuLabelEdit";
            this.menuLabelEdit.Size = new System.Drawing.Size(180, 22);
            this.menuLabelEdit.Text = "라벨 수정";
            this.menuLabelEdit.Click += new System.EventHandler(this.menuLabelEdit_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(180, 22);
            this.menuDelete.Text = "삭제";
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // BlobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSendToMain);
            this.Controls.Add(this.imageViewCtrl1);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.numMaxArea);
            this.Controls.Add(this.numMinArea);
            this.Controls.Add(this.lblStat);
            this.Controls.Add(this.dgvBlobResult);
            this.Name = "BlobForm";
            this.Text = "BlobForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlobResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxArea)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBlobResult;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.NumericUpDown numMinArea;
        private System.Windows.Forms.NumericUpDown numMaxArea;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private ImageViewCtrl imageViewCtrl1;
        private System.Windows.Forms.Button btnSendToMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuLabelEdit;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
    }
}