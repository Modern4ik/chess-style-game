public class UnitFactoryStub : IUnitFactory
{
    public BaseUnit createUnit(Faction faction)
    {
        var unitSettings = new UnitSettings(new UnityObjectStub(), new HealthViewStub(), new Element());
        return new Pawn("new_pawn", faction, unitSettings);
    }
}
