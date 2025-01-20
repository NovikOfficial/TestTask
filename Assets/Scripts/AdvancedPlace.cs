using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlace : MonoBehaviour
{
    /* Скрипт называется Advanced, ведь я хотел сделать более интересную механику, чем просто притягивание к одной точке, 
        но в игре-референсе было сделано так. */
    private void OnTriggerStay2D(Collider2D other){
        if(other.GetComponent<Item>() != null){
            if(!other.GetComponent<Item>().isDragged && !other.GetComponent<Item>().isPlaced){
            other.GetComponent<Item>().isPlaced = true;
            other.GetComponent<Item>().targetPos = transform.GetChild(0).position;
            }
        }
    }
/*     private void OnTriggerExit2D(Collider2D other){
    } */
}
