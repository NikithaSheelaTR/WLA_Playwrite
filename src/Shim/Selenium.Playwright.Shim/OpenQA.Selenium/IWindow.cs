using System.Drawing;

namespace OpenQA.Selenium
{
    public interface IWindow
    {
        Point Position { get; set; }
        Size Size { get; set; }
        void Maximize();
        void Minimize();
        void FullScreen();
    }
}
