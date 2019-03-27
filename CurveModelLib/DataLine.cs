using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurveModelLib
{
    public class DataLine : BaseLine
    {
        public DataLine(CoordinateAxisName name)
        {
            this.name = name;
        }
        public List<LineDataModel> LineData { get; set; }
        CoordinateAxisName name;
        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}