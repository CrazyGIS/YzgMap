using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YzgMap.Core
{
    public class Projection : ICoordinate3
    {
        #region 构造函数

        public Projection()
        {

        }

        public Projection(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #endregion

        #region 公共方法

        public Projection Clone()
        {
            return new Projection(this.x, this.y, this.z);
        }

        ICoordinate3 ICoordinate3.Clone()
        {
            return new Projection(this.x, this.y, this.z);
        }

        #endregion

        #region 成员变量

        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        double ICoordinate3.XAxis
        {
            get => x;
            set => x = value;
        }
        double ICoordinate3.YAxis
        {
            get => y;
            set => y = value;
        }
        double ICoordinate3.ZAxis
        {
            get => z;
            set => z = value;
        }
        #endregion
    }
}
