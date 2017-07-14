using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YzgMap.Core
{
    /// <summary>
    /// 椭球体
    /// </summary>
    public class Ellipsoid
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">X轴半径</param>
        /// <param name="y">Y轴半径</param>
        /// <param name="z">Z轴半径</param>
        public Ellipsoid(double x, double y, double z)
        {
            check(x, y, z);
            initialize(x, y, z);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">椭球名称</param>
        /// <param name="x">X轴半径</param>
        /// <param name="y">Y轴半径</param>
        /// <param name="z">Z轴半径</param>
        public Ellipsoid(string name, double x, double y, double z)
        {
            this.Name = name;
            check(x, y, z);
            initialize(x, y, z);
        }

        #endregion

        #region 成员变量

        public string Name { get; set; }
        public double SemiMajorAxis { get; private set; }
        public double SemiMinorAxis { get; private set; }
        public Cartesian3 Radii { get; private set; }
        public Cartesian3 RadiiSquared { get; private set; }
        public Cartesian3 RadiiToTheFourth { get; private set; }
        public Cartesian3 OneOverRadii { get; private set; }
        public Cartesian3 OneOverRadiiSquared { get; private set; }
        public double MinimumRadius { get; private set; }
        public double MaximumRadius { get; private set; }
        public double CenterToleranceSquared { get; private set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 参数检查
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void check(double x, double y, double z)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException("x参数值不能小于0");
            }

            if (y < 0)
            {
                throw new ArgumentOutOfRangeException("y参数值不能小于0");
            }

            if (z < 0)
            {
                throw new ArgumentOutOfRangeException("z参数值不能小于0");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void initialize(double x, double y, double z)
        {
            this.SemiMajorAxis = x;
            this.SemiMinorAxis = z;
            this.Radii = new Cartesian3(x, y, z);
            this.RadiiSquared = new Cartesian3(x * x, y * y, z * z);
            this.RadiiToTheFourth = new Cartesian3(
                Math.Pow(x, 4), 
                Math.Pow(y, 4), 
                Math.Pow(z, 4));
            this.OneOverRadii = new Cartesian3(
                x == 0.0 ? 0.0 : 1.0 / x,
                y == 0.0 ? 0.0 : 1.0 / y,
                z == 0.0 ? 0.0 : 1.0 / z);
            this.OneOverRadiiSquared = new Cartesian3(
                x == 0.0 ? 0.0 : 1.0 / (x * x),
                y == 0.0 ? 0.0 : 1.0 / (y * y),
                z == 0.0 ? 0.0 : 1.0 / (z * z));
            this.MinimumRadius = Math.Min(Math.Min(x, y), z);
            this.MaximumRadius = Math.Max(Math.Max(x, y), z);
            this.CenterToleranceSquared = 0.1;
        }

        #endregion
    }
}