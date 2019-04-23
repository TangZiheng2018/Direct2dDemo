using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveModelLib
{
  public class LineDataModel
  {
        public LineDataModel()
        {
            dataUnit = DataUnit.M;
            Times = 1;
        }
        public float Values { set; get; }
        public DataUnit dataUnit { set; get; }//数值绘制单位
        public float Times { set; get; }//数值倍数
    }
}
