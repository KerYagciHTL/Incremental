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
        //TODO check increment again bc what if suffix of b is way higher (maybe increment suffix of a until b suffix is none?)
        var amount = a.Number + b.Number;
        while (amount >= 1000)
        {
            amount /= 1000;
            a.Suffix++;
        }

        return new BigNum(amount, a.Suffix);
    }

    public static (BigNum, bool) operator -(BigNum a, BigNum b)
    {
        //TODO fix decrement between suffix
        var worked = true;
        double amount = 0;
        if (a.Suffix < b.Suffix || a.Suffix == b.Suffix && a.Number < b.Number)
        {
            worked = false;
        }
        else
        {
            amount = a.Number - b.Number;
            if (amount < 0) amount = 1000 - amount;
            if (amount == 0)
            {
                amount = 0.001;
            }

            while (amount < 1 && a.Suffix != Suffix.None)
            {
                amount *= 1000;
                a.Suffix--;
            }
        }

        return (new BigNum(amount, a.Suffix), worked);
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