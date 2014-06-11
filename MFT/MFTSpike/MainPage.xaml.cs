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
using Windows.Storage;

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
            
            //var file = Windows.Storage.KnownFolders.CameraRoll.CreateFileAsync("_exposure.jpg", CreationCollisionOption.ReplaceExisting);
            //m_camera.StartRecordToStorageFileAsync(Windows.Media.MediaProperties.MediaEncodingProfile.CreateAvi(Windows.Media.MediaProperties.VideoEncodingQuality.Vga), );
            
            m_camera.AddEffectAsync(MediaStreamType.VideoPreview, "GrayscaleTransform.GrayscaleEffect", null);

            float maxExp = m_camera.VideoDeviceController.ExposureCompensationControl.Max;
            float minExp = m_camera.VideoDeviceController.ExposureCompensationControl.Min;
            float step = m_camera.VideoDeviceController.ExposureCompensationControl.Step;

            TimeSpan maxExpControl = m_camera.VideoDeviceController.ExposureControl.Max;
            TimeSpan minExpControl = m_camera.VideoDeviceController.ExposureControl.Min;
            TimeSpan stepExp = m_camera.VideoDeviceController.ExposureControl.Step;

            TimeSpan value = m_camera.VideoDeviceController.ExposureControl.Value;
            TimeSpan k = new TimeSpan(83333);
            
            m_camera.VideoDeviceController.ExposureControl.SetValueAsync(k);
            
            //m_camera.VideoDeviceController.ExposureCompensationControl.SetValueAsync(0);
            //m_camera.VideoDeviceController.ExposureCompensationControl.SetValueAsync(0.0f);

            //double exp;
            //bool b = m_camera.VideoDeviceController.Exposure.TryGetValue(out exp);

           // m_camera.VideoDeviceController.ExposureControl.SetValueAsync(new TimeSpan(1))
            //m_camera.VideoDeviceController.Exposure.TrySetValue(0.00833);
            //m_camera.VideoDeviceController.ExposureControl.SetAutoAsync(false);
            //m_camera.VideoDeviceController.ExposureControl.SetValueAsync(new TimeSpan(10 * 2000));

          //  m_camera.VideoDeviceController.ExposureControl.SetValueAsync(minExpControl);

        }
    }
}
