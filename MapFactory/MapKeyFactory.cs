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

namespace TelerikSilverlightAppMap1.MapFactory
{
    /// <summary>
    /// 地图识别码工厂
    /// </summary>
    public class MapKeyFactory
    {
        /// <summary>
        /// 地图识别码接口
        /// </summary>
        public interface IKey {
            string GetResult(int levelOfDetail, int tileX, int tileY);
        }
        /// <summary>
        /// BING的瓦片图标识Quadkey
        /// </summary>
        public class BingQuadkey : IKey
        {
            /// <summary>
            /// 由地图缩放级别，经纬度获取一张瓦片图的Quadkey
            /// </summary>
            /// <param name="levelOfDetail"></param>
            /// <param name="tileX"></param>
            /// <param name="tileY"></param>
            /// <returns></returns>
            public static string GetQuadkey(int levelOfDetail, int tileX, int tileY)
            {
                StringBuilder quadKey = new StringBuilder();
                for (int i = levelOfDetail; i > 0; i--)
                {
                    char digit = '0';
                    int mask = 1 << (i - 1);
                    if ((tileX & mask) != 0)
                    {
                        digit++;
                    }
                    if ((tileY & mask) != 0)
                    {
                        digit++;
                        digit++;
                    }
                    quadKey.Append(digit);
                }
                return quadKey.ToString();
            }
            /// <summary>
            /// 工厂接口输出quadkey
            /// </summary>
            /// <param name="levelOfDetail"></param>
            /// <param name="tileX"></param>
            /// <param name="tileY"></param>
            /// <returns></returns>
            public string GetResult(int levelOfDetail, int tileX, int tileY)
            {
                return GetQuadkey(levelOfDetail, tileX, tileY);
            }
        }


        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public IKey KeyHandle(string command)
            {
                if (command == FactoryCommand.KeyCommand.BingQuadkey)
                {
                    return new BingQuadkey();
                }
                throw new Exception(FactoryCommand.NullException);
            }
        }

    }
}
