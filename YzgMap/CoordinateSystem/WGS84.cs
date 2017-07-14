using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YzgMap.Core;

namespace YzgMap.CoordinateSystem
{
    public class WGS84
    {
        public static Ellipsoid GetEllipsoid()
        {
            return new Ellipsoid(6378137.0, 6378137.0, 6356752.3142451793);
        }
    }
}