using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YzgMap.Core;

namespace YzgMap.Service
{
    public class Cartesian3Service
    {
        public Cartesian3Service()
        {

        }

        #region 接口方法

        public Cartesian3 Cartographic3ToCartesian3(Cartographic3 cartographic3)
        {
            if(cartographic3 == null)
            {
                throw new ArgumentNullException("Cartographic3");
            }
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(cartographic3.Longitude, -180.0, 180.0);
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(cartographic3.Latitude, -90.0, 90.0);
            YzgMath.CheckNumber_MoreThanOrEqualTo(cartographic3.Height, 0.0);

            Ellipsoid ellipsoid = CoordinateSystem.WGS84.GetEllipsoid();
            return this.fromDegrees(cartographic3.Longitude, cartographic3.Latitude, cartographic3.Height, ellipsoid);
        }

        public Cartesian3 Cartographic3ToCartesian3(Cartographic3 cartographic3, Ellipsoid ellipsoid)
        {
            if (cartographic3 == null)
            {
                throw new ArgumentNullException("Cartographic3");
            }
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(cartographic3.Longitude, -180.0, 180.0);
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(cartographic3.Latitude, -90.0, 90.0);
            YzgMath.CheckNumber_MoreThanOrEqualTo(cartographic3.Height, 0.0);

            return this.fromDegrees(cartographic3.Longitude, cartographic3.Latitude, cartographic3.Height, ellipsoid);
        }

        #endregion

        #region 公共方法

        public Cartesian3 Normalize(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.normalize(cartesian3);
        }

        public Cartesian3 Add(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.add(left, right);
        }

        public Cartesian3 Subtract(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.subtract(left, right);
        }

        public double Dot(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.dot(left, right);
        }

        public Cartesian3 Cross(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.cross(left, right);
        }

        public double MaximumComponent(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.maximumComponent(cartesian3);
        }

        public double MinimumComponent(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.minimumComponent(cartesian3);
        }

        public Cartesian3 MaximumByComponent(Cartesian3 first, Cartesian3 second)
        {
            checkCartesian3(first);
            checkCartesian3(second);

            return this.MaximumByComponent(first, second);
        }

        public Cartesian3 MinimumByComponent(Cartesian3 first, Cartesian3 second)
        {
            checkCartesian3(first);
            checkCartesian3(second);

            return this.minimumByComponent(first, second);
        }

        public Cartesian3 MultiplyComponents(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.multiplyComponents(left, right);
        }

        public Cartesian3 DivideComponents(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3_NotEqualTo(right, 0.0);

            return this.multiplyComponents(left, right);
        }

        public Cartesian3 MultiplyByScalar(Cartesian3 cartesian3, double scalar)
        {
            checkCartesian3(cartesian3);
            return this.multiplyByScalar(cartesian3, scalar);
        }

        public Cartesian3 DivideByScalar(Cartesian3 cartesian3, double scalar)
        {
            checkCartesian3(cartesian3);
            YzgMath.CheckNumber_NotEqualTo(scalar, 0.0);
            return this.divideByScalar(cartesian3, scalar);
        }

        public Cartesian3 Negate(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.negate(cartesian3);
        }

        public Cartesian3 Abs(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.abs(cartesian3);
        }

        public double MagnitudeSquared(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.magnitudeSquared(cartesian3);
        }

        public double Magnitude(Cartesian3 cartesian3)
        {
            checkCartesian3(cartesian3);
            return this.magnitude(cartesian3);
        }

        public double Distance(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.distance(left, right);
        }

        public double DistanceSquared(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.distanceSquared(left, right);
        }

        public Cartesian3 Lerp(Cartesian3 start, Cartesian3 end, double scalar)
        {
            checkCartesian3(start);
            checkCartesian3(end);
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(scalar, 0.0, 1.0);
            return this.lerp(start, end, scalar);
        }

        public double AngleBetween(Cartesian3 left, Cartesian3 right)
        {
            checkCartesian3(left);
            checkCartesian3(right);

            return this.angleBetween(left, right);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 对Cartesian3执行检查(只检查是否为null)
        /// </summary>
        /// <param name="cartesian3"></param>
        private void checkCartesian3(Cartesian3 cartesian3)
        {
            if(cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }
        }

        /// <summary>
        /// 对Cartesian3执行检查(xyz属性需要大于standard)
        /// </summary>
        /// <param name="cartesian3"></param>
        /// <param name="standard"></param>
        private void checkCartesian3_MoreThan(Cartesian3 cartesian3, double standard)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }

            if (cartesian3.X <= standard || cartesian3.Y <= standard || cartesian3.Z <= standard)
            {
                throw new ArgumentOutOfRangeException("Cartesian3的XYZ属性需要大于" + standard);
            }
        }

        /// <summary>
        /// 对Cartesian3执行检查(xyz属性需要大于等于standard)
        /// </summary>
        /// <param name="cartesian3"></param>
        /// <param name="standard"></param>
        private void checkCartesian3_MoreThanOrEqualTo(Cartesian3 cartesian3, double standard)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }

            if (cartesian3.X < standard || cartesian3.Y < standard || cartesian3.Z < standard)
            {
                throw new ArgumentOutOfRangeException("Cartesian3的XYZ属性需要大于等于" + standard);
            }
        }

        /// <summary>
        /// 对Cartesian3执行检查(xyz属性需要小于standard)
        /// </summary>
        /// <param name="cartesian3"></param>
        /// <param name="standard"></param>
        private void checkCartesian3_LessThan(Cartesian3 cartesian3, double standard)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }

            if (cartesian3.X >= standard || cartesian3.Y >= standard || cartesian3.Z >= standard)
            {
                throw new ArgumentOutOfRangeException("Cartesian3的XYZ属性需要小于" + standard);
            }
        }

        /// <summary>
        /// 对Cartesian3执行检查(xyz属性需要小于等于standard)
        /// </summary>
        /// <param name="cartesian3"></param>
        /// <param name="standard"></param>
        private void checkCartesian3_LessThanOrEqualTo(Cartesian3 cartesian3, double standard)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }

            if (cartesian3.X > standard || cartesian3.Y > standard || cartesian3.Z > standard)
            {
                throw new ArgumentOutOfRangeException("Cartesian3的XYZ属性需要小于等于" + standard);
            }
        }

        /// <summary>
        /// 对Cartesian3执行检查(xyz属性不能等于standard)
        /// </summary>
        /// <param name="cartesian3"></param>
        /// <param name="standard"></param>
        private void checkCartesian3_NotEqualTo(Cartesian3 cartesian3, double standard)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }

            if (cartesian3.X == standard || cartesian3.Y == standard || cartesian3.Z == standard)
            {
                throw new ArgumentOutOfRangeException("Cartesian3的XYZ属性不能等于" + standard);
            }
        }

        private Cartesian3 getCartesian3_ZERO()
        {
            return new Cartesian3(0.0, 0.0, 0.0);
        }

        private Cartesian3 getCartesian3_UNIT_X()
        {
            return new Cartesian3(1.0, 0.0, 0.0);
        }

        private Cartesian3 getCartesian3_UNIT_Y()
        {
            return new Cartesian3(0.0, 1.0, 0.0);
        }

        private Cartesian3 getCartesian3_UNIT_Z()
        {
            return new Cartesian3(0.0, 0.0, 1.0);
        }

        private Cartesian3 normalize(Cartesian3 cartesian3)
        {
            double magnitude = this.magnitude(cartesian3);

            double resultX = cartesian3.X / magnitude;
            double resultY = cartesian3.Y / magnitude;
            double resultZ = cartesian3.Z / magnitude;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 add(Cartesian3 left, Cartesian3 right)
        {
            double resultX = left.X + right.X;
            double resultY = left.Y + right.Y;
            double resultZ = left.Z + right.Z;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 subtract(Cartesian3 left, Cartesian3 right)
        {
            double resultX = left.X - right.X;
            double resultY = left.Y - right.Y;
            double resultZ = left.Z - right.Z;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private double dot(Cartesian3 left, Cartesian3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        private Cartesian3 cross(Cartesian3 left, Cartesian3 right)
        {
            double resultX = left.Y * right.Z - left.Z * right.Y;
            double resultY = left.Z * right.X - left.X * right.Z;
            double resultZ = left.X * right.Y - left.Y * right.X;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private double maximumComponent(Cartesian3 cartesian3)
        {
            return Math.Max(Math.Max(cartesian3.X, cartesian3.Y), cartesian3.Z);
        }

        private double minimumComponent(Cartesian3 cartesian3)
        {
            return Math.Min(Math.Min(cartesian3.X, cartesian3.Y), cartesian3.Z);
        }

        private Cartesian3 maximumByComponent(Cartesian3 first, Cartesian3 second)
        {
            double resultX = Math.Max(first.X, second.X);
            double resultY = Math.Max(first.Y, second.Y);
            double resultZ = Math.Max(first.Z, second.Z);

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 minimumByComponent(Cartesian3 first, Cartesian3 second)
        {
            double resultX = Math.Min(first.X, second.X);
            double resultY = Math.Min(first.Y, second.Y);
            double resultZ = Math.Min(first.Z, second.Z);

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 multiplyComponents(Cartesian3 left, Cartesian3 right)
        {
            double resultX = left.X * right.X;
            double resultY = left.Y * right.Y;
            double resultZ = left.Z * right.Z;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 divideComponents(Cartesian3 left, Cartesian3 right)
        {
            double resultX = left.X / right.X;
            double resultY = left.Y / right.Y;
            double resultZ = left.Z / right.Z;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 multiplyByScalar(Cartesian3 cartesian3, double scalar)
        {
            double resultX = cartesian3.X * scalar;
            double resultY = cartesian3.Y * scalar;
            double resultZ = cartesian3.Z * scalar;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 divideByScalar(Cartesian3 cartesian3, double scalar)
        {
            double resultX = cartesian3.X / scalar;
            double resultY = cartesian3.Y / scalar;
            double resultZ = cartesian3.Z / scalar;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 negate(Cartesian3 cartesian3)
        {
            double resultX = -cartesian3.X;
            double resultY = -cartesian3.Y;
            double resultZ = -cartesian3.Z;

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private Cartesian3 abs(Cartesian3 cartesian3)
        {
            double resultX = Math.Abs(cartesian3.X);
            double resultY = Math.Abs(cartesian3.Y);
            double resultZ = Math.Abs(cartesian3.Z);

            return new Cartesian3(resultX, resultY, resultZ);
        }

        private double magnitudeSquared(Cartesian3 cartesian3)
        {
            return Math.Pow(cartesian3.X, 2) + Math.Pow(cartesian3.Y, 2) + Math.Pow(cartesian3.Z, 2);
        }

        private double magnitude(Cartesian3 cartesian3)
        {
            return Math.Sqrt(this.magnitudeSquared(cartesian3));
        }

        private double distance(Cartesian3 left, Cartesian3 right)
        {
            Cartesian3 subCartesian3 = this.subtract(left, right);
            return this.magnitude(subCartesian3);
        }

        private double distanceSquared(Cartesian3 left, Cartesian3 right)
        {
            Cartesian3 subCartesian3 = this.subtract(left, right);
            return this.magnitudeSquared(subCartesian3);
        }

        private Cartesian3 lerp(Cartesian3 start, Cartesian3 end, double scalar)
        {
            Cartesian3 multiplyStart = this.multiplyByScalar(start, 1.0 - scalar);
            Cartesian3 multiplyEnd = this.multiplyByScalar(end, scalar);
            return this.add(multiplyStart, multiplyEnd);
        }

        private double angleBetween(Cartesian3 left, Cartesian3 right)
        {
            Cartesian3 normalizeLeft = this.normalize(left);
            Cartesian3 normalizeRight = this.normalize(right);
            double cosine = this.dot(normalizeLeft, normalizeRight);
            double sine = this.magnitude(this.cross(normalizeLeft, normalizeRight));

            return Math.Atan2(sine, cosine);
        }

        private Cartesian3 mostOrthogonalAxis(Cartesian3 cartesian3)
        {
            Cartesian3 normalizeCartesian3 = this.normalize(cartesian3);
            Cartesian3 absCartesian3 = this.abs(normalizeCartesian3);

            Cartesian3 result = null;
            if (absCartesian3.X <= absCartesian3.Y)
            {
                if (absCartesian3.X <= absCartesian3.Z)
                {
                    result = this.getCartesian3_UNIT_X();
                }
                else
                {
                    result = this.getCartesian3_UNIT_Z();
                }
            }
            else if (absCartesian3.Y <= absCartesian3.Z)
            {
                result = this.getCartesian3_UNIT_Y();
            }
            else
            {
                result = this.getCartesian3_UNIT_Z();
            }

            return result;
        }

        private bool equals(Cartesian3 left, Cartesian3 right)
        {
            return left == right || (left.X == right.Y && left.Y == right.Y && left.Z == right.Z);
        }

        private Cartesian3 fromDegrees(double longitude, double latitude, double height, Ellipsoid ellipsoid)
        {
            longitude = YzgMath.DegreeToRadian(longitude);
            latitude = YzgMath.DegreeToRadian(latitude);

            return this.fromRadians(longitude, latitude, height, ellipsoid);
        }

        private Cartesian3 fromRadians(double longitude, double latitude, double height, Ellipsoid ellipsoid)
        {
            Cartesian3 radiiSquared = ellipsoid.RadiiSquared;
            Cartesian3 scratchN = new Cartesian3();
            Cartesian3 scratchK = new Cartesian3();

            double cosLatitude = Math.Cos(latitude);
            scratchN.X = cosLatitude * Math.Cos(longitude);
            scratchN.Y = cosLatitude * Math.Sin(longitude);
            scratchN.Z = Math.Sin(latitude);
            scratchN = this.normalize(scratchN);

            scratchK = this.multiplyComponents(radiiSquared, scratchN);
            double gamma = Math.Sqrt(this.dot(scratchN, scratchK));
            scratchK = this.divideByScalar(scratchK, gamma);
            scratchN = this.multiplyByScalar(scratchN, height);

            return this.add(scratchK, scratchN);
        }
        #endregion
    }
}