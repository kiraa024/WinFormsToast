using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsExtras;

namespace ToastNotificationDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ----- Default Toasts -----
            ToastNotification.Show("Info", "This is a standard info toast", ToastNotification.ToastType.Info);
            ToastNotification.Show("Success", "Operation completed successfully", ToastNotification.ToastType.Success);
            ToastNotification.Show("Warning", "This is a warning", ToastNotification.ToastType.Warning);
            ToastNotification.Show("Error", "Something went wrong!", ToastNotification.ToastType.Error);

            // ----- Custom Color -----
            ToastNotification.Show(
                "Custom Color",
                "This toast has a custom purple accent",
                ToastNotification.ToastType.Info,
                Color.MediumPurple
            );

            // ----- Custom Fonts -----
            ToastNotification.Show(
                "Custom Fonts",
                "Title is Arial Bold, message is Consolas",
                ToastNotification.ToastType.Success,
                null,
                new Font("Arial", 11, FontStyle.Bold),
                new Font("Consolas", 9)
            );

            // ----- Custom Lifetime -----
            ToastNotification.Show(
                "Long Toast",
                "This toast will stay for 6 seconds",
                ToastNotification.ToastType.Warning,
                null,
                null,
                null,
                6000
            );

            // ----- Custom Position -----
            ToastNotification.Show(
                "Top Left",
                "This toast appears at a custom position",
                ToastNotification.ToastType.Info,
                null,
                null,
                null,
                null,
                new Point(20, 20)
            );

            // Keep the app running to see the toasts
            Application.Run();
        }
    }
}
