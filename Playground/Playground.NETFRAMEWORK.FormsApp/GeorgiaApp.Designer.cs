namespace Playground.NETFRAMEWORK.FormsApp
{
    partial class GeorgiaApp
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
            this.Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ToTextBox = new System.Windows.Forms.TextBox();
            this.MessageField = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Name
            // 
            this.Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name.ForeColor = System.Drawing.Color.Red;
            this.Name.Location = new System.Drawing.Point(304, 83);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(300, 31);
            this.Name.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To";
            // 
            // ToTextBox
            // 
            this.ToTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToTextBox.ForeColor = System.Drawing.Color.Red;
            this.ToTextBox.Location = new System.Drawing.Point(304, 155);
            this.ToTextBox.Name = "ToTextBox";
            this.ToTextBox.Size = new System.Drawing.Size(300, 31);
            this.ToTextBox.TabIndex = 2;
            // 
            // MessageField
            // 
            this.MessageField.Location = new System.Drawing.Point(304, 224);
            this.MessageField.Name = "MessageField";
            this.MessageField.Size = new System.Drawing.Size(300, 127);
            this.MessageField.TabIndex = 4;
            this.MessageField.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Message";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(623, 83);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(308, 268);
            this.SendButton.TabIndex = 6;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // GeorgiaApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 404);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MessageField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ToTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Name);
            this.Text = "Georgia App";
            this.Load += new System.EventHandler(this.GeorgiaApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ToTextBox;
        private System.Windows.Forms.RichTextBox MessageField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SendButton;
    }
}

