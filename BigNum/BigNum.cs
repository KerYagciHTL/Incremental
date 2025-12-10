// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System.Globalization;

namespace BigNum;

public class BigNum : IEquatable<BigNum>
{
    public double Number { get; set; }
    public Suffix Suffix { get; set; }

    public BigNum(double number, Suffix suffix)
    {
        Number = number;
        Suffix = suffix;
    }

    public BigNum(string number)
    {
        var bigNum = ToBigNum(number);
        Number = bigNum.Number;
        Suffix = bigNum.Suffix;
    }

    public static BigNum ToBigNum(string number)
    {
        var parts = number.Split(' ');
        var num = double.Parse(parts[0], CultureInfo.InvariantCulture);
        var suffix = parts.Length == 2 ? Enum.Parse<Suffix>(parts[1]) : Suffix.None;
        return new BigNum(num, suffix);
    }

    public override string ToString()
    {
        var result = Number.ToString(CultureInfo.InvariantCulture);
        if (Suffix != Suffix.None) result += " " + Suffix;
        return result;
    }

    public static BigNum operator +(BigNum a, BigNum b)
    {
        // Normalize to the higher suffix
        var resultSuffix = a.Suffix > b.Suffix ? a.Suffix : b.Suffix;
        var aValue = a.Number;
        var bValue = b.Number;
        
        // Convert a to result suffix
        var aSuffixCopy = a.Suffix;
        while (aSuffixCopy < resultSuffix)
        {
            aValue /= 1000;
            aSuffixCopy++;
        }
        
        // Convert b to result suffix
        var bSuffixCopy = b.Suffix;
        while (bSuffixCopy < resultSuffix)
        {
            bValue /= 1000;
            bSuffixCopy++;
        }
        
        var amount = aValue + bValue;
        
        // Handle overflow to next suffix
        while (amount >= 1000 && resultSuffix < Suffix.Infinite)
        {
            amount /= 1000;
            resultSuffix++;
        }

        return new BigNum(amount, resultSuffix);
    }

    public static (BigNum, bool) operator -(BigNum a, BigNum b)
    {
        // Check if a < b (can't subtract)
        if (a.Suffix < b.Suffix || (a.Suffix == b.Suffix && a.Number < b.Number))
        {
            return (new BigNum(0, Suffix.None), false);
        }
        
        // Normalize to the higher suffix
        var resultSuffix = a.Suffix > b.Suffix ? a.Suffix : b.Suffix;
        var aValue = a.Number;
        var bValue = b.Number;
        
        // Convert a to result suffix
        var aSuffixCopy = a.Suffix;
        while (aSuffixCopy < resultSuffix)
        {
            aValue /= 1000;
            aSuffixCopy++;
        }
        
        // Convert b to result suffix
        var bSuffixCopy = b.Suffix;
        while (bSuffixCopy < resultSuffix)
        {
            bValue /= 1000;
            bSuffixCopy++;
        }
        
        var amount = aValue - bValue;
        
        // If result is exactly zero, keep the suffix
        if (Math.Abs(amount) < 0.0000001)
        {
            return (new BigNum(0, resultSuffix), true);
        }
        
        // Normalize result - reduce suffix if number is too small
        while (amount < 1 && amount > 0 && resultSuffix > Suffix.None)
        {
            amount *= 1000;
            resultSuffix--;
        }

        return (new BigNum(amount, resultSuffix), true);
    }

    #region Equals

    public override bool Equals(object? obj) =>
        Equals(obj as BigNum);

    public bool Equals(BigNum? other) =>
        other is not null
        && Number.Equals(other.Number)
        && Suffix == other.Suffix;

    public override int GetHashCode() =>
        HashCode.Combine(Number, Suffix);

    public static bool operator ==(BigNum? a, BigNum? b) =>
        a?.Equals(b) ?? b is null;

    public static bool operator !=(BigNum? a, BigNum? b) =>
        !(a == b);

    #endregion
}