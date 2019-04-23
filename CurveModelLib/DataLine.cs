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
        private float UnitLength { set; get; }
        public  CoordinateAxisName name { set; get; }
        public CanvasParam cp { set; get; }
        public string Caption { get; set; } 
        public CoordinateParam coordianteParam {set; get;}
        public override void Draw()
        {
            UnitLength = coordianteParam.UnitLength;
            throw new NotImplementedException();
        }
    }
}