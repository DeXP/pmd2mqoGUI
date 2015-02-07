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
using System.Collections.Generic;
using System.Globalization;


namespace pmd2mqoGUI
{
	/// <summary>
	/// MainForm of pmd to mqo converter.
	/// </summary>
	public partial class MainForm : Form
	{
		private BackgroundWorker backgroundWorker = new BackgroundWorker();
		public String errorStr;
		public String warningStr;
		public String notPmdError;
		public String doneCorrectly;
		public String texNotFound;
		public List<pmd2mqo.MqoMaterial> Materials;
		
		public MainForm()
		{
			InitializeComponent();		
			LangLocalize();
			
			this.DragEnter += new DragEventHandler(MainForm_DragEnter);
			this.DragDrop += new DragEventHandler(MainForm_DragDrop);
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(MainForm_HelpButtonClicked);
			
			this.backgroundWorker.WorkerReportsProgress = false;
			this.backgroundWorker.WorkerSupportsCancellation = false;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			
			String[] arguments = Environment.GetCommandLineArgs();
			if( (arguments.Length > 1) && ( File.Exists(arguments[1]) ) ){
				fileEdit.Text = arguments[1];
				FileOpened();
			}
		}
		
		void LangLocalize()
		{
			this.errorStr = "Error!";
			this.notPmdError = "Convertation Error: \nThe file is not in PMD format!";
			this.doneCorrectly = "Done correctly.\nYou can find resulting file in the same directory.";
			this.texNotFound = "Some textures file was not found!\nErrors may occur when you import the file.\n\nNot found file list:\n";
			
			CultureInfo ci = CultureInfo.InstalledUICulture;
			if( ci.TwoLetterISOLanguageName == "ru" ){
				this.inputLabel.Text = stringsRu.input;
				this.outputLabel.Text = stringsRu.output;
				this.openButton.Text = stringsRu.chooseFile;
				this.scaleLabel.Text = stringsRu.scale;
				this.convertButton.Text = stringsRu.convert;
				this.infoBox.Text = stringsRu.fileInfo;
				this.fileEdit.Text =stringsRu.inputHelp;
				this.nameLabel.Text = stringsRu.name;
				this.commentLabel.Text = stringsRu.comment;
				this.errorStr = stringsRu.error;
				this.notPmdError = stringsRu.notPmdError;
				this.doneCorrectly = stringsRu.doneCorrectly;
				this.texNotFound = stringsRu.texNotFound;
				this.warningStr = stringsRu.warning;
			}
		}
		
		private void MainForm_HelpButtonClicked(Object sender, System.ComponentModel.CancelEventArgs e)
		{
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
			string modelName;
			string modelComment;
			Regex ext_pmd = new Regex(@"\.pmd$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string mqoFile = ext_pmd.Replace(fileEdit.Text, ".mqo");
            if (!ext_pmd.IsMatch(fileEdit.Text)) mqoFile = fileEdit.Text + ".mqo"; //return false;//error_NoPMD();
            outputEdit.Text = mqoFile;
            
			convertButton.Enabled = true;
			nameText.Text = "";
			commentText.Text = "";
			infoBox.Enabled = false;
			
			if( pmd2mqo.Pmd2mqo.getInfo(fileEdit.Text, out modelName, out modelComment) ){ 
				nameText.Text = modelName;
                commentText.Text = modelComment;
				infoBox.Enabled = true;
			}
		}
		
	
		void ConvertButtonClick(object sender, EventArgs e)
		{
			string modelName;
			string modelComment;
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
				MessageBox.Show(notPmdError, errorStr, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void backgroundWorker_DoWork(object sender, EventArgs e){
			var hashNames = new HashSet<string>();
			String badNames = "";
			try
            {
				if( pmd2mqo.Pmd2mqo.pmd2mqo(fileEdit.Text, outputEdit.Text, (float)scaleUpDown.Value, out Materials) ){
					for(int i=0; i<Materials.Count; i++){
						if( (Materials[i].fixtex != "") &&  !File.Exists( Path.Combine(Path.GetDirectoryName(fileEdit.Text), Materials[i].fixtex) ) ){
							hashNames.Add(Materials[i].fixtex);
							//badNames += Materials[i].fixtex;
						}
					}
					
					foreach (var str in hashNames)
						badNames += str + "\n";
					
					if( hashNames.Count ==0 ) MessageBox.Show(doneCorrectly, "Ok", MessageBoxButtons.OK ,MessageBoxIcon.Information);
					else MessageBox.Show(texNotFound+badNames, warningStr, MessageBoxButtons.OK ,MessageBoxIcon.Warning);
					
				}
			}
			catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString(), errorStr, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}
		
		void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			convertButton.Enabled = true;
			processIndicator.Visible = false;
		}
		
	}
}
