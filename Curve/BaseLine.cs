using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curve
{
    public abstract  class BaseLine
    {
        public BaseLine(CanvasParam _cp)
        {
            this.cp = _cp;
            StartPointX = cp.OriginX;
            StartPointY = cp.OriginY;
            Hlength = cp.HorizontalLength - cp.ArrowLength - cp.BlankLegend;//减去箭头的长度以及空白长度
            Vlength = cp.VerticalLength - cp.ArrowLength - cp.BlankLegend;//减去箭头的长度以及空白长度
        }
        public float Hlength { set; get; }//纵向减去箭头的长度以及空白长度
        public float Vlength { set; get; }//横向减去箭头的长度以及空白长
        public CanvasParam cp { set; get; }
        //规定箭头方向为结束方向
        public float LineLength { set; get; }
        public float StartPointX { set; get; }
        public float StartPointY { get; set; }
        public float EndPointX { get; set; }
        public float EndPointY { set; get; }
        public RawColor4 color { set; get; }
        public abstract void Draw();
    }
}
