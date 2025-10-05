namespace Incremental;

using BigNum;

public class Money
{
    public BigNum Amount { get; set; }
    public string StringAmount { get; set; }

    public Money(BigNum amount)
    {
        Amount = amount;
        StringAmount = amount.ToString();
    }

    public Money(string amount)
    {
        Amount = BigNum.ToBigNum(amount);
        StringAmount = amount;
    }

    public Money AddMoney(BigNum amount)
    {
        return new Money(Amount + amount);
    }
}