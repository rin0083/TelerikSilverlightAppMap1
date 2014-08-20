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
using System.IO;

namespace TelerikSilverlightAppMap1.MapFactory
{
    /// <summary>
    /// 地图定时器工厂
    /// </summary>
    public class MapTimerFacotry
    {
        /// <summary>
        /// 地图定时器接口
        /// </summary>
        public interface IMapTimer
        {
            void StartTimer(object sender, RoutedEventArgs e);
            void EndTimer(object sender, RoutedEventArgs e);
            FrameworkElement Element { get;set;}
        }
        /// <summary>
        /// 通过DispatcherTimer完成定时器
        /// </summary>
        public class BlinkDispatcherTimer : IMapTimer
        {
            /// <summary>
            /// 实例化DispatcherTimer定时器
            /// </summary>
            System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            /// <summary>
            /// 定时器开始
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void StartTimer(object sender, RoutedEventArgs e)
            {
                
                myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); 
                myDispatcherTimer.Tick += new EventHandler(Blink);
                myDispatcherTimer.Start();
                
            }
            /// <summary>
            /// 定时器结束
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void EndTimer(object sender, RoutedEventArgs e)
            {
                myDispatcherTimer.Stop();
            }
            /// <summary>
            /// 闪烁事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void Blink(object sender, EventArgs e)
            {
                if (this.Element.Visibility == Visibility.Collapsed)
                { this.Element.Visibility = Visibility.Visible; }
                else
                { this.Element.Visibility = Visibility.Collapsed; }
            }
            /// <summary>
            /// 闪烁元素
            /// </summary>
            public FrameworkElement Element
            {
                get;
                set;
            }
        }

        /// <summary>
        /// 工厂模式分配操作
        /// </summary>
        public class Assemble
        {
            public IMapTimer MapTimerHandle(string command)
            {
                if (command == FactoryCommand.MapTimerCommand.BlinkDispatcherTimer)
                {
                    return new BlinkDispatcherTimer();
                }
                throw new Exception(FactoryCommand.NullException);
            }
        }


    }
}
