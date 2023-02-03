using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPS_ScriptsNameSpace; // RPS 온라인 폴더에 있음
public class Visual_ImageChangeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite rock_Img;
    public Sprite paper_Img;
    public Sprite scissors_Img;
    public Sprite null_Img;

    private Image change_Img;
    void Start()
    {
        change_Img = this.GetComponent<Image>();
    }
    void Update()
    {
        //Debug.Log("imageRPS "+ RockPaperScissorsScripts.choiceRPS);
        if (RockPaperScissorsScripts.choiceRPS == 1)
            change_Img.sprite = rock_Img;
        else if (RockPaperScissorsScripts.choiceRPS == 2)
            change_Img.sprite = paper_Img;
        else if (RockPaperScissorsScripts.choiceRPS == 3)
            change_Img.sprite = scissors_Img;
        else
            change_Img.sprite = null_Img;  //0

    }
}
