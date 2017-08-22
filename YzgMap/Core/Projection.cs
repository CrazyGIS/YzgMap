using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YzgMap.Core
{
    public class Projection : ICoordinate2
    {
        #region 构造函数

        public Projection()
        {

        }

        public Projection(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region 公共方法

        public Projection Clone()
        {
            return new Projection(this.X, this.Y);
        }

        ICoordinate2 ICoordinate2.Clone()
        {
            return new Projection(this.X, this.Y);
        }

        #endregion

        #region 成员变量

        public double X
        {
            get
            {
                return XAxis;
            }
            set
            {
                XAxis = value;
            }
        }
        public double Y
        {
            get
            {
                return YAxis;
            }
            set
            {
                YAxis = value;
            }
        }

        public double XAxis { get; set; }
        public double YAxis { get; set; }
        #endregion
    }
}
