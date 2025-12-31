// ToastNotification.cs (Made in .NET 8.0)
// Created by kira024
// GitHub: https://github.com/kira024

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsExtras
{
    public partial class ToastNotification : Form
    {
        private static List<ToastNotification> Toasts = new List<ToastNotification>();

        private System.Windows.Forms.Timer timer;
        private int targetY;
        private int lifeTime = 3500;
        private int elapsed;

        public enum ToastType
        {
            Info,
            Success,
            Error,
            Warning
        }

        private enum State { Showing, Waiting, Closing }
        private State state = State.Showing;

        private Color accentColor;

        // Updated static Show method with customization
        public static void Show(
            string title,
            string message,
            ToastType type,
            Color? customColor = null,
            Font? titleFont = null,
            Font? messageFont = null,
            int? customLifeTime = null,
            Point? customLocation = null
        )
        {
            new ToastNotification(title, message, type, customColor, titleFont, messageFont, customLifeTime, customLocation).Show();
        }

        // Updated constructor with optional customization
        public ToastNotification(
            string title,
            string message,
            ToastType type,
            Color? customColor = null,
            Font? titleFont = null,
            Font? messageFont = null,
            int? customLifeTime = null,
            Point? customLocation = null
        )
        {
            // ---- Base Form ----
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.FromArgb(20, 20, 20);
            Size = new Size(350, 95);
            Opacity = 0;
            Font = new Font("Verdana", 9);

            accentColor = customColor ?? GetAccentColor(type);

            if (customLifeTime.HasValue)
                lifeTime = customLifeTime.Value;

            // ---- Title ----
            Label lblTitle = new Label
            {
                Text = title,
                Font = titleFont ?? new Font("Verdana", 9.5f, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(28, 12),
                AutoSize = true
            };

            // ---- Message ----
            Label lblMessage = new Label
            {
                Text = message,
                Font = messageFont ?? new Font("Verdana", 8.5f),
                ForeColor = Color.Gainsboro,
                Location = new Point(28, 36),
                MaximumSize = new Size(300, 0),
                AutoSize = true
            };

            Controls.Add(lblTitle);
            Controls.Add(lblMessage);

            // ---- Timer ----
            timer = new System.Windows.Forms.Timer { Interval = 15 };
            timer.Tick += Animate;

            // ---- Custom Location ----
            if (customLocation.HasValue)
                Location = customLocation.Value;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            int margin = 12;
            Rectangle wa = Screen.PrimaryScreen.WorkingArea;

            int index = Toasts.Count;
            if (Location == Point.Empty) // Only reposition if not custom location
                targetY = wa.Bottom - Height - margin - index * (Height + margin);

            if (Location == Point.Empty)
                Location = new Point(wa.Right - Width - margin, wa.Bottom);

            Toasts.Add(this);

            timer.Start();
        }

        private void Animate(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Showing:
                    Opacity = Math.Min(1, Opacity + 0.06);
                    Top -= 6;
                    if (Opacity >= 1 && Top <= targetY)
                    {
                        Top = targetY;
                        state = State.Waiting;
                    }
                    break;

                case State.Waiting:
                    elapsed += timer.Interval;
                    if (elapsed >= lifeTime)
                        state = State.Closing;
                    break;

                case State.Closing:
                    Opacity -= 0.06;
                    Top += 6;
                    if (Opacity <= 0)
                    {
                        timer.Stop();
                        Toasts.Remove(this);
                        Close();
                        Reposition();
                    }
                    break;
            }
        }

        private static void Reposition()
        {
            int margin = 12;
            Rectangle wa = Screen.PrimaryScreen.WorkingArea;

            for (int i = 0; i < Toasts.Count; i++)
            {
                Toasts[i].targetY =
                    wa.Bottom - Toasts[i].Height - margin - i * (Toasts[i].Height + margin);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = RoundedRect(ClientRectangle, 12))
            using (SolidBrush bg = new SolidBrush(BackColor))
            using (SolidBrush accent = new SolidBrush(accentColor))
            {
                e.Graphics.FillPath(bg, path);
                e.Graphics.FillRectangle(accent, 0, 0, 6, Height);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private Color GetAccentColor(ToastType type)
        {
            return type switch
            {
                ToastType.Success => Color.MediumSeaGreen,
                ToastType.Error => Color.IndianRed,
                ToastType.Warning => Color.Goldenrod,
                _ => Color.DodgerBlue
            };
        }
    }
}
