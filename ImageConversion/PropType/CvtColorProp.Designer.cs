namespace ImageConversion
{
    partial class CvtColorProp
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
            this.cvtMonoBtn = new System.Windows.Forms.RadioButton();
            this.cvtHSVBtn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cvtMonoBtn
            // 
            this.cvtMonoBtn.AutoSize = true;
            this.cvtMonoBtn.Location = new System.Drawing.Point(28, 42);
            this.cvtMonoBtn.Name = "cvtMonoBtn";
            this.cvtMonoBtn.Size = new System.Drawing.Size(71, 16);
            this.cvtMonoBtn.TabIndex = 0;
            this.cvtMonoBtn.TabStop = true;
            this.cvtMonoBtn.Text = "cvtMono";
            this.cvtMonoBtn.UseVisualStyleBackColor = true;
            // 
            // cvtHSVBtn
            // 
            this.cvtHSVBtn.AutoSize = true;
            this.cvtHSVBtn.Location = new System.Drawing.Point(28, 81);
            this.cvtHSVBtn.Name = "cvtHSVBtn";
            this.cvtHSVBtn.Size = new System.Drawing.Size(63, 16);
            this.cvtHSVBtn.TabIndex = 1;
            this.cvtHSVBtn.TabStop = true;
            this.cvtHSVBtn.Text = "cvtHSV";
            this.cvtHSVBtn.UseVisualStyleBackColor = true;
            // 
            // CvtColorProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cvtHSVBtn);
            this.Controls.Add(this.cvtMonoBtn);
            this.Name = "CvtColorProp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RadioButton cvtMonoBtn;
        public System.Windows.Forms.RadioButton cvtHSVBtn;
    }
}
