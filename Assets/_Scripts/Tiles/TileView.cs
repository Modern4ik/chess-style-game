using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TileView : MonoBehaviour
{
    public string TileName;
    public GameObject tileInfo;
    public GameObject tileUnitInfo;
    private Color transparentWhite = new Color(1f, 1f, 1f, 0.35f);
    public List<List<UnitMove>> unitOnTileMoves = new List<List<UnitMove>>();

    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] public GameObject _highlight;
    [SerializeField] public bool _isWalkable;

    public virtual void Init(GameObject tileInfo, GameObject tileUnitInfo)
    {
        this.tileInfo = tileInfo;
        this.tileUnitInfo = tileUnitInfo;
    }

    private void GetUnitOnTileMoves(GameTile tileToView)
    {
        if (unitOnTileMoves.Count > 0) unitOnTileMoves.Clear();

        foreach (List<Coordinate> sequence in tileToView.occupiedUnit.getMoveSequences())
        {
            unitOnTileMoves.Add(SequenceValidator.GetValidUnitMoves(sequence, tileToView, tileToView.occupiedUnit.getFaction()));
        }
    }

    public void HighlightUnitActions(GameTile tileToView)
    {
        GetUnitOnTileMoves(tileToView);

        foreach (List<UnitMove> unitMoves in unitOnTileMoves)
        {
            foreach (UnitMove move in unitMoves)
            {
                ActivateHighlight(move);
            }
        }
    }

    private void ActivateHighlight(UnitMove unitMove)
    {
        switch (unitMove)
        {
            case MoveTo:
                HighlightTileToMoveOn((MoveTo)unitMove);
                break;
            case AttackUnit:
                HighlightTileToFightOn((AttackUnit)unitMove);
                break;
            case AttackMain:
                HighlightMainAttackMarker((AttackMain)unitMove);
                break;
        }
    }

    private void DeactivateHighlight(UnitMove unitMove)
    {
        switch (unitMove)
        {
            case MoveTo:
            case AttackUnit:
                unitMove.validTileToMove.tileView._highlight.GetComponent<SpriteRenderer>().color = transparentWhite;
                unitMove.validTileToMove.tileView._highlight.SetActive(false);

                break;
            case AttackMain:
                AttackMain attackMain = (AttackMain)unitMove;
                attackMain.mainHeroToAttack.SetUnderAttackMark(false);

                break;
        }
    }

    private void HighlightTileToMoveOn(MoveTo unitAction)
    {
        unitAction.validTileToMove.tileView._highlight.GetComponent<SpriteRenderer>().color = Color.green;

        unitAction.validTileToMove.tileView._highlight.SetActive(true);
    }

    private void HighlightTileToFightOn(AttackUnit unitAction)
    {
        unitAction.validTileToMove.tileView._highlight.GetComponent<SpriteRenderer>().color = Color.red;

        unitAction.validTileToMove.tileView._highlight.SetActive(true);
    }

    public void HighlightTilesToMoveOff()
    {
        foreach (List<UnitMove> unitMoves in unitOnTileMoves)
        {
            foreach (UnitMove move in unitMoves)
            {
                DeactivateHighlight(move);
            }
        }
    }

    private void HighlightMainAttackMarker(AttackMain unitAction) => unitAction.mainHeroToAttack.SetUnderAttackMark(true);

    public void ShowTileInfo(BaseUnit unitOnTile)
    {
        tileInfo.GetComponentInChildren<Text>().text = this.TileName;
        tileInfo.SetActive(true);

        if (unitOnTile != null)
        {
            tileUnitInfo.GetComponentInChildren<Text>().text = unitOnTile.getName();
            tileUnitInfo.SetActive(true);
        }
    }

    public void HideTileInfo()
    {
        tileInfo.SetActive(false);
        tileUnitInfo.SetActive(false);
    }
}