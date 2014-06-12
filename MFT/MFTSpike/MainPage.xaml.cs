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

        //public FileIO io = new FileIO()

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        private async void Button_ClickStop(object sender, RoutedEventArgs e)
        {
            m_camera.StopRecordAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            capture_element.Source = m_camera;
            m_camera.StartPreviewAsync();
            
            //StorageFile file = await KnownFolders.DocumentsLibrary.CreateFileAsync("_exposure.jpg", CreationCollisionOption.ReplaceExisting);
            
            
            m_camera.AddEffectAsync(MediaStreamType.VideoPreview, "GrayscaleTransform.GrayscaleEffect", null);
            
          
            //m_camera.StartRecordToStorageFileAsync()
            float maxExp = m_camera.VideoDeviceController.ExposureCompensationControl.Max;
            float minExp = m_camera.VideoDeviceController.ExposureCompensationControl.Min;
            float step = m_camera.VideoDeviceController.ExposureCompensationControl.Step;

            TimeSpan maxExpControl = m_camera.VideoDeviceController.ExposureControl.Max;
            TimeSpan minExpControl = m_camera.VideoDeviceController.ExposureControl.Min;
            TimeSpan stepExp = m_camera.VideoDeviceController.ExposureControl.Step;

            TimeSpan value = m_camera.VideoDeviceController.ExposureControl.Value;
             var supportedVideoFormats = new List<string> { "nv12", "rgb32" };

             var availableMediaStreamProperties = m_camera.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoRecord).OfType<Windows.Media.MediaProperties.VideoEncodingProperties>().Where(p => p != null 
                && !String.IsNullOrEmpty(p.Subtype) 
                && supportedVideoFormats.Contains(p.Subtype.ToLower()))
            .ToList();

             Windows.Media.MediaProperties.VideoEncodingProperties previewFormat = availableMediaStreamProperties.FirstOrDefault();

            //previewFormat.FrameRate.Numerator = 15;
            //previewFormat.FrameRate.Denominator = 1;

             //availableMediaStreamProperties.All;
            m_camera.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoRecord, previewFormat );

            m_camera.VideoDeviceController.ExposureControl.SetAutoAsync(false);

            TimeSpan k = new TimeSpan(833);
            m_camera.VideoDeviceController.ExposureControl.SetValueAsync(minExpControl);

            await this.StreamToFile();

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

        private async Task StreamToFile()
        {
            try
            {
              
                StorageFile file = await KnownFolders.VideosLibrary.CreateFileAsync("file.mp4", CreationCollisionOption.ReplaceExisting);
                await m_camera.StartRecordToStorageFileAsync(Windows.Media.MediaProperties.MediaEncodingProfile.CreateMp4(Windows.Media.MediaProperties.VideoEncodingQuality.HD720p), file);
            }
            catch
            {
                int i;
            }
        }
    }
}
