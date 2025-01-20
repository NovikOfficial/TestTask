using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{

    /* БАЗУ СКРИПТА Я ВЗЯЛ ИЗ ИНТЕРНЕТА, ИБО РЕДКО ВЗАИМОДЕЙСТВУЮ С ANDROID. 
        Дальше я поднастроил его под свои нужды. 
        Весь код в скрипте я понимаю и мне не составит труда в следующий раз написать уже полностью свой код */
    [HideInInspector]public bool isDragging = true;
    private Vector2 screenPos;
    private Vector3 worldPos;
    private Item lastItem;
    void Awake()
    {
        screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Drop();
    }
    void Update()
    {
        //При отпускании ЛКМ/поднятии пальца предмет бросается
        if(isDragging && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))){
            Drop();
            return;
        }
        //Управление и для мыши(для теста) и для нажатий
        if(Input.GetMouseButton(0)){
            screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        } else if(Input.touchCount > 0){
            screenPos = Input.GetTouch(0).position;
        } else return;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        if(isDragging){
            
            if(lastItem != null)lastItem.transform.position = new Vector3(worldPos.x, worldPos.y, -2f);
        } else {
            //Если предмета "в руке" нет, то мы проверяем, коснулся ли своим нажатием игрок предмета
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if(hit.collider != null){
                Item item = hit.transform.gameObject.GetComponent<Item>();
                if(item != null){
                    lastItem = item;
                    item.isDragged = true;
                    StartDragging();
                }
            }
        }
    }
    // Подобное разделение на отдельные "воиды"(методы) полезно для понимания, но если бы я писал полностю сам, то закинул бы всё в Update
    void StartDragging(){
        isDragging = true;
        lastItem.isPlaced = false;
    }
    void Drop(){
        if(lastItem != null)lastItem.isDragged = false;
        isDragging = false;

    }
}
