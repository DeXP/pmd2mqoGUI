/*
 * Created in SharpDevelop.
 * By DeXPeriX a.k.a. Hrabrov Dmitry
 * Date: 05.01.2015
 * Time: 10:54
 * 
 */
using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace pmd2mqoGUI
{
	/// <summary>
	/// MainForm of pmd to mqo converter.
	/// </summary>
	public partial class MainForm : Form
	{
		private BackgroundWorker backgroundWorker = new BackgroundWorker();
		
		public MainForm()
		{
			InitializeComponent();		
			this.DragEnter += new DragEventHandler(MainForm_DragEnter);
			this.DragDrop += new DragEventHandler(MainForm_DragDrop);
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(MainForm_HelpButtonClicked);
			
			this.backgroundWorker.WorkerReportsProgress = false;
			this.backgroundWorker.WorkerSupportsCancellation = false;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);

		}
		
		private void MainForm_HelpButtonClicked(Object sender, System.ComponentModel.CancelEventArgs e)
		{
			/*MessageBox.Show("pmd2mqoGUI\nA simple .PMD to .MQO converter GUI\n\n"+
			                "Convertation code by:\nKazumasa Honda\nhttps://onedrive.live.com/?cid=9DA0FA00AC5A8258\n\n"+
			                "GUI code by:\nDmitry Hrabrov a.k.a. DeXPeriX\nhttp://dexperix.net\n\n"+
			                "The program is made specifically for\nhttp://OneMangaDay.dexp.in"
			                , "About", MessageBoxButtons.OK ,MessageBoxIcon.Information);*/
			//AboutForm.S
			AboutForm frm = new AboutForm();
			frm.ShowDialog();
			e.Cancel = true;
		}
		
		void MainForm_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}
		
		void MainForm_DragDrop(object sender, DragEventArgs e) {
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			//foreach (string file in files) Console.WriteLine(file);
			fileEdit.Text = files[0];
			FileOpened();
		}
		
		void OpenButtonClick(object sender, EventArgs e)
		{	
			if( openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK ){
				fileEdit.Text = openDialog.FileName;
				saveDialog.InitialDirectory = Path.GetDirectoryName(openDialog.FileName);
				FileOpened();
			} else convertButton.Enabled = false;
		}
		
		void FileOpened(){
			Regex ext_pmd = new Regex(@"\.pmd$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string mqoFile = ext_pmd.Replace(fileEdit.Text, ".mqo");
            if (!ext_pmd.IsMatch(fileEdit.Text)) mqoFile = fileEdit.Text + ".mqo"; //return false;//error_NoPMD();
            outputEdit.Text = mqoFile;
            
			convertButton.Enabled = true;
			nameText.Text = "";
			commentText.Text = "";
			infoBox.Enabled = false;
		}
		
		void ConvertButtonClick(object sender, EventArgs e)
		{
			convertButton.Enabled = false;
			processIndicator.Visible = true;
			if( pmd2mqo.Pmd2mqo.getInfo(fileEdit.Text, out modelName, out modelComment) ){ 
				nameText.Text = modelName;
				commentText.Text = modelComment;
				infoBox.Enabled = true;
				backgroundWorker.RunWorkerAsync();
			} 
			else
			{
				processIndicator.Visible = false;
				infoBox.Enabled = false;
				MessageBox.Show("Convertation Error: \nThe file is not in PMD format!",
				                "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void backgroundWorker_DoWork(object sender, EventArgs e){
			try
            {
				if( pmd2mqo.Pmd2mqo.pmd2mqo(fileEdit.Text, outputEdit.Text, (float)scaleUpDown.Value ) ) 
					MessageBox.Show("Done correctly.\nYou can find resulting file in the same directory.", "Ok",
					                MessageBoxButtons.OK ,MessageBoxIcon.Information);
			}
			catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString(), 
                                "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}
		
		void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			convertButton.Enabled = true;
			processIndicator.Visible = false;
		}
		
	}
}
