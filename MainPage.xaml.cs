using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;


namespace TelerikSilverlightAppMap1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
           
            InitializeComponent();
            //ShapeFileViewModel bb = new ShapeFileViewModel();
            //Uri aa = new Uri("/TelerikSilverlightAppMap1;component/SHPTest/Frame.shp", UriKind.Relative);
            //Stream a = Application.GetResourceStream(aa).Stream;
            //int c = 1;
            //img1.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("http://192.168.1.164:8088//SHPTest//test.png", UriKind.Absolute));
            //Shape1.Source = new Uri("http://192.168.1.164:8088//SHPTest//ESA_points_r24.shp", UriKind.Absolute);
            //Shape1.DataSource = new Uri("http://192.168.1.164:8088//SHPTest//ESA_points_r24.dbf", UriKind.Absolute);
           
        }

        private void MapShapeReader_PreviewReadCompleted(object sender, PreviewReadShapesCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                // e.Items contains the list of objects which are created by MapShapeReader
                foreach (object item in e.Items)
                {
                    MapShape shape = item as MapShape;
                    if (shape != null)
                    {
                        shape.Fill = new SolidColorBrush(Color.FromArgb(0x7f, 0x1f, 0x7f, 0x3f));
                        shape.StrokeThickness = 3;
                        shape.Stroke = new SolidColorBrush(Colors.Blue);
                    }
                }
            }
        }

        private void MapShapeReader_ReadCompleted(object sender, ReadShapesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                LocationRect bestView = this.europeLayer.GetBestView(this.europeLayer.Items);
                this.radMap.SetView(bestView);
            }
        }
        
    }
}
