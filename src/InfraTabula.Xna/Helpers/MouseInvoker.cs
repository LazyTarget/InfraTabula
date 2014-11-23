using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using XnaLibrary;

namespace InfraTabula.Xna
{
    public static class MouseInvoker
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);


        public static Point GetMousePos(this GameBase game)
        {
            var inputState = game.InputState;
            var mouseState = inputState.CurrentState.Mouse;
            var mx = (uint)mouseState.X;
            var my = (uint)mouseState.Y;
            mx = (uint)MathHelper.Clamp(mx, 0, game.GraphicsDevice.DisplayMode.Width);
            my = (uint)MathHelper.Clamp(my, 0, game.GraphicsDevice.DisplayMode.Height);
            var point = new Point((int)mx, (int)my);
            return point;
        }

        public static void InvokeMouseDown(this GameBase game, MouseButtons mouseButton)
        {
            MouseEventFlags flags;
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    flags = MouseEventFlags.LEFTDOWN | MouseEventFlags.ABSOLUTE;
                    break;
                case MouseButtons.Right:
                    flags = MouseEventFlags.RIGHTDOWN | MouseEventFlags.ABSOLUTE;
                    break;
                case MouseButtons.Middle:
                    flags = MouseEventFlags.MIDDLEDOWN | MouseEventFlags.ABSOLUTE;
                    break;
                default:
                    throw new NotSupportedException();
            }

            var point = GetMousePos(game);
            mouse_event((uint) (flags), (uint) point.X, (uint) point.Y, 0, UIntPtr.Zero);
        }


        public static void InvokeMouseUp(this GameBase game, MouseButtons mouseButton)
        {
            MouseEventFlags flags;
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    flags = MouseEventFlags.LEFTUP | MouseEventFlags.ABSOLUTE;
                    break;
                case MouseButtons.Right:
                    flags = MouseEventFlags.RIGHTUP | MouseEventFlags.ABSOLUTE;
                    break;
                case MouseButtons.Middle:
                    flags = MouseEventFlags.MIDDLEUP | MouseEventFlags.ABSOLUTE;
                    break;
                default:
                    throw new NotSupportedException();
            }

            var point = GetMousePos(game);
            mouse_event((uint)(flags), (uint)point.X, (uint)point.Y, 0, UIntPtr.Zero);
        }
    }


    [Flags]
    public enum MouseEventFlags : uint
    {
        LEFTDOWN = 0x00000002,
        LEFTUP = 0x00000004,
        MIDDLEDOWN = 0x00000020,
        MIDDLEUP = 0x00000040,
        MOVE = 0x00000001,
        ABSOLUTE = 0x00008000,
        RIGHTDOWN = 0x00000008,
        RIGHTUP = 0x00000010
    }
}
