
namespace Slideshow.WPF.Services
{
    public interface IMediaService
    {
        void Play();
        void Pause();
        void Stop();
        void Rewind();
        void FastForward();
    }
}
