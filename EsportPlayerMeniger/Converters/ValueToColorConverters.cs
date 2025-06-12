using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace EsportPlayerMeniger.Converters;

public class FatigueToColorConverter : IValueConverter
{
    public static readonly FatigueToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int fatigueLevel)
        {
            return fatigueLevel switch
            {
                >= 80 => Colors.Red,
                >= 60 => Colors.Orange,
                >= 40 => Colors.Yellow,
                _ => Colors.LightGreen
            };
        }
        return Colors.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StressToColorConverter : IValueConverter
{
    public static readonly StressToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int stressLevel)
        {
            return stressLevel switch
            {
                >= 80 => Colors.DarkRed,
                >= 60 => Colors.Red,
                >= 40 => Colors.Orange,
                _ => Colors.LightBlue
            };
        }
        return Colors.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class FatigueToTextColorConverter : IValueConverter
{
    public static readonly FatigueToTextColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int fatigueLevel)
        {
            return fatigueLevel >= 60 ? Colors.White : Colors.Black;
        }
        return Colors.Black;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StressToTextColorConverter : IValueConverter
{
    public static readonly StressToTextColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int stressLevel)
        {
            return stressLevel >= 60 ? Colors.White : Colors.Black;
        }
        return Colors.Black;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}