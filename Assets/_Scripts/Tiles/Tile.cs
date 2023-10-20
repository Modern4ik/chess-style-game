using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: этот класс был абстрактным. Временно убрано из-за unit-тестов.
public class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    public BaseUnityUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    public int x;
    public int y;


    public virtual void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    void OnMouseDown() {
        if(GameManager.Instance.GameState != GameState.SpawnHeroes) return;

        if (OccupiedUnit == null)
        {
            //Generate and set unit
            //End turn
            UnitManager.Instance.SpawnHero(this);
        }

        //if (OccupiedUnit != null) {
        //    if(OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        //    else {
        //        if (UnitManager.Instance.SelectedHero != null) {
        //            var enemy = (BaseEnemy) OccupiedUnit;
        //            Destroy(enemy.gameObject);
        //            UnitManager.Instance.SetSelectedHero(null);
        //        }
        //    }
        //}
        //else {
        //    if (UnitManager.Instance.SelectedHero != null) {
        //        SetUnit(UnitManager.Instance.SelectedHero);
        //        UnitManager.Instance.SetSelectedHero(null);
        //    }
        //}

    }

    public void SetUnit(BaseUnityUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}