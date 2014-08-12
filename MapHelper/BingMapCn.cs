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

namespace TelerikSilverlightAppMap1.MapHelper
{
    public class BingMapCnProvider : TiledProvider
    {
        

        /// <summary>
        /// Initializes a new instance of the BingMapCnProvider class.
        /// </summary>
        public BingMapCnProvider()
            : base()
        {


            var bcms = new BingCnMapSource();
            this.MapSources.Add(bcms.UniqueId, bcms);

          
        }

        /// <summary>
        /// Returns the SpatialReference for the map provider.
        /// </summary>
        public override ISpatialReference SpatialReference
        {
            get
            {
                return new MercatorProjection();
            }
        }
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
