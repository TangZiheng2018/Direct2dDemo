using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CurveModelLib
{
    using D2D = SharpDX.Direct2D1;
    using WIC = SharpDX.WIC;
    using DW = SharpDX.DirectWrite;
    using DXGI = SharpDX.DXGI;
    using SharpDX.DirectWrite;
    using SharpDX.Mathematics.Interop;

    public class MainCurve
    {
        D2D.Factory factory = new D2D.Factory();
        DW.Factory dwfactory = new DW.Factory();
        D2D.PixelFormat pf = new D2D.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
        D2D.RenderTargetProperties renderTargetProperties;
        D2D.HwndRenderTargetProperties hwndRenderTargetProperties = new D2D.HwndRenderTargetProperties();
        CanvasParam canvasParam = null;
        CoordinateParam coordinateParam = null;
        private List<CoordinateAxis> coordinateAxisList {set; get;}//坐标轴列表
        public List<DataLine> dataLineList { set; get; }
        public MainCurve(float width,float height)
        {
            InitCanvasParam(width, height);
            InitCoordinateParam(canvasParam);
            InitcoordinateAxis(coordinateParam,canvasParam);
        }
        /// <summary>
        /// 初始化画布各种参数
        /// </summary>
        /// <param name="width">画布宽</param>
        /// <param name="height">画布高</param>
        private void InitCanvasParam(float width,float height)
        {
            canvasParam = new CanvasParam();
            canvasParam.OriginX = 30;
            canvasParam.OriginY = 300;
            canvasParam.VerticalLength = height;
            canvasParam.HorizontalLength = width;
            canvasParam.Padding = 0;
        }
        /// <summary>
        ///初始化坐标轴固定参数
        /// </summary>
        /// <param name="cp">画布参数</param>
        private void InitCoordinateParam(CanvasParam cp)
        {
            coordinateParam.ArrowBlankLength = 20;
            coordinateParam.ArrowLength = 5;
            coordinateParam.HLength = cp.HorizontalLength - coordinateParam.ArrowLength - coordinateParam.ArrowBlankLength;
            coordinateParam.VLength = cp.VerticalLength - coordinateParam.ArrowLength - coordinateParam.ArrowBlankLength;
        }
        /// <summary>
        /// 初始化坐标轴
        /// </summary>
        /// <param name="coordinateParam">坐标轴参数</param>
        /// <param name="cp">画布参数</param>
        private void InitcoordinateAxis(CoordinateParam coordinateParam, CanvasParam cp)
        {
            RawColor4 color = new RawColor4(0, 0, 1, 1);

            CoordinateAxis coordinateAxisTime = new CoordinateAxis(coordinateParam);
            coordinateAxisTime.Name = CoordinateAxisName.Time;
            coordinateAxisTime.CanvasParam = cp;
            coordinateAxisTime.Color = color;
            coordinateAxisTime.virtualLineVisible = VirtualLineVisible.Visible;
            coordinateAxisTime.lineVisible = LineVisible.Visible;
            coordinateAxisTime.lineDirection = LineDireciton.Horizontal;
            coordinateAxisTime.Caption = "min";
            coordinateAxisTime.MaxValue = 60;
            coordinateAxisTime.MinValue = 0;
            coordinateAxisTime.Interval = 10;
            
            CoordinateAxis coordinateAxisTemp = new CoordinateAxis(coordinateParam);
            coordinateAxisTemp.Name = CoordinateAxisName.Temperature;
            coordinateAxisTemp.CanvasParam = cp;
            coordinateAxisTemp.Color = color;
            coordinateAxisTemp.virtualLineVisible = VirtualLineVisible.Visible;
            coordinateAxisTemp.lineVisible = LineVisible.Visible;
            coordinateAxisTemp.lineDirection = LineDireciton.Vertical;
            coordinateAxisTemp.lineLocation = LineLocation.Left;
            coordinateAxisTemp.Caption = "℃";
            coordinateAxisTemp.MaxValue = 40;
            coordinateAxisTemp.MinValue = -40;
            coordinateAxisTemp.Interval = 10;

            CoordinateAxis coordinateAxisPower = new CoordinateAxis(coordinateParam);
            coordinateAxisPower.Name = CoordinateAxisName.Power;
            coordinateAxisPower.CanvasParam = cp;
            coordinateAxisPower.Color = color;
            coordinateAxisPower.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateAxisPower.lineVisible = LineVisible.Visible;
            coordinateAxisPower.lineDirection = LineDireciton.Vertical;
            coordinateAxisPower.lineLocation = LineLocation.Right;
            coordinateAxisPower.Index = 0;
            coordinateAxisPower.Caption = "Power";
            coordinateAxisPower.MaxValue = 500;
            coordinateAxisPower.MinValue = 0;
            coordinateAxisPower.Interval = 50;

            CoordinateAxis coordinateAxisCurrent = new CoordinateAxis(coordinateParam);
            coordinateAxisPower.Name = CoordinateAxisName.Current;
            coordinateAxisCurrent.CanvasParam = cp;
            coordinateAxisCurrent.Color = color;
            coordinateAxisCurrent.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateAxisCurrent.lineVisible = LineVisible.Visible;
            coordinateAxisCurrent.lineDirection = LineDireciton.Vertical;
            coordinateAxisCurrent.lineLocation = LineLocation.Right;
            coordinateAxisCurrent.Index = 1;
            coordinateAxisCurrent.Caption = "I";
            coordinateAxisCurrent.MaxValue = 5;
            coordinateAxisCurrent.MinValue = 0;
            coordinateAxisCurrent.Interval = 1;

            CoordinateAxis coordinateAxisVoltage = new CoordinateAxis(coordinateParam);
            coordinateAxisVoltage.Name = CoordinateAxisName.Voltage;
            coordinateAxisVoltage.CanvasParam = cp;
            coordinateAxisVoltage.Color = color;
            coordinateAxisVoltage.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateAxisVoltage.lineVisible = LineVisible.Visible;
            coordinateAxisVoltage.lineDirection = LineDireciton.Vertical;
            coordinateAxisVoltage.lineLocation = LineLocation.Right;
            coordinateAxisVoltage.Index = 1;
            coordinateAxisVoltage.Caption = "Vol";
            coordinateAxisVoltage.MaxValue = 400;
            coordinateAxisVoltage.MinValue = 0;
            coordinateAxisVoltage.Interval =50;

            CoordinateAxis coordinateAxisPF = new CoordinateAxis(coordinateParam);
            coordinateAxisPF.Name = CoordinateAxisName.PowerFactor;
            coordinateAxisPF.CanvasParam = cp;
            coordinateAxisPF.Color = color;
            coordinateAxisPF.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateAxisPF.lineVisible = LineVisible.Visible;
            coordinateAxisPF.lineDirection = LineDireciton.Vertical;
            coordinateAxisPF.lineLocation = LineLocation.Right;
            coordinateAxisPF.Index = 1;
            coordinateAxisPF.Caption = "PF";
            coordinateAxisPF.MaxValue = 1;
            coordinateAxisPF.MinValue = 0;
            coordinateAxisPF.Interval = 0.1F;

            coordinateAxisList.Add(coordinateAxisTime);
            coordinateAxisList.Add(coordinateAxisTemp);
            coordinateAxisList.Add(coordinateAxisPower);
            coordinateAxisList.Add(coordinateAxisCurrent);
            coordinateAxisList.Add(coordinateAxisVoltage);
            coordinateAxisList.Add(coordinateAxisPF);
        }
    }
}