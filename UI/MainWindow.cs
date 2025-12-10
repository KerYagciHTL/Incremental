// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace UI;

public class MainWindow : Window
{
    private readonly StackDisplay _display = new();
    private string _currentInput = "";

    public MainWindow()
    {
        Title = "Incremental UI";
        Background = Brushes.Black;
        CanResize = false;

        AddHandler(KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);

        var mainPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10),
            Spacing = 10
        };

        mainPanel.Children.Add(_display);
        Content = mainPanel;

    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        string? label = e.Key switch
        {
            Key.D0 or Key.NumPad0 => "0",
            Key.D1 or Key.NumPad1 => "1",
            Key.D2 or Key.NumPad2 => "2",
            Key.D3 or Key.NumPad3 => "3",
            Key.D4 or Key.NumPad4 => "4",
            Key.D5 or Key.NumPad5 => "5",
            Key.D6 or Key.NumPad6 => "6",
            Key.D7 or Key.NumPad7 => "7",
            Key.D8 or Key.NumPad8 => "8",
            Key.D9 or Key.NumPad9 => "9",

            Key.Decimal => ".",

            Key.Add => "+",
            Key.Subtract => "-",
            Key.Multiply => "*",
            Key.Divide => "/",

            Key.Enter => "Enter",

            Key.C => "Clear",
            Key.S => "Swap",

            _ => null
        };

        if (label == null) return;
        OnKeypadButtonClicked(label);
        e.Handled = true;
    }

    private void OnKeypadButtonClicked(string label)
    {

    }
}