using UnityEngine;
using UnityEngine.UI;

public class EnergyPuzzleButton : MonoBehaviour
{
    Button puzzleButton;
    RectTransform rectTransform;
    void Start()
    {
        puzzleButton = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        puzzleButton.onClick.AddListener(RotateButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RotateButton()
    {
        rectTransform.Rotate(new Vector3(0, 0, -90));
    }
}
