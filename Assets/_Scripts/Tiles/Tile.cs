using System.Collections;
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
    public static string droppedUnitTag;
    public static Color droppedUnitColor;
    private List<List<UnitMove>> unitOnTileMoves = new List<List<UnitMove>>();
    
    public virtual void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseEnter()
    {
        if (GameManager.Instance.IsGameEnded()) return;

        if (this.OccupiedUnit != null) HighlightUnitActions();
            
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this); 
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.IsGameEnded()) return;

        if (unitOnTileMoves.Count > 0) HighlightTilesToMoveOff();
        
         _highlight.SetActive(false);

        MenuManager.Instance.EnableHeroAttackMark(false);
        MenuManager.Instance.EnableEnemyAttackMark(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState != GameState.SpawnHeroes) return;

        if (OccupiedUnit == null && y == 0)
        {
            GameObject droppedUnit = eventData.pointerDrag;
            droppedUnitTag = droppedUnit.tag;
            droppedUnitColor = droppedUnit.GetComponent<Image>().color;
            //Generate and set unit
            //End turn
            UnitManager.Instance.SpawnHero(this);
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.getUnityObject().SetPosition(transform.position);
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
            case AttackMain:
                HighlightMainAttackMarkers((AttackMain) unitMove);
                break;
        }
    }

    private void HighlightTileToMoveOn(MoveTo unitAction)
    {
        if (unitAction.validTileToMove.OccupiedUnit != null) unitAction.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = Color.red;
        else unitAction.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = Color.green;

        unitAction.validTileToMove._highlight.SetActive(true);
    }

    private void HighlightTilesToMoveOff()
    {
        foreach (List<UnitMove> unitMoves in unitOnTileMoves)
        {   
            foreach (UnitMove move in unitMoves)
            {   
                if (move.GetType() == typeof(MoveTo))
                {   
                    MoveTo moveTo = (MoveTo) move;
                    moveTo.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.35f);
                    moveTo.validTileToMove._highlight.SetActive(false);
                }  
            }
        }
    }

    private void HighlightMainAttackMarkers(AttackMain unitAction)
    {
        MenuManager.Instance.EnableHeroAttackMark(unitAction.isAttackHeroMainHealth);
        MenuManager.Instance.EnableEnemyAttackMark(unitAction.isAttackOpponentMainHealth);
    }
}