using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScalePanel : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Screen width : " + Screen.width);
        Debug.Log("Screen height : " + Screen.height);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
