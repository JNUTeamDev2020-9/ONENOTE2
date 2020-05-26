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
            this.urlname_textBox = new System.Windows.Forms.TextBox();
            this.url_textBox = new System.Windows.Forms.TextBox();
            this.urlname_label = new System.Windows.Forms.Label();
            this.url_label = new System.Windows.Forms.Label();
            this.urlok_button = new System.Windows.Forms.Button();
            this.urlcancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urlname_textBox
            // 
            this.urlname_textBox.Location = new System.Drawing.Point(121, 46);
            this.urlname_textBox.Name = "urlname_textBox";
            this.urlname_textBox.Size = new System.Drawing.Size(100, 21);
            this.urlname_textBox.TabIndex = 0;
            // 
            // url_textBox
            // 
            this.url_textBox.Location = new System.Drawing.Point(121, 125);
            this.url_textBox.Name = "url_textBox";
            this.url_textBox.Size = new System.Drawing.Size(100, 21);
            this.url_textBox.TabIndex = 1;
            // 
            // urlname_label
            // 
            this.urlname_label.AutoSize = true;
            this.urlname_label.Location = new System.Drawing.Point(119, 19);
            this.urlname_label.Name = "urlname_label";
            this.urlname_label.Size = new System.Drawing.Size(89, 12);
            this.urlname_label.TabIndex = 2;
            this.urlname_label.Text = "请输入链接名称";
            // 
            // url_label
            // 
            this.url_label.AutoSize = true;
            this.url_label.Location = new System.Drawing.Point(119, 92);
            this.url_label.Name = "url_label";
            this.url_label.Size = new System.Drawing.Size(65, 12);
            this.url_label.TabIndex = 3;
            this.url_label.Text = "请输入链接";
            // 
            // urlok_button
            // 
            this.urlok_button.Location = new System.Drawing.Point(73, 185);
            this.urlok_button.Name = "urlok_button";
            this.urlok_button.Size = new System.Drawing.Size(75, 23);
            this.urlok_button.TabIndex = 4;
            this.urlok_button.Text = "button1";
            this.urlok_button.UseVisualStyleBackColor = true;
            this.urlok_button.Click += new System.EventHandler(this.urlok_button_Click);
            // 
            // urlcancel_button
            // 
            this.urlcancel_button.Location = new System.Drawing.Point(231, 185);
            this.urlcancel_button.Name = "urlcancel_button";
            this.urlcancel_button.Size = new System.Drawing.Size(75, 23);
            this.urlcancel_button.TabIndex = 5;
            this.urlcancel_button.Text = "button2";
            this.urlcancel_button.UseVisualStyleBackColor = true;
            this.urlcancel_button.Click += new System.EventHandler(this.urlcancel_button_Click);
            // 
            // UrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 223);
            this.Controls.Add(this.urlcancel_button);
            this.Controls.Add(this.urlok_button);
            this.Controls.Add(this.url_label);
            this.Controls.Add(this.urlname_label);
            this.Controls.Add(this.url_textBox);
            this.Controls.Add(this.urlname_textBox);
            this.Name = "UrlForm";
            this.Text = "插入链接";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlname_textBox;
        private System.Windows.Forms.TextBox url_textBox;
        private System.Windows.Forms.Label urlname_label;
        private System.Windows.Forms.Label url_label;
        private System.Windows.Forms.Button urlok_button;
        private System.Windows.Forms.Button urlcancel_button;
    }
}