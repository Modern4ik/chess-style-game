public class Tile
{
    public string TileName;

    public BaseUnit occupiedUnit;
    public bool walkable;
    public int x;
    public int y;
    public TileView tileView;

    public Tile(int x, int y, TileView tileView)
    {
        this.x = x;
        this.y = y;

        this.tileView = tileView;
        this.walkable = tileView._isWalkable && occupiedUnit == null;
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.occupiedUnit = null;
        unit.getUnitView().SetPosition(tileView.transform.position);

        occupiedUnit = unit;
        unit.OccupiedTile = this;
        tileView.isOccupied = true;
    }
}