using System;

public class AliveUnitWrapper
{
    private bool _isAlive = true;
    private BaseUnityUnit baseUnityUnit;
    public AliveUnitWrapper(BaseUnityUnit baseUnityUnit)
    {
        this.baseUnityUnit = baseUnityUnit;
        _isAlive = true;
    }

    public bool isAlive()
    {
        return _isAlive;
    }

    public void MarkForDeletion()
    {
        _isAlive = false;
    }

    public BaseUnityUnit getUnit()
    {
        return baseUnityUnit;
    }
}

