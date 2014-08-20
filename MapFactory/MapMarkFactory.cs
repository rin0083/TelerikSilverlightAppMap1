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
    /// 地图标记工厂
    /// </summary>
    public class MapMarkFactory
    {
  
        /// <summary>
        /// 地图标记工厂接口 
        /// </summary>
        public interface IMapMark
        {
            MapMarkPoints InitializeMapMarkPoints();
            /// <summary>
            /// 获取地图标记点组
            /// </summary>
            /// <param name="mapMarkPoints"></param>
            /// <returns></returns>
            MapMarkPoints GetEmptyMapMarkPoints();
             /// <summary>
            /// 在标记点组中增加地图标记点
            /// </summary>
            /// <param name="point">要增加的标记点</param>
            /// <param name="mapMarkPoints">目标标记点组</param>
            /// <returns></returns>
            MapMarkPoints AddMapMarkPoint(MapFactory.MapPointFactory.MapPoint point, MapMarkPoints mapMarkPoints);
            /// <summary>
            /// 在标记点组中删除地图标记点
            /// </summary>
            /// <param name="point"></param>
            /// <param name="mapMarkPoints"></param>
            /// <returns></returns>

            MapMarkPoints DeleteMapMarkPoint(MapFactory.MapPointFactory.MapPoint point, MapMarkPoints mapMarkPoints);
            /// <summary>
            /// 清空标记点组
            /// </summary>
            /// <param name="mapMarkPoints"></param>
            /// <returns></returns>
            MapMarkPoints ClearMapMarkPoint(MapMarkPoints mapMarkPoints);
            /// <summary>
            /// 声明标记点组改动完成事件
            /// </summary>
            event EventHandler<MarkPointsCompletedEventArgs> MarkPointsCompleted;
        }
        /// <summary>
        /// 标记点组获取完成事件类
        /// </summary>
        public class MarkPointsCompletedEventArgs : EventArgs
        {
            private MapMarkPoints completedMapMarkPoints;
            public MapMarkPoints CompletedMapMarkPoints
            { 
                get { return this.completedMapMarkPoints; } 
                set { this.completedMapMarkPoints = value;}
            }
            public MarkPointsCompletedEventArgs(MapMarkPoints mapMarkPoints)
            {
                completedMapMarkPoints = mapMarkPoints;
            }

        }
        /// <summary>
        /// 地图标记点组
        /// </summary>
        public class MapMarkPoints
        {
            private MapPointFactory.MapPoints markPoints = new MapPointFactory.MapPoints();
            public MapPointFactory.MapPoints MarkPoints { get { return this.markPoints; } set { this.markPoints = value; } } 
        }

        /// <summary>
        /// Telerik地图标记
        /// </summary>
        public class TelerikMapMark : IMapMark
        {
            /// <summary>
            /// 实例化Points工厂
            /// </summary>
            MapPointFactory.Assemble mapPointAssemble = new MapPointFactory.Assemble();
            /// <summary>
            /// 声明标记点组完成事件委托
            /// </summary>
            public event EventHandler<MarkPointsCompletedEventArgs> MarkPointsCompleted;

            //public void MarkPointsCompletedHandle(MapMarkPoints mapMarkPoints)
            //{
            //    OnMarkPointsCompleted(new MarkPointsCompletedEventArgs(mapMarkPoints));
            //}
            /// <summary>
            /// 调用标记点组完成事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void OnMarkPointsCompleted(MarkPointsCompletedEventArgs e)
            {
                EventHandler<MarkPointsCompletedEventArgs> hander = MarkPointsCompleted;
                if (hander != null)
                {
                    hander(this, e);
                }
            }

       
            /// <summary>
            /// 调用Point工厂在TelerikMap标记点组中增加地图标记点
            /// </summary>
            /// <param name="point">要增加的标记点</param>
            /// <param name="mapMarkPoints">目标标记点组</param>
            /// <returns></returns>
            public MapMarkPoints AddMapMarkPoint(MapPointFactory.MapPoint point, MapMarkPoints mapMarkPoints)
            {
                mapPointAssemble.PointHandle(FactoryCommand.PointCommand.PointMethod).AddPoint(mapMarkPoints.MarkPoints, point);  
                
                return mapMarkPoints;
            }

            /// <summary>
            /// 调用Point工厂在TelerikMap标记点组中删除地图标记点
            /// </summary>
            /// <param name="point"></param>
            /// <param name="mapMarkPoints"></param>
            /// <returns></returns>
            public MapMarkPoints DeleteMapMarkPoint(MapPointFactory.MapPoint point, MapMarkPoints mapMarkPoints)
            {
                mapPointAssemble.PointHandle(FactoryCommand.PointCommand.PointMethod).DeletePoint(mapMarkPoints.MarkPoints, point);
                return mapMarkPoints;
            }

            /// <summary>
            /// 调用Point工厂清空TelerikMap标记点组
            /// </summary>
            /// <param name="mapMarkPoints"></param>
            /// <returns></returns>
            public MapMarkPoints ClearMapMarkPoint(MapMarkPoints mapMarkPoints)
            {
                mapPointAssemble.PointHandle(FactoryCommand.PointCommand.PointMethod).ClearPoint(mapMarkPoints.MarkPoints);
                return mapMarkPoints;
            }

            /// <summary>
            /// 调用Point工厂获取TelerikMap标记点组
            /// </summary>
            /// <param name="mapMarkPoints"></param>
            /// <returns></returns>
            public MapMarkPoints GetEmptyMapMarkPoints()
            {

                return new MapMarkPoints();
            }
            /// <summary>
            /// 由本地XML初始化一个MARK点列表
            /// </summary>
            /// <returns></returns>
            public MapMarkPoints InitializeMapMarkPoints()
            {
                MapMarkPoints mapMarkPoints = new MapMarkPoints();

                StreamResourceInfo streamInfo = Application.GetResourceStream(
           new Uri("/TelerikSilverlightAppMap1;component/MPS.xml", UriKind.RelativeOrAbsolute));
                StreamReader reader = new StreamReader(streamInfo.Stream);


                XDocument document = XDocument.Load(reader);
                XElement root = document.FirstNode as XElement;
                if (root != null)
                {
                    foreach (XNode child in root.Nodes())
                    {
                        XElement element = child as XElement;
                        MapFactory.MapPointFactory.MapPoint mp = new MapFactory.MapPointFactory.MapPoint(this.GetLocation(element, "Location").Latitude, this.GetLocation(element, "Location").Longitude, this.GetString(element, "Message"), this.GetString(element, "Uri"));
                        mapMarkPoints.MarkPoints.Add(mp);
                    }
                }
                reader.Close();

                return mapMarkPoints;
            }

            private XElement GetChildByName(XElement element, string nodeName)
            {
                for (element = element.FirstNode as XElement;
                    element != null;
                    element = element.NextNode as XElement)
                {
                    if (element.Name.LocalName == nodeName)
                    {
                        return element;
                    }
                }

                return null;
            }

            private Location GetLocation(XElement element, string elementName)
            {
                Location location = Location.Empty;
                XElement child = this.GetChildByName(element, elementName);
                if (child != null)
                {
                    location = Location.Parse(child.Value);
                }

                return location;
            }

            private string GetString(XElement element, string elementName)
            {
                string value = string.Empty;
                XElement child = this.GetChildByName(element, elementName);
                if (child != null)
                {
                    value = child.Value;
                }

                return value;
            }


        }
        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public IMapMark MapMarkHandle(string command)
            {
                if (command == FactoryCommand.MapMarkCommand.TelerikMapMark)
                {
                    return new TelerikMapMark();
                }

                    IMapMark test = new TelerikMapMark();
                    test.MarkPointsCompleted += HandlerShapeChanged;
                    throw new Exception(FactoryCommand.NullException);
                
            }
            static void HandlerShapeChanged(object sender, MarkPointsCompletedEventArgs e)
            {
                MapMarkPoints a = e.CompletedMapMarkPoints;
            }
            
        }
    }
}
