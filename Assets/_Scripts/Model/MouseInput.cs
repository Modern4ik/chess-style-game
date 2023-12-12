using System.Threading.Tasks;

public class MouseInput : PlayerInput
{
    public async Task<InputData> SelectUnitToResp()
    {
        bool isTileSelected = false;

        while (!isTileSelected)
        {
            isTileSelected = Tile.tileDroppedOn != null;
            await Task.Delay(25);
        }

        return new InputData { tileToSpawn = Tile.tileDroppedOn, unitTag = Tile.droppedUnitTag, unitColor = Tile.droppedUnitColor };
    }
}