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
    using SharpDX.DirectWrite;

    public enum LineVisiable
    {
        Visible=0,
        Hide=1
    }
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
    public enum DataUnit
    {
        //分钟
        M = 0,
        //秒
        S = 1,
        //小时
        H = 2
    }
    public enum ShowDataPoint
    {  
        //显示
        Visible = 0,
        //隐藏
        Hide = 1
    }
    public enum ShowCursorData
    {
        //显示
        Visible = 0,
        //隐藏
        Hide = 1
    }
    public class MainCurve
    {
        RawColor4 color = new RawColor4(0, 0, 1, 1);
        public  CanvasParam canvasparam = new CanvasParam();
        D2D.Factory factory = new D2D.Factory();
        DW.Factory dwfactory = new DW.Factory();
        D2D.PixelFormat pf = new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
        D2D.RenderTargetProperties renderTargetProperties;
        D2D.HwndRenderTargetProperties hwndRenderTargetProperties = new D2D.HwndRenderTargetProperties();
        List<DataLine> dlList = new List<DataLine>();
        List<AxisLineParam> listAxisParam = new List<AxisLineParam>();
        List<BaseLine> listBaseLine = new List<BaseLine>();
        private D2D.RenderTarget _renderTarget;
        public MainCurve(Panel panel)
        {
            renderTargetProperties = new D2D.RenderTargetProperties(D2D.RenderTargetType.Default, pf, 0, 0, D2D.RenderTargetUsage.None, D2D.FeatureLevel.Level_DEFAULT);
            hwndRenderTargetProperties.Hwnd = panel.Handle;
            hwndRenderTargetProperties.PixelSize = new SharpDX.Size2(panel.Width, panel.Height);
            _renderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
            InitDataline();
        }
        private void InitDataline()
        {
            coordinate();
            DataLine dl = new DataLine(canvasparam, listAxisParam, "Temp");
            dl.Caption = "T1";
            dl.lineWith = 2;
            dl.listData = CreateData(10000);
            RawColor4 color = new RawColor4(1, 0, 0, 1);
            dl.color = color;
            dlList.Add(dl);
        }
        public void Draw(float x,float y)
        {
            _renderTarget.BeginDraw();
            _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));

            foreach (var item in listBaseLine)
            {
                item.Draw();
            }
            for (int i = 0; i < dlList.Count; i++)
            {
                dlList[i].Draw();
            }
             CursorData(x, y);     
            _renderTarget.EndDraw();
        }
        private void CursorData(float x, float y)
        {
            if (canvasparam.showcursordata==ShowCursorData.Hide)
            {
                return;
            }
            string value = "时间：";
            float time = 0F;
            if (dlList.Count>0)
            {
                var c= listAxisParam.Find(t => t.Attributes == "Time");
                time = (x - canvasparam.OriginX) < 0 ? 0 : (x - canvasparam.OriginX) / (dlList[0].Hlength/(c.MaxScale - c.MinScale));
            }
            value +=time.ToString("f2") + "\r\n";
            for (int i = 0; i <dlList.Count ; i++)
            {
                value += dlList[i].Caption + ":" + dlList[i].GetLineData(x, y)+"\r\n";
            }  
            var brush = new SharpDX.Direct2D1.SolidColorBrush(_renderTarget, new RawColor4(0,0,1,1));
            RawVector2 pointS = new RawVector2(x,canvasparam.OriginY);
            RawVector2 pointE = new RawVector2(x, canvasparam.OriginY-canvasparam.VerticalLength);
            _renderTarget.DrawLine(pointS, pointE, brush);
            _renderTarget.DrawText(value, new TextFormat(dwfactory, "Arial", 12), new RawRectangleF(x, y, x + 100, y + 100), brush);
        }
        private List<LineDataModel> CreateData(int count)
        {
            List<LineDataModel> data = new List<LineDataModel>();
            Random rd = new Random();
            for (int i = 0; i < count; i++)
            {
                LineDataModel ldm = new LineDataModel();
                ldm.dataUnit = DataUnit.M;
                ldm.Times = 1F;
                ldm.Values = 20;
                ldm.Values = rd.Next(-20, 20);
                data.Add(ldm);
            }
            return data;
        }
        public void coordinate()
        {
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
            canvasparam.showcursordata = ShowCursorData.Hide;
            canvasparam.showdatapoint = ShowDataPoint.Hide;
            AxisLineParam bHParam = new AxisLineParam();
            bHParam.Direction = LineDirection.Horizontal;
            bHParam.MaxScale = 10000;
            bHParam.MinScale = 0;
            bHParam.CellScale = 1000;
            bHParam.showVirtualLine = ShowVirtualLine.Visible;
            bHParam.Caption = "Time(Min)";
            bHParam.Attributes = "Time";
            BaseLine bH = new AxisLine(canvasparam, bHParam);
            bH.color = color;
            //bH.Draw();

            AxisLineParam bVParam = new AxisLineParam();
            bVParam.lineVisible = LineVisiable.Visible;
            bVParam.Direction = LineDirection.Vertical;
            bVParam.showVirtualLine = ShowVirtualLine.Visible;
            bVParam.MaxScale = 40;
            bVParam.MinScale = -40;
            bVParam.CellScale =10;
            bVParam.Caption = "Temp";
            bVParam.Attributes = "Temp";
            BaseLine bV = new AxisLine(canvasparam, bVParam);
            bV.color = color;
            //bV.Draw();

            AxisLineParam PowerParam = new AxisLineParam();
            PowerParam.lineVisible = LineVisiable.Visible;
            PowerParam.Direction = LineDirection.Vertical;
            PowerParam.showVirtualLine = ShowVirtualLine.Hide;
            PowerParam.lineLocation = LineLocation.Right;
            PowerParam.MaxScale = 500;
            PowerParam.MinScale = 0;
            PowerParam.CellScale = 100;
            PowerParam.Caption = "功率";
            PowerParam.Attributes = "Power";
            PowerParam.Index = 0;
            BaseLine bPower = new AxisLine(canvasparam, PowerParam);
            bPower.color = color;
            //bPower.Draw();
            listBaseLine.Add(bV);
            listBaseLine.Add(bH);
            listBaseLine.Add(bPower);
            listAxisParam.Add(bVParam);
            listAxisParam.Add(bHParam);
            listAxisParam.Add(PowerParam);
        }
    }

}
