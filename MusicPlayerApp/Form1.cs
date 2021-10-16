using System;
using System.Windows.Forms;

namespace MusicPlayerApp
{
    public partial class frmMusicPlayer : Form
    {
        string[] paths, files;

        public frmMusicPlayer()
        {
            InitializeComponent();

            trackVolume.Value = 50;
            lblMaximumVolume.Text = "50%";
        }

        private void trackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mediaPlayer.URL = paths[trackList.SelectedIndex];
            mediaPlayer.Ctlcontrols.play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.stop();
            progressBar.Value = 0;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            mediaPlayer.Ctlcontrols.play();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (trackList.SelectedIndex < trackList.Items.Count - 1){
                trackList.SelectedIndex = trackList.SelectedIndex + 1;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (trackList.SelectedIndex > 0)
            {
                trackList.SelectedIndex = trackList.SelectedIndex - 1;
            }
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar.Maximum = (int)mediaPlayer.Ctlcontrols.currentItem.duration;
                progressBar.Value = (int)mediaPlayer.Ctlcontrols.currentPosition;
            }

            try
            {
                lblTrackStart.Text = mediaPlayer.Ctlcontrols.currentPositionString;
                lblTrackEnd.Text = mediaPlayer.Ctlcontrols.currentItem.durationString.ToString();
            }
            catch
            {

            }
        }

        private void trackVolume_Scroll(object sender, EventArgs e)
        {
            mediaPlayer.settings.volume = trackVolume.Value;
            lblMaximumVolume.Text = trackVolume.Value.ToString() + "%";
        }

        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            mediaPlayer.Ctlcontrols.currentPosition = mediaPlayer.currentMedia.duration * e.X / progressBar.Width;
        }

        private void frmMusicPlayer_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Musiqi pleyerinə xoş gəldiniz!");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                files = openFileDialog.FileNames;
                paths = openFileDialog.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    trackList.Items.Add(files[i]);
                }
            }
        }
    }
}
