using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Resources;
using System.Windows.Ink;
using System.Windows.Input;
using System.Xml.Linq;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Map;
using Telerik.Windows.Controls;

namespace TelerikSilverlightAppMap1.MapFactory
{
    /// <summary>
    /// 封装点对象操作 
    /// </summary>
    public class MapPointFactory
    {
        /// <summary>
        /// 定义点对象 ,Latitude表示经度，Longitude表示纬度，Message表示该点的文字信息，URI表示该点可能的引用资源地址
        /// </summary>
        public class MapPoint : ViewModelBase
        {
            private Location location;
            private string message;
            private string uri;

            public Location Location 
            {
                get { return location; }
                set { location = value; this.OnPropertyChanged("Location"); }
            }
            public string Message {
                get { return message; }
                set { message = value; this.OnPropertyChanged("Message"); }
            }
            public string URI
            {
                get { return uri; }
                set { uri = value; this.OnPropertyChanged("URI"); }
            }
            public MapPoint()
            { }
            public MapPoint(double latitude, double longitude, string message, string uri)
            {
                this.location = new Telerik.Windows.Controls.Map.Location(latitude, longitude);
                this.message = message;
                this.uri = uri;
            }

        }

        /// <summary>
        /// 点对象组工厂接口
        /// </summary>
        public interface IPoints
        {
            /// <summary>
            /// 由MapPoints提取Telerik经纬坐标组对象
            /// </summary>
            /// <param name="points"></param>
            /// <returns></returns>
            LocationCollection Locations(MapPoints points);
        }

        /// <summary>
        /// 点对象组
        /// </summary>
        public class MapPoints: ObservableCollection<MapPoint>
        {

    
        }
        /// <summary>
        /// 点对象工厂接口
        /// </summary>
        public interface IPoint {
            /// <summary>
            /// 在点对象组中增加一个点对象
            /// </summary>
            /// <param name="points"></param>
            /// <param name="point"></param>
            /// <returns></returns>
            MapPoints AddPoint(MapPoints points, MapPoint point);
            /// <summary>
            /// 在点对象组中删除一个点对象
            /// </summary>
            /// <param name="points"></param>
            /// <param name="point"></param>
            /// <returns></returns>
            MapPoints DeletePoint(MapPoints points, MapPoint point);
            /// <summary>
            /// 清空一个点对象组
            /// </summary>
            /// <param name="points"></param>
            /// <returns></returns>
            MapPoints ClearPoint(MapPoints points);
        }


        /// <summary>
        /// 获取Point对象组基本操作
        /// </summary>
        public class PointMethod : IPoint
        {

            public MapPoints AddPoint(MapPoints points, MapPoint point)
            {
                
                points.Add(point);
                return points;
            }

            public MapPoints DeletePoint(MapPoints points, MapPoint point)
            {
                points.Remove(point);
                return points;
            }

            public MapPoints ClearPoint(MapPoints points)
            {
                points.Clear();
                return points;
            }
        }


        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public IPoint PointHandle(string command)
            {
                if (command == FactoryCommand.PointCommand.PointMethod)
                {
                    return new PointMethod(); 
                }
                throw new Exception(FactoryCommand.NullException);
            }
        }
    }
}
