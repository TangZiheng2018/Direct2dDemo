using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Mathematics.Interop;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System.Windows.Forms;
namespace Curve
{
    using D2D = SharpDX.Direct2D1;
    using WIC = SharpDX.WIC;
    using DW = SharpDX.DirectWrite;
    using DXGI = SharpDX.DXGI;

    public enum LineDirection
    {
        //水平
        Horizontal = 0,
        //垂直
        Vertical = 1
    }
    public enum LineLocation
    {
        //居左
        Left = 0,
        //居右
        Right = 1
    }
    public enum ShowVirtualLine
    {
        //显示
        Visible = 0,
        //隐藏
        Hide = 1
    }
    public class MainCurve
    {
        RawColor4 color = new RawColor4(0, 0,1, 1);
        CanvasParam canvasparam = new CanvasParam();
        D2D.Factory factory = new D2D.Factory();
        DW.Factory dwfactory = new DW.Factory();
        D2D.PixelFormat pf = new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
        D2D.RenderTargetProperties renderTargetProperties;
        D2D.HwndRenderTargetProperties hwndRenderTargetProperties = new D2D.HwndRenderTargetProperties();
        private D2D.RenderTarget _renderTarget;
        public MainCurve(Panel panel)
        {
            renderTargetProperties = new D2D.RenderTargetProperties(D2D.RenderTargetType.Default, pf, 0, 0, D2D.RenderTargetUsage.None, D2D.FeatureLevel.Level_DEFAULT);
            hwndRenderTargetProperties.Hwnd =panel.Handle;
            hwndRenderTargetProperties.PixelSize = new SharpDX.Size2(panel.Width,panel.Height);
            _renderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
        }
        public void Draw()
        {
            _renderTarget.BeginDraw();
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
            _renderTarget.EndDraw();
            canvasparam.factory = factory;
            canvasparam.dwFactory = dwfactory;
            canvasparam.ArrowLength = 6;
            canvasparam._renderTarget = _renderTarget;
            canvasparam.OriginX = 30;
            canvasparam.OriginY = 300;
            canvasparam.VerticalLength = 280;
            canvasparam.HorizontalLength = 600;
            canvasparam.BlankLegend = 30;
            canvasparam.ScaleLength = 5;
            canvasparam.ScalePadding = 0;
            LineParam bHParam = new LineParam();
            bHParam.Direction = LineDirection.Horizontal;
            bHParam.MaxScale = 80;
            bHParam.MinScale = -40;
            bHParam.CellScale = 10;
            bHParam.showVirtualLine = ShowVirtualLine.Visible;
            bHParam.Caption = "Time(Min)";
            AxisLine bH = new AxisLine(canvasparam, bHParam);
            bH.Draw(color);
            LineParam bVParam = new LineParam();
            bVParam.Direction = LineDirection.Vertical;
            bVParam.showVirtualLine = ShowVirtualLine.Visible;
            bVParam.MaxScale = 115;
            bVParam.MinScale = 0;
            bVParam.CellScale = 5;
            bVParam.Caption = "Temp";
            AxisLine bV = new AxisLine(canvasparam, bVParam);
            bV.Draw(color);
        }
    }

}
