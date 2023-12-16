using System.Threading.Tasks;

public class MouseInput : PlayerInput
{
    public async Task<InputData> SelectUnitToResp()
    {
        bool isTileSelected = false;

        while (!isTileSelected)
        {
            isTileSelected = TileInput.tileDroppedOn != null;
            await Task.Delay(25);
        }

        return new InputData { tileToSpawn = TileInput.tileDroppedOn, unitTag = TileInput.droppedUnitTag, unitColor = TileInput.droppedUnitColor };
    }
}