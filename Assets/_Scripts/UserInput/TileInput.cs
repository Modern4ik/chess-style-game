using GameLogic;
using GameSettings;
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

        private GameTile tileToView;

        public virtual void Init(GameTile tileToView)
        {
            this.tileToView = tileToView;
        }

        void OnMouseEnter()
        {
            if (!GameStatus.isGameActive) return;

            if (tileToView.occupiedUnit != null) tileToView.tileView.HighlightUnitActions(tileToView);

            tileToView.tileView._highlight.SetActive(true);
            tileToView.tileView.ShowTileInfo(tileToView.occupiedUnit);
        }

        void OnMouseExit()
        {
            if (!GameStatus.isGameActive) return;

            if (tileToView.tileView.unitOnTileMoves.Count > 0) tileToView.tileView.HighlightTilesToMoveOff();

            tileToView.tileView._highlight.SetActive(false);
            tileToView.tileView.HideTileInfo();
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
    }
}