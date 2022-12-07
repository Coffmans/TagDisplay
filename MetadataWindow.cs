using System.Text;
using Un4seen.Bass.AddOn.Tags;

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

            try
            {
                if (_tagInfo.PictureCount > 0)
                {
                    TagPicture picture = _tagInfo.PictureGet(0);
                    if (picture.PictureStorage == TagPicture.PICTURE_STORAGE.Internal)
                    {
                        MemoryStream ms = new MemoryStream(picture.Data);
                        Image returnImage = Image.FromStream(ms);
                        pictureBox1.Image = returnImage.GetThumbnailImage(150, 150, null, System.IntPtr.Zero);
                        //pictureBox1.Image = returnImage;
                    }
                    else
                    {
                        txtArtworkUrl.Text = System.Text.Encoding.UTF8.GetString(picture.Data, 0, picture.Data.Length);
                    }

                    txtArtworkType.Text = GetArtworkTypeInStringFormat(picture.PictureType);
                    txtArtworkDescription.Text = picture.Description;
                    txtArtworkMimeType.Text = picture.MIMEType;
                }
                else
                {
                    groupBoxArtwork.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
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

        private string GetArtworkTypeInStringFormat(TagPicture.PICTURE_TYPE type)
        {
            switch (type)
            {
                case TagPicture.PICTURE_TYPE.Artists:
                    return "Artists";
                case TagPicture.PICTURE_TYPE.BackAlbumCover:
                    return "Back Album Covert";
                case TagPicture.PICTURE_TYPE.BandLogo:
                    return "Band Logo";
                case TagPicture.PICTURE_TYPE.ColoredFish:
                    return "Colored Fish";
                case TagPicture.PICTURE_TYPE.Composer:
                    return "Composer";
                case TagPicture.PICTURE_TYPE.Conductor:
                    return "Conductor";
                case TagPicture.PICTURE_TYPE.FrontAlbumCover:
                    return "Front Album Cover";
                case TagPicture.PICTURE_TYPE.Icon32:
                    return "Icon";
                case TagPicture.PICTURE_TYPE.Illustration:
                    return "Illustration";
                case TagPicture.PICTURE_TYPE.LeadArtist:
                    return "Lead Artist";
                case TagPicture.PICTURE_TYPE.LeafletPage:
                    return "Leaflet Page";
                case TagPicture.PICTURE_TYPE.Media:
                    return "Media";
                case TagPicture.PICTURE_TYPE.Location:
                    return "Location";
                case TagPicture.PICTURE_TYPE.Orchestra:
                    return "Orchestra";
                case TagPicture.PICTURE_TYPE.OtherIcon:
                    return "Other Icon";
                case TagPicture.PICTURE_TYPE.PublisherLogo:
                    return "Publisher Logo";
                case TagPicture.PICTURE_TYPE.RecordingSession:
                    return "Recording Session";
                case TagPicture.PICTURE_TYPE.VideoCapture:
                    return "Video Capture";
                case TagPicture.PICTURE_TYPE.Writer:
                    return "Writer";
                default:
                    return "Unknown";
            }
        }
    }
}
