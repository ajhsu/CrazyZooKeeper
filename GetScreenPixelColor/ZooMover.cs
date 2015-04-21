using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MouseKeyboardLibrary;
using com.artgital.ajhsu;

namespace GetScreenPixelColor
{
    public class ZooMover
    {
        public event EventHandler MoveCompleted;

        public void Move(Point from, Point to)
        {
            MouseSimulator.Position = AGTGeometric.WPoint2DPoint(from);
            MouseSimulator.Click(MouseButton.Left);
            MouseSimulator.Position = AGTGeometric.WPoint2DPoint(to);
            MouseSimulator.Click(MouseButton.Left);

            Console.WriteLine("Move [{0},{1}] to [{2},{3}].", from.X, from.Y, to.X, to.Y);

            if (MoveCompleted != null)
            {
                MoveCompleted(this, null);
            }
        }
    }
}
