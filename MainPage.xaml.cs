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
<<<<<<< HEAD
=======
using TelerikSilverlightAppMap1.MapHelper;
>>>>>>> origin/master
using TelerikSilverlightAppMap1.MapFactory;

namespace TelerikSilverlightAppMap1
{
    public partial class MainPage : UserControl
    {
<<<<<<< HEAD
        //private string bingApplicationId = "AghJLZu7knk6ZkqH82ZjMjWM_b-gWhTlrD-A15qA72fVtSNpU9oz5ejC7vUxPCfd";
        //private BingRouteProvider routeProvider;


        private static RadMap radmap;
        /// <summary>
        /// 成员变量，以经纬坐标组存储寻路结果
        /// </summary>
        private LocationCollection routeResultPoints = new LocationCollection();
        /// <summary>
        /// 成员变量，通过XML文件初始化热点列表
        /// </summary>
        private MapFactory.MapMarkFactory.MapMarkPoints markPoints = new MapMarkFactory.Assemble().MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).InitializeMapMarkPoints();
        /// <summary>
        /// 成员变量，初始化寻路点列表
        /// </summary>
        private MapFactory.MapMarkFactory.MapMarkPoints routePoints = new MapMarkFactory.Assemble().MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).GetEmptyMapMarkPoints();
        /// <summary>
        /// 成员变量，判断寻路出发点是否已获得
        /// </summary>
        private bool firstRoutePointChecked;
        /// <summary>
        /// 地图标记接口
        /// </summary>
        private MapFactory.MapMarkFactory.IMapMark Imapmark = new MapFactory.MapMarkFactory.Assemble().MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark);
        /// <summary>
        /// 地图供应商接口
        /// </summary>
        private MapFactory.MapProviderFactory.IMapProvider Imapprovider = new MapFactory.MapProviderFactory.Assemble().MapProviderHandle(FactoryCommand.MapProviderCommand.BingCNMapProvider);
        /// <summary>
        /// 地图寻路接口
        /// </summary>
        private MapFactory.MapRouteFactory.IMapRoute Imaproute = new MapFactory.MapRouteFactory.Assemble().MapRouteHandle(FactoryCommand.MapRouteCommand.BingRoute);
        /// <summary>
        /// 地图异步定时器接口
        /// </summary>
        private MapFactory.MapTimerFacotry.IMapTimer IblinkTimer = new MapFactory.MapTimerFacotry.Assemble().MapTimerHandle(FactoryCommand.MapTimerCommand.BlinkDispatcherTimer);
        /// <summary>
        /// 地图调色器接口
        /// </summary>
        private MapFactory.MapColorFactory.IMapColor Icolor = new MapFactory.MapColorFactory.Assemble().MapColorHandle(FactoryCommand.MapColorCommand.MapSolidColorBrush);
        /// <summary>
        /// 地图线条对象接口
        /// </summary>
        private MapFactory.MapLineFactory.IMapPolyline Ipolyline = new MapFactory.MapLineFactory.Assemble().PolylineHandle(FactoryCommand.LineCommand.TelerikPolyline);
        /// <summary>
        /// 地图点对象接口
        /// </summary>
        private MapFactory.MapPointFactory.IPoint Ipoint = new MapFactory.MapPointFactory.Assemble().PointHandle(FactoryCommand.PointCommand.PointMethod);
        /// <summary>
        /// 地图动画接口
        /// </summary>
        private MapFactory.MapAnimationFactory.IMapAnimation ImapAnimation = new MapFactory.MapAnimationFactory.Assemble().MapAnimationHandle(FactoryCommand.MapAnimationCommand.PolyLineAnimation);
        //private RadComboBox lineColorCombo =new RadComboBox();
        /// <summary>
        /// 将寻路点组暴露给JS
        /// </summary>
        [ScriptableMember]
        public MapFactory.MapPointFactory.MapPoints RoutePoints { get { return this.routePoints.MarkPoints; } set { this.routePoints.MarkPoints = value; } }
        /// <summary>
        /// 将地图缩放级别暴露给JS
        /// </summary>
=======
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
>>>>>>> origin/master
        [ScriptableMember]
        public int Zoom { get { return this.RadMap1.ZoomLevel; } }
        /// <summary>
        /// 声明经纬坐标组以存储寻路结果
        /// </summary>
        public LocationCollection RouteResultLocations { get { return this.routeResultPoints; } set { this.routeResultPoints = value; } }
        /// <summary>
        /// 将经纬坐标点添入寻路结果坐标组
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        [ScriptableMember]
        public void NewLocation(double latitude, double longitude)
        {            
            RouteResultLocations.Add(new Location(latitude, longitude));
        }
        /// <summary>
        /// 从MapPoint对象提取纬度
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [ScriptableMember]
        public double LocationLatitude(MapFactory.MapPointFactory.MapPoint point)
        {
<<<<<<< HEAD
            return Ipoint.GetLatitude(point);
=======
            return point.Location.Latitude;
>>>>>>> origin/master
        }
        /// <summary>
        /// 从MapPoint对象提取经度
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [ScriptableMember]
        public double LocationLongitude(MapFactory.MapPointFactory.MapPoint point)
        {
<<<<<<< HEAD
            return Ipoint.GetLongitude(point);
=======
            return point.Location.Longitude;
>>>>>>> origin/master
        }

        public MainPage()
        {
            InitializeComponent();
<<<<<<< HEAD
            radmap = this.RadMap1;
            this.RadMap1.Provider = Imapprovider.GetTiledProvider();
           
=======
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
>>>>>>> origin/master
        }


        /// <summary>
        /// 地图鼠标点击事件，目的为成功选择热点如警车后，点击地图任意点增加热点的寻路目的地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void MapMouseClick(object sender, MapMouseRoutedEventArgs eventArgs)
        {
            if (this.routePoints.MarkPoints.Count != 0)
            {
<<<<<<< HEAD
                this.firstRoutePointChecked = Imapmark.AddMapMarkPoint(new MapFactory.MapPointFactory.MapPoint(eventArgs.Location.Latitude, eventArgs.Location.Longitude, "目的地" + this.routePoints.MarkPoints.Count.ToString(), null), this.routePoints, this.firstRoutePointChecked);
            }    
            
=======
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
>>>>>>> origin/master
        }
        /// <summary>
        /// 当出发点与目的地同时存在时，点击触发寻路事件，调用JS中的Bing寻路方法，获取经纬坐标组作为寻路结果，每次加载页面要初始化后使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindRouteClicked(object sender, RoutedEventArgs e)
        {
            RadButton mybutton = sender as RadButton;
<<<<<<< HEAD
            MapPolyline mymappolyline = Ipolyline.GetMapPolyline(RouteResultLocations, Icolor.GetBrushFromCombo(255, 255, 0, 0), 2);
            if (this.routePoints.MarkPoints.Count > 1)
            {
                if (mybutton.Content.ToString() == "Initialize Route")
                {
                    Imaproute.JsRoute("GetDirection", this.routePoints, RouteResultLocations, mymappolyline, this.informationLayer3);
                    mybutton.Content = "Start Route";
                }
                else
                {
                    
                    Imaproute.JsRoute("GetDirection", this.routePoints, RouteResultLocations, mymappolyline, this.informationLayer3);

                    
                }
            }
=======

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
>>>>>>> origin/master
        }




        /// <summary>
        /// 清除寻路点与寻路结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearRouteClicked(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (this.informationLayer4 != null && this.informationLayer4.Items.Count > 0)
            {
                ImapAnimation.StopAnimation();
                this.informationLayer4.Items.Clear();
            }
=======
            this.findRouteButton.IsEnabled = true;
            this.firstRoutePointChecked = false;
            this.routePoints.MarkPoints.Clear();
            this.informationLayer3.Items.Clear();
            this.ErrorSummary.Visibility = Visibility.Collapsed;
            this.blinkTimer.EndTimer(sender, e);
        }
>>>>>>> origin/master


            if (this.routePoints.MarkPoints.Count>0)
            {
                this.findRouteButton.IsEnabled = true;
                this.firstRoutePointChecked = false;
                this.routePoints.MarkPoints.Clear();
                this.informationLayer3.Items.Clear();
                this.ErrorSummary.Visibility = Visibility.Collapsed;
                this.IblinkTimer.EndTimer(sender, e);
            }

        }

  
        /// <summary>
        /// 加载Shape文件的前置函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 加载Shape文件的后置函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapShapeReader_ReadCompleted(object sender, ReadShapesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //LocationRect bestView = this.worldLayer.GetBestView(this.worldLayer.Items);
                //this.RadMap1.SetView(bestView);
<<<<<<< HEAD
            }
            else
            { 
              
            }
        }

        /// <summary>
        /// 在地图上显示热点，热点列表存储在本地XML中，当然也可通过数据库等方式获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAmbulanceClicked(object sender, RoutedEventArgs e)
        {
            //markPointsAssemble.MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).AddMapMarkPoint(new MapFactory.MapPointFactory.MapPoint(30.0332255425012, 120.88011828324, null, null), this.markPoints,true);
            this.markPoints = Imapmark.InitializeMapMarkPoints();
=======
            }            
        }

        private void ShowAmbulanceClicked(object sender, RoutedEventArgs e)
        {
            markPointsAssemble.MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).AddMapMarkPoint(new MapFactory.MapPointFactory.MapPoint(30.0332255425012, 120.88011828324, null, null), this.markPoints);
>>>>>>> origin/master
            MapFactory.MapMarkFactory.TelerikMapMark telerikmapmark = new MapMarkFactory.TelerikMapMark();
            MapFactory.MapMarkFactory.IMapMark mapMark = telerikmapmark;
            mapMark.MarkPointsCompleted += MapMarkHandle;
            telerikmapmark.OnMarkPointsCompleted(new MapMarkFactory.MarkPointsCompletedEventArgs(this.markPoints));
<<<<<<< HEAD
            List<double> a = new List<double>();
            for (int i = 0; i < this.markPoints.MarkPoints.Count-1; i++)
            { 
               a.Add(Ipolyline.GetLineLength(this.markPoints.MarkPoints[i].Location,this.markPoints.MarkPoints[i+1].Location,this.RadMap1));
            }

        }

        /// <summary>
        /// 通过MarkPointsCompleted事件将热点组与UI绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
=======
            LocationRect bestView = this.informationLayer.GetBestView(this.informationLayer.Items);
            this.RadMap1.SetView(bestView);
        }

>>>>>>> origin/master
        private void MapMarkHandle(object sender, MapFactory.MapMarkFactory.MarkPointsCompletedEventArgs e)
        {
            Binding binding = new Binding();
            binding.Source = e.CompletedMapMarkPoints.MarkPoints;
            //binding.Source = this.routePoints;
            this.informationLayer.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

<<<<<<< HEAD
        /// <summary>
        /// 清除热点组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearMarkClicked(object sender, RoutedEventArgs e)
        {
            //this.markPointsAssemble.MapMarkHandle(FactoryCommand.MapMarkCommand.TelerikMapMark).ClearMapMarkPoint(
            Imapmark.ClearMapMarkPoint(this.markPoints);
        }
        /// <summary>
        /// 点击热点，开始闪烁效果，同时作为寻路出发点，此后可以通过左键点击在地图上增加目的地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mark_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.routePoints.MarkPoints.Count == 0)
            {
                Grid control = sender as Grid;
                MapFactory.MapPointFactory.MapPoint point = control.DataContext as MapFactory.MapPointFactory.MapPoint;
                int count = this.markPoints.MarkPoints.Count;
                this.routePoints.MarkPoints.Clear();
                this.routePoints.MarkPoints.Add(Ipoint.Clone(point));
                Binding binding = new Binding();
                binding.Source = this.routePoints.MarkPoints;
                this.informationLayer2.SetBinding(ItemsControl.ItemsSourceProperty, binding);
                IblinkTimer.Element = control;
                IblinkTimer.StartTimer(sender, e);
            }
        }

        /// <summary>
        /// 缩放时自动改变寻路精度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadMap1_ZoomChanged_1(object sender, EventArgs e)
        {
            if (this.informationLayer4 != null && this.informationLayer4.Items.Count > 0)
            {
                ImapAnimation.StopAnimation();
                this.informationLayer4.Items.Clear();
            }
            if (this.informationLayer3 != null && this.informationLayer3.Items.Count >0)
            {
                 Imaproute.JsRoute("GetDirection", this.routePoints, RouteResultLocations, Ipolyline.GetMapPolyline(RouteResultLocations, Icolor.GetBrushFromCombo(255, 255, 0, 0), 2), this.informationLayer3);
            }

        }

        /// <summary>
        /// 折线动画事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationClicked(object sender, RoutedEventArgs e)
        {
            this.informationLayer4.Items.Clear();
            Pushpin pin = new Pushpin();
            //MapLayer.SetLocation(pin, this.routePoints.MarkPoints[0].Location);
            this.informationLayer4.Items.Add(pin);
            ImapAnimation.ElementAnimation((FrameworkElement)this.informationLayer3.Items[0], 1, pin, this.RadMap1);
          
        }

=======

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
>>>>>>> origin/master
        }
    }


