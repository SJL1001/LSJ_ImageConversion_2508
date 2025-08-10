namespace ImageConversion
{
    partial class PyramidProp
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
            this.pyramidUpButton = new System.Windows.Forms.RadioButton();
            this.pyramidDownButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // pyramidUpButton
            // 
            this.pyramidUpButton.AutoSize = true;
            this.pyramidUpButton.Location = new System.Drawing.Point(35, 54);
            this.pyramidUpButton.Name = "pyramidUpButton";
            this.pyramidUpButton.Size = new System.Drawing.Size(39, 16);
            this.pyramidUpButton.TabIndex = 0;
            this.pyramidUpButton.TabStop = true;
            this.pyramidUpButton.Text = "UP";
            this.pyramidUpButton.UseVisualStyleBackColor = true;
            // 
            // pyramidDownButton
            // 
            this.pyramidDownButton.AutoSize = true;
            this.pyramidDownButton.Location = new System.Drawing.Point(35, 93);
            this.pyramidDownButton.Name = "pyramidDownButton";
            this.pyramidDownButton.Size = new System.Drawing.Size(59, 16);
            this.pyramidDownButton.TabIndex = 1;
            this.pyramidDownButton.TabStop = true;
            this.pyramidDownButton.Text = "DOWN";
            this.pyramidDownButton.UseVisualStyleBackColor = true;
            // 
            // PyramidProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pyramidDownButton);
            this.Controls.Add(this.pyramidUpButton);
            this.Name = "PyramidProp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RadioButton pyramidUpButton;
        public System.Windows.Forms.RadioButton pyramidDownButton;
    }
}
