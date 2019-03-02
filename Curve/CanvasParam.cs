using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Curve
{
    using D2D = SharpDX.Direct2D1;
    using WIC = SharpDX.WIC;
    using DW = SharpDX.DirectWrite;
    using DXGI = SharpDX.DXGI;
    using SharpDX.Mathematics.Interop;
    public  class CanvasParam
    {
        //线箭头长度
        public float ArrowLength { get; set; }
        public float VerticalLength { set; get; } //垂直长度
        public float HorizontalLength { set; get; }//水平长度
        public float BlankLegend { set; get; }//给线标留空白
        public float OriginX { get; set; }
        public float OriginY { get; set; }
        public SharpDX.Direct2D1.RenderTarget _renderTarget { set; get; }
        public float ScaleLength { set; get; }//刻度线长度
        public float ScalePadding { set; get; }
        public D2D.Factory factory { set; get; }
        public DW.Factory dwFactory { set; get; }
        public ShowDataPoint showdatapoint{set;get;}
        public ShowCursorData showcursordata { set; get; }

    }
}
