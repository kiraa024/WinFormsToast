ToastNotification
=================

A fully customizable toast notification system for C# WinForms.
Created by kira024: https://github.com/kira024

Display lightweight notifications with support for custom colors, fonts, lifetime, and position.

Features
--------

- Thread-safe, non-blocking toast notifications
- Supports multiple types: Info, Success, Warning, Error
- Customizable accent color per toast
- Custom fonts for title and message
- Custom display duration (lifetime)
- Custom screen position
- Automatic stacking and repositioning of multiple toasts
- Rounded corners and smooth fade/slide animations

Installation
------------

1. Clone or download this repository.
2. Add ToastNotification.cs to your WinForms project.
3. Import the namespace:

    using WinFormsExtras;

Showcase
-----
![Demo of ToastNotification](Docs/ToastNotification/showcase.gif)

Usage
-----

Default Toasts:

    ToastNotification.Show("Info", "This is a standard info toast", ToastNotification.ToastType.Info);
    ToastNotification.Show("Success", "Operation completed successfully", ToastNotification.ToastType.Success);
    ToastNotification.Show("Warning", "This is a warning", ToastNotification.ToastType.Warning);
    ToastNotification.Show("Error", "Something went wrong!", ToastNotification.ToastType.Error);

Custom Accent Color:

    ToastNotification.Show(
        "Custom Color",
        "This toast has a custom purple accent",
        ToastNotification.ToastType.Info,
        Color.MediumPurple
    );

Custom Fonts:

    ToastNotification.Show(
        "Custom Fonts",
        "Title is Arial Bold, message is Consolas",
        ToastNotification.ToastType.Success,
        null,
        new Font("Arial", 11, FontStyle.Bold),    // Title font
        new Font("Consolas", 9)                   // Message font
    );

Custom Lifetime:

    ToastNotification.Show(
        "Long Toast",
        "This toast will stay for 6 seconds",
        ToastNotification.ToastType.Warning,
        null,
        null,
        null,
        6000   // Lifetime in milliseconds
    );

Custom Position:

    ToastNotification.Show(
        "Top Left",
        "This toast appears at a custom position",
        ToastNotification.ToastType.Info,
        null,
        null,
        null,
        null,
        new Point(20, 20) // X, Y coordinates on screen
    );

ToastType Enum
--------------

Type       Default Color
----       -------------
Info       DodgerBlue
Success    MediumSeaGreen
Warning    Goldenrod
Error      IndianRed

Customization Options
---------------------

- customColor – Accent color of the toast.
- titleFont – Font for the title label.
- messageFont – Font for the message label.
- customLifeTime – Duration in milliseconds before the toast closes.
- customLocation – Position on screen (Point struct).

License
-------

MIT License – free to use, modify, and distribute.
