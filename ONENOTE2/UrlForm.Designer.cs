namespace ONENOTE2
{
    partial class UrlForm
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
            this.url_textBox = new System.Windows.Forms.TextBox();
            this.url_label = new System.Windows.Forms.Label();
            this.urlok_button = new System.Windows.Forms.Button();
            this.urlcancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // url_textBox
            // 
            this.url_textBox.Location = new System.Drawing.Point(161, 156);
            this.url_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.url_textBox.Name = "url_textBox";
            this.url_textBox.Size = new System.Drawing.Size(132, 25);
            this.url_textBox.TabIndex = 1;
            // 
            // url_label
            // 
            this.url_label.AutoSize = true;
            this.url_label.Location = new System.Drawing.Point(159, 115);
            this.url_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.url_label.Name = "url_label";
            this.url_label.Size = new System.Drawing.Size(82, 15);
            this.url_label.TabIndex = 3;
            this.url_label.Text = "请输入链接";
            // 
            // urlok_button
            // 
            this.urlok_button.Location = new System.Drawing.Point(97, 231);
            this.urlok_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.urlok_button.Name = "urlok_button";
            this.urlok_button.Size = new System.Drawing.Size(100, 29);
            this.urlok_button.TabIndex = 4;
            this.urlok_button.Text = "确定";
            this.urlok_button.UseVisualStyleBackColor = true;
            this.urlok_button.Click += new System.EventHandler(this.urlok_button_Click);
            // 
            // urlcancel_button
            // 
            this.urlcancel_button.Location = new System.Drawing.Point(308, 231);
            this.urlcancel_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.urlcancel_button.Name = "urlcancel_button";
            this.urlcancel_button.Size = new System.Drawing.Size(100, 29);
            this.urlcancel_button.TabIndex = 5;
            this.urlcancel_button.Text = "取消";
            this.urlcancel_button.UseVisualStyleBackColor = true;
            this.urlcancel_button.Click += new System.EventHandler(this.urlcancel_button_Click);
            // 
            // UrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 279);
            this.Controls.Add(this.urlcancel_button);
            this.Controls.Add(this.urlok_button);
            this.Controls.Add(this.url_label);
            this.Controls.Add(this.url_textBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UrlForm";
            this.Text = "插入链接";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox url_textBox;
        private System.Windows.Forms.Label url_label;
        private System.Windows.Forms.Button urlok_button;
        private System.Windows.Forms.Button urlcancel_button;
    }
}