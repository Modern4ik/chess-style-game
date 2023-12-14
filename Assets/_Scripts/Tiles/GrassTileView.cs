using UnityEngine;

public class GrassTileView : TileView
{
    [SerializeField] private Color _baseColor, _offsetColor;

    public override void Init(GameObject tileInfo, GameObject tileUnitInfo, Tile tileToView)
    {
        //TODO: это должно происходить в базовом классе

        this.tileInfo = tileInfo;
        this.tileUnitInfo = tileUnitInfo;
        this.tileToView = tileToView;

        var isOffset = (tileToView.x + tileToView.y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
}
