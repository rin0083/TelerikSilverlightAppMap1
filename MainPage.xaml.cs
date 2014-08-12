using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;
using System.Windows.Browser;
using Telerik.Windows.Controls.Input;
using ItemsControl = Telerik.Windows.Controls.ItemsControl;
using SelectionChangedEventArgs = Telerik.Windows.Controls.SelectionChangedEventArgs;
using TelerikSilverlightAppMap1.MapHelper;

namespace TelerikSilverlightAppMap1
{
    public partial class MainPage : UserControl
    {
        private string bingApplicationId = "AghJLZu7knk6ZkqH82ZjMjWM_b-gWhTlrD-A15qA72fVtSNpU9oz5ejC7vUxPCfd";
        private BingRouteProvider routeProvider;
        private LocationCollection routePoints = new LocationCollection();
        private LocationCollection routeResultPoints = new LocationCollection();
        private MapPolyline routeLine = new MapPolyline();
        private RadComboBox lineColorCombo =new RadComboBox();
        [ScriptableMember]
        public LocationCollection RouteLocations { get { return this.routePoints; } set { this.routePoints = value; } }
        [ScriptableMember]
        public int Zoom { get { return this.RadMap1.ZoomLevel; } }
        //[ScriptableMember]
        public LocationCollection RouteResultLocations { get { return this.routeResultPoints; } set { this.routeResultPoints = value; } }
        [ScriptableMember]
        public void NewLocation(double latitude, double longitude)
        {            
            RouteResultLocations.Add(new Location(latitude, longitude));
        }
        [ScriptableMember]
        public double LocationLatitude(Location location)
        {
            return location.Latitude;
        }
        [ScriptableMember]
        public double LocationLongitude(Location location)
        {
            return location.Longitude;
        }

        public MainPage()
        {
            InitializeComponent();
            Binding binding = new Binding();
            binding.Source = this.routePoints;
            this.informationLayer.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            //this.listBox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            //this.RadMap1.Provider = new BingMapCnProvider();
            //this.RadMap1.Provider = new OpenStreetMapProvider();
            //SetProvider();            
        }
        private void SetProvider()
        {
            // Init route provider.
            routeProvider = new BingRouteProvider();
            routeProvider.ApplicationId = this.bingApplicationId;
            routeProvider.MapControl = this.RadMap1;
            routeProvider.RoutingCompleted += new EventHandler<RoutingCompletedEventArgs>(Provider_RoutingCompleted);
        }

        private void MapMouseClick(object sender, MapMouseRoutedEventArgs eventArgs)
        {
            this.routePoints.Add(eventArgs.Location);
            //if (this.routePoints.Count > 1)
            //{
            //    HtmlPage.Window.Invoke("GetDirection");
            //}
        }

        private void FindRouteClicked(object sender, RoutedEventArgs e)
        {
            this.informationLayer2.Items.Clear();           
            //this.ErrorSummary.Visibility = Visibility.Collapsed;
            //RouteRequest routeRequest = new RouteRequest();
            //routeRequest.Culture = new System.Globalization.CultureInfo("zh-cn");
            //routeRequest.Options.RoutePathType = RoutePathType.Points;
            HtmlPage.Window.Invoke("GetDirection");
            if (this.routePoints.Count > 1)
            {
                this.findRouteButton.IsEnabled = false;
                //foreach (Location location in this.routePoints)
                //{
                //    routeRequest.Waypoints.Add(location);
                //}
                //this.routeProvider.CalculateRouteAsync(routeRequest);
                
                int a = RouteResultLocations.Count;
                RouteTest();
            }
        }

        private void RouteTest()
        {
            this.findRouteButton.IsEnabled = true;
            if (RouteResultLocations.Count != 0)
            {                
                routeLine.Points = RouteResultLocations;
                routeLine.Stroke = this.GetBrushFromCombo(lineColorCombo);
                routeLine.StrokeThickness = 2;
                this.informationLayer2.Items.Add(routeLine);
                RouteResultLocations.Clear();
            }
            else
            {
                this.ErrorSummary.Visibility = Visibility.Visible;
            }
        }

        private void Provider_RoutingCompleted(object sender, RoutingCompletedEventArgs e)
        {
            this.findRouteButton.IsEnabled = true;

            RouteResponse routeResponse = e.Response as RouteResponse;
            if (routeResponse != null &&
                routeResponse.Result != null &&
                routeResponse.Result.RoutePath != null)
            {
                routeLine.Points = routeResponse.Result.RoutePath.Points;
                routeLine.Stroke = this.GetBrushFromCombo(lineColorCombo);
                routeLine.StrokeThickness = 2;
                this.informationLayer2.Items.Add(routeLine);
            }
            else
            {
                this.ErrorSummary.Visibility = Visibility.Visible;
            }

        }

        private void ClearRouteClicked(object sender, RoutedEventArgs e)
        {
            this.findRouteButton.IsEnabled = true;

            this.routePoints.Clear();
            this.informationLayer2.Items.Clear();
            this.ErrorSummary.Visibility = Visibility.Collapsed;
        }

        private SolidColorBrush GetBrushFromCombo(RadComboBox comboBox)
        {
            ColorStringConverter converter = new ColorStringConverter();
            SolidColorBrush brush = converter.ConvertBack("Red",
                typeof(SolidColorBrush),
                null,
                System.Globalization.CultureInfo.CurrentUICulture) as SolidColorBrush;
            return brush;
        }

        private void LineColorChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.routeLine != null)
            {
                //lineColorCombo.SelectedValue = "Red";
                SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                routeLine.Stroke = brush;
            }
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
                //LocationRect bestView = this.worldLayer.GetBestView(this.worldLayer.Items);
                //this.RadMap1.SetView(bestView);
            }
        }
    
    }
}
