using GameLogic.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace View
{   
    namespace UI
    {
        public class UnitMenuView : MonoBehaviour
        {
            public void UpdateUnitMenuView()
            {
                for (int i = 1; i <= transform.childCount; i++)
                {
                    GameObject.Find($"UnitBlank{i}").GetComponent<Image>().color = PrefabSettingsChanger.SetRandomColor();
                }
            }
        }
    } 
}