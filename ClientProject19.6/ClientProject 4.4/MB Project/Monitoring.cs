using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ClientProject.MainWindow;

namespace ClientProject
{
    public class Monitoring
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetCapture(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern long ReleaseCapture();
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point p);
        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string lpString);
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int nVirtKey);

        //WORKS ONLY ON WPF APPLICATIONS
        /*
        private void InitializeCursorMonitoring()
        {
            Point pointToWindow = Mouse.GetPosition(this);
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += delegate
            {
                Application.Current.MainWindow.CaptureMouse();
                if (pointToWindow != Mouse.GetPosition(Application.Current.MainWindow)) // אם העכבר יוצא מגבולות 
                {
                    pointToWindow = Mouse.GetPosition(Application.Current.MainWindow);
                    Point pointToScreen = pointToScreen(pointToWindow);
                    Title = pointToScreen.ToString();
                    Title = String.Format($"X:{pointToScreen.X} Y:{pointToScreen.Y}");
                    Point p = Point(Convert.ToInt32(pointToScreen.X), Convert.ToInt32(pointToScreen.Y));
                    IntPtr hWnd = WindowFromPoint(p); // מחזירה אובייקט שהוא חלון של ווינדוז
                    if (hWnd != IntPtr.Zero) // מוודא שהחלון קיים
                        SetWindowText(hWnd, String.Format($"X:{pointToScreen.X} Y:{pointToScreen.Y}"));
                }

                Application.Current.MainWindow.ReleaseMouseCapture();
            };
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }
        */

        // WORKS ONLY ON CONSOLE APPS
        public static void keyboardMonitoring()
        {
            MainWindow mainWindow = Application.Current.Windows[0] as MainWindow; // ?
            //mainWindow.status.Content = "monitoring";
            int counter = 0;
            do
            {
                if (GetAsyncKeyState((int)ConsoleKey.O) != 0)
                    counter++;
                if (GetAsyncKeyState((int)ConsoleKey.K) != 0)
                    counter--;
            } while (GetAsyncKeyState((int)ConsoleKey.Escape) == 0); // immediatly exits the method
        }
    }
}