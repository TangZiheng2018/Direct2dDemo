﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件所做的更改。
// </auto-generated>
//------------------------------------------------------------------------------
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CurveModelLib
{
    public class CoordinateLine:BaseLine
    {
        public string Caption { get; set; }
        public CoordinateAxisName Name { get; set; }
        public VirtualLineVisible virtualLineVisible { get; set; }
        public LineVisible lineVisible { get; set; }
        public LineDireciton lineDirection { get; set; }
        public LineLocation lineLocation { set; get; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        public float Interval { set; get; }
        public int Index { set; get; }
        public float UnitLength { set; get; }
        public float ArrowPointX1 { set; get; }
        public float ArrowPointY1 { set; get; }
        public float ArrowPointX2 { set; get; }
        public float ArrowPointY2 { set; get; }
        public List<Tuple<RawVector2, RawVector2>> scalePointList = new List<Tuple<RawVector2, RawVector2>>();
        public override void Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
