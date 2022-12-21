using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace percentage 
{
    class TrayIcon 
    {
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        static extern bool DestroyIcon(IntPtr handle);

        //**original-line   |   private const int fontSize = 18;
        private const int fontSize = 19;
        private const string font = "Segoe UI";

        private NotifyIcon notifyIcon;

        public TrayIcon() 
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();

            notifyIcon = new NotifyIcon();

            contextMenu.MenuItems.AddRange(new MenuItem[] { menuItem });

            menuItem.Click += new System.EventHandler(MenuItemClick);
            menuItem.Index = 0;
            menuItem.Text = "E&xit";

            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Visible = true;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }

        //posible problema al definir el espacio que necesita el Bitmap para mostrar el texto
        private Bitmap GetTextBitmap(String text, Font font, Color fontColor) 
        {            
            SizeF imageSize = GetStringImageSize(text, font);
            Bitmap bitmap = new Bitmap((int)imageSize.Width, (int)imageSize.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap)) 
            {
                graphics.Clear(Color.FromArgb(0, 0, 0, 0));
                using (Brush brush = new SolidBrush(fontColor)) 
                {
                    graphics.DrawString(text, font, brush, 0, 0);
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    graphics.Save();
                }
            }
            return bitmap;
        }
        /***POSIBLE SOLUCION***
        private Bitmap GetTextBitmap(String text, Font font, Color fontColor)
        {
            // Obtener solo los primeros 4 caracteres de la cadena de texto
            String truncatedText = text.Substring(0, Math.Min(text.Length, 4));
        
            // Calcular el tamaño de la imagen necesaria para la cadena truncada
            SizeF imageSize = GetStringImageSize(truncatedText, font);
        
            Bitmap bitmap = new Bitmap((int)imageSize.Width, (int)imageSize.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.FromArgb(0, 0, 0, 0));
                using (Brush brush = new SolidBrush(fontColor))
                {
                    graphics.DrawString(truncatedText, font, brush, 0, 0);
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    graphics.Save();
                }
            }
            return bitmap;
        }
        */

        //posible problema al definir el espacio que necesita el Bitmap para mostrar el texto
        private static SizeF GetStringImageSize(string text, Font font)
        {
            using (Image image = new Bitmap(1, 1))
            using (Graphics graphics = Graphics.FromImage(image))
                return graphics.MeasureString(text, font);
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            String percentage = (powerStatus.BatteryLifePercent * 100).ToString();
            bool isCharging = SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;
            //**original-line   |   String bitmapText = isCharging ? percentage + "*" : percentage;
            String bitmapText = isCharging ? percentage + "%" : percentage + "%";
            //when printing the text, the space has a small size that does not adapt when adding more than 2 characters: example [60]
            //it must be adapted to fit 4 characters: example [100%].
            Color bitmapColor;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (key != null)
            {
                bitmapColor = (int)key.GetValue("SystemUsesLightTheme") == 0 ? Color.White : Color.Black;
                key.Close();
            }
            else
            {
                bitmapColor = Color.White;
            }
            using (Bitmap bitmap = new Bitmap(GetTextBitmap(bitmapText, new Font(font, fontSize), bitmapColor)))
            {
                System.IntPtr intPtr = bitmap.GetHicon();
                try
                {
                    using (Icon icon = Icon.FromHandle(intPtr))
                    {
                        notifyIcon.Icon = icon;
                        //**original-line   |   String toolTipText = percentage + "%" + (isCharging ? " Charging" : "");
                        String toolTipText = percentage + "%" + (isCharging ? " Charging" : " Disconnected");
                        notifyIcon.Text = toolTipText;
                    }
                }
                finally
                {
                    DestroyIcon(intPtr);
                }
            }
        }
    }
}
