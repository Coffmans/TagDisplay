namespace TagDisplay
{
    partial class TagDisplayMainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagDisplayMainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.txtAudioFolder = new System.Windows.Forms.TextBox();
            this.btnFolderBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lvAudioFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.lblProgress = new System.Windows.Forms.Label();
            this.chkSearchSubDirectories = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory";
            // 
            // txtAudioFolder
            // 
            this.txtAudioFolder.Location = new System.Drawing.Point(71, 18);
            this.txtAudioFolder.Name = "txtAudioFolder";
            this.txtAudioFolder.Size = new System.Drawing.Size(413, 23);
            this.txtAudioFolder.TabIndex = 1;
            // 
            // btnFolderBrowse
            // 
            this.btnFolderBrowse.Location = new System.Drawing.Point(490, 17);
            this.btnFolderBrowse.Name = "btnFolderBrowse";
            this.btnFolderBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnFolderBrowse.TabIndex = 2;
            this.btnFolderBrowse.Text = "...";
            this.btnFolderBrowse.UseVisualStyleBackColor = true;
            this.btnFolderBrowse.Click += new System.EventHandler(this.btnFolderBrowse_Click);
            // 
            // lvAudioFiles
            // 
            this.lvAudioFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvAudioFiles.FullRowSelect = true;
            this.lvAudioFiles.Location = new System.Drawing.Point(13, 86);
            this.lvAudioFiles.Name = "lvAudioFiles";
            this.lvAudioFiles.Size = new System.Drawing.Size(665, 591);
            this.lvAudioFiles.TabIndex = 6;
            this.lvAudioFiles.UseCompatibleStateImageBehavior = false;
            this.lvAudioFiles.View = System.Windows.Forms.View.Details;
            this.lvAudioFiles.DoubleClick += new System.EventHandler(this.lvAudioFiles_DoubleClick);
            this.lvAudioFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvAudioFiles_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Filename";
            this.columnHeader1.Width = 600;
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(16, 680);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(628, 23);
            this.lblProgress.TabIndex = 0;
            // 
            // chkSearchSubDirectories
            // 
            this.chkSearchSubDirectories.AutoSize = true;
            this.chkSearchSubDirectories.Location = new System.Drawing.Point(539, 20);
            this.chkSearchSubDirectories.Name = "chkSearchSubDirectories";
            this.chkSearchSubDirectories.Size = new System.Drawing.Size(139, 19);
            this.chkSearchSubDirectories.TabIndex = 4;
            this.chkSearchSubDirectories.Text = "Search Subdirectories";
            this.chkSearchSubDirectories.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "List of Audio Files";
            // 
            // TagDisplayMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 741);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkSearchSubDirectories);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lvAudioFiles);
            this.Controls.Add(this.btnFolderBrowse);
            this.Controls.Add(this.txtAudioFolder);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TagDisplayMainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox txtAudioFolder;
        private Button btnFolderBrowse;
        private FolderBrowserDialog folderBrowserDialog1;
        private ListView lvAudioFiles;
        private Label lblProgress;
        private ColumnHeader columnHeader1;
        private CheckBox chkSearchSubDirectories;
        private Label label2;
    }
}