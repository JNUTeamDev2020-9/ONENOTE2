namespace ONENOTE2
{
    partial class NodeForm
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
            this.edit_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // edit_richTextBox
            // 
            this.edit_richTextBox.Location = new System.Drawing.Point(1, 2);
            this.edit_richTextBox.Name = "edit_richTextBox";
            this.edit_richTextBox.Size = new System.Drawing.Size(660, 380);
            this.edit_richTextBox.TabIndex = 0;
            this.edit_richTextBox.Text = "";
            // 
            // NodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 381);
            this.Controls.Add(this.edit_richTextBox);
            this.Name = "NodeForm";
            this.Text = "笔记内容";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox edit_richTextBox;
        
    }
}