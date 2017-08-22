using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YzgMap.Core
{
    public interface ICoordinate3
    {
        /// <summary>
		/// 横坐标
		/// </summary>
		double XAxis { get; set; }
        /// <summary>
        /// 纵坐标
        /// </summary>
        double YAxis { get; set; }
        /// <summary>
        /// 竖坐标
        /// </summary>
        double ZAxis { get; set; }

        ICoordinate3 Clone();
    }
}
