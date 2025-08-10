namespace ImageConversion
{
    partial class BlurProp
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
            this.blurList = new System.Windows.Forms.ComboBox();
            this.sigmaColor = new System.Windows.Forms.NumericUpDown();
            this.sigmaSpace = new System.Windows.Forms.NumericUpDown();
            this.kernalSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSigmaColor = new System.Windows.Forms.Label();
            this.labelSigmaSpace = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kernalSize)).BeginInit();
            this.SuspendLayout();
            // 
            // blurList
            // 
            this.blurList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blurList.FormattingEnabled = true;
            this.blurList.Items.AddRange(new object[] {
            "Gaussian",
            "Median",
            "Bilateral"});
            this.blurList.Location = new System.Drawing.Point(14, 14);
            this.blurList.Name = "blurList";
            this.blurList.Size = new System.Drawing.Size(173, 20);
            this.blurList.TabIndex = 0;
            // 
            // sigmaColor
            // 
            this.sigmaColor.Location = new System.Drawing.Point(61, 124);
            this.sigmaColor.Name = "sigmaColor";
            this.sigmaColor.Size = new System.Drawing.Size(117, 21);
            this.sigmaColor.TabIndex = 1;
            this.sigmaColor.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // sigmaSpace
            // 
            this.sigmaSpace.Location = new System.Drawing.Point(61, 179);
            this.sigmaSpace.Name = "sigmaSpace";
            this.sigmaSpace.Size = new System.Drawing.Size(117, 21);
            this.sigmaSpace.TabIndex = 2;
            this.sigmaSpace.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // kernalSize
            // 
            this.kernalSize.Location = new System.Drawing.Point(61, 67);
            this.kernalSize.Name = "kernalSize";
            this.kernalSize.Size = new System.Drawing.Size(117, 21);
            this.kernalSize.TabIndex = 3;
            this.kernalSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "커널크기\r\n(홀수)";
            // 
            // labelSigmaColor
            // 
            this.labelSigmaColor.AutoSize = true;
            this.labelSigmaColor.Location = new System.Drawing.Point(3, 121);
            this.labelSigmaColor.Name = "labelSigmaColor";
            this.labelSigmaColor.Size = new System.Drawing.Size(41, 24);
            this.labelSigmaColor.TabIndex = 3;
            this.labelSigmaColor.Text = "Sigma\r\nColor";
            // 
            // labelSigmaSpace
            // 
            this.labelSigmaSpace.AutoSize = true;
            this.labelSigmaSpace.Location = new System.Drawing.Point(3, 176);
            this.labelSigmaSpace.Name = "labelSigmaSpace";
            this.labelSigmaSpace.Size = new System.Drawing.Size(41, 24);
            this.labelSigmaSpace.TabIndex = 4;
            this.labelSigmaSpace.Text = "Sigma\r\nSpace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "적정 범위 : 3~15";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "적정 범위 : 10~150";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "적정 범위 : 10~150";
            // 
            // BlurProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSigmaSpace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sigmaSpace);
            this.Controls.Add(this.labelSigmaColor);
            this.Controls.Add(this.sigmaColor);
            this.Controls.Add(this.kernalSize);
            this.Controls.Add(this.blurList);
            this.Name = "BlurProp";
            this.Size = new System.Drawing.Size(200, 230);
            ((System.ComponentModel.ISupportInitialize)(this.sigmaColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kernalSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox blurList;
        private System.Windows.Forms.NumericUpDown sigmaColor;
        private System.Windows.Forms.NumericUpDown sigmaSpace;
        private System.Windows.Forms.NumericUpDown kernalSize;
        private System.Windows.Forms.Label labelSigmaSpace;
        private System.Windows.Forms.Label labelSigmaColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
