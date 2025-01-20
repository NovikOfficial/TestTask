using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    //Как и все скрипты, кроме DragManager написан уже полностью мной. 
    [HideInInspector] public Vector3 lastPose;
    public bool isDragged = false;
    public bool isPlaced = false;
    [SerializeField]private float floorMinHeight = -1.5f;
    public Vector3 targetPos;

    /* Здесь были попытки создать более интересную механику гравитации, но:
        а)В оригинальном проекте-референсе гравитация была реализована по другому
        б)Такая гравитация была бы менее понятной для игрока 
        
        Можете прочитать код и в теории понять, как бы она работала или просто вернуть ее в код.
        Но настройки слетели, ведь я их настраивал непосредственно в самом Unity.*/

    /* public GameObject shade;
    public GameObject trigger;
    [SerializeField] float diagonalCof = 0.2f;
    [SerializeField] float minValue = -1.9f;
    [SerializeField] float maxHValue = 1.1f;
    [SerializeField] float minHValue = 0f;
    [SerializeField] float dopValue = 2f;
    public float PlaceHeight = 0f; */
    
    void Update(){
        if(isDragged) lastPose = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        /* float heightDop = (dopValue + lastPose.y) * diagonalCof;
        if(heightDop > maxHValue)heightDop = maxHValue;
        if(heightDop < minHValue)heightDop = minHValue;
        floorMinHeight = minValue + heightDop;
        if(PlaceHeight != 0)floorMinHeight = PlaceHeight; */

        //Вот новая гравитация, прямо как в проекте. Крайне незамысловатая.
        if(transform.position.y > floorMinHeight && !isDragged && !isPlaced){
            transform.position += new Vector3(0f, -Time.deltaTime * 10f, 0f);
        }
        if(isPlaced)transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 20f);

        //В старой системе гравитации была тень и специальный триггер.

        /* if(isDragged)trigger.transform.position = new Vector3(transform.position.x, minValue + heightDop, 0f);
        shade.transform.position = new Vector3(transform.position.x, floorMinHeight - (shade.transform.localScale.y), floorMinHeight + 0.1f); */
        
        //Когда перемещаешь предмет, он всегда должен быть виден.
        if(isDragged){
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        } else GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
