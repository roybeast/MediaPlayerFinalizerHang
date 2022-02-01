using System;
using System.Windows;
using System.Windows.Media;

namespace MediaPlayerFinalizerHang.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _width = 1280;
            _height = 720;
            _rect = new Rect(new Size(_width, _height));
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            PlayGoodVideo();
        }

        public string Title
        {
            get => VerifyDispatcher(_title);
            private set => SetPropertyField(value, ref _title);
        }

        public void PlayGoodVideo()
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Open(new Uri("goodVideo.mp4", UriKind.Relative));
            _mediaPlayer.Play();
            Title = "Now playing Good Video";
        }

        public void PlayBadVideo()
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Open(new Uri("badVideo.mp4", UriKind.Relative));
            _mediaPlayer.Play();
            Title = "Now playing Bad Video";
        }

        public double Width
        {
            get => VerifyDispatcher(_width);
        }

        public double Height
        {
            get => VerifyDispatcher(_height);
        }

        public Rect Rect
        {
            get => VerifyDispatcher(_rect);
        }

        public MediaPlayer MediaPlayer
        {
            get => VerifyDispatcher(_mediaPlayer);
            private set => SetPropertyField(value, ref _mediaPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    _mediaPlayer.MediaEnded -= MediaPlayer_MediaEnded;
                    _mediaPlayer.Stop();
                    _mediaPlayer.Close();
                    MediaPlayer = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            // Keep looping
            _mediaPlayer.Position = TimeSpan.Zero;
            _mediaPlayer.Play();
        }

        string _title;
        MediaPlayer _mediaPlayer;
        double _width;
        double _height;
        Rect _rect;
    }
}
