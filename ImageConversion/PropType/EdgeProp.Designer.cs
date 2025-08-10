namespace ImageConversion
{
    partial class EdgeProp
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboMethod;

        public System.Windows.Forms.Panel panelCanny;
        private System.Windows.Forms.Label lbThresholdMin;
        private System.Windows.Forms.Label labelAperture;
        public System.Windows.Forms.NumericUpDown numAperture;

        public System.Windows.Forms.Panel panelSobel;
        private System.Windows.Forms.Label labelSobelDx;
        public System.Windows.Forms.NumericUpDown numSobelDx;
        private System.Windows.Forms.Label labelSobelDy;
        public System.Windows.Forms.NumericUpDown numSobelDy;
        private System.Windows.Forms.Label labelSobelKsize;
        public System.Windows.Forms.NumericUpDown numSobelKsize;

        public System.Windows.Forms.Panel panelLaplacian;
        private System.Windows.Forms.Label labelLapKsize;
        public System.Windows.Forms.NumericUpDown numLapKsize;

        private System.Windows.Forms.TableLayoutPanel layoutRoot;

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
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelCanny = new System.Windows.Forms.Panel();
            this.rangeSliderEdge = new ImageConversion.RangeSliderCtrl();
            this.lbThresholdMin = new System.Windows.Forms.Label();
            this.lbThresholdMax = new System.Windows.Forms.Label();
            this.labelAperture = new System.Windows.Forms.Label();
            this.numAperture = new System.Windows.Forms.NumericUpDown();
            this.panelSobel = new System.Windows.Forms.Panel();
            this.labelSobelDx = new System.Windows.Forms.Label();
            this.numSobelDx = new System.Windows.Forms.NumericUpDown();
            this.labelSobelDy = new System.Windows.Forms.Label();
            this.numSobelDy = new System.Windows.Forms.NumericUpDown();
            this.labelSobelKsize = new System.Windows.Forms.Label();
            this.numSobelKsize = new System.Windows.Forms.NumericUpDown();
            this.panelLaplacian = new System.Windows.Forms.Panel();
            this.labelLapKsize = new System.Windows.Forms.Label();
            this.numLapKsize = new System.Windows.Forms.NumericUpDown();
            this.comboMethod = new System.Windows.Forms.ComboBox();
            this.layoutRoot.SuspendLayout();
            this.panelCanny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAperture)).BeginInit();
            this.panelSobel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSobelDx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSobelDy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSobelKsize)).BeginInit();
            this.panelLaplacian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLapKsize)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutRoot
            // 
            this.layoutRoot.ColumnCount = 2;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.panelCanny, 0, 1);
            this.layoutRoot.Controls.Add(this.panelSobel, 0, 2);
            this.layoutRoot.Controls.Add(this.panelLaplacian, 0, 3);
            this.layoutRoot.Controls.Add(this.comboMethod, 1, 0);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layoutRoot.Size = new System.Drawing.Size(448, 476);
            this.layoutRoot.TabIndex = 0;
            this.layoutRoot.Paint += new System.Windows.Forms.PaintEventHandler(this.layoutRoot_Paint);
            // 
            // panelCanny
            // 
            this.layoutRoot.SetColumnSpan(this.panelCanny, 2);
            this.panelCanny.Controls.Add(this.rangeSliderEdge);
            this.panelCanny.Controls.Add(this.lbThresholdMin);
            this.panelCanny.Controls.Add(this.lbThresholdMax);
            this.panelCanny.Controls.Add(this.labelAperture);
            this.panelCanny.Controls.Add(this.numAperture);
            this.panelCanny.Location = new System.Drawing.Point(3, 33);
            this.panelCanny.Name = "panelCanny";
            this.panelCanny.Size = new System.Drawing.Size(280, 84);
            this.panelCanny.TabIndex = 2;
            // 
            // rangeSliderEdge
            // 
            this.rangeSliderEdge.Location = new System.Drawing.Point(42, 3);
            this.rangeSliderEdge.Maximum = 255;
            this.rangeSliderEdge.Minimum = 0;
            this.rangeSliderEdge.Name = "rangeSliderEdge";
            this.rangeSliderEdge.Size = new System.Drawing.Size(187, 45);
            this.rangeSliderEdge.SliderMax = 200;
            this.rangeSliderEdge.SliderMin = 30;
            this.rangeSliderEdge.TabIndex = 6;
            this.rangeSliderEdge.ValueChanged += new System.EventHandler(this.rangeSliderEdge_ValueChanged);
            // 
            // lbThresholdMin
            // 
            this.lbThresholdMin.AutoSize = true;
            this.lbThresholdMin.Location = new System.Drawing.Point(10, 28);
            this.lbThresholdMin.Name = "lbThresholdMin";
            this.lbThresholdMin.Size = new System.Drawing.Size(26, 12);
            this.lbThresholdMin.TabIndex = 0;
            this.lbThresholdMin.Text = "min";
            this.lbThresholdMin.Click += new System.EventHandler(this.lbThresholdMin_Click);
            // 
            // lbThresholdMax
            // 
            this.lbThresholdMax.AutoSize = true;
            this.lbThresholdMax.Location = new System.Drawing.Point(235, 28);
            this.lbThresholdMax.Name = "lbThresholdMax";
            this.lbThresholdMax.Size = new System.Drawing.Size(30, 12);
            this.lbThresholdMax.TabIndex = 2;
            this.lbThresholdMax.Text = "max";
            // 
            // labelAperture
            // 
            this.labelAperture.AutoSize = true;
            this.labelAperture.Location = new System.Drawing.Point(10, 65);
            this.labelAperture.Name = "labelAperture";
            this.labelAperture.Size = new System.Drawing.Size(35, 12);
            this.labelAperture.TabIndex = 4;
            this.labelAperture.Text = "ksize";
            // 
            // numAperture
            // 
            this.numAperture.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numAperture.Location = new System.Drawing.Point(100, 63);
            this.numAperture.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numAperture.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numAperture.Name = "numAperture";
            this.numAperture.Size = new System.Drawing.Size(120, 21);
            this.numAperture.TabIndex = 5;
            this.numAperture.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // panelSobel
            // 
            this.layoutRoot.SetColumnSpan(this.panelSobel, 2);
            this.panelSobel.Controls.Add(this.labelSobelDx);
            this.panelSobel.Controls.Add(this.numSobelDx);
            this.panelSobel.Controls.Add(this.labelSobelDy);
            this.panelSobel.Controls.Add(this.numSobelDy);
            this.panelSobel.Controls.Add(this.labelSobelKsize);
            this.panelSobel.Controls.Add(this.numSobelKsize);
            this.panelSobel.Location = new System.Drawing.Point(3, 123);
            this.panelSobel.Name = "panelSobel";
            this.panelSobel.Size = new System.Drawing.Size(280, 84);
            this.panelSobel.TabIndex = 3;
            // 
            // labelSobelDx
            // 
            this.labelSobelDx.AutoSize = true;
            this.labelSobelDx.Location = new System.Drawing.Point(10, 10);
            this.labelSobelDx.Name = "labelSobelDx";
            this.labelSobelDx.Size = new System.Drawing.Size(19, 12);
            this.labelSobelDx.TabIndex = 0;
            this.labelSobelDx.Text = "dx";
            // 
            // numSobelDx
            // 
            this.numSobelDx.Location = new System.Drawing.Point(100, 8);
            this.numSobelDx.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSobelDx.Name = "numSobelDx";
            this.numSobelDx.Size = new System.Drawing.Size(120, 21);
            this.numSobelDx.TabIndex = 1;
            this.numSobelDx.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelSobelDy
            // 
            this.labelSobelDy.AutoSize = true;
            this.labelSobelDy.Location = new System.Drawing.Point(10, 40);
            this.labelSobelDy.Name = "labelSobelDy";
            this.labelSobelDy.Size = new System.Drawing.Size(19, 12);
            this.labelSobelDy.TabIndex = 2;
            this.labelSobelDy.Text = "dy";
            // 
            // numSobelDy
            // 
            this.numSobelDy.Location = new System.Drawing.Point(100, 38);
            this.numSobelDy.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSobelDy.Name = "numSobelDy";
            this.numSobelDy.Size = new System.Drawing.Size(120, 21);
            this.numSobelDy.TabIndex = 3;
            // 
            // labelSobelKsize
            // 
            this.labelSobelKsize.AutoSize = true;
            this.labelSobelKsize.Location = new System.Drawing.Point(10, 65);
            this.labelSobelKsize.Name = "labelSobelKsize";
            this.labelSobelKsize.Size = new System.Drawing.Size(35, 12);
            this.labelSobelKsize.TabIndex = 4;
            this.labelSobelKsize.Text = "ksize";
            // 
            // numSobelKsize
            // 
            this.numSobelKsize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSobelKsize.Location = new System.Drawing.Point(100, 63);
            this.numSobelKsize.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numSobelKsize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSobelKsize.Name = "numSobelKsize";
            this.numSobelKsize.Size = new System.Drawing.Size(120, 21);
            this.numSobelKsize.TabIndex = 5;
            this.numSobelKsize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // panelLaplacian
            // 
            this.layoutRoot.SetColumnSpan(this.panelLaplacian, 2);
            this.panelLaplacian.Controls.Add(this.labelLapKsize);
            this.panelLaplacian.Controls.Add(this.numLapKsize);
            this.panelLaplacian.Location = new System.Drawing.Point(3, 213);
            this.panelLaplacian.Name = "panelLaplacian";
            this.panelLaplacian.Size = new System.Drawing.Size(280, 100);
            this.panelLaplacian.TabIndex = 4;
            // 
            // labelLapKsize
            // 
            this.labelLapKsize.AutoSize = true;
            this.labelLapKsize.Location = new System.Drawing.Point(10, 12);
            this.labelLapKsize.Name = "labelLapKsize";
            this.labelLapKsize.Size = new System.Drawing.Size(35, 12);
            this.labelLapKsize.TabIndex = 0;
            this.labelLapKsize.Text = "ksize";
            // 
            // numLapKsize
            // 
            this.numLapKsize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numLapKsize.Location = new System.Drawing.Point(100, 10);
            this.numLapKsize.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numLapKsize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLapKsize.Name = "numLapKsize";
            this.numLapKsize.Size = new System.Drawing.Size(120, 21);
            this.numLapKsize.TabIndex = 1;
            this.numLapKsize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // comboMethod
            // 
            this.comboMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMethod.Location = new System.Drawing.Point(71, 3);
            this.comboMethod.Name = "comboMethod";
            this.comboMethod.Size = new System.Drawing.Size(154, 20);
            this.comboMethod.TabIndex = 1;
            // 
            // EdgeProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutRoot);
            this.Name = "EdgeProp";
            this.Size = new System.Drawing.Size(448, 476);
            this.layoutRoot.ResumeLayout(false);
            this.panelCanny.ResumeLayout(false);
            this.panelCanny.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAperture)).EndInit();
            this.panelSobel.ResumeLayout(false);
            this.panelSobel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSobelDx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSobelDy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSobelKsize)).EndInit();
            this.panelLaplacian.ResumeLayout(false);
            this.panelLaplacian.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLapKsize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RangeSliderCtrl rangeSliderEdge;
        private System.Windows.Forms.Label lbThresholdMax;
    }
}
