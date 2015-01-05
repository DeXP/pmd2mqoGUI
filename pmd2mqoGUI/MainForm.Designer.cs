/*
 * Создано в SharpDevelop.
 * Пользователь: DeXPeriX
 * Дата: 05.01.2015
 * Время: 10:54
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace pmd2mqoGUI
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.TextBox fileEdit;
		private System.Windows.Forms.Button openButton;
		private System.Windows.Forms.Label scaleLabel;
		private System.Windows.Forms.Button convertButton;
		private System.Windows.Forms.NumericUpDown scaleUpDown;
		private System.Windows.Forms.PictureBox processIndicator;
		private System.Windows.Forms.GroupBox infoBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.TextBox commentText;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.Label inputLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox outputEdit;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.fileEdit = new System.Windows.Forms.TextBox();
			this.openButton = new System.Windows.Forms.Button();
			this.scaleLabel = new System.Windows.Forms.Label();
			this.convertButton = new System.Windows.Forms.Button();
			this.scaleUpDown = new System.Windows.Forms.NumericUpDown();
			this.processIndicator = new System.Windows.Forms.PictureBox();
			this.infoBox = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.commentText = new System.Windows.Forms.TextBox();
			this.nameText = new System.Windows.Forms.TextBox();
			this.inputLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.outputEdit = new System.Windows.Forms.TextBox();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.scaleUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.processIndicator)).BeginInit();
			this.infoBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// openDialog
			// 
			this.openDialog.Filter = "PMD|*.pmd|All files|*";
			// 
			// fileEdit
			// 
			this.fileEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.fileEdit.Enabled = false;
			this.fileEdit.Location = new System.Drawing.Point(57, 12);
			this.fileEdit.Name = "fileEdit";
			this.fileEdit.Size = new System.Drawing.Size(246, 20);
			this.fileEdit.TabIndex = 0;
			this.fileEdit.Text = "Please, choose PMD-file";
			// 
			// openButton
			// 
			this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openButton.Location = new System.Drawing.Point(309, 9);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(75, 23);
			this.openButton.TabIndex = 1;
			this.openButton.Text = "Choose File";
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.OpenButtonClick);
			// 
			// scaleLabel
			// 
			this.scaleLabel.Location = new System.Drawing.Point(12, 66);
			this.scaleLabel.Name = "scaleLabel";
			this.scaleLabel.Size = new System.Drawing.Size(50, 18);
			this.scaleLabel.TabIndex = 3;
			this.scaleLabel.Text = "Scale:";
			// 
			// convertButton
			// 
			this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.convertButton.Enabled = false;
			this.convertButton.Location = new System.Drawing.Point(292, 64);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(92, 25);
			this.convertButton.TabIndex = 4;
			this.convertButton.Text = "Convert";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.ConvertButtonClick);
			// 
			// scaleUpDown
			// 
			this.scaleUpDown.DecimalPlaces = 3;
			this.scaleUpDown.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.scaleUpDown.Location = new System.Drawing.Point(57, 64);
			this.scaleUpDown.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.scaleUpDown.Name = "scaleUpDown";
			this.scaleUpDown.Size = new System.Drawing.Size(85, 20);
			this.scaleUpDown.TabIndex = 5;
			this.scaleUpDown.Value = new decimal(new int[] {
			10,
			0,
			0,
			0});
			// 
			// processIndicator
			// 
			this.processIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.processIndicator.Image = global::pmd2mqoGUI.Resources.process;
			this.processIndicator.InitialImage = global::pmd2mqoGUI.Resources.process;
			this.processIndicator.Location = new System.Drawing.Point(270, 68);
			this.processIndicator.Name = "processIndicator";
			this.processIndicator.Size = new System.Drawing.Size(16, 16);
			this.processIndicator.TabIndex = 6;
			this.processIndicator.TabStop = false;
			this.processIndicator.Visible = false;
			// 
			// infoBox
			// 
			this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.infoBox.Controls.Add(this.label1);
			this.infoBox.Controls.Add(this.nameLabel);
			this.infoBox.Controls.Add(this.commentText);
			this.infoBox.Controls.Add(this.nameText);
			this.infoBox.Enabled = false;
			this.infoBox.Location = new System.Drawing.Point(12, 95);
			this.infoBox.Name = "infoBox";
			this.infoBox.Size = new System.Drawing.Size(371, 136);
			this.infoBox.TabIndex = 7;
			this.infoBox.TabStop = false;
			this.infoBox.Text = "File info:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Comment:";
			// 
			// nameLabel
			// 
			this.nameLabel.Location = new System.Drawing.Point(6, 22);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(56, 15);
			this.nameLabel.TabIndex = 2;
			this.nameLabel.Text = "Name:";
			// 
			// commentText
			// 
			this.commentText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.commentText.Location = new System.Drawing.Point(66, 45);
			this.commentText.Multiline = true;
			this.commentText.Name = "commentText";
			this.commentText.Size = new System.Drawing.Size(297, 85);
			this.commentText.TabIndex = 1;
			// 
			// nameText
			// 
			this.nameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.nameText.Location = new System.Drawing.Point(68, 19);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(297, 20);
			this.nameText.TabIndex = 0;
			// 
			// inputLabel
			// 
			this.inputLabel.Location = new System.Drawing.Point(12, 14);
			this.inputLabel.Name = "inputLabel";
			this.inputLabel.Size = new System.Drawing.Size(39, 18);
			this.inputLabel.TabIndex = 8;
			this.inputLabel.Text = "Input:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 18);
			this.label2.TabIndex = 10;
			this.label2.Text = "Output:";
			// 
			// outputEdit
			// 
			this.outputEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.outputEdit.Location = new System.Drawing.Point(57, 38);
			this.outputEdit.Name = "outputEdit";
			this.outputEdit.Size = new System.Drawing.Size(327, 20);
			this.outputEdit.TabIndex = 9;
			// 
			// MainForm
			// 
			this.AcceptButton = this.convertButton;
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 243);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.outputEdit);
			this.Controls.Add(this.inputLabel);
			this.Controls.Add(this.infoBox);
			this.Controls.Add(this.processIndicator);
			this.Controls.Add(this.scaleUpDown);
			this.Controls.Add(this.convertButton);
			this.Controls.Add(this.scaleLabel);
			this.Controls.Add(this.openButton);
			this.Controls.Add(this.fileEdit);
			this.HelpButton = true;
			this.Icon = global::pmd2mqoGUI.Resources.icon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(290, 110);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pmd 2 mqo GUI";
			((System.ComponentModel.ISupportInitialize)(this.scaleUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.processIndicator)).EndInit();
			this.infoBox.ResumeLayout(false);
			this.infoBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
