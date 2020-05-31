namespace ONENOTE2
{
    partial class Tip
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
            this.tip_label = new System.Windows.Forms.Label();
            this.name_textBox = new System.Windows.Forms.TextBox();
            this.sure_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tip_label
            // 
            this.tip_label.AutoSize = true;
            this.tip_label.Location = new System.Drawing.Point(96, 32);
            this.tip_label.Name = "tip_label";
            this.tip_label.Size = new System.Drawing.Size(101, 12);
            this.tip_label.TabIndex = 0;
            this.tip_label.Text = "请输入知识库名字";
            // 
            // name_textBox
            // 
            this.name_textBox.Location = new System.Drawing.Point(64, 69);
            this.name_textBox.Name = "name_textBox";
            this.name_textBox.Size = new System.Drawing.Size(193, 21);
            this.name_textBox.TabIndex = 1;
            // 
            // sure_button
            // 
            this.sure_button.Location = new System.Drawing.Point(54, 120);
            this.sure_button.Name = "sure_button";
            this.sure_button.Size = new System.Drawing.Size(75, 23);
            this.sure_button.TabIndex = 2;
            this.sure_button.Text = "确定";
            this.sure_button.UseVisualStyleBackColor = true;
            this.sure_button.Click += new System.EventHandler(this.sure_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(172, 120);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 3;
            this.cancel_button.Text = "取消";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // Tip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 175);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.sure_button);
            this.Controls.Add(this.name_textBox);
            this.Controls.Add(this.tip_label);
            this.Name = "Tip";
            this.Text = "添加提示框";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tip_label;
        private System.Windows.Forms.TextBox name_textBox;
        private System.Windows.Forms.Button sure_button;
        private System.Windows.Forms.Button cancel_button;
    }
}