using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using System.Threading.Tasks;

namespace MFTSpike
{

    public sealed partial class MainPage : Page
    {
        public MediaCapture m_camera;


        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            m_camera = new MediaCapture();
            m_camera.InitializeAsync();
        }
        
  
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            capture_element.Source = m_camera;
            m_camera.StartPreviewAsync();
            m_camera.AddEffectAsync(MediaStreamType.VideoPreview, "GrayscaleTransform.GrayscaleEffect", null);
        }
    }
}
