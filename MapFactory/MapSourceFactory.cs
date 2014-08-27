using System;
using System.Net;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Map;

namespace TelerikSilverlightAppMap1.MapFactory
{
    /// <summary>
    /// 封装地图资源工厂
    /// </summary>
    public class MapSourceFactory
    {
        /// <summary>
        /// 地图资源工厂接口
        /// </summary>
        public interface ICommonMapSource {
            /// <summary>
            /// 获取瓦片地图资源
            /// </summary>
            /// <returns></returns>
            TiledMapSource GetTiledMapSource();
            /// <summary>
            /// 获取Shape地图资源
            /// </summary>
            /// <returns></returns>
            MapShapeReader GetMapShapeReader();
        }
        /// <summary>
        /// 构建一个BING中国地图资源的对象
        /// </summary>
        public class BingCNMapSource : TiledMapSource, ICommonMapSource
        { 
        private const int TileSize = 256;
        private string tileUrlFormat;
        /// <summary>
        /// 实例化一个TELERIK的TiledMapSource对象，数值分别表示最小缩放值，最大缩放值，瓦片图宽，瓦片图高，为与BING的地图设定相适应，请勿随意修改
        /// </summary>
        public BingCNMapSource()
            : base(1, 20, TileSize, TileSize)
        {
            MapUriFactory.Assemble uriAssemble = new MapUriFactory.Assemble();
            this.tileUrlFormat = uriAssemble.UriHandle(FactoryCommand.URICommand.BingTileURI).GetResult();
        }
        /// <summary>
        /// 注册初始化事件，此处默认无任何行为
        /// </summary>
        public override void Initialize()
        {
            this.RaiseInitializeCompleted();
        }
       /// <summary>
       /// 通过参数调用URI相关函数，获取当前缩放度与经纬度所应对的瓦片地图 
       /// </summary>
       /// <param name="tileLevel"></param>
       /// <param name="tilePositionX"></param>
       /// <param name="tilePositionY"></param>
       /// <returns></returns>
        protected override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            int zoomLevel = ConvertTileToZoomLevel(tileLevel);
            // Prepare tile url somehow ...
            string url = this.tileUrlFormat;
            //TELERIK内部方法，URL格式化
            url = ProtocolHelper.SetScheme(url);
            //调用工厂命令获取quadkey
            MapKeyFactory.Assemble uriassemble = new MapKeyFactory.Assemble();
            MapKeyFactory.IKey bingQuadkey = uriassemble.KeyHandle(FactoryCommand.KeyCommand.BingQuadkey);
            string quadkey = bingQuadkey.GetResult(zoomLevel, tilePositionX, tilePositionY);
            //UriHelper.TileXYToQuadKey(zoomLevel, tilePositionX, tilePositionY);
            url = url.Replace("{quadkey}", quadkey.ToString(CultureInfo.InvariantCulture));
            //string url = "http://t2.tiles.ditu.live.com/tiles/r" + quadkey + ".png?g=2732&mkt=zh-cn&n=z";
            //string url = "http://t0.tiles.ditu.live.com/tiles/r13212023000.png?g=2732&mkt=zh-cn&n=z";
            return new Uri(url);
        }


            /// <summary>
            /// 工厂接口输出瓦片地图资源
            /// </summary>
            /// <returns></returns>
        public TiledMapSource GetTiledMapSource()
        {
            return this;
        }


        public MapShapeReader GetMapShapeReader()
        {
            throw new NotImplementedException();
        }
        }

        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public ICommonMapSource CommonMapSourceHandle(string command)
            {
                if (command == FactoryCommand.MapSourceCommand.BingCNMapSource)
                {
                    return new BingCNMapSource();
                }
                throw new Exception(FactoryCommand.NullException);
            }
        }

    }
}
