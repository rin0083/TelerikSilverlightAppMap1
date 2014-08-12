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
 
    public class CommonMapProvider : TiledProvider
    {
        public CommonMapProvider()
            : base()
        {
            MyMapSource source = new MyMapSource("");

            this.MapSources.Add(source.UniqueId, source);
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
    public class MyMapSource : TiledMapSource
    {
        private const int TileSize = 256;
        private string tileUrlFormat;
        /// <summary>
        /// Initializes a new instance of the MyMapSource class.
        /// </summary>
        public MyMapSource(string tilesLocation)
            : base(1, 20, TileSize, TileSize)
        {
            this.tileUrlFormat = tilesLocation;
        }
        /// <summary>
        /// Initialize provider.
        /// </summary>
        public override void Initialize()
        {
            this.RaiseInitializeCompleted();

        }
        /// <summary>
        /// Gets the image URI.
        /// </summary>
        /// <param name="tileLevel">Tile level.</param>
        /// <param name="tilePositionX">Tile X.</param>
        /// <param name="tilePositionY">Tile Y.</param>
        /// <returns>URI of image.</returns>
        protected override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            int zoomLevel = ConvertTileToZoomLevel(tileLevel);
            // Prepare tile url somehow ...
            string url = this.tileUrlFormat;
            url = ProtocolHelper.SetScheme(url);
            string quadkey = UriHelper.TileXYToQuadKey(zoomLevel, tilePositionX, tilePositionY);
            url = url.Replace("{quadkey}", quadkey.ToString(CultureInfo.InvariantCulture));
            //string url = "http://t2.tiles.ditu.live.com/tiles/r" + quadkey + ".png?g=2732&mkt=zh-cn&n=z";
            //string url = "http://t0.tiles.ditu.live.com/tiles/r13212023000.png?g=2732&mkt=zh-cn&n=z";
            return new Uri(url);
        }
    }
}
