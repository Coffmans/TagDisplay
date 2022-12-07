

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;


namespace TagDisplay
{
    public partial class TagDisplayMainWindow : Form
    {
        private string _songsDirectory = "";
        private readonly string[] _extension = { "*.mp3", "*.wav", "*.mp2", "*.flac", "*.mp4", "*.ogg",  ".ape"};
        private IEnumerable<string> _songList = new List<string>();

        private delegate void InvokeToListViewDelegate(string filename, TAG_INFO fileTagInfo);

        private delegate void InvokeToProgressBarLabelDelegate(string sText);

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();


        public TagDisplayMainWindow()
        {
            InitializeComponent();
        }

        private void btnFolderBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                txtAudioFolder.Text = BrowseForFolder(txtAudioFolder.Text);

                if (String.IsNullOrEmpty(txtAudioFolder.Text)) return;

                lvAudioFiles.Items.Clear();
                _songsDirectory = txtAudioFolder.Text;
                _songList = GetFiles(txtAudioFolder.Text, _extension, chkSearchSubDirectories.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                if (!_songList.Any())
                {
                    return;
                }

                lvAudioFiles.Enabled = false;
                txtAudioFolder.Enabled = false;
                btnFolderBrowse.Enabled = false;
                chkSearchSubDirectories.Enabled = false;

                Un4seen.Bass.Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, this.Handle);

                _backgroundWorker.RunWorkerAsync();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }

        private string BrowseForFolder(string sExistingDirectory)
        {
            FolderBrowserDialog browserDialog1 = new FolderBrowserDialog
            {
                SelectedPath = sExistingDirectory
            };
            return DialogResult.OK == browserDialog1.ShowDialog() ? browserDialog1.SelectedPath : sExistingDirectory;
        }


        private void LoadFilesIntoListView(string sMusicDirectory)
        {
            try
            {
                var nSong = 0;
                var nAllSongs = _songList.Count();

                foreach (var f in _songList)
                {
                    int handlePtr = Un4seen.Bass.Bass.BASS_StreamCreateFile(f, 0, 0, BASSFlag.BASS_DEFAULT);

                    var tagInfo = new Un4seen.Bass.AddOn.Tags.TAG_INFO(f);

                    BassTags.BASS_TAG_GetFromFile(handlePtr, tagInfo);

                    InvokeToListView(f, tagInfo);
                    InvokeToProgressBarLabel("Reading " + f);
                    nSong++;
                }

                Un4seen.Bass.Bass.BASS_Free();
            }
            catch (UnauthorizedAccessException uaEx)
            {
                MessageBox.Show(uaEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (PathTooLongException pathEx)
            {
                MessageBox.Show(pathEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chkSearchSubDirectories.Checked = false;

            _backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            _backgroundWorker.DoWork += new DoWorkEventHandler(Bw_DoWork);

            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);
        }

        private void Bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                InvokeToProgressBarLabel("Starting Loading of Songs, Please Wait...");

                LoadFilesIntoListView(_songsDirectory);
            }
            catch (System.Exception ex)
            {
                InvokeToProgressBarLabel(ex.ToString());
            }
        }

        private void Bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            lvAudioFiles.Enabled = true;
            txtAudioFolder.Enabled = true;
            btnFolderBrowse.Enabled = true;
            chkSearchSubDirectories.Enabled = true;
            InvokeToProgressBarLabel("Loading of Audio Files Completed!");
        }

        private void InvokeToProgressBarLabel(string sText)
        {
            if (lblProgress.InvokeRequired)
            {
                this.Invoke(new InvokeToProgressBarLabelDelegate(InvokeToProgressBarLabel), sText);
                return;
            }

            lblProgress.Text = sText;
        }

        private void InvokeToListView(string filename, TAG_INFO fileTagInfo)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (lvAudioFiles.InvokeRequired)
            {
                this.Invoke(new InvokeToListViewDelegate(InvokeToListView), filename, fileTagInfo);
                return;
            }

            var lvItem = new ListViewItem
            {
                Text = filename,
                Tag = fileTagInfo
            };
            lvItem.SubItems.Add(fileTagInfo.title);
            lvItem.SubItems.Add(fileTagInfo.artist);
            lvAudioFiles.Items.Add(lvItem);

        }

        private void lvAudioFiles_DoubleClick(object sender, EventArgs e)
        {
            var lvItem = (ListViewItem)lvAudioFiles.SelectedItems[0];
            var metadataWindow = new MetadataWindow((TAG_INFO)lvItem.Tag);
            metadataWindow.ShowDialog();
            
        }

        private void lvAudioFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                var lvItem = (ListViewItem)lvAudioFiles.SelectedItems[0];
                var metadataWindow = new MetadataWindow((TAG_INFO)lvItem.Tag);
                metadataWindow.ShowDialog();
            }
        }
    }
}