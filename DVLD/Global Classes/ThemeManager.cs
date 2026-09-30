using System;
using System.Drawing;
using System.Runtime.InteropServices;   //  مشان لشريط يلي فوق II
using System.Windows.Forms;

namespace DVLD.Global_Classes
{
    public static class ThemeManager  //  مشان الوان الفورمز تتفير II
    {
        public static bool IsDarkMode { get; private set; } = false;

        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_TEXT_COLOR = 36;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd,
            int dwAttribute,
            ref int pvAttribute,
            int cbAttribute
        );

        private static void SetTitleBarColors(
            Form form,
            Color backgroundColor,
            Color textColor)
        {
            int background =
                backgroundColor.R |
                (backgroundColor.G << 8) |
                (backgroundColor.B << 16);

            int text =
                textColor.R |
                (textColor.G << 8) |
                (textColor.B << 16);

            DwmSetWindowAttribute(
                form.Handle,
                DWMWA_CAPTION_COLOR,
                ref background,
                sizeof(int)
            );

            DwmSetWindowAttribute(
                form.Handle,
                DWMWA_TEXT_COLOR,
                ref text,
                sizeof(int)
            );
        }

        public static void SetDarkMode()
        {
            IsDarkMode = true;

            foreach (Form form in Application.OpenForms)
            {
                ApplyTheme(form);
            }
        }

        public static void SetLightMode()
        {
            IsDarkMode = false;

            foreach (Form form in Application.OpenForms)
            {
                ApplyTheme(form);
            }
        }

        public static void ApplyTheme(Form form)
        {
            if (IsDarkMode)
            {
                form.BackColor = Color.Silver;

                SetTitleBarColors(
                    form,
                    Color.FromArgb(31, 31, 31),
                    Color.White
                );
            }
            else
            {
                form.BackColor = Color.White;

                SetTitleBarColors(
                    form,
                    Color.FromArgb(255,255,255),
                    Color.Black
                );
            }
        }

        public static void ApplyThemeToOpenForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                Color targetColor =
                    IsDarkMode ? Color.Silver : Color.White;

                if (form.BackColor != targetColor)
                {
                    ApplyTheme(form);
                }
            }
        }
    }
}