namespace ImageConversion
{
    partial class Calibration
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
            this.btnStartCalib = new System.Windows.Forms.Button();
            this.btnApplyCalib = new System.Windows.Forms.Button();
            this.lblCalibGuide = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblCalibResult = new System.Windows.Forms.Label();
            this.txtRealLength = new System.Windows.Forms.TextBox();
            this.textPixelLength = new System.Windows.Forms.TextBox();
            this.textPPM = new System.Windows.Forms.TextBox();
            this.lbPPM = new System.Windows.Forms.Label();
            this.lbRealLength = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartCalib
            // 
            this.btnStartCalib.Location = new System.Drawing.Point(7, 62);
            this.btnStartCalib.Name = "btnStartCalib";
            this.btnStartCalib.Size = new System.Drawing.Size(75, 23);
            this.btnStartCalib.TabIndex = 0;
            this.btnStartCalib.Text = "시작(두점)";
            this.btnStartCalib.UseVisualStyleBackColor = true;
            // 
            // btnApplyCalib
            // 
            this.btnApplyCalib.Location = new System.Drawing.Point(9, 190);
            this.btnApplyCalib.Name = "btnApplyCalib";
            this.btnApplyCalib.Size = new System.Drawing.Size(66, 27);
            this.btnApplyCalib.TabIndex = 1;
            this.btnApplyCalib.Text = "적용";
            this.btnApplyCalib.UseVisualStyleBackColor = true;
            // 
            // lblCalibGuide
            // 
            this.lblCalibGuide.AutoSize = true;
            this.lblCalibGuide.Location = new System.Drawing.Point(66, 27);
            this.lblCalibGuide.Name = "lblCalibGuide";
            this.lblCalibGuide.Size = new System.Drawing.Size(63, 12);
            this.lblCalibGuide.TabIndex = 2;
            this.lblCalibGuide.Text = "calibration";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(13, 158);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(69, 12);
            this.lblUnit.TabIndex = 3;
            this.lblUnit.Text = "픽셀 거리 : ";
            // 
            // lblCalibResult
            // 
            this.lblCalibResult.AutoSize = true;
            this.lblCalibResult.Location = new System.Drawing.Point(54, 307);
            this.lblCalibResult.Name = "lblCalibResult";
            this.lblCalibResult.Size = new System.Drawing.Size(29, 12);
            this.lblCalibResult.TabIndex = 4;
            this.lblCalibResult.Text = "상태";
            // 
            // txtRealLength
            // 
            this.txtRealLength.Location = new System.Drawing.Point(117, 107);
            this.txtRealLength.Name = "txtRealLength";
            this.txtRealLength.Size = new System.Drawing.Size(100, 21);
            this.txtRealLength.TabIndex = 5;
            // 
            // textPixelLength
            // 
            this.textPixelLength.Location = new System.Drawing.Point(117, 151);
            this.textPixelLength.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textPixelLength.Name = "textPixelLength";
            this.textPixelLength.Size = new System.Drawing.Size(100, 21);
            this.textPixelLength.TabIndex = 6;
            // 
            // textPPM
            // 
            this.textPPM.Location = new System.Drawing.Point(117, 227);
            this.textPPM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textPPM.Name = "textPPM";
            this.textPPM.Size = new System.Drawing.Size(100, 21);
            this.textPPM.TabIndex = 7;
            // 
            // lbPPM
            // 
            this.lbPPM.AutoSize = true;
            this.lbPPM.Location = new System.Drawing.Point(12, 236);
            this.lbPPM.Name = "lbPPM";
            this.lbPPM.Size = new System.Drawing.Size(61, 12);
            this.lbPPM.TabIndex = 8;
            this.lbPPM.Text = "Pixel/mm";
            // 
            // lbRealLength
            // 
            this.lbRealLength.AutoSize = true;
            this.lbRealLength.Location = new System.Drawing.Point(7, 110);
            this.lbRealLength.Name = "lbRealLength";
            this.lbRealLength.Size = new System.Drawing.Size(101, 12);
            this.lbRealLength.TabIndex = 9;
            this.lbRealLength.Text = "실제 거리(mm) : ";
            // 
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbRealLength);
            this.Controls.Add(this.lbPPM);
            this.Controls.Add(this.textPPM);
            this.Controls.Add(this.textPixelLength);
            this.Controls.Add(this.txtRealLength);
            this.Controls.Add(this.lblCalibResult);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblCalibGuide);
            this.Controls.Add(this.btnApplyCalib);
            this.Controls.Add(this.btnStartCalib);
            this.Name = "Calibration";
            this.Text = "Calibration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartCalib;
        private System.Windows.Forms.Button btnApplyCalib;
        private System.Windows.Forms.Label lblCalibGuide;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblCalibResult;
        private System.Windows.Forms.TextBox txtRealLength;
        private System.Windows.Forms.TextBox textPixelLength;
        private System.Windows.Forms.TextBox textPPM;
        private System.Windows.Forms.Label lbPPM;
        private System.Windows.Forms.Label lbRealLength;
    }
}