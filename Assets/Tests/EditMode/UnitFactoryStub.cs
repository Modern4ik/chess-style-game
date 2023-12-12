public class UnitFactoryStub : IUnitFactory
{
    public BaseUnit createUnit(Faction faction, InputData inputData)
    {
        var unitSettings = new UnitSettings(new UnitViewStub(), new HealthViewStub(), new ElementalType());
        return new Pawn("new_pawn", faction, unitSettings);
    }
}
