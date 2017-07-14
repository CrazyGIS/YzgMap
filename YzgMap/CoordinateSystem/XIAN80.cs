using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YzgMap.Core;

namespace YzgMap.CoordinateSystem
{
    public class XIAN80
    {
        public static Ellipsoid GetEllipsoid()
        {
            return new Ellipsoid(6378140.0, 6378140.0, 6356755.2882);
        }
    }
}