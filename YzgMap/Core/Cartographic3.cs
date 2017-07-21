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

        /// <summary>
        /// 构造函数
        /// </summary>
        public Cartographic3()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="longitude">经度(弧度值)</param>
        /// <param name="latitude">纬度(弧度值)</param>
        public Cartographic3(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Height = 0.0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="longitude">经度(弧度值)</param>
        /// <param name="latitude">纬度(弧度值)</param>
        /// <param name="height">高程(米)</param>
        public Cartographic3(double longitude, double latitude, double height)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Height = height;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cartographic2"></param>
        public Cartographic3(Cartographic2 cartographic2)
        {
            if(cartographic2 == null)
            {
                throw new ArgumentNullException("Cartographic2");
            }
            this.Longitude = cartographic2.Longitude;
            this.Latitude = cartographic2.Latitude;
            this.Height = 0.0;
        }

        #endregion

        #region 公共方法

        public Cartographic3 Clone()
        {
            return new Cartographic3(this.Longitude, this.Latitude, this.Height);
        }

        override
        public string ToString()
        {
            return "(" + this.Longitude + "," + this.Latitude + "," + this.Height + ")";
        }

        #endregion

        #region 成员变量

        /// <summary>
        /// 经度(弧度值)
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度(弧度值)
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 高程(米)
        /// </summary>
        public double Height { get; set; }

        #endregion
    }
}