using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TelerikSilverlightAppMap1.MapFactory
{
    /// <summary>
    /// 工厂选择配件的命令
    /// </summary>
    public static class FactoryCommand
    {

        /// <summary>
        /// 点对象工厂命令集
        /// </summary>
        public static class PointCommand
        {
            /// <summary>
            /// 从点对象获得TELERIK的经纬度点Location类
            /// </summary>
            public static string PointMethod { get { return "PointMethod"; } }
        }
        /// <summary>
        /// URI对象命令集
        /// </summary>
        public static class URICommand
        {
            /// <summary>
            /// 待定参数为{quadkey}的BingTileURI字符串
            /// </summary>
            public static string BingTileURI { get { return "BingTileURI"; } }
           
        }

        /// <summary>
        /// 地图资源标识命令集
        /// </summary>
        public static class KeyCommand
        {
            /// <summary>
            /// 根据缩放度和经纬度获取TileMap对应的标识参数quadkey
            /// </summary>
            public static string BingQuadkey { get { return "BingQuadkey"; } }

        }
        /// <summary>
        /// 地图资源命令集
        /// </summary>
        public static class MapSourceCommand
        {
            /// <summary>
            /// Bing中国地图瓦片资源
            /// </summary>
            public static string BingCNMapSource { get { return "BingCNMapSource"; } }
        }

        /// <summary>
        /// 地图供应商命令集
        /// </summary>
        public static class MapProviderCommand
        {
            /// <summary>
            /// Bing中国地图供应商
            /// </summary>
            public static string BingCNMapProvider { get { return "BingCNMapProvider"; } }
        }

        /// <summary>
        /// 地图标记点命令集
        /// </summary>
        public static class MapMarkCommand
        {
            /// <summary>
            /// Telerik地图标记
            /// </summary>
            public static string TelerikMapMark { get { return "TelerikMapMark"; } }
        }

        /// <summary>
        /// 地图定时器命令集
        /// </summary>
        public static class MapTimerCommand
        {
            /// <summary>
            /// DispatcherTimer定时器闪烁命令
            /// </summary>
            public static string BlinkDispatcherTimer { get { return "BlinkDispatcherTimer"; } }
        }
        /// <summary>
        /// 找不到命令时的报错信息
        /// </summary>
        public static string NullException { get { return "Not Found Command"; } }   
    }
    
}
