using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class ICheckings
{
	/// <summary>
	/// Checks if current cursor is IBeam.
	/// </summary>
	public static bool IsICursor()
	{
	    var h = Cursors.IBeam.Handle;
	    CURSORINFO cInfo;
	    cInfo.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
	    GetCursorInfo(out cInfo);
	    return cInfo.hCursor == h;
	}
	[StructLayout(LayoutKind.Sequential)]
	struct POINT
	{
	    public Int32 x;
	    public Int32 y;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	struct CURSORINFO
	{
	    public Int32 cbSize;
	    public Int32 flags;
	    public IntPtr hCursor; 
	    public POINT ptScreenPos; 
	}
	
	[DllImport("user32.dll")]
	static extern bool GetCursorInfo(out CURSORINFO pci);
}
