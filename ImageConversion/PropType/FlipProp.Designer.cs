namespace ImageConversion
{
    partial class FlipProp
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
            this.flipXbutton = new System.Windows.Forms.RadioButton();
            this.flipYbutton = new System.Windows.Forms.RadioButton();
            this.flipXYbutton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // flipXbutton
            // 
            this.flipXbutton.AutoSize = true;
            this.flipXbutton.Location = new System.Drawing.Point(21, 42);
            this.flipXbutton.Name = "flipXbutton";
            this.flipXbutton.Size = new System.Drawing.Size(47, 16);
            this.flipXbutton.TabIndex = 0;
            this.flipXbutton.TabStop = true;
            this.flipXbutton.Text = "수직";
            this.flipXbutton.UseVisualStyleBackColor = true;
            // 
            // flipYbutton
            // 
            this.flipYbutton.AutoSize = true;
            this.flipYbutton.Location = new System.Drawing.Point(21, 80);
            this.flipYbutton.Name = "flipYbutton";
            this.flipYbutton.Size = new System.Drawing.Size(47, 16);
            this.flipYbutton.TabIndex = 1;
            this.flipYbutton.TabStop = true;
            this.flipYbutton.Text = "수평";
            this.flipYbutton.UseVisualStyleBackColor = true;
            // 
            // flipXYbutton
            // 
            this.flipXYbutton.AutoSize = true;
            this.flipXYbutton.Location = new System.Drawing.Point(21, 117);
            this.flipXYbutton.Name = "flipXYbutton";
            this.flipXYbutton.Size = new System.Drawing.Size(77, 16);
            this.flipXYbutton.TabIndex = 2;
            this.flipXYbutton.TabStop = true;
            this.flipXYbutton.Text = "수직+수평";
            this.flipXYbutton.UseVisualStyleBackColor = true;
            // 
            // FlipProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flipXYbutton);
            this.Controls.Add(this.flipYbutton);
            this.Controls.Add(this.flipXbutton);
            this.Name = "FlipProp";
            this.Size = new System.Drawing.Size(164, 162);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RadioButton flipXbutton;
        public System.Windows.Forms.RadioButton flipYbutton;
        public System.Windows.Forms.RadioButton flipXYbutton;
    }
}
