using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YzgMap.Core
{
    public static class YzgMath
    {
        public const double PI = 3.1415926535897931;
        public const double E = 2.7182818284590451;

        #region 角度计算

        public static double DegreeToRadian(double degree)
        {
            return degree * PI / 180.0;
        }

        public static double RadianToDegree(double radian)
        {
            return radian * 180.0 / PI;
        }

        #endregion

        #region 数字计算

        public static void CheckNumber_MoreThan(double number, double standard)
        {
            if(number <= standard)
            {
                throw new ArgumentOutOfRangeException("Number参数需要大于" + standard);
            }
        }

        public static void CheckNumber_MoreThanOrEqualTo(double number, double standard)
        {
            if (number < standard)
            {
                throw new ArgumentOutOfRangeException("Number参数不能小于" + standard);
            }
        }

        public static void CheckNumber_LessThan(double number, double standard)
        {
            if (number >= standard)
            {
                throw new ArgumentOutOfRangeException("Number参数需要小于" + standard);
            }
        }

        public static void CheckNumber_LessThanOrEqualTo(double number, double standard)
        {
            if (number > standard)
            {
                throw new ArgumentOutOfRangeException("Number参数不能大于" + standard);
            }
        }

        public static void CheckNumber_NotEqualTo(double number, double standard)
        {
            if (number == standard)
            {
                throw new ArgumentOutOfRangeException("Number参数不能等于" + standard);
            }
        }

        public static void CheckNumber_ButweenLeftRight_EqualLeft(double number, double left, double right)
        {
            if (number < left || number >= right)
            {
                throw new ArgumentOutOfRangeException("Number参数应介于" + left + "和" + right + "之间，可以等于左边界值");
            }
        }

        public static void CheckNumber_ButweenLeftRight_EqualRight(double number, double left, double right)
        {
            if (number <= left || number > right)
            {
                throw new ArgumentOutOfRangeException("Number参数应介于" + left + "和" + right + "之间，可以等于右边界值");
            }
        }

        public static void CheckNumber_ButweenLeftRight_EqualLeftRight(double number, double left, double right)
        {
            if (number < left || number > right)
            {
                throw new ArgumentOutOfRangeException("Number参数应介于" + left + "和" + right + "之间，可以等于边界值");
            }
        }

        #endregion

        #region Cartesian3计算

        #endregion

        #region Cartographic3计算

        #endregion
    }
}