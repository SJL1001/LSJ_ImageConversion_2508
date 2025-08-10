namespace ImageConversion
{
    partial class PropertiesForm
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
            this.tabPropControl = new System.Windows.Forms.TabControl();
            this.panelProp = new System.Windows.Forms.Panel();
            this.cropApplyButton = new System.Windows.Forms.Button();
            this.cropButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.checkBoxKeepResult = new System.Windows.Forms.CheckBox();
            this.undoButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.panelProp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPropControl
            // 
            this.tabPropControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPropControl.ItemSize = new System.Drawing.Size(10, 20);
            this.tabPropControl.Location = new System.Drawing.Point(0, 0);
            this.tabPropControl.Multiline = true;
            this.tabPropControl.Name = "tabPropControl";
            this.tabPropControl.SelectedIndex = 0;
            this.tabPropControl.Size = new System.Drawing.Size(800, 450);
            this.tabPropControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabPropControl.TabIndex = 0;
            // 
            // panelProp
            // 
            this.panelProp.Controls.Add(this.cropApplyButton);
            this.panelProp.Controls.Add(this.cropButton);
            this.panelProp.Controls.Add(this.resetButton);
            this.panelProp.Controls.Add(this.checkBoxKeepResult);
            this.panelProp.Controls.Add(this.undoButton);
            this.panelProp.Controls.Add(this.redoButton);
            this.panelProp.Controls.Add(this.applyButton);
            this.panelProp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProp.Location = new System.Drawing.Point(0, 275);
            this.panelProp.Name = "panelProp";
            this.panelProp.Size = new System.Drawing.Size(800, 175);
            this.panelProp.TabIndex = 1;
            // 
            // cropApplyButton
            // 
            this.cropApplyButton.Location = new System.Drawing.Point(113, 98);
            this.cropApplyButton.Name = "cropApplyButton";
            this.cropApplyButton.Size = new System.Drawing.Size(85, 23);
            this.cropApplyButton.TabIndex = 7;
            this.cropApplyButton.Text = "자르기 적용";
            this.cropApplyButton.UseVisualStyleBackColor = true;
            this.cropApplyButton.Click += new System.EventHandler(this.cropApplyButton_Click);
            // 
            // cropButton
            // 
            this.cropButton.Location = new System.Drawing.Point(7, 98);
            this.cropButton.Name = "cropButton";
            this.cropButton.Size = new System.Drawing.Size(75, 23);
            this.cropButton.TabIndex = 6;
            this.cropButton.Text = "ROI그리기";
            this.cropButton.UseVisualStyleBackColor = true;
            this.cropButton.Click += new System.EventHandler(this.cropButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(113, 55);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 28);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // checkBoxKeepResult
            // 
            this.checkBoxKeepResult.AutoSize = true;
            this.checkBoxKeepResult.Location = new System.Drawing.Point(13, 25);
            this.checkBoxKeepResult.Name = "checkBoxKeepResult";
            this.checkBoxKeepResult.Size = new System.Drawing.Size(76, 16);
            this.checkBoxKeepResult.TabIndex = 3;
            this.checkBoxKeepResult.Text = "변환 유지";
            this.checkBoxKeepResult.UseVisualStyleBackColor = true;
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(7, 140);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(75, 28);
            this.undoButton.TabIndex = 2;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.Location = new System.Drawing.Point(113, 140);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(75, 28);
            this.redoButton.TabIndex = 1;
            this.redoButton.Text = "Redo";
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(7, 55);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 28);
            this.applyButton.TabIndex = 0;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelProp);
            this.Controls.Add(this.tabPropControl);
            this.Name = "PropertiesForm";
            this.Text = "PropertiesForm";
            this.panelProp.ResumeLayout(false);
            this.panelProp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelProp;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button redoButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button resetButton;
        public System.Windows.Forms.TabControl tabPropControl;
        public System.Windows.Forms.CheckBox checkBoxKeepResult;
        private System.Windows.Forms.Button cropButton;
        private System.Windows.Forms.Button cropApplyButton;
    }
}