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
using TelerikSilverlightAppMap1.MapFactory;

namespace TelerikSilverlightAppMap1
{
    public partial class MainPage : UserControl
    {
        private string bingApplicationId = "AghJLZu7knk6ZkqH82ZjMjWM_b-gWhTlrD-A15qA72fVtSNpU9oz5ejC7vUxPCfd";
        private BingRouteProvider routeProvider;

        private LocationCollection routeResultPoints = new LocationCollection();
        private MapPolyline routeLine = new MapPolyline();


        private MapFactory.MapMarkFactory.Assemble markPointsAssemble = new MapMarkFactory.Assemble();
        private MapFactory.MapMarkFactory.MapMarkPoints markPoints = new MapMarkFactory.Assemble().MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).InitializeMapMarkPoints();
        private MapFactory.MapMarkFactory.MapMarkPoints routePoints = new MapMarkFactory.Assemble().MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).GetEmptyMapMarkPoints();
        private bool firstRoutePointChecked;

        MapFactory.MapTimerFacotry.IMapTimer blinkTimer = new MapFactory.MapTimerFacotry.Assemble().MapTimerHandle(FactoryCommand.MapTimerCommand.BlinkDispatcherTimer);

        private RadComboBox lineColorCombo =new RadComboBox();
        [ScriptableMember]
        public MapFactory.MapPointFactory.MapPoints RoutePoints { get { return this.routePoints.MarkPoints; } set { this.routePoints.MarkPoints = value; } }
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
        public double LocationLatitude(MapFactory.MapPointFactory.MapPoint point)
        {
            return point.Location.Latitude;
        }
        [ScriptableMember]
        public double LocationLongitude(MapFactory.MapPointFactory.MapPoint point)
        {
            return point.Location.Longitude;
        }

        public MainPage()
        {
            InitializeComponent();
            BingMapCnProvider bmcp = new BingMapCnProvider();
            this.RadMap1.Provider = bmcp.Provider;
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
            if (this.routePoints.MarkPoints.Count != 0)
            {
                if (this.firstRoutePointChecked == false)
                {
                    this.firstRoutePointChecked = true;
                }
                else
                {
                    this.routePoints.MarkPoints.Add(new MapFactory.MapPointFactory.MapPoint(eventArgs.Location.Latitude, eventArgs.Location.Longitude, "目的地" + this.routePoints.MarkPoints.Count.ToString(), null));
                }
            }
            int count = this.routePoints.MarkPoints.Count;
            //if (this.routePoints.Count > 1)
            //{
            //    HtmlPage.Window.Invoke("GetDirection");
            //}
        }

        private void FindRouteClicked(object sender, RoutedEventArgs e)
        {
            RadButton mybutton = sender as RadButton;

            this.informationLayer3.Items.Clear();
            //this.ErrorSummary.Visibility = Visibility.Collapsed;
            //RouteRequest routeRequest = new RouteRequest();
            //routeRequest.Culture = new System.Globalization.CultureInfo("zh-cn");
            //routeRequest.Options.RoutePathType = RoutePathType.Points;

            if (mybutton.Content.ToString() == "Initialize Route")
            {
                HtmlPage.Window.Invoke("GetDirection");
                if (this.routePoints.MarkPoints.Count > 1)
                {
                    this.findRouteButton.IsEnabled = false;
                    //foreach (Location location in this.routePoints)
                    //{
                    //    routeRequest.Waypoints.Add(location);
                    //}
                    //this.routeProvider.CalculateRouteAsync(routeRequest);


                    RouteTest();
                }
                mybutton.Content = "Start Route";
            }
            else
            {
                HtmlPage.Window.Invoke("GetDirection");
                if (this.routePoints.MarkPoints.Count > 1)
                {
                    this.findRouteButton.IsEnabled = false;
                    //foreach (Location location in this.routePoints)
                    //{
                    //    routeRequest.Waypoints.Add(location);
                    //}
                    //this.routeProvider.CalculateRouteAsync(routeRequest);


                    RouteTest();
                }
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
                this.informationLayer3.Items.Add(routeLine);
                RouteResultLocations.Clear();
            }
            //else
            //{
            //    this.ErrorSummary.Visibility = Visibility.Visible;
            //}
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
            this.firstRoutePointChecked = false;
            this.routePoints.MarkPoints.Clear();
            this.informationLayer3.Items.Clear();
            this.ErrorSummary.Visibility = Visibility.Collapsed;
            this.blinkTimer.EndTimer(sender, e);
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

        private void ShowAmbulanceClicked(object sender, RoutedEventArgs e)
        {
            markPointsAssemble.MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).AddMapMarkPoint(new MapFactory.MapPointFactory.MapPoint(30.0332255425012, 120.88011828324, null, null), this.markPoints);
            MapFactory.MapMarkFactory.TelerikMapMark telerikmapmark = new MapMarkFactory.TelerikMapMark();
            MapFactory.MapMarkFactory.IMapMark mapMark = telerikmapmark;
            mapMark.MarkPointsCompleted += MapMarkHandle;
            telerikmapmark.OnMarkPointsCompleted(new MapMarkFactory.MarkPointsCompletedEventArgs(this.markPoints));
            LocationRect bestView = this.informationLayer.GetBestView(this.informationLayer.Items);
            this.RadMap1.SetView(bestView);
        }

        private void MapMarkHandle(object sender, MapFactory.MapMarkFactory.MarkPointsCompletedEventArgs e)
        {
            Binding binding = new Binding();
            binding.Source = e.CompletedMapMarkPoints.MarkPoints;
            //binding.Source = this.routePoints;
            this.informationLayer.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }


        private void ClearMarkClicked(object sender, RoutedEventArgs e)
        {
            this.markPointsAssemble.MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).ClearMapMarkPoint(this.markPoints);
        }

        private void Mark_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid control = sender as Grid;
            MapFactory.MapPointFactory.MapPoint point = control.DataContext as MapFactory.MapPointFactory.MapPoint;
            int count = this.markPoints.MarkPoints.Count;
            this.routePoints.MarkPoints.Clear();
            this.routePoints.MarkPoints.Add(point);
     
            Binding binding = new Binding();
            binding.Source = this.routePoints.MarkPoints;
            this.informationLayer2.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            blinkTimer.Element = control;
            blinkTimer.StartTimer(sender, e);
        }
    
    }
}
