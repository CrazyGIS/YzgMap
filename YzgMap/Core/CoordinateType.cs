using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YzgMap.Core
{
    /// <summary>
	/// 坐标类型
	/// </summary>
	public enum CoordinateType
    {
        /// <summary>
        /// 地理坐标
        /// </summary>
        Cartographic = 0,
        /// <summary>
        /// 笛卡尔坐标
        /// </summary>
        Cartesian = 1,
        /// <summary>
        /// 投影坐标
        /// </summary>
        Projection = 2
    }
}