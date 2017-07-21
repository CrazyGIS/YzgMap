using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YzgMap;
using YzgMap.Core;
using YzgMap.Service;
using YzgMap.CoordinateSystem;

namespace ConsoleYzgMap
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
