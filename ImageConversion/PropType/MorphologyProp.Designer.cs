namespace ImageConversion
{
    partial class MorphologyProp
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
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.cbShape = new System.Windows.Forms.ComboBox();
            this.numKernel = new System.Windows.Forms.NumericUpDown();
            this.numIter = new System.Windows.Forms.NumericUpDown();
            this.lbKernel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIter)).BeginInit();
            this.SuspendLayout();
            // 
            // cbOperation
            // 
            this.cbOperation.FormattingEnabled = true;
            this.cbOperation.Location = new System.Drawing.Point(39, 29);
            this.cbOperation.Name = "cbOperation";
            this.cbOperation.Size = new System.Drawing.Size(121, 20);
            this.cbOperation.TabIndex = 0;
            // 
            // cbShape
            // 
            this.cbShape.FormattingEnabled = true;
            this.cbShape.Location = new System.Drawing.Point(39, 143);
            this.cbShape.Name = "cbShape";
            this.cbShape.Size = new System.Drawing.Size(121, 20);
            this.cbShape.TabIndex = 1;
            this.cbShape.SelectedIndexChanged += new System.EventHandler(this.cbShape_SelectedIndexChanged);
            // 
            // numKernel
            // 
            this.numKernel.Location = new System.Drawing.Point(92, 65);
            this.numKernel.Name = "numKernel";
            this.numKernel.Size = new System.Drawing.Size(120, 21);
            this.numKernel.TabIndex = 2;
            // 
            // numIter
            // 
            this.numIter.Location = new System.Drawing.Point(92, 101);
            this.numIter.Name = "numIter";
            this.numIter.Size = new System.Drawing.Size(120, 21);
            this.numIter.TabIndex = 3;
            // 
            // lbKernel
            // 
            this.lbKernel.AutoSize = true;
            this.lbKernel.Location = new System.Drawing.Point(37, 67);
            this.lbKernel.Name = "lbKernel";
            this.lbKernel.Size = new System.Drawing.Size(35, 12);
            this.lbKernel.TabIndex = 4;
            this.lbKernel.Text = "ksize";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Iteration";
            // 
            // MorphologyProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbKernel);
            this.Controls.Add(this.numIter);
            this.Controls.Add(this.numKernel);
            this.Controls.Add(this.cbShape);
            this.Controls.Add(this.cbOperation);
            this.Name = "MorphologyProp";
            this.Size = new System.Drawing.Size(269, 392);
            ((System.ComponentModel.ISupportInitialize)(this.numKernel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbOperation;
        private System.Windows.Forms.ComboBox cbShape;
        private System.Windows.Forms.NumericUpDown numKernel;
        private System.Windows.Forms.NumericUpDown numIter;
        private System.Windows.Forms.Label lbKernel;
        private System.Windows.Forms.Label label2;
    }
}
