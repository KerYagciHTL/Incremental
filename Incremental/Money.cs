// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

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