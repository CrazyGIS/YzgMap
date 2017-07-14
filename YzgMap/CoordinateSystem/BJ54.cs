using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YzgMap.Core;

namespace YzgMap.CoordinateSystem
{
    public class BJ54
    {
        public static Ellipsoid GetEllipsoid()
        {
            return new Ellipsoid(6378245.0, 6378245.0, 6356863.0188);
        }
    }
}