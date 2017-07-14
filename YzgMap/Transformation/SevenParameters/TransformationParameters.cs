using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YzgMap.Core;

namespace YzgMap.Transformation.SevenParameters
{
    /// <summary>
    /// 转换参数
    /// </summary>
    public class TransformationParameters
    {
        /// <summary>
		/// 七参数
		/// </summary>
		public SevenParameters SevenParams { get; set; }

        /// <summary>
        /// 源椭球
        /// </summary>
        public Ellipsoid SourceEllipsoid { get; set; }

        /// <summary>
        /// 目标椭球
        /// </summary>
        public Ellipsoid TargetEllipsoid { get; set; }

        /// <summary>
        /// 源坐标系统中央经线
        /// </summary>
        public double SourceCenterMeridian { get; set; }
        /// <summary>
        /// 目标坐标系统中央经线
        /// </summary>
        public double TargetCenterMeridian { get; set; }

        /// <summary>
        /// 源坐标类型
        /// </summary>
        public CoordinateType SourceCoordinateType { get; set; }

        /// <summary>
        /// 目标坐标类型
        /// </summary>
        public CoordinateType TargetCoordinateType { get; set; }
    }
}