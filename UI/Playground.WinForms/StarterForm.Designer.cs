namespace Playground.WinForms
{
	partial class StarterForm
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
			this.categoriesIdTextBox = new System.Windows.Forms.TextBox();
			this.GoBtn = new System.Windows.Forms.Button();
			this.categoriesList = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// categoriesIdTextBox
			// 
			this.categoriesIdTextBox.Location = new System.Drawing.Point(12, 33);
			this.categoriesIdTextBox.Name = "categoriesIdTextBox";
			this.categoriesIdTextBox.Size = new System.Drawing.Size(776, 20);
			this.categoriesIdTextBox.TabIndex = 1;
			this.categoriesIdTextBox.Text = "1,2,3,4,5";
			this.categoriesIdTextBox.TextChanged += new System.EventHandler(this.CategoriesIdTextBox_TextChanged);
			// 
			// GoBtn
			// 
			this.GoBtn.Location = new System.Drawing.Point(12, 60);
			this.GoBtn.Name = "GoBtn";
			this.GoBtn.Size = new System.Drawing.Size(776, 23);
			this.GoBtn.TabIndex = 2;
			this.GoBtn.Text = "Go";
			this.GoBtn.UseVisualStyleBackColor = true;
			this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
			// 
			// categoriesList
			// 
			this.categoriesList.FormattingEnabled = true;
			this.categoriesList.Location = new System.Drawing.Point(12, 89);
			this.categoriesList.Name = "categoriesList";
			this.categoriesList.Size = new System.Drawing.Size(776, 355);
			this.categoriesList.TabIndex = 3;
			// 
			// StarterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.categoriesList);
			this.Controls.Add(this.GoBtn);
			this.Controls.Add(this.categoriesIdTextBox);
			this.Name = "StarterForm";
			this.Text = "StarterForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox categoriesIdTextBox;
		private System.Windows.Forms.Button GoBtn;
		private System.Windows.Forms.ListBox categoriesList;
	}
}