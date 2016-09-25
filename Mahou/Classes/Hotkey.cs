using System;

namespace Mahou
{
	public class Hotkey
	{
		public readonly int keyCode;
		public readonly bool[] modifs;
		/// <summary>
		/// Initializes an hotkey, modifs are [ctrl, shift, alt].
		/// </summary>
		public Hotkey(int keyCode, bool[] modifs)
		{
			this.keyCode = keyCode;
			this.modifs = modifs;
		}
		public override bool Equals(object o)
		{
			return o is Hotkey && ((Hotkey)o).keyCode == keyCode &&
			((Hotkey)o).modifs[0] == modifs[0] &&
			((Hotkey)o).modifs[1] == modifs[1] &&
			((Hotkey)o).modifs[2] == modifs[2];
		}
		public override int GetHashCode()
		{
			return this.keyCode.GetHashCode() + this.modifs.GetHashCode();
		}
		public static bool[] GetMods(string hkmods) //Gets awaible in hotkey modifiers
		{
			return new bool[] { hkmods.Contains("Control"), hkmods.Contains("Shift"), hkmods.Contains("Alt") };
		}
	}
}
