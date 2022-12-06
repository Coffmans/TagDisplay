namespace TagDisplay
{
    public partial class MetadataWindow : Form
    {
        private Un4seen.Bass.AddOn.Tags.TAG_INFO _tagInfo;

        public MetadataWindow(Un4seen.Bass.AddOn.Tags.TAG_INFO fileMetadataTagInfo)
        {
            InitializeComponent();

            _tagInfo = fileMetadataTagInfo;
        }

        private void Metadata_Load(object sender, EventArgs e)
        {
            if (_tagInfo.NativeTags.Any())
            {
                foreach (var tag in _tagInfo.NativeTags)
                {
                    txtNativeTags.Text += tag.ToString() + "\r\n";
                }
            }

            txtTitle.Text = _tagInfo.title;
            txtAlbum.Text = _tagInfo.album;
            txtArtist.Text = _tagInfo.artist;
            txtAlbumArtist.Text = _tagInfo.albumartist;
            txtComments.Text = _tagInfo.comment;
            txtComposer.Text = _tagInfo.composer;
            txtConductor.Text = _tagInfo.conductor;
            txtBitRate.Text = _tagInfo.bitrate.ToString();
            txtCopyRight.Text = _tagInfo.copyright;

            TimeSpan tsDuration = TimeSpan.FromSeconds(_tagInfo.duration);
            txtDuration.Text = tsDuration.ToString(@"hh\:mm\:ss");
            
            txtFileName.Text = _tagInfo.filename;
            txtGenre.Text = _tagInfo.genre;
            txtGrouping.Text = _tagInfo.grouping;
            txtISRC.Text = _tagInfo.isrc;
            txtLyrist.Text = _tagInfo.lyricist;
            txtMood.Text = _tagInfo.mood;
            txtProducer.Text = _tagInfo.producer;
            txtPublisher.Text = _tagInfo.publisher;
            txtRating.Text = _tagInfo.rating;
            txtRemixer.Text = _tagInfo.remixer;
            txtTrack.Text = _tagInfo.track;
            txtYear.Text = _tagInfo.year;

            txtEncodedBy.Text = _tagInfo.encodedby;
            txtDisc.Text = _tagInfo.disc;

            
            var channelInfo = _tagInfo.channelinfo;

            this.Text = channelInfo.ToString();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
