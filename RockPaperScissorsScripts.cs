using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPS_ScriptsNameSpace
{
    public class RockPaperScissorsScripts : MonoBehaviour
    {
        public static int choiceRPS;

        public void Rock()
        {
            choiceRPS = 1; //Rock
            Debug.Log(choiceRPS);
        }
        public void Paper()
        {
            choiceRPS = 2; //Paper
        }
        public void Scissors()
        {
            choiceRPS = 3; //Scissors
            Debug.Log(choiceRPS);
        }
        public void RandomRPS()
        {
            choiceRPS = Random.Range(1, 4); // 1,2,3
            Debug.Log(choiceRPS);
        }

    }
}