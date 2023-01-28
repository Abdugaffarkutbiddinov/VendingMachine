namespace vendingMachine;

public class VendingMachine
{
    private readonly uint _percent;
    private readonly uint _a;
    private readonly uint _b;
    private readonly uint _sum;

    public VendingMachine(uint sum, uint a, uint b, uint percent)
    {
        _sum = sum;
        _a = a;
        _b = b;
        _percent = percent;
    }

    public Tuple<uint, uint, uint>? Change()
    {
        uint delta = 5;
        var changeWithPercent = _percent == 0 ? null : ChangeWithPercent();
        var changeStandard = ChangeStandard();
        if (changeWithPercent != null)
        {
            if (Math.Abs(_percent - changeWithPercent.Item4) <= delta)
            {
                return new Tuple<uint, uint, uint>(changeWithPercent.Item1, changeWithPercent.Item2,
                    changeWithPercent.Item3);
            }
        }

        return changeStandard;
    }

    private Tuple<uint, uint, uint>? ChangeStandard()
    {
        var changePriorityA = FindChangeWithPriority(sum: _sum, xPriority: _a, x: _b);
        var changePriorityB = FindChangeWithPriority(sum: _sum, xPriority: _b, x: _a);

        return changePriorityA != null || changePriorityB != null
            ? changePriorityA!.Item3 < changePriorityB!.Item3
                ? new Tuple<uint, uint, uint>(changePriorityA.Item1, changePriorityA.Item2, changePriorityA.Item3)
                : new Tuple<uint, uint, uint>(changePriorityB.Item2, changePriorityB.Item1, changePriorityB.Item3)
            : null;
    }

    private Tuple<uint, uint, uint>? FindChangeWithPriority(uint sum, uint xPriority, uint x)
    {
        uint xPriorityCount;
        uint xCount;
        uint remainder = 0;

        xPriorityCount = sum / xPriority;

        if (xPriorityCount == 0)
        {
            xCount = sum / x;
            if (xCount == 0)
            {
                return null;
            }

            remainder = sum - x * xCount;
        }

        xCount = (sum - xPriority * xPriorityCount) / x;
        if (xCount == 0) remainder = sum - xPriority * xPriorityCount;
        return new Tuple<uint, uint, uint>(xPriorityCount, xCount, remainder);
    }

    private Tuple<uint, uint, uint, uint>? ChangeWithPercent()
    {
        uint remainder = 0;

        var aCount = _sum / (_a + (100 - _percent) * _b / _percent);
        var bCount = _sum / (_b + _a * _percent / (100 - _percent));

        if (aCount == 0 || bCount == 0)
        {
            return null;
        }

        bCount = (_sum - _a * aCount) / _b;
        if (bCount > 0) remainder = (_sum - _a * aCount) % _b;

        var approxPercent = aCount * 100 / (aCount + bCount);
        return new Tuple<uint, uint, uint, uint>(aCount, bCount, remainder, approxPercent);
    }
}