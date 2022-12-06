

using System;
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

        private delegate void InvokeToListViewDelegate(string filename);

        private delegate void InvokeToProgressBarLabelDelegate(string sText);

        private delegate void InvokeToProgressBarDelegate(int nPercentage);

        private bool _bInit = false;

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

                LoadFilesIntoGrid(_songsDirectory);

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
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                SelectedPath = sExistingDirectory
            };
            return DialogResult.OK == folderBrowserDialog1.ShowDialog() ? folderBrowserDialog1.SelectedPath : sExistingDirectory;
        }


        private void LoadFilesIntoGrid(string sMusicDirectory)
        {


            try
            {
                var nSong = 0;
                var nAllSongs = _songList.Count();

                //3Tag _DummyID3 = new Id3Tag();

                foreach (var f in _songList)
                {
                    InvokeToListView(f);
                    InvokeToProgressBarLabel("Reading " + f);
                    nSong++;
                }


                InvokeToProgressBarLabel("Completed");
            }
            catch (UnauthorizedAccessException UAEx)
            {
                MessageBox.Show(UAEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (PathTooLongException PathEx)
            {
                MessageBox.Show(PathEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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

        private void InvokeToListView(string filename)
        {
            // If it's coming from another thread, Invoke _InvokeToListView trough the _InvokeToListViewDelegate and end this thing.
            if (lvAudioFiles.InvokeRequired)
            {
                this.Invoke(new InvokeToListViewDelegate(InvokeToListView), filename);
                return;
            }

            lvAudioFiles.Items.Add(filename);


        }

        private void lvAudioFiles_DoubleClick(object sender, EventArgs e)
        {
            ReadMetadataFromFile();
        }

        private void ReadMetadataFromFile()
        {
            try
            {
                _bInit = Un4seen.Bass.Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, this.Handle);

                if (_bInit)
                {
                    int handlePtr = Un4seen.Bass.Bass.BASS_StreamCreateFile(lvAudioFiles.SelectedItems[0].Text, 0, 0, BASSFlag.BASS_DEFAULT);

                    var tagInfo = new Un4seen.Bass.AddOn.Tags.TAG_INFO(lvAudioFiles.SelectedItems[0].Text);
                    if (BassTags.BASS_TAG_GetFromFile(handlePtr, tagInfo))
                    {
                        var metadataWindow = new MetadataWindow(tagInfo);
                        metadataWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Unable to detect metadata tags in file!", "No Tags Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    Un4seen.Bass.Bass.BASS_Free();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void lvAudioFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ReadMetadataFromFile();
            }
        }
    }
}