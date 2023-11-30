using System.Threading.Tasks;

public class MouseInput : PlayerInput
{
    public async Task SelectUnitToResp()
    {
        bool isTileSelected = false;

        while (!isTileSelected)
        {
            isTileSelected = Tile.tileDroppedOn != null;
            await Task.Delay(25);
        }
    }
}