using System.Collections.Generic;
using UnityEngine;

public class PuzzleBrain : MonoBehaviour
{
    [SerializeField]
    ActivatableObject puzzleObjective;
    [SerializeField]
    List<EnergyPuzzleButton> energyButtons = new List<EnergyPuzzleButton>();
    //int successScore = 0;
    public int currentScore = 0;
    void Start()
    {
        
    }

    public void CheckCompletion()
    {
        foreach (var obj in energyButtons)
        {
            if (!obj.isCorrect)
            {
                Debug.Log("FUCK");
                currentScore = 0;
                break;
            }
            else
            {
                currentScore++;
            }
        }
        if (currentScore == energyButtons.Count)
        {
            puzzleObjective.OnActivation();
        }
        //if (currentScore >= successScore)
        //{
        //    //Call PuzzleFinished
        //}
    }
}
