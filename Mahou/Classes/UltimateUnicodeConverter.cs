using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
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
        //If input cant be converted in any of selected locales to any of them
        if (Mahou.KMHook.bothnotmatch)
        {
            return "ERROR";
        }
        //Check if all of them are not recongized by SECOND parse(because of firstall variable)
        var countofminus = ercher.Count(m1 => m1 == -1);
        if (countofminus == ercher.Count && countofminus > 0 && !firstcall) //because of two locales we should use !firstcall
        {
            //this will stop even foreach class in KeyHook class(it is in Try/Catch, so this will stop it immediately)
            throw Mahou.KMHook.notINany;
        }
        if (ercher.Contains(-1) && !Mahou.KMHook.bothnotmatch && firstcall)
        {
            StringBuilder inputfixed = new StringBuilder(input);
            //This gets all indexes of an -1 chars in input,
            var indexes = Enumerable.Range(0, ercher.Count).Where(i => ercher[i] == -1).ToList();
            //gets all chars by indexes from input,
            var chars = indexes.Select(index => input[index]).ToList();
            //these(above) save me a lot of time, thank you LINQ!
            //Replaces all unrecongnized chars by current locale(uID1), by another(uID2)
            for (int i = 0; i != indexes.Count; i++)
            {
                inputfixed.Remove(indexes[i], 1);
                string remaked = UltimateUnicodeConverter.InAnother(chars[i].ToString(), uID2, uID1, false);
                inputfixed.Insert(indexes[i], remaked);
            }
            return inputfixed.ToString();
        }
        #endregion
        #region Add another locale's char to result
        foreach (CaseChar sh in CaseChars.ToArray())
        {
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

