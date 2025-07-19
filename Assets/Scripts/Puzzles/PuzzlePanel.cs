using UnityEngine;

public class PuzzlePanel : MonoBehaviour
{
    [SerializeField]
    GameObject CanvasToOpen;
    [SerializeField]
    int respectiveLvlBeat;

    bool canBeInteracted = false;
    void Start()
    {
        LevelManager.OnLevelBeatChange += CanBeInteracted;
    }

    void CanBeInteracted(int currentBeat)
    {
        if (currentBeat == respectiveLvlBeat)
        {
            canBeInteracted = true;
        }
    }

    public void ActivatePuzzleCanvas()
    {
        if (canBeInteracted)
        {
            CanvasToOpen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            HudManager.Instance.canPause = false;           
        }
    }
}
