using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleBrain_Energy : PuzzleBrain
{
    [SerializeField]
    ActivatableObject puzzleObjective;
    [SerializeField]
    List<EnergyPuzzleButton> energyButtons = new();
    [SerializeField]
    TextMeshProUGUI feedbackText;
    //int successScore = 0;
    public int currentScore = 0;
    void Start()
    {
        
    }

    public override void CheckCompletion()
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
            OnPuzzleCompleted();
        }
        //if (currentScore >= successScore)
        //{
        //    //Call PuzzleFinished
        //}
    }

    protected override void OnPuzzleCompleted()
    {
        puzzleObjective.OnActivation();
        feedbackText.text = "Energía restaurada";
        feedbackText.color = Color.green;
    }
}
