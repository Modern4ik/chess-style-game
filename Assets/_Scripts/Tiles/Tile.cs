using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//TODO: этот класс был абстрактным. Временно убрано из-за unit-тестов.
public class Tile : MonoBehaviour, IDropHandler {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    
    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    public int x;
    public int y;
    public static Tile tileDroppedOn;
    public static string droppedUnitTag;

    public GameObject tileInfo;
    public GameObject tileUnitInfo;
    public static Color droppedUnitColor;

    private Color transparentWhite = new Color(1f, 1f, 1f, 0.35f);
    private List<List<UnitMove>> unitOnTileMoves = new List<List<UnitMove>>();
    
    public virtual void Init(int x, int y, GameObject tileInfo, GameObject tileUnitInfo)
    {
        this.x = x;
        this.y = y;

        this.tileInfo = tileInfo;
        this.tileUnitInfo = tileUnitInfo;
    }

    void OnMouseEnter()
    {
        if (!GameStatus.isGameActive) return;

        if (this.OccupiedUnit != null) HighlightUnitActions();
            
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

        if (OccupiedUnit == null && y == 0)
        {
            GameStatus.isAwaitPlayerInput = false;

            GameObject droppedUnit = eventData.pointerDrag;
            droppedUnitTag = droppedUnit.tag;
            droppedUnitColor = droppedUnit.GetComponent<Image>().color;
            //Generate and set unit
            //End turn
            tileDroppedOn = this;
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.getUnitView().SetPosition(transform.position);
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    private void GetUnitOnTileMoves(BaseUnit unitOnTile)
    {
        if (unitOnTileMoves.Count > 0) unitOnTileMoves.Clear();

        foreach (List<Coordinate> sequence in unitOnTile.getMoveSequences())
        {
            unitOnTileMoves.Add(SequenceValidator.GetValidUnitMoves(sequence, this, unitOnTile.getFaction()));
        }
    }

    private void HighlightUnitActions()
    {
        GetUnitOnTileMoves(this.OccupiedUnit);

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
                HighlightTileToMoveOn((MoveTo) unitMove);
                break;
            case AttackUnit:
                HighlightTileToFightOn((AttackUnit) unitMove);
                break;
            case AttackMain:
                HighlightMainAttackMarker((AttackMain) unitMove);
                break;
        }
    }

    private void DeactivateHighlight(UnitMove unitMove)
    {
        switch (unitMove)
        {
            case MoveTo:
            case AttackUnit:
                unitMove.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = transparentWhite;
                unitMove.validTileToMove._highlight.SetActive(false);

                break;
            case AttackMain:
                AttackMain attackMain = (AttackMain)unitMove;
                attackMain.mainHeroToAttack.SetUnderAttackMark(false);

                break;
        }
    }

    private void HighlightTileToMoveOn(MoveTo unitAction)
    {
        unitAction.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = Color.green;

        unitAction.validTileToMove._highlight.SetActive(true);
    }

    private void HighlightTileToFightOn(AttackUnit unitAction)
    {
        unitAction.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = Color.red;

        unitAction.validTileToMove._highlight.SetActive(true);
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

        if (this.OccupiedUnit != null)
        {
            tileUnitInfo.GetComponentInChildren<Text>().text = this.OccupiedUnit.getName();
            tileUnitInfo.SetActive(true);
        }
    }

    private void HideTileInfo()
    {
        tileInfo.SetActive(false);
        tileUnitInfo.SetActive(false);
    }
}