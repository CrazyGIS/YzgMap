using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YzgMap;
using YzgMap.Core;
using YzgMap.Service;
using YzgMap.CoordinateSystem;
using YzgMap.Transformation.SevenParameters;

namespace ConsoleYzgMap
{
    class Program
    {
        static void Main(string[] args)
        {
            test2();
        }

        static void test1()
        {
            Cartesian3Service cartesian3Service = new Cartesian3Service();
            Cartographic3Service cartographic3Service = new Cartographic3Service();

            Cartesian3 cartesian3 = new Cartesian3(Math.PI, Math.PI / 6);
            Console.WriteLine("转换前的Cartesian3经度:" + YzgMath.RadianToDegree(cartesian3.X));
            Console.WriteLine("转换前的Cartesian3纬度:" + YzgMath.RadianToDegree(cartesian3.Y));

            Cartographic3 cartographic3 = cartographic3Service.Cartesian3ToCartographic3(cartesian3);
            Console.WriteLine("转换后的Cartographic3:" + cartographic3.ToString());

            cartesian3 = cartesian3Service.Cartographic3ToCartesian3(cartographic3);
            Console.WriteLine("反转后的Cartesian3经度:" + YzgMath.RadianToDegree(cartesian3.X));
            Console.WriteLine("反转后的Cartesian3纬度:" + YzgMath.RadianToDegree(cartesian3.Y));
        }

        static void test2()
        {

            SevenParameters sevenParameters = new SevenParameters();
            sevenParameters.XaxisDeviation = 0;
            sevenParameters.YaxisDeviation = 0;
            sevenParameters.ZaxisDeviation = 0;
            sevenParameters.XaxisRotateRadian = 0;
            sevenParameters.YaxisRotateRadian = 0;
            sevenParameters.ZaxisRotateRadian = 0;
            sevenParameters.ScaleParameter = 0;
            TransformationParameters parameters = new TransformationParameters();
            parameters.SevenParams = sevenParameters;
            parameters.SourceEllipsoid = WGS84.GetEllipsoid();
            parameters.TargetEllipsoid = CGCS2000.GetEllipsoid();
            parameters.SourceCenterMeridian = 120;
            parameters.TargetCenterMeridian = 120;
            parameters.SourceCoordinateType = CoordinateType.Cartographic;
            parameters.TargetCoordinateType = CoordinateType.Cartographic;

            SevenParamsTransformationService service = new SevenParamsTransformationService(parameters);

            Cartographic3 wgs84Point = new Cartographic3(Math.PI * 2 / 3, Math.PI / 6, 0);
            Console.WriteLine("wgs84经度:" + YzgMath.RadianToDegree(wgs84Point.Longitude));
            Console.WriteLine("wgs84纬度:" + YzgMath.RadianToDegree(wgs84Point.Latitude));
            Cartographic3 cgcs2000Point = (Cartographic3)service.SourceToTarget(wgs84Point);
            Console.WriteLine("cgcs2000经度:" + YzgMath.RadianToDegree(cgcs2000Point.Longitude));
            Console.WriteLine("cgcs2000纬度:" + YzgMath.RadianToDegree(cgcs2000Point.Latitude));
            Cartographic3 wgs84PointReverse = (Cartographic3)service.TargetToSource(cgcs2000Point);
            Console.WriteLine("反算后wgs84经度:" + YzgMath.RadianToDegree(wgs84PointReverse.Longitude));
            Console.WriteLine("反算后wgs84纬度:" + YzgMath.RadianToDegree(wgs84PointReverse.Latitude));
        }
    }
}
