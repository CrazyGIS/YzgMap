using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YzgMap.Core;

namespace YzgMap.Transformation.SevenParameters
{
    /// <summary>
	/// 布尔萨-沃尔夫 转换
	/// </summary>
    public class BursaWolfTransformation
    {
        private double rx; // X轴旋转角度(弧度)
        private double ry; // Y轴旋转角度(弧度)
        private double rz; // Z轴旋转角度(弧度)
        private double dx; // X轴平移长度(米)
        private double dy; // Y轴平移长度(米)
        private double dz; // Z轴平移长度(米)
        private double m;  // 尺度参数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameters">七参数</param>
        public BursaWolfTransformation(SevenParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("SevenParameters");
            }

            this.rx = parameters.XaxisRotateRadian;
            this.ry = parameters.YaxisRotateRadian;
            this.rz = parameters.ZaxisRotateRadian;
            this.dx = parameters.XaxisDeviation;
            this.dy = parameters.YaxisDeviation;
            this.dz = parameters.ZaxisDeviation;
            this.m = parameters.ScaleParameter;
        }

        /// <summary>
        /// 根据七参数对坐标进行转换
        /// </summary>
        /// <param name="cartesian">转换前坐标</param>
        /// <returns>转换后坐标</returns>
        public Cartesian3 Transform(Cartesian3 cartesian)
        {
            if (cartesian == null)
            {
                return null;
            }
            double inputX = cartesian.X;
            double inputY = cartesian.Y;
            double inputZ = cartesian.Z;
            double outputX = 0;
            double outputY = 0;
            double outputZ = 0;
            this.sevenParamTransform(inputX, inputY, inputZ, out outputX, out outputY, out outputZ);

            Cartesian3 result = new Cartesian3(outputX, outputY, outputZ);
            return result;
        }

        /// <summary>
        /// 七参数转换
        /// </summary>
        /// <param name="inputX"></param>
        /// <param name="inputY"></param>
        /// <param name="inputZ"></param>
        /// <param name="outputX"></param>
        /// <param name="outputY"></param>
        /// <param name="outputZ"></param>
        private void sevenParamTransform(double inputX, double inputY, double inputZ,
            out double outputX, out double outputY, out double outputZ)
        {
            double[,] rotationMatrix = MatrixTransformation.RotationMatrix(rx, ry, rz);
            double[,] originalCoord = new double[3, 1];
            originalCoord[0, 0] = inputX;
            originalCoord[1, 0] = inputY;
            originalCoord[2, 0] = inputZ;

            double[,] resCoord = MatrixTransformation.MatrixProduct(rotationMatrix, originalCoord);

            outputX = resCoord[0, 0] * (m + 1) + dx;
            outputY = resCoord[1, 0] * (m + 1) + dy;
            outputZ = resCoord[2, 0] * (m + 1) + dz;
        }
    }
}
