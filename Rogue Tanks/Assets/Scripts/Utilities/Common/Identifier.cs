using System;

public class Identifier
{
    private string tankId;
    private string groupId;

    private Identifier(string groupId)
    {
        tankId = System.Guid.NewGuid().ToString();
        this.groupId = groupId;
    }

    private bool TankIsNotHittingHimself(Identifier other) => tankId != other.tankId;
    private bool TankFromDifferentGroupIsHit(Identifier other) => groupId != other.groupId;

    public Identifier HitsAnotherTank(Identifier other, Action onOtherTankIsHit)
    {
        if (TankIsNotHittingHimself(other))
            onOtherTankIsHit();
        return this;
    }

    public Identifier HitsTankFromAnotherGroup(Identifier other, Action hitsTankOnAnotherGroup)
    {
        if (TankIsNotHittingHimself(other) && TankFromDifferentGroupIsHit(other))
            hitsTankOnAnotherGroup();
        return this;
    }

    public static Identifier Create(string groupId) => new Identifier(groupId);
}
