using View;
using GameLogic.Units;
using GameLogic.Fruits;

namespace GameLogic
{
    public class GameTile
    {
        public string TileName;
        public BaseUnit occupiedUnit;
        public BaseFruit fruitOnTile;

        public int x;
        public int y;
        public TileView tileView;

        public bool walkable => tileView._isWalkable && occupiedUnit == null;

        public GameTile(int x, int y, TileView tileView)
        {
            this.x = x;
            this.y = y;

            this.tileView = tileView;
        }

        public void SetUnit(BaseUnit unit)
        {
            if (unit.OccupiedTile != null) unit.OccupiedTile.occupiedUnit = null;
            unit.getUnitView().SetPosition(tileView.transform.position);

            occupiedUnit = unit;
            unit.OccupiedTile = this;
        }
    }
}