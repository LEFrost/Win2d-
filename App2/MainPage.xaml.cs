using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CanvasBitmap cb;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {

            GaussianBlurEffect gbe = new GaussianBlurEffect()
            {
                Source = cb,
                BlurAmount = 10
            };
            Rect rect = new Rect(50, 50, Width, Height);

            args.DrawingSession.DrawEllipse(500, 500, 80, 56, Colors.Red);
            args.DrawingSession.DrawImage(offscreen, 23, 34);
        }
        CanvasRenderTarget offscreen;
        private void CanvasControl_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }
        async Task CreateResourcesAsync(CanvasControl sender)
        {
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/2.png", UriKind.RelativeOrAbsolute));
            ////using (IRandomAccessStream stream =file.OpenReadAsync().GetResults())
            ////{
            ////}
            //BitmapImage bi = new BitmapImage(new Uri(@"ms-appx:///Assets/2.png", UriKind.RelativeOrAbsolute));
            //using (IRandomAccessStreamWithContentType stream = file.OpenReadAsync().GetResults())
            //{
            //    IRandomAccessStream stream1 = stream.AsStreamForWrite().AsRandomAccessStream();
            //    byte[] by = new byte[stream.Size];
            //    await stream.ReadAsync(by.AsBuffer(), (uint)stream.Size, InputStreamOptions.ReadAhead);

            //    //await bi1.SetSourceAsync(stream1);
            //}
            cb = await CanvasBitmap.LoadAsync(sender, new Uri(@"ms-appx:///Assets/2.png"));
            CanvasDevice device = CanvasDevice.GetSharedDevice();
            offscreen = new CanvasRenderTarget(device,500, 500, 96);
            using (CanvasDrawingSession ds = offscreen.CreateDrawingSession())
            {
                ds.DrawImage(cb);
            }
        }
    }
}
