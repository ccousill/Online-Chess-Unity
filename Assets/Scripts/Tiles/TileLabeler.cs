
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TileLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates;
    void Awake(){
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying){
            DisplayCoordinates();
            UpdateName();
        }
    }

    void DisplayCoordinates(){
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.y);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateName(){
        transform.parent.name = coordinates.ToString();
    }
}
