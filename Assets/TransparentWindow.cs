/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;

public class TransparentWindow : MonoBehaviour {

    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    static extern long GetWindowRect(IntPtr hWnd, ref RECT lpRect);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern int GetWindowTextLength(IntPtr hWnd);

    private struct MARGINS {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_EXSTYLE = -20;

    const int GWL_STYLE = -16;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    const uint LWA_COLORKEY = 0x00000001;

    private IntPtr hWnd;

    private IntPtr dolphinHandle = IntPtr.Zero;

    [DllImport("user32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

    /// <summary>
    /// Returns a list of child windows
    /// </summary>
    /// <param name="parent">Parent of the windows to return</param>
    /// <returns>List of child windows</returns>
    public static List<IntPtr> GetChildWindows(IntPtr parent)
    {
        List<IntPtr> result = new List<IntPtr>();
        GCHandle listHandle = GCHandle.Alloc(result);
        try
        {
            EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
            EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
        }
        finally
        {
            if (listHandle.IsAllocated)
                listHandle.Free();
        }
        return result;
    }

    /// <summary>
    /// Callback method to be used when enumerating windows.
    /// </summary>
    /// <param name="handle">Handle of the next window</param>
    /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
    /// <returns>True to continue the enumeration, false to bail</returns>
    private static bool EnumWindow(IntPtr handle, IntPtr pointer)
    {
        GCHandle gch = GCHandle.FromIntPtr(pointer);
        List<IntPtr> list = gch.Target as List<IntPtr>;
        if (list == null)
        {
            throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
        }
        list.Add(handle);
        //  You can modify this to check to see if you want to cancel the operation, then return a null here
        return true;
    }

    /// <summary>
    /// Delegate for the EnumChildWindows method
    /// </summary>
    /// <param name="hWnd">Window handle</param>
    /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
    /// <returns>True to continue enumerating, false to bail.</returns>
    public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

    public static string GetText(IntPtr hWnd)
    {
        // Allocate correct string length first
        int length = GetWindowTextLength(hWnd);
        StringBuilder sb = new StringBuilder(length + 1);
        GetWindowText(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }


    private void Start() {
        //MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);

#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        //SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 1000, 1000, 0);
#endif

        Application.runInBackground = true;
    }

    private void Update()
    {
#if !UNITY_EDITOR
        if (dolphinHandle == IntPtr.Zero)
        {
            Process[] processes = Process.GetProcessesByName("Dolphin");
            if (processes.Length == 1)
            {
                List<IntPtr> children = GetChildWindows(processes[0].MainWindowHandle);
                foreach (IntPtr handle in children)
                {
                    string title = GetText(handle);
                    if (title != null && title.Contains("FPS"))
                    {
                        dolphinHandle = handle;
                    }
                }
            }
        }
        else
        {
            RECT rect = new RECT();
            GetWindowRect(dolphinHandle, ref rect);
            SetWindowPos(hWnd, HWND_TOPMOST, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom-rect.Top, 0);
        }
#endif
    }
}
