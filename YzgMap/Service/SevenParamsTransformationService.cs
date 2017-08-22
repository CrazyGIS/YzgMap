using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YzgMap.Core;
using YzgMap.Transformation;
using YzgMap.Transformation.SevenParameters;

namespace YzgMap.Service
{
    /// <summary>
    /// 七参数坐标转换服务
    /// </summary>
    public class SevenParamsTransformationService
    {
        private SevenParameters sevenParams; // 七参数
        private Ellipsoid sourceEllipsoid; // 源椭球
        private Ellipsoid targetEllipsoid; // 目标椭球
        private double sourceCenterMeridian; // 源中央经线
        private double targetCenterMeridian; // 目标中央经线
        private CoordinateType sourceCoordinateType; // 源坐标类型
        private CoordinateType targetCoordinateType; // 目标坐标类型

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="transformationParameters"></param>
        public SevenParamsTransformationService(TransformationParameters transformationParameters)
        {
            // 参数检查
            if (transformationParameters == null)
            {
                throw new ArgumentNullException("转换参数为空");
            }

            if(transformationParameters.SevenParams == null)
            {
                throw new ArgumentNullException("七参数为空");
            }

            if(transformationParameters.SourceEllipsoid == null)
            {
                throw new ArgumentNullException("源椭球为空");
            }

            if(transformationParameters.TargetEllipsoid == null)
            {
                throw new ArgumentNullException("目标椭球为空");
            }
            // 局部变量赋值
            this.sevenParams = transformationParameters.SevenParams;
            this.sourceEllipsoid = transformationParameters.SourceEllipsoid;
            this.targetEllipsoid = transformationParameters.TargetEllipsoid;
            this.sourceCenterMeridian = transformationParameters.SourceCenterMeridian;
            this.targetCenterMeridian = transformationParameters.TargetCenterMeridian;
            this.sourceCoordinateType = transformationParameters.SourceCoordinateType;
            this.targetCoordinateType = transformationParameters.TargetCoordinateType;
        }

        /// <summary>
        /// 源坐标转目标坐标
        /// </summary>
        /// <param name="sourceCoordinate"></param>
        /// <returns></returns>
        public ICoordinate3 SourceToTarget(ICoordinate3 sourceCoordinate)
        {
            if (sourceCoordinate == null)
            {
                return null;
            }
            // 中间变量
            Cartesian2 cartesian2 = null;
            Projection projection = null;

            // ------------------------
            // 第一步：源坐标转换为地理坐标
            // ------------------------

            Cartesian3 cartesian3 = null;
            Cartographic3 cartographic3 = null;

            switch (sourceCoordinateType)
            {
                case CoordinateType.Cartographic:
                    // 如果源是地理坐标，则不需要处理
                    cartographic3 = new Cartographic3(sourceCoordinate.XAxis, sourceCoordinate.YAxis, sourceCoordinate.ZAxis);
                    break;
                case CoordinateType.Cartesian:
                    // 如果源是笛卡尔坐标
                    // 处理方法：
                    // 1.高斯反算,转为地理坐标
                    cartesian3 = new Cartesian3(sourceCoordinate.XAxis, sourceCoordinate.YAxis, sourceCoordinate.ZAxis);
                    GaussKrugerTransformation gauss_source_1 = new GaussKrugerTransformation(sourceEllipsoid);
                    cartographic3 = gauss_source_1.GaussKrugerReverse(cartesian3, sourceCenterMeridian);
                    break;
                case CoordinateType.Projection:
                    // 如果源是投影坐标
                    // 处理方法：
                    // 1.投影坐标转笛卡尔坐标
                    // 2.高斯反算,转为地理坐标
                    GaussKrugerTransformation gauss_source_2 = new GaussKrugerTransformation(sourceEllipsoid);
                    projection = new Projection(sourceCoordinate.XAxis, sourceCoordinate.YAxis);
                    cartesian2 = gauss_source_2.ProjectionToCartesian(projection);
                    cartesian3 = new Cartesian3(cartesian2);
                    cartographic3 = gauss_source_2.GaussKrugerReverse(cartesian3, sourceCenterMeridian);
                    break;
                default:
                    break;
            }

            // ------------------------
            // 第二步：地理坐标转换为笛卡尔空间坐标
            // ------------------------
            Cartesian3Service cartesian3Service = new Cartesian3Service();
            cartesian3 = cartesian3Service.Cartographic3ToCartesian3(cartographic3, sourceEllipsoid);

            // ------------------------
            // 第三步：通过七参数模型，对笛卡尔空间坐标进行转换，转换后同样是笛卡尔空间坐标
            // ------------------------
            BursaWolfTransformation bursaWolfTransformation = new BursaWolfTransformation(sevenParams);
            cartesian3 = bursaWolfTransformation.Transform(cartesian3);

            // ------------------------
            // 第四步：笛卡尔空间坐标转换为地理坐标
            // ------------------------
            Cartographic3Service cartographic3Service = new Cartographic3Service();
            cartographic3 = cartographic3Service.Cartesian3ToCartographic3(cartesian3, targetEllipsoid);

            // ------------------------
            // 第五步：地理坐标转换为目标坐标
            // ------------------------
            ICoordinate3 result = null;

            switch(targetCoordinateType)
            {
                case CoordinateType.Cartographic:
                    // 如果目标是地理坐标，则不需要处理
                    result = cartographic3.Clone();
                    break;
                case CoordinateType.Cartesian:
                    // 如果目标是笛卡尔坐标
                    // 处理方法：
                    // 1.高斯正算,转为笛卡尔坐标系
                    GaussKrugerTransformation gauss_target_1 = new GaussKrugerTransformation(targetEllipsoid);
                    cartesian3 = gauss_target_1.GaussKrugerForward(cartographic3, targetCenterMeridian);
                    result = cartesian3.Clone();
                    break;
                case CoordinateType.Projection:
                    // 如果目标是投影坐标
                    // 处理方法：
                    // 1.高斯正算,转为笛卡尔坐标系
                    // 2.笛卡尔坐标转投影坐标
                    GaussKrugerTransformation gauss_target_2 = new GaussKrugerTransformation(targetEllipsoid);
                    cartesian3 = gauss_target_2.GaussKrugerForward(cartographic3, targetCenterMeridian);
                    cartesian2 = new Cartesian2(cartesian3);
                    projection = gauss_target_2.CartesianToProjection(cartesian2);
                    result = new Cartesian3(projection.X, projection.Y, 0);
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// 目标坐标转源坐标
        /// </summary>
        /// <param name="targetCoordinate"></param>
        /// <returns></returns>
        public ICoordinate3 TargetToSource(ICoordinate3 targetCoordinate)
        {
            return null;
        }

        /// <summary>
        /// 源坐标转目标坐标(批量)
        /// </summary>
        /// <param name="sourceCoordinates"></param>
        /// <returns></returns>
        public List<ICoordinate3> SourceToTargetBatch(IEnumerable<ICoordinate3> sourceCoordinates)
        {
            return null;
        }

        /// <summary>
        /// 目标坐标转源坐标(批量)
        /// </summary>
        /// <param name="targetCoordinates"></param>
        /// <returns></returns>
        public List<ICoordinate3> TargetToSourceBatch(IEnumerable<ICoordinate3> targetCoordinates)
        {
            return null;
        }
    }
}
