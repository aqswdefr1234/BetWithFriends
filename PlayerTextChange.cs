using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTextChange : MonoBehaviour
{
    public GameObject player;
    public TMP_Text thisText;
    void Start()
    {
        thisText.text = player.name;
    }
}
