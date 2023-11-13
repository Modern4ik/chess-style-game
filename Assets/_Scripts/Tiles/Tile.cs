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

        if (this.OccupiedUnit != null) HighlightTilesToMoveOn();
            
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
            unitOnTileMoves.Add(SequenceValidator.GetValidUnitMove(sequence, this, unitOnTile.getFaction()));
        }
    }

    private void HighlightTilesToMoveOn()
    {
        GetUnitOnTileMoves(this.OccupiedUnit);

        foreach (List<UnitMove> unitMoves in unitOnTileMoves)
        {   
            foreach (UnitMove move in unitMoves)
            {
                if (move.validTileToMove != null)
                {
                    if (move.validTileToMove.OccupiedUnit != null) move.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = Color.red;
                    else move.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = Color.green;

                    move.validTileToMove._highlight.SetActive(true);
                }

                if (move.isAttackHeroMainHealth) MenuManager.Instance.EnableHeroAttackMark(move.isAttackHeroMainHealth);
                if (move.isAttackOpponentMainHealth) MenuManager.Instance.EnableEnemyAttackMark(move.isAttackOpponentMainHealth);
            }
        }
    }

    private void HighlightTilesToMoveOff()
    {
        foreach (List<UnitMove> unitMoves in unitOnTileMoves)
        {   
            foreach (UnitMove move in unitMoves)
            {   
                if (move.validTileToMove != null)
                {
                    move.validTileToMove._highlight.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.35f);
                    move.validTileToMove._highlight.SetActive(false);
                }  
            }
        }
    }
}