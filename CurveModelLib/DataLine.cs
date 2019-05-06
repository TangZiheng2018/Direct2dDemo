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
        public CoordinateLine coordinateLine { set; get;}
        public  void Draw()
        {
            UnitLength = coordinateLine.UnitLength;
            throw new NotImplementedException();
        }

        public override void Calculate()
        {
            throw new NotImplementedException();
        }
    }
}