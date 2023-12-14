using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class TileView : MonoBehaviour, IDropHandler
{
    public Tile tileToView;

    public string TileName;
    public static Tile tileDroppedOn;
    public static string droppedUnitTag;
    public bool isOccupied;
    public GameObject tileInfo;
    public GameObject tileUnitInfo;
    public static Color droppedUnitColor;
    private Color transparentWhite = new Color(1f, 1f, 1f, 0.35f);
    private List<List<UnitMove>> unitOnTileMoves = new List<List<UnitMove>>();

    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] public bool _isWalkable;

    public virtual void Init(GameObject tileInfo, GameObject tileUnitInfo, Tile tileToView)
    {
        this.tileInfo = tileInfo;
        this.tileUnitInfo = tileUnitInfo;

        this.tileToView = tileToView; 
    }

    void OnMouseEnter()
    {
        if (!GameStatus.isGameActive) return;

        if (tileToView.occupiedUnit != null) HighlightUnitActions();

        _highlight.SetActive(true);
        ShowTileInfo();
    }

    void OnMouseExit()
    {
        if (!GameStatus.isGameActive) return;

        if (unitOnTileMoves.Count > 0) HighlightTilesToMoveOff();

        _highlight.SetActive(false);
        HideTileInfo();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameStatus.isAwaitPlayerInput || !GameStatus.isGameActive) return;

        if (tileToView.occupiedUnit == null && tileToView.y == 0)
        {
            GameStatus.isAwaitPlayerInput = false;

            GameObject droppedUnit = eventData.pointerDrag;
            droppedUnitTag = droppedUnit.tag;
            droppedUnitColor = droppedUnit.GetComponent<Image>().color;
            //Generate and set unit
            //End turn
            tileDroppedOn = tileToView;
        }
    }

    private void GetUnitOnTileMoves(BaseUnit unitOnTile)
    {
        if (unitOnTileMoves.Count > 0) unitOnTileMoves.Clear();

        foreach (List<Coordinate> sequence in unitOnTile.getMoveSequences())
        {
            unitOnTileMoves.Add(SequenceValidator.GetValidUnitMoves(sequence, tileToView, unitOnTile.getFaction()));
        }
    }

    private void HighlightUnitActions()
    {
        GetUnitOnTileMoves(tileToView.occupiedUnit);

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

    private void HighlightTilesToMoveOff()
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

    private void ShowTileInfo()
    {
        tileInfo.GetComponentInChildren<Text>().text = this.TileName;
        tileInfo.SetActive(true);

        if (tileToView.occupiedUnit != null)
        {
            tileUnitInfo.GetComponentInChildren<Text>().text = tileToView.occupiedUnit.getName();
            tileUnitInfo.SetActive(true);
        }
    }

    private void HideTileInfo()
    {
        tileInfo.SetActive(false);
        tileUnitInfo.SetActive(false);
    }
}