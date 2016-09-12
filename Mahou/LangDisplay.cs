using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mahou
{
	public partial class LangDisplay : Form
	{
		public LangDisplay()
		{
			InitializeComponent();
		}
		public void ChangeLD(string to)
		{
			lbLang.Text = to;
		}
		public void ChangeColors(Color fore, Color back)
		{
			lbLang.ForeColor = fore;
			lbLang.BackColor = back;
		}
	}
}
