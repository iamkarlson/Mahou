using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;

class UltimateUnicodeConverter
{
    public static string InAnother(string input, uint uID1, uint uID2, bool firstcall)
    {
        //actaully this impossible, but anyway...
        if (input == "")
        {
            return "ERROR";
        }
        #region Variables
        //This is CaseChars(chars with attribute lower or upper case)
        List<CaseChar> CaseChars = new List<CaseChar>();
        //This is our return string
        var result = "";
        #endregion
        #region Make CaseChars from input
        foreach (char c in input.ToCharArray())
        {
            var upper = false;
            var cc = c;
            //Checks if 'c' is Upper Case or Matches UpperSymbols Regex
            if (char.IsUpper(cc) ||
                //This is the symbols that can be writen just with Shift, for most locales these are fine, but...
                new Regex("[!@#$%^&*()_+|{}:\"<>?№~]").IsMatch(cc.ToString()))
            { upper = true; }
            //this scans char 'c' code and adds it to CaseChars list
            CaseChars.Add(new CaseChar { chcode = VkKeyScanEx(cc, (IntPtr)uID1), upper = upper });
        }
        #endregion
        #region Check for errors
        //Error checker list of shorts
        List<short> ercher = new List<short>();
        foreach (CaseChar cc in CaseChars)
        {
            ercher.Add(cc.chcode);
        }
        /*THERE WAS ERROR CHECKER CODE...
          DUE TO NEW TECHNOLOGY IS NOT USED ANYMORE*/
        #endregion
        #region Add another locale's char to result
        foreach (CaseChar sh in CaseChars.ToArray())
        {
            if (sh.chcode == -1)
            {
                result += '♥'; //Pretty :3
            }
            byte[] byt = new byte[256];
            //it needs just 1 but,anyway let it be 10, i think that's better
            StringBuilder s = new StringBuilder(10);
            if (sh.upper)
            {
                byt[(int)Keys.ShiftKey] = 0xFF;
            }
            //"Convert magick✩" is the string below
            var ant = ToUnicodeEx((uint)sh.chcode, (uint)sh.chcode, byt, s, s.Capacity, 0, (IntPtr)uID2);
            if (sh.chcode != -1)
                result += s;
        }
        // Restores not recongized chars
        StringBuilder inputfixed = new StringBuilder(result);
        var indexes = Enumerable.Range(0, ercher.Count).Where(i => ercher[i] == -1).ToList();
        for (int i = 0; i != indexes.Count; i++)
        {
            inputfixed.Remove(indexes[i], 1);
            inputfixed.Insert(indexes[i], input[indexes[i]].ToString());
            result = inputfixed.ToString();
        }
        #endregion
        return result;
    }
    public struct CaseChar //Case Char struct
    {
        public short chcode;
        public bool upper;
    }
    #region DLL Imports
    [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
    private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState,
     StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern short VkKeyScanEx(char ch, IntPtr dwhkl);
    #endregion
}

