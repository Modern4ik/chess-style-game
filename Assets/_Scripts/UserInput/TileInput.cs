using GameLogic;
using GameLogic.UnitMoves;
using GameSettings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInput
{
    public class TileInput : MonoBehaviour, IDropHandler
    {
        public static GameTile tileDroppedOn;
        public static string droppedUnitTag;
        public static Color droppedUnitColor;
        public List<List<UnitMove>> unitOnTileMoves = new List<List<UnitMove>>();

        private GameTile tileToView;

        void OnMouseEnter()
        {
            tileToView = GridManager.Instance.GetTileAtPosition(new Vector2(transform.position.x, transform.position.y));

            if (!GameStatus.isGameActive) return;

            if (tileToView.occupiedUnit != null)
            {
                HighlightUnitActions(tileToView);
                tileToView.tileView.ShowUnitInfo(tileToView.occupiedUnit.getName());
            }

            tileToView.tileView.HighlightEmptyTile();
            tileToView.tileView.ShowTileInfo();
        }

        void OnMouseExit()
        {
            if (!GameStatus.isGameActive) return;

            if (unitOnTileMoves.Count > 0) HighlightTilesToMoveOff();

            tileToView.tileView.DeactivateHighlight();
            tileToView.tileView.HideAllInfo();
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

        private void GetUnitOnTileMoves(GameTile tileToView)
        {
            if (unitOnTileMoves.Count > 0) unitOnTileMoves.Clear();

            foreach (List<Coordinate> sequence in tileToView.occupiedUnit.getMoveSequences())
            {
                unitOnTileMoves.Add(SequenceValidator.GetValidUnitMoves(sequence, tileToView, tileToView.occupiedUnit.getFaction()));
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

        private void HighlightTileToMoveOn(MoveTo unitAction) => unitAction.validTileToMove.tileView.HighlightToMoveOn();

        private void HighlightTileToFightOn(AttackUnit unitAction) => unitAction.validTileToMove.tileView.HighlightToFightOn();
        
        private void HighlightMainAttackMarker(AttackMain unitAction) => unitAction.mainHeroToAttack.heroView.SetUnderAttackMark(true);

        public void HighlightTilesToMoveOff()
        {
            foreach (List<UnitMove> unitMoves in unitOnTileMoves)
            {
                foreach (UnitMove move in unitMoves)
                {
                    DeactivateTileHighlight(move);
                }
            }
        }

        private void DeactivateTileHighlight(UnitMove unitMove)
        {
            switch (unitMove)
            {
                case MoveTo:
                case AttackUnit:
                    unitMove.validTileToMove.tileView.DeactivateHighlight();
                    
                    break;
                case AttackMain:
                    AttackMain attackMain = (AttackMain)unitMove;
                    attackMain.mainHeroToAttack.heroView.SetUnderAttackMark(false);

                    break;
            }
        }
    }
}