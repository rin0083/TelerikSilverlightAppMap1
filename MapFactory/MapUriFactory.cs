using System;
using System.Text;
using System.Net;
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
    /// 封装URI对象操作
    /// </summary>
    public class MapUriFactory
    {
        /// <summary>
        /// URI工厂接口
        /// </summary>
        public interface IUri {
             string GetResult();
        }
        /// <summary>
        /// bingmaptile的URI格式 其中{quadkey}表示BING特有的表示经纬度与缩放度的参数
        /// </summary>
        public class BingTileURI : IUri
        {
            public static string URIString()
            { 
              const string TileBingCnMapUrlFormat = @"http://t0.tiles.ditu.live.com/tiles/r{quadkey}.png?g=2732&mkt=zh-cn&n=z";

              return TileBingCnMapUrlFormat;
            }
            public string GetResult()
            {
                return URIString();
            }
        }

        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public IUri UriHandle(string command)
            {
                if (command == FactoryCommand.URICommand.BingTileURI)
                {
                    return new BingTileURI();
                }
                throw new Exception(FactoryCommand.NullException);
            }
        }

    }
}
