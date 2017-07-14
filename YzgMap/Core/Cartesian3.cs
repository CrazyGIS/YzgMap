using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YzgMap.Core
{
    /// <summary>
    /// 笛卡尔坐标(3D)
    /// </summary>
    public class Cartesian3
    {
        #region 构造函数

        public Cartesian3()
        {

        }

        public Cartesian3(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }

        public Cartesian3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Cartesian3(Cartesian2 cartesian2)
        {
            if(cartesian2 == null)
            {
                throw new ArgumentNullException("cartesian2");
            }

            this.X = cartesian2.X;
            this.Y = cartesian2.Y;
            this.Z = 0.0;
        }

        #endregion

        #region 成员变量

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        #endregion
    }
}