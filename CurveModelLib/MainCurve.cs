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
    using System.Windows.Forms;

    public class MainCurve
    {
        D2D.Factory factory = new D2D.Factory();
        DW.Factory dwfactory = new DW.Factory();
        D2D.PixelFormat pf = new D2D.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
        D2D.RenderTargetProperties renderTargetProperties;
        D2D.HwndRenderTargetProperties hwndRenderTargetProperties = new D2D.HwndRenderTargetProperties();
        CanvasParam canvasParam = null;
        CoordinateAxis coordinateAxis = null;
        public List<DataLine> dataLineList { set; get; }
        public List<CoordinateLine> coordianteParamList = new List<CoordinateLine>();
        private D2D.RenderTarget _renderTarget;
        //public MainCurve(int width,int height)
        public MainCurve(Control panel)
        {
            renderTargetProperties = new D2D.RenderTargetProperties(D2D.RenderTargetType.Default, pf, 0, 0, D2D.RenderTargetUsage.None, D2D.FeatureLevel.Level_DEFAULT);
            hwndRenderTargetProperties.Hwnd = panel.Handle;
            hwndRenderTargetProperties.PixelSize = new SharpDX.Size2(panel.Width, panel.Height);
            _renderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
            InitCanvasParam(panel.Width, panel.Height);
            InitCoordinateAxis(canvasParam);
            InitCoordinateParam();
        }
        int a = 0;
        public void Draw()
        {

                canvasParam._renderTarget.BeginDraw();
                _renderTarget.Clear(new RawColor4(0.752F, 0.862F, 0752F, 0));
                coordinateAxis.Draw();
                canvasParam._renderTarget.EndDraw();


            //foreach (var item in dataLineList)
            //{
            //    item.coordinateLine = coordianteParamList.Find(t => t.Name == item.name);
            //    item.cp = canvasParam;
            //    item.Draw();
            //}
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
            canvasParam._renderTarget = this._renderTarget;
            canvasParam.dwFactory = this.dwfactory;
            canvasParam.factory = this.factory;
        }
        private void InitCoordinateAxis(CanvasParam cp)
        {
            coordinateAxis = new CoordinateAxis(cp);
        }
        /// <summary>
        /// 初始化坐标轴参数
        /// </summary>
        /// <param name="coordinateParam">坐标轴参数</param>
        /// <param name="cp">画布参数</param>
        private void InitCoordinateParam()
        {
            coordianteParamList.Clear();
            CoordinateLine coordinateParamTime = new CoordinateLine();
            coordinateParamTime.Caption = "min";
            coordinateParamTime.Name = CoordinateAxisName.Time;
            coordinateParamTime.virtualLineVisible = VirtualLineVisible.Visible;
            coordinateParamTime.lineVisible = LineVisible.Visible;
            coordinateParamTime.lineDirection = LineDireciton.Horizontal;
            coordinateParamTime.MaxValue = 60;
            coordinateParamTime.MinValue = 0;
            coordinateParamTime.Interval = 10;
            coordinateParamTime.LineWidth = 1;

            CoordinateLine coordinateParamTemp = new CoordinateLine();
            coordinateParamTemp.Caption = "℃";
            coordinateParamTemp.Name = CoordinateAxisName.Temperature;
            coordinateParamTemp.virtualLineVisible = VirtualLineVisible.Visible;
            coordinateParamTemp.lineVisible = LineVisible.Visible;
            coordinateParamTemp.lineDirection = LineDireciton.Vertical;
            coordinateParamTemp.lineLocation = LineLocation.Left;
            coordinateParamTemp.MaxValue = 40;
            coordinateParamTemp.MinValue = -40;
            coordinateParamTemp.Interval = 10;
            coordinateParamTemp.LineWidth = 1;

            CoordinateLine coordinateParamPower = new CoordinateLine();
            coordinateParamPower.Caption = "Power";
            coordinateParamPower.Name = CoordinateAxisName.Power;
            coordinateParamPower.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateParamPower.lineVisible = LineVisible.Visible;
            coordinateParamPower.lineDirection = LineDireciton.Vertical;
            coordinateParamPower.lineLocation = LineLocation.Right;
            coordinateParamPower.Index = 0;
            coordinateParamPower.MaxValue = 500;
            coordinateParamPower.MinValue = 0;
            coordinateParamPower.Interval = 50;
            coordinateParamPower.LineWidth = 1F;

            CoordinateLine coordinateParamCurrent = new CoordinateLine();
            coordinateParamCurrent.Caption = "I";
            coordinateParamCurrent.Name = CoordinateAxisName.Current;
            coordinateParamCurrent.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateParamCurrent.lineVisible = LineVisible.Visible;
            coordinateParamCurrent.lineDirection = LineDireciton.Vertical;
            coordinateParamCurrent.lineLocation = LineLocation.Right;
            coordinateParamCurrent.Index = 1;
            coordinateParamCurrent.MaxValue = 5;
            coordinateParamCurrent.MinValue = 0;
            coordinateParamCurrent.Interval = 1;
            coordinateParamCurrent.LineWidth = 1F;

            CoordinateLine coordinateParamVoltage = new CoordinateLine();
            coordinateParamVoltage.Caption = "Vol";
            coordinateParamVoltage.Name = CoordinateAxisName.Voltage;
            coordinateParamVoltage.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateParamVoltage.lineVisible = LineVisible.Visible;
            coordinateParamVoltage.lineDirection = LineDireciton.Vertical;
            coordinateParamVoltage.lineLocation = LineLocation.Right;
            coordinateParamVoltage.Index = 2;
            coordinateParamVoltage.MaxValue = 400;
            coordinateParamVoltage.MinValue = 0;
            coordinateParamVoltage.Interval = 50;
            coordinateParamVoltage.LineWidth = 1F;

            CoordinateLine coordinateParamPF = new CoordinateLine();
            coordinateParamPF.Caption = "PF";
            coordinateParamPF.Name = CoordinateAxisName.PowerFactor;
            coordinateParamPF.virtualLineVisible = VirtualLineVisible.Hide;
            coordinateParamPF.lineVisible = LineVisible.Visible;
            coordinateParamPF.lineDirection = LineDireciton.Vertical;
            coordinateParamPF.lineLocation = LineLocation.Right;
            coordinateParamPF.Index = 3;
            coordinateParamPF.MaxValue = 1;
            coordinateParamPF.MinValue = 0;
            coordinateParamPF.Interval = 0.1F;
            coordinateParamPF.LineWidth = 1F;

            coordianteParamList.Add(coordinateParamTime);
            coordianteParamList.Add(coordinateParamTemp);
            coordianteParamList.Add(coordinateParamPower);
            coordianteParamList.Add(coordinateParamCurrent);
            coordianteParamList.Add(coordinateParamVoltage);
            coordianteParamList.Add(coordinateParamPF);
            coordinateAxis.coordianteParamList = coordianteParamList;
        }
    }
}