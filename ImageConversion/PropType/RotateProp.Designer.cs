namespace ImageConversion
{
    partial class RotateProp
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            this.radioClockwise = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownAngle
            // 
            this.numericUpDownAngle.Location = new System.Drawing.Point(64, 80);
            this.numericUpDownAngle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Size = new System.Drawing.Size(160, 21);
            this.numericUpDownAngle.TabIndex = 0;
            // 
            // radioClockwise
            // 
            this.radioClockwise.AutoSize = true;
            this.radioClockwise.Location = new System.Drawing.Point(64, 119);
            this.radioClockwise.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioClockwise.Name = "radioClockwise";
            this.radioClockwise.Size = new System.Drawing.Size(84, 16);
            this.radioClockwise.TabIndex = 1;
            this.radioClockwise.Text = "반시계방향";
            this.radioClockwise.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "각도";
            // 
            // RotateProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioClockwise);
            this.Controls.Add(this.numericUpDownAngle);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RotateProp";
            this.Size = new System.Drawing.Size(427, 309);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownAngle;
        private System.Windows.Forms.CheckBox radioClockwise;
        private System.Windows.Forms.Label label1;
    }
}
