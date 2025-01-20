using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    /* Минимальные и максимальные значения, к сожалению работают не на всех разрешениях экрана
        Боюсь, что это решается настройкой для каждого разрешения отдельно
        Либо же, если делать сразу всю сцену и следственно игру в UI */
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    //Coff в данной ситуации скорость
    [SerializeField] private float coff;
    Vector2 startS;
    Vector2 startV;
    Vector3 startPos;
    float finalX;
    
    void Update()
    {
        //Берем стартовую позицию при нажатии   
        if(Input.GetMouseButtonDown(0)){
            startS = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            startPos = transform.position;
        } else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            startS = Input.GetTouch(0).position;
            startPos = transform.position;
        }
        startV =  Camera.main.ScreenToWorldPoint(startS);
        //Пока есть нажатие ЛКМ/прикосновение пальцем находим расстояние между стартовой позицией и нынешним положением курсора/пальца на экране
        if(Input.GetMouseButton(0)){
            Vector2 S = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 V =  Camera.main.ScreenToWorldPoint(S);
            float diff = (startV.x - V.x) * coff;
            
            //Если мы перетаскиваем объект(то есть нажали на него изначально), то перемещения не будет.
            if(FindObjectOfType<DragManager>().isDragging)diff = 0;

            float finalX = startPos.x + diff;

            //Ограничиваем значения x
            if(finalX > maxX)finalX = maxX;
            if(finalX < minX)finalX = minX;

            transform.position = new Vector3(finalX, startPos.y , startPos.z);
        } else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Vector2 S = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            Vector2 V =  Camera.main.ScreenToWorldPoint(S);
            float diff = (startV.x - V.x) * coff;
            if(FindObjectOfType<DragManager>().isDragging)diff = 0;

            float finalX = startPos.x + diff;
            if(finalX > maxX)finalX = maxX;
            if(finalX < minX)finalX = minX;

            transform.position = new Vector3(finalX, startPos.y , startPos.z);
        }
        //При отпусканим ЛКМ/поднятии пальца меняем стартовую позицию
        if(Input.GetMouseButtonUp(0)){startPos = new Vector3(finalX, startPos.y , startPos.z); Debug.Log(startPos);}
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended){
            startPos = new Vector3(finalX, startPos.y , startPos.z);
        }
    }
}
