using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.artgital.ajhsu
{
    static public class AGTGeometric
    {
        static public System.Windows.Point DPoint2WPoint(System.Drawing.Point Point)
        {
            return new System.Windows.Point(Point.X, Point.Y);
        }

        static public System.Drawing.Point WPoint2DPoint(System.Windows.Point Point)
        {
            return new System.Drawing.Point((int)Point.X, (int)Point.Y);
        }
    }
}
