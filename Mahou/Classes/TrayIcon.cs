using System;
using System.Windows.Forms;

namespace Mahou
{
    public class TrayIcon
    {
        public event EventHandler<EventArgs> Exit;
        public event EventHandler<EventArgs> ShowHide;
        private NotifyIcon trIcon;
        private ContextMenu cMenu;
        private MenuItem Exi, ShHi;
        public TrayIcon(bool? visible = true)
        {
            trIcon = new NotifyIcon();
            cMenu = new ContextMenu();
            trIcon.Icon = Properties.Resources.Mahou;
            trIcon.Visible = visible == true;
            Exi = new MenuItem("Exit", ExitHandler);
            ShHi = new MenuItem("Show/Hide", ShowHideHandler);
            cMenu.MenuItems.Add(ShHi);
            cMenu.MenuItems.Add(Exi);
            trIcon.Text = "Mahou (魔法)\nA magical layout switcher.";
            trIcon.ContextMenu = cMenu;
            trIcon.MouseDoubleClick += ShowHideHandler;
            trIcon.BalloonTipClicked += ExitHandler;
        }
        private void ExitHandler(object sender, EventArgs e)
        {
            if (Exit != null)
            {
                Exit(this, null);
            }
        }
        private void ShowHideHandler(object sender, EventArgs e)
        {
            if (ShowHide != null)
            {
                ShowHide(this, null);
            }
        }
        public void Hide()
        {
            trIcon.Visible = false;
        }
        public void Show()
        {
            trIcon.Visible = true;
        }
        public void RefreshText(string TrText, string ShHiText, string ExiText)
        {
            trIcon.Text = TrText;
            ShHi.Text = ShHiText;
            Exi.Text = ExiText;
        }
    }
}
