

using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using ManagedBass;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;


namespace TagDisplay
{
    public partial class Form1 : Form
    {
        private string songsDirectory = "";
        private string[] extension = { "*.mp3", "*.wav", "*.mp3", "*.flac", "*.mp4", ".ogg",  ".ape"};
        private IEnumerable<string> songList;
        private List<string> listAudioFiles = new List<string>();

        private delegate void InvokeToListViewDelegate(string filename);

        //private delegate void InvokeToDataGridSourceDelegate(bool refreshOnly);

        private delegate void InvokeToProgressBarLabelDelegate(string sText);

        private delegate void InvokeToProgressBarDelegate(int nPercentage);

        private bool bInit = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnFolderBrowse_Click(object sender, EventArgs e)
        {
            txtAudioFolder.Text = BrowseForFolder(txtAudioFolder.Text);

            if (!String.IsNullOrEmpty(txtAudioFolder.Text))
            {
                //if (songsDirectory != txtAudioFolder.Text)                
                {
                    lvAudioFiles.Items.Clear();
                    songsDirectory = txtAudioFolder.Text;
                    songList = GetFiles(txtAudioFolder.Text, extension, chkSearchSubDirectories.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                    if (!songList.Any())
                    {
                        return;
                    }

                    LoadFilesIntoGrid(songsDirectory);
                }
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
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                return folderBrowserDialog1.SelectedPath;
            }

            return sExistingDirectory;
        }


        private void LoadFilesIntoGrid(string sMusicDirectory)
        {
            

            try
            {
                listAudioFiles.Clear();
                int nSong = 0;
                int nAllSongs = songList.Count();

                //3Tag _DummyID3 = new Id3Tag();

                foreach (var f in songList)
                {
                    //InvokeToProgressBarLabel("Reading In " + f.nSong + " of " + nAllSongs + " songs");

                    listAudioFiles.Add(f);
                    InvokeToListView(f);
                    InvokeToProgressBarLabel("Reading " + f);
                    nSong++;
                }


                InvokeToProgressBarLabel("Completed");
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
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
            try
            {
                bInit = Un4seen.Bass.Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, this.Handle);

                if (bInit)
                {
                    int handlePtr = Un4seen.Bass.Bass.BASS_StreamCreateFile(lvAudioFiles.SelectedItems[0].Text, 0, 0, BASSFlag.BASS_DEFAULT);

                    Un4seen.Bass.AddOn.Tags.TAG_INFO tagInfo = new Un4seen.Bass.AddOn.Tags.TAG_INFO(lvAudioFiles.SelectedItems[0].Text);
                    if (BassTags.BASS_TAG_GetFromFile(handlePtr, tagInfo))
                    {
                        Metadata metadataWindow = new Metadata(tagInfo);
                        metadataWindow.ShowDialog();
                    }


                    Un4seen.Bass.Bass.BASS_Free();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}