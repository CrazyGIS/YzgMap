using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YzgMap.Core
{
    /// <summary>
    /// 笛卡尔坐标
    /// </summary>
    public class Cartesian2 : ICoordinate2
    {
        #region 构造函数

        public Cartesian2()
        {

        }

        public Cartesian2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Cartesian2(Cartesian3 cartesian3)
        {
            if(cartesian3 == null)
            {
                throw new ArgumentNullException("cartesian3");
            }

            this.X = cartesian3.X;
            this.Y = cartesian3.Y;
        }

        #endregion

        #region 公共方法

        public Cartesian2 Clone()
        {
            return new Cartesian2(this.X, this.Y);
        }

        ICoordinate2 ICoordinate2.Clone()
        {
            return new Cartesian2(this.X, this.Y);
        }

        #endregion

        #region 成员变量

        public double X { get; set; }
        public double Y { get; set; }
        double ICoordinate2.XAxis
        {
            get => X;
            set => X = value;
        }
        double ICoordinate2.YAxis
        {
            get => Y;
            set => Y = value;
        }
        #endregion
    }
}
