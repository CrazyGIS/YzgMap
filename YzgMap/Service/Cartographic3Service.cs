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
    public class Cartographic3Service
    {
        #region 构造函数

        public Cartographic3Service()
        {

        }

        #endregion

        #region 接口方法

        public Cartographic3 Cartesian3ToCartographic3(Cartesian3 cartesian3)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }
            Ellipsoid ellipsoid = CoordinateSystem.WGS84.GetEllipsoid();
            return this.fromCartesian(cartesian3, ellipsoid);
        }

        public Cartographic3 Cartesian3ToCartographic3(Cartesian3 cartesian3, Ellipsoid ellipsoid)
        {
            if (cartesian3 == null)
            {
                throw new ArgumentNullException("Cartesian3");
            }

            if (ellipsoid == null)
            {
                throw new ArgumentNullException("Ellipsoid");
            }
            return this.fromCartesian(cartesian3, ellipsoid);
        }

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        private Cartographic3 fromDegrees(double longitude, double latitude, double height)
        {
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(longitude, -180, 180);
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(latitude, -90, 90);

            longitude = YzgMath.DegreeToRadian(longitude);
            latitude = YzgMath.DegreeToRadian(latitude);

            return new Cartographic3(longitude, latitude, height);
        }

        private Cartographic3 fromRadians(double longitude, double latitude, double height)
        {
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(longitude, -Math.PI, Math.PI);
            YzgMath.CheckNumber_ButweenLeftRight_EqualLeftRight(latitude, -Math.PI / 2, Math.PI / 2);

            return new Cartographic3(longitude, latitude, height);
        }

        private Cartographic3 fromCartesian(Cartesian3 cartesian3, Ellipsoid ellipsoid)
        {
            Cartesian3Service cartesian3Service = new Cartesian3Service();

            var cartesianToCartographicN = new Cartesian3();
            var cartesianToCartographicP = new Cartesian3();
            var cartesianToCartographicH = new Cartesian3();

            Cartesian3 oneOverRadii = ellipsoid.OneOverRadii;
            Cartesian3 oneOverRadiiSquared = ellipsoid.OneOverRadiiSquared;
            double centerToleranceSquared = ellipsoid.CenterToleranceSquared;

            Cartesian3 p = scaleToGeodeticSurface(cartesian3, ellipsoid, cartesian3Service);

            if(p == null)
            {
                return null;
            }

            cartesianToCartographicN = cartesian3Service.MultiplyComponents(p, oneOverRadiiSquared);
            cartesianToCartographicN = cartesian3Service.Normalize(cartesianToCartographicN);

            cartesianToCartographicH = cartesian3Service.Subtract(cartesian3, p);

            var longitude = Math.Atan2(cartesianToCartographicN.Y, cartesianToCartographicN.X);
            var latitude = Math.Asin(cartesianToCartographicN.Z);
            var height = Math.Sign(cartesian3Service.Dot(cartesianToCartographicH, cartesian3)) * cartesian3Service.Magnitude(cartesianToCartographicH);

            Cartographic3 result = new Cartographic3();
            result.Longitude = longitude;
            result.Latitude = latitude;
            result.Height = height;

            return result;
        }

        private Cartesian3 scaleToGeodeticSurface(Cartesian3 cartesian3, Ellipsoid ellipsoid, Cartesian3Service cartesian3Service)
        {
            Cartesian3 oneOverRadii = ellipsoid.OneOverRadii;
            Cartesian3 oneOverRadiiSquared = ellipsoid.OneOverRadiiSquared;
            double centerToleranceSquared = ellipsoid.CenterToleranceSquared;

            var positionX = cartesian3.X;
            var positionY = cartesian3.Y;
            var positionZ = cartesian3.Z;

            var oneOverRadiiX = oneOverRadii.X;
            var oneOverRadiiY = oneOverRadii.Y;
            var oneOverRadiiZ = oneOverRadii.Z;

            var x2 = positionX * positionX * oneOverRadiiX * oneOverRadiiX;
            var y2 = positionY * positionY * oneOverRadiiY * oneOverRadiiY;
            var z2 = positionZ * positionZ * oneOverRadiiZ * oneOverRadiiZ;

            // Compute the squared ellipsoid norm.
            var squaredNorm = x2 + y2 + z2;
            var ratio = Math.Sqrt(1.0 / squaredNorm);

            // As an initial approximation, assume that the radial intersection is the projection point.
            var intersection = cartesian3Service.MultiplyByScalar(cartesian3, ratio);

            // If the position is near the center, the iteration will not converge.
            if (squaredNorm < centerToleranceSquared)
            {
                return double.IsInfinity(ratio) ? null : intersection;
            }

            var oneOverRadiiSquaredX = oneOverRadiiSquared.X;
            var oneOverRadiiSquaredY = oneOverRadiiSquared.Y;
            var oneOverRadiiSquaredZ = oneOverRadiiSquared.Z;

            // Use the gradient at the intersection point in place of the true unit normal.
            // The difference in magnitude will be absorbed in the multiplier.
            var gradient = new Cartesian3();
            gradient.X = intersection.X * oneOverRadiiSquaredX * 2.0;
            gradient.Y = intersection.Y * oneOverRadiiSquaredY * 2.0;
            gradient.Z = intersection.Z * oneOverRadiiSquaredZ * 2.0;

            // Compute the initial guess at the normal vector multiplier, lambda.
            var lambda = (1.0 - ratio) * cartesian3Service.Magnitude(cartesian3) / (0.5 * cartesian3Service.Magnitude(gradient));
            var correction = 0.0;

            double func;
            double denominator;
            double xMultiplier;
            double yMultiplier;
            double zMultiplier;
            double xMultiplier2;
            double yMultiplier2;
            double zMultiplier2;
            double xMultiplier3;
            double yMultiplier3;
            double zMultiplier3;

            do
            {
                lambda -= correction;

                xMultiplier = 1.0 / (1.0 + lambda * oneOverRadiiSquaredX);
                yMultiplier = 1.0 / (1.0 + lambda * oneOverRadiiSquaredY);
                zMultiplier = 1.0 / (1.0 + lambda * oneOverRadiiSquaredZ);

                xMultiplier2 = xMultiplier * xMultiplier;
                yMultiplier2 = yMultiplier * yMultiplier;
                zMultiplier2 = zMultiplier * zMultiplier;

                xMultiplier3 = xMultiplier2 * xMultiplier;
                yMultiplier3 = yMultiplier2 * yMultiplier;
                zMultiplier3 = zMultiplier2 * zMultiplier;

                func = x2 * xMultiplier2 + y2 * yMultiplier2 + z2 * zMultiplier2 - 1.0;

                // "denominator" here refers to the use of this expression in the velocity and acceleration
                // computations in the sections to follow.
                denominator = x2 * xMultiplier3 * oneOverRadiiSquaredX + y2 * yMultiplier3 * oneOverRadiiSquaredY + z2 * zMultiplier3 * oneOverRadiiSquaredZ;

                var derivative = -2.0 * denominator;

                correction = func / derivative;
            } while (Math.Abs(func) > 0.000000000001);


            Cartesian3 result = new Cartesian3();
            result.X = positionX * xMultiplier;
            result.Y = positionY * yMultiplier;
            result.Z = positionZ * zMultiplier;
            return result;
        }

        #endregion
    }
}