﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    
    public virtual void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseEnter()
    {;
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState != GameState.SpawnHeroes) return;

        if (OccupiedUnit == null && y == 0)
        {
            GameObject droppedUnit = eventData.pointerDrag;
            droppedUnitTag = droppedUnit.tag;

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
}