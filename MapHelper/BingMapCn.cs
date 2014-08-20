using System;
using System.Net;
using System.Windows;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text;
using System.Windows.Threading;
using Telerik.Windows.Controls.Map;
using TelerikSilverlightAppMap1.MapFactory;

namespace TelerikSilverlightAppMap1.MapHelper
{
    public class BingMapCnProvider 
    {

        /// <summary>
        /// Initializes a new instance of the BingMapCnProvider class.
        /// </summary>
    
            MapProviderFactory.Assemble mapProviderAssemble = new MapProviderFactory.Assemble();

        public  TiledProvider Provider{
            get {return mapProviderAssemble.MapProviderHandle(FactoryCommand.MapProviderCommand.BingCNMapProvider).GetTiledProvider(); }
        }

        /// <summary>
        /// Returns the SpatialReference for the map provider.
        /// </summary>
        //public override ISpatialReference SpatialReference
        //{
        //    get
        //    {
        //        return new MercatorProjection();
        //    }
        //}
    }

    public class BingCnMapSource : MyMapSource
    {
        private const string TileBingCnMapUrlFormat = @"http://t0.tiles.ditu.live.com/tiles/r{quadkey}.png?g=2732&mkt=zh-cn&n=z";
        /// <summary>
        /// Initializes a new instance of the BingCnMapSource class.
        /// </summary>
        public BingCnMapSource()
            : base(TileBingCnMapUrlFormat)
        {
        }
    }
}
