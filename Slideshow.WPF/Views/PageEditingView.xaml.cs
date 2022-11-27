using Slideshow.WPF.Services;
using System;
using System.Windows.Controls;

namespace Slideshow.WPF.Views
{
    /// <summary>
    /// Interaction logic for PageEditingView
    /// </summary>
    public partial class PageEditingView : UserControl, IMediaService
    {
        public PageEditingView()
        {
            InitializeComponent();
        }

        void IMediaService.FastForward()
        {
            this.MediaPlayer.Position += TimeSpan.FromSeconds(10);
        }

        void IMediaService.Pause()
        {
            this.MediaPlayer.Pause();
        }

        void IMediaService.Play()
        {
            this.MediaPlayer.Position = TimeSpan.Zero;
            this.MediaPlayer.Visibility = System.Windows.Visibility.Visible;
            this.MediaPlayer.LoadedBehavior = MediaState.Manual;

            this.MediaPlayer.Play();
        }

        void IMediaService.Rewind()
        {
            this.MediaPlayer.Position -= TimeSpan.FromSeconds(10);
        }

        void IMediaService.Stop()
        {
            this.MediaPlayer.Visibility = System.Windows.Visibility.Visible;
            this.MediaPlayer.LoadedBehavior = MediaState.Manual;

            this.MediaPlayer.Stop();
        }
    }
}
