using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;

    public override void Init(int x, int y, GameObject tileInfo, GameObject tileUnitInfo) {
        var isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        //TODO: это должно происходить в базовом классе
        this.x = x;
        this.y = y;

        this.tileInfo = tileInfo;
        this.tileUnitInfo = tileUnitInfo;
    }
}
