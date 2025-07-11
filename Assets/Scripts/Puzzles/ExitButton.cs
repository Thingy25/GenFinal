using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField]
    GameObject canvasToClose;
    Button exitButton;
    void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(ClosePanel);
    }

    void ClosePanel()
    {
        canvasToClose.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HudManager.Instance.canPause = true;
    }
}
