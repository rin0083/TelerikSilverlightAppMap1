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
using Telerik.Windows.Controls.Map;

namespace TelerikSilverlightAppMap1.MapFactory
{
    /// <summary>
    /// 封装地图供应商工厂
    /// </summary>
    public class MapProviderFactory
    {
        /// <summary>
        /// 地图供应商工厂接口
        /// </summary>
        public interface IMapProvider {
            /// <summary>
            /// 获得瓦片地图供应商
            /// </summary>
            /// <returns></returns>
            TiledProvider GetTiledProvider();
        }
        /// <summary>
        /// 构建Bing中国地图供应商
        /// </summary>
        public class BingCNMapProvider:TiledProvider,IMapProvider
        {
            /// <summary>
            /// 实例化一个Bing中国地图供应商对象
            /// </summary>
            public BingCNMapProvider()
                : base()
        {

            MapSourceFactory.Assemble mapSourceFactory = new MapSourceFactory.Assemble();
            var bcms = mapSourceFactory.CommonMapSourceHandle(FactoryCommand.MapSourceCommand.BingCNMapSource).GetTiledMapSource();
            this.MapSources.Add(bcms.UniqueId, bcms);

        }
            /// <summary>
            /// 声明该地图坐标体系为墨卡托投影
            /// </summary>
            public override ISpatialReference SpatialReference
            {
                get
                {
                    return new MercatorProjection();
                }
            }
            /// <summary>
            /// 工厂接口输出地图供应商
            /// </summary>
            /// <returns></returns>
            public TiledProvider GetTiledProvider()
            {
                return this;
            }
        }

        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public IMapProvider MapProviderHandle(string command)
            {
                if (command == FactoryCommand.MapProviderCommand.BingCNMapProvider)
                {
                    return new BingCNMapProvider();
                }
                throw new Exception(FactoryCommand.NullException);
            }
        }

    }
}
