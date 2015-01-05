using System;
using System.Drawing;
using System.Windows.Forms;

namespace pmd2mqoGUI
{
	/// <summary>
	/// Description of AboutForm.
	/// </summary>
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
			this.aboutText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(AboutLink_Clicked);
		}
		
		private void AboutLink_Clicked (object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
