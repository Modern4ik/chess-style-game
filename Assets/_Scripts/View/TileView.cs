using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public abstract class TileView : MonoBehaviour
    {
        public string TileName;
        public GameObject tileInfo;
        public GameObject tileUnitInfo;
        private Color transparentWhite = new Color(1f, 1f, 1f, 0.35f);
        
        [SerializeField] protected SpriteRenderer _renderer;
        [SerializeField] public GameObject _highlight;
        [SerializeField] public bool _isWalkable;

        public virtual void Init(GameObject tileInfo, GameObject tileUnitInfo)
        {
            this.tileInfo = tileInfo;
            this.tileUnitInfo = tileUnitInfo;
        }

        public void HighlightToMoveOn()
        {
            _highlight.GetComponent<SpriteRenderer>().color = Color.green;
            _highlight.SetActive(true);
        }

        public void HighlightToFightOn()
        {
            _highlight.GetComponent<SpriteRenderer>().color = Color.red;
            _highlight.SetActive(true);
        }

        public void HighlightEmptyTile() => _highlight.SetActive(true);

        public void DeactivateHighlight()
        {
            _highlight.GetComponent<SpriteRenderer>().color = transparentWhite;
            _highlight.SetActive(false);
        }

        public void ShowTileInfo()
        {
            tileInfo.GetComponentInChildren<Text>().text = this.TileName;
            tileInfo.SetActive(true);
        }

        public void ShowUnitInfo(string unitName)
        {
            tileUnitInfo.GetComponentInChildren<Text>().text = unitName;
            tileUnitInfo.SetActive(true);
        }

        public void HideAllInfo()
        {
            tileInfo.SetActive(false);
            tileUnitInfo.SetActive(false);
        }
    }
}