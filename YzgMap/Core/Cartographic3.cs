using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YzgMap.Core
{
    /// <summary>
    /// 地理坐标
    /// </summary>
    public class Cartographic3
    {
        #region 构造函数

        public Cartographic3()
        {

        }

        public Cartographic3(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Height = 0.0;
        }

        public Cartographic3(double longitude, double latitude, double height)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Height = height;
        }

        public Cartographic3(Cartographic2 cartographic2)
        {
            if(cartographic2 == null)
            {
                throw new ArgumentNullException("cartographic2");
            }
            this.Longitude = cartographic2.Longitude;
            this.Latitude = cartographic2.Latitude;
            this.Height = 0.0;
        }

        #endregion

        #region 成员变量

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Height { get; set; }

        #endregion
    }
}