using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    public interface IPositionData
    {
        int SizeLogo { get; }
        Size SizeRectangle { get; }
        int TransparencyRectangle { get; }
        Point RectanglePosition { get; }
        PointF LogoPosition { get; }
    }
}