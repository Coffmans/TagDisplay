using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagDisplay
{
    public partial class Metadata : Form
    {
        private Un4seen.Bass.AddOn.Tags.TAG_INFO tagInfo = new Un4seen.Bass.AddOn.Tags.TAG_INFO();

        public Metadata(Un4seen.Bass.AddOn.Tags.TAG_INFO fileMetadataTagInfo)
        {
            InitializeComponent();

            tagInfo = fileMetadataTagInfo;
        }

        private void Metadata_Load(object sender, EventArgs e)
        {
            if (tagInfo.NativeTags.Any())
            {
                foreach (var tag in tagInfo.NativeTags)
                {
                    txtNativeTags.Text += tag.ToString() + "\r\n";
                }
            }

            txtTitle.Text = tagInfo.title;
            txtAlbum.Text = tagInfo.album;
            txtArtist.Text = tagInfo.artist;
            txtComment.Text = tagInfo.comment;
            txtConductor.Text = tagInfo.conductor;
            txtBitRate.Text = tagInfo.bitrate.ToString();
            txtCopyRight.Text = tagInfo.copyright;
            txtDuration.Text = tagInfo.duration.ToString();
            txtFileName.Text = tagInfo.filename;
            txtGenre.Text = tagInfo.genre;
            txtGrouping.Text = tagInfo.grouping;
            txtISRC.Text = tagInfo.isrc;
            txtLyrist.Text = tagInfo.lyricist;
            txtMood.Text = tagInfo.mood;
            txtProducer.Text = tagInfo.producer;
            txtPublisher.Text = tagInfo.publisher;
            txtRating.Text = tagInfo.rating;
            txtRemixer.Text = tagInfo.remixer;
            txtTrack.Text = tagInfo.track;
            txtYear.Text = tagInfo.year;

        }
    }
}
