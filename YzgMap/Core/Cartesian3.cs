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
    public class Cartesian3 : ICoordinate3
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

        #region 公共方法

        public Cartesian3 Clone()
        {
            return new Cartesian3(this.X, this.Y, this.Z);
        }

        override
        public string ToString()
        {
            return "(" + this.X + "," + this.Y + "," + this.Z + ")";
        }

        ICoordinate3 ICoordinate3.Clone()
        {
            return new Cartesian3(this.X, this.Y, this.Z);
        }

        #endregion

        #region 成员变量

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        double ICoordinate3.XAxis
        {
            get => X;
            set => X = value;
        }
        double ICoordinate3.YAxis
        {
            get => Y;
            set => Y = value;
        }
        double ICoordinate3.ZAxis
        {
            get => Z;
            set => Z = value;
        }

        #endregion
    }
}