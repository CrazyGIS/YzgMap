using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YzgMap.Core
{
    /// <summary>
    /// 地理坐标
    /// </summary>
    public class Cartographic2
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Cartographic2()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="longitude">经度(弧度值)</param>
        /// <param name="latitude">纬度(弧度值)</param>
        public Cartographic2(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cartographic3"></param>
        public Cartographic2(Cartographic3 cartographic3)
        {
            if(cartographic3 == null)
            {
                throw new ArgumentNullException("cartographic3");
            }
            this.Longitude = cartographic3.Longitude;
            this.Latitude = cartographic3.Latitude;
        }

        #endregion

        #region 公共方法

        public Cartographic2 Clone()
        {
            return new Cartographic2(this.Longitude, this.Latitude);
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

        #endregion
    }
}
