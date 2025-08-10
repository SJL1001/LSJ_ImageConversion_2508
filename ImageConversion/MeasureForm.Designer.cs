namespace ImageConversion
{
    partial class MeasureForm
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
            this.btnStartMeasure = new System.Windows.Forms.Button();
            this.btnResetMeasure = new System.Windows.Forms.Button();
            this.listMeasurements = new System.Windows.Forms.ListBox();
            this.lblCurrentResult = new System.Windows.Forms.Label();
            this.comboUnit = new System.Windows.Forms.ComboBox();
            this.btnCopyResult = new System.Windows.Forms.Button();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartMeasure
            // 
            this.btnStartMeasure.Location = new System.Drawing.Point(27, 58);
            this.btnStartMeasure.Name = "btnStartMeasure";
            this.btnStartMeasure.Size = new System.Drawing.Size(75, 23);
            this.btnStartMeasure.TabIndex = 0;
            this.btnStartMeasure.Text = "측정시작";
            this.btnStartMeasure.UseVisualStyleBackColor = true;
            this.btnStartMeasure.Click += new System.EventHandler(this.btnStartMeasure_Click);
            // 
            // btnResetMeasure
            // 
            this.btnResetMeasure.Location = new System.Drawing.Point(255, 166);
            this.btnResetMeasure.Name = "btnResetMeasure";
            this.btnResetMeasure.Size = new System.Drawing.Size(75, 23);
            this.btnResetMeasure.TabIndex = 1;
            this.btnResetMeasure.Text = "초기화";
            this.btnResetMeasure.UseVisualStyleBackColor = true;
            this.btnResetMeasure.Click += new System.EventHandler(this.btnResetMeasure_Click);
            // 
            // listMeasurements
            // 
            this.listMeasurements.FormattingEnabled = true;
            this.listMeasurements.ItemHeight = 12;
            this.listMeasurements.Location = new System.Drawing.Point(174, 72);
            this.listMeasurements.Name = "listMeasurements";
            this.listMeasurements.Size = new System.Drawing.Size(156, 88);
            this.listMeasurements.TabIndex = 2;
            // 
            // lblCurrentResult
            // 
            this.lblCurrentResult.AutoSize = true;
            this.lblCurrentResult.Location = new System.Drawing.Point(25, 128);
            this.lblCurrentResult.Name = "lblCurrentResult";
            this.lblCurrentResult.Size = new System.Drawing.Size(53, 12);
            this.lblCurrentResult.TabIndex = 3;
            this.lblCurrentResult.Text = "측정결과";
            // 
            // comboUnit
            // 
            this.comboUnit.FormattingEnabled = true;
            this.comboUnit.Location = new System.Drawing.Point(174, 195);
            this.comboUnit.Name = "comboUnit";
            this.comboUnit.Size = new System.Drawing.Size(156, 20);
            this.comboUnit.TabIndex = 4;
            this.comboUnit.SelectedIndexChanged += new System.EventHandler(this.comboUnit_SelectedIndexChanged);
            // 
            // btnCopyResult
            // 
            this.btnCopyResult.Location = new System.Drawing.Point(27, 227);
            this.btnCopyResult.Name = "btnCopyResult";
            this.btnCopyResult.Size = new System.Drawing.Size(75, 23);
            this.btnCopyResult.TabIndex = 5;
            this.btnCopyResult.Text = "결과 복사";
            this.btnCopyResult.UseVisualStyleBackColor = true;
            this.btnCopyResult.Click += new System.EventHandler(this.btnCopyResult_Click);
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Location = new System.Drawing.Point(27, 272);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(105, 23);
            this.btnSaveResult.TabIndex = 6;
            this.btnSaveResult.Text = "결과 파일저장";
            this.btnSaveResult.UseVisualStyleBackColor = true;
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click_1);
            // 
            // MeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 384);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.btnCopyResult);
            this.Controls.Add(this.comboUnit);
            this.Controls.Add(this.lblCurrentResult);
            this.Controls.Add(this.listMeasurements);
            this.Controls.Add(this.btnResetMeasure);
            this.Controls.Add(this.btnStartMeasure);
            this.Name = "MeasureForm";
            this.Text = "MeasureForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartMeasure;
        private System.Windows.Forms.Button btnResetMeasure;
        private System.Windows.Forms.ListBox listMeasurements;
        private System.Windows.Forms.Label lblCurrentResult;
        private System.Windows.Forms.ComboBox comboUnit;
        private System.Windows.Forms.Button btnCopyResult;
        private System.Windows.Forms.Button btnSaveResult;
    }
}