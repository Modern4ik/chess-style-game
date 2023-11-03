using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ƒанный класс отвечает за логику перетаскивани€ спрайта юнита из меню выбора юнитов, что позвол€ет
// реализовать "Drag and Drop" систему. Ћогика така€:

// ¬ OnBeginDrag(при начале перетаскивани€) мы мен€ем родител€ у спрайта юнита в UnitSelectionMenu,
// предварительно сохранив изначального родител€,и назначем нового родител€,а именно root(корневой объект, т.е. сам Canvas).
// —делано дл€ того, чтобы остальные элементы UI не перекрывали спрайт, который тащим(т.е. чтобы он был в самом низу Canvas по иерархии).
// “акже выставл€ем значение Raycast Target на false, чтобы сам спрайт не перекрывал собой дл€ курсора Tile, на который
// хотим дропнуть спрайт дл€ спавна фигурки, иначе Event OnDrop в Tile не сработает.

// ¬ OnEndDrag(как заканчиваем тащить) мы задаЄм родител€ до перетаскивани€ и вовзращаем значение Raycast Target на true.
public class SpawnUnitBlank : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Begin Drag {image.name}");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"Dragging {image.name}");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"EndDrag {image.name}");
        transform.SetParent(parentAfterDrag);

        image.raycastTarget = true;
    }
}
