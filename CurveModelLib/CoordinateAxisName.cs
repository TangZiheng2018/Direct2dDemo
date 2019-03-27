using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveModelLib
{
   public enum CoordinateAxisName
    {
        /// <summary>
        /// 温度
        /// </summary>
        Temperature=0,
        /// <summary>
        /// 功率
        /// </summary>
        Power=1,
        /// <summary>
        /// 电流
        /// </summary>
        Current=2,
        /// <summary>
        /// 电压
        /// </summary>
        Voltage=3,
        /// <summary>
        /// 功率因数
        /// </summary>
        PowerFactor=4,
        /// <summary>
        /// 时间
        /// </summary>
        Time=5
    }
}
