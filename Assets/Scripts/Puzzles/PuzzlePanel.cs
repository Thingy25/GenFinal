using UnityEngine;

public class PuzzlePanel : MonoBehaviour
{
    [SerializeField]
    GameObject CanvasToOpen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePuzzleCanvas()
    {
        CanvasToOpen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        HudManager.Instance.canPause = false;
    }
}
