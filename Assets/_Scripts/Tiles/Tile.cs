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
    private List<Tile> validTiles = new List<Tile>();
    
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

        MenuManager.Instance.EnableHeroAttackMark(SequenceValidator.isHeroAttackMarkActive);
        MenuManager.Instance.EnableEnemyAttackMark(SequenceValidator.isEnemyAttackMarkActive);
        MenuManager.Instance.ShowTileInfo(this); 
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.IsGameEnded()) return;

        if (validTiles.Count > 0) HighlightTilesToMoveOff();
        
         _highlight.SetActive(false);

        MenuManager.Instance.EnableHeroAttackMark(SequenceValidator.isHeroAttackMarkActive = false);
        MenuManager.Instance.EnableEnemyAttackMark(SequenceValidator.isEnemyAttackMarkActive = false);
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

    private void HighlightTilesToMoveOn()
    {
        validTiles = SequenceValidator.GetValidTiles(this.OccupiedUnit);

        foreach (Tile validTile in validTiles)
        {
            if (validTile.OccupiedUnit != null) validTile._highlight.GetComponent<SpriteRenderer>().color = Color.red;
            else validTile._highlight.GetComponent<SpriteRenderer>().color = Color.green;

            validTile._highlight.SetActive(true);
        }
    }

    private void HighlightTilesToMoveOff()
    {
        foreach (Tile tile in validTiles)
        {
            tile._highlight.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.35f);
            tile._highlight.SetActive(false);
        }
    }
}