using System.Threading.Tasks;

public class MouseInput : PlayerInput
{
    public async Task<InputData> SelectUnitToResp()
    {
        bool isTileSelected = false;

        while (!isTileSelected)
        {
            isTileSelected = TileView.tileDroppedOn != null;
            await Task.Delay(25);
        }

        return new InputData { tileToSpawn = TileView.tileDroppedOn, unitTag = TileView.droppedUnitTag, unitColor = TileView.droppedUnitColor };
    }
}