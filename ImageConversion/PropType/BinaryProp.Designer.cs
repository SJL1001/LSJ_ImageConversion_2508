namespace ImageConversion
{
    partial class BinaryProp
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
            this.binaryText = new System.Windows.Forms.TextBox();
            this.checkBoxBinaryInvert = new System.Windows.Forms.CheckBox();
            this.rangeSliderBinary = new ImageConversion.RangeSliderCtrl();
            this.SuspendLayout();
            // 
            // binaryText
            // 
            this.binaryText.Location = new System.Drawing.Point(15, 214);
            this.binaryText.Multiline = true;
            this.binaryText.Name = "binaryText";
            this.binaryText.ReadOnly = true;
            this.binaryText.Size = new System.Drawing.Size(236, 110);
            this.binaryText.TabIndex = 1;
            this.binaryText.Text = "\r\n\r\nROI자르기는 이진화 한 후에 실행 하세요.\r\n\r\n\r\n기본 binary는 범위 안의 값이 흰색, \r\n뒤집으면 범위 밖이 흰색.";
            // 
            // checkBoxBinaryInvert
            // 
            this.checkBoxBinaryInvert.AutoSize = true;
            this.checkBoxBinaryInvert.Location = new System.Drawing.Point(15, 172);
            this.checkBoxBinaryInvert.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxBinaryInvert.Name = "checkBoxBinaryInvert";
            this.checkBoxBinaryInvert.Size = new System.Drawing.Size(60, 16);
            this.checkBoxBinaryInvert.TabIndex = 2;
            this.checkBoxBinaryInvert.Text = "뒤집기";
            this.checkBoxBinaryInvert.UseVisualStyleBackColor = true;
            // 
            // rangeSliderBinary
            // 
            this.rangeSliderBinary.Location = new System.Drawing.Point(15, 95);
            this.rangeSliderBinary.Maximum = 255;
            this.rangeSliderBinary.Minimum = 0;
            this.rangeSliderBinary.Name = "rangeSliderBinary";
            this.rangeSliderBinary.Size = new System.Drawing.Size(208, 48);
            this.rangeSliderBinary.SliderMax = 200;
            this.rangeSliderBinary.SliderMin = 30;
            this.rangeSliderBinary.TabIndex = 0;
            this.rangeSliderBinary.ValueChanged += new System.EventHandler(this.rangeSliderBinary_ValueChanged);
            // 
            // BinaryProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxBinaryInvert);
            this.Controls.Add(this.binaryText);
            this.Controls.Add(this.rangeSliderBinary);
            this.Name = "BinaryProp";
            this.Size = new System.Drawing.Size(263, 403);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public RangeSliderCtrl rangeSliderBinary;
        private System.Windows.Forms.TextBox binaryText;
        private System.Windows.Forms.CheckBox checkBoxBinaryInvert;
    }
}
