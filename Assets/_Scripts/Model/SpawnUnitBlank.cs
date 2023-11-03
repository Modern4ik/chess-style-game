using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Данный класс отвечает за логику перетаскивания спрайта юнита из меню выбора юнитов, что позволяет
// реализовать "Drag and Drop" систему. Логика такая:

// В OnBeginDrag(при начале перетаскивания) мы меняем родителя у спрайта юнита в UnitSelectionMenu,
// предварительно сохранив изначального родителя,и назначем нового родителя,а именно root(корневой объект, т.е. сам Canvas).
// Сделано для того, чтобы остальные элементы UI не перекрывали спрайт, который тащим(т.е. чтобы он был в самом низу Canvas по иерархии).
// Также выставляем значение Raycast Target на false, чтобы сам спрайт не перекрывал собой для курсора Tile, на который
// хотим дропнуть спрайт для спавна фигурки, иначе Event OnDrop в Tile не сработает.

// В OnEndDrag(как заканчиваем тащить) мы задаём родителя до перетаскивания и вовзращаем значение Raycast Target на true.
// 
// Ссылка на видео с примером реализации такой логики: https://www.youtube.com/watch?v=kWRyZ3hb1Vc
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
