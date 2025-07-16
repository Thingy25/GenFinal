using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleBrain : MonoBehaviour
{
    void Start()
    {

    }

    public abstract void CheckCompletion();

    protected abstract void OnPuzzleCompleted();

}
