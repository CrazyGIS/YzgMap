using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YzgMap.Core
{
    public interface ICoordinate2
    {
        /// <summary>
		/// 横坐标
		/// </summary>
		double XAxis { get; set; }
        /// <summary>
        /// 纵坐标
        /// </summary>
        double YAxis { get; set; }

        ICoordinate2 Clone();
    }
}
