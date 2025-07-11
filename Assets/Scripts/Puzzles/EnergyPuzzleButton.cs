using UnityEngine;
using UnityEngine.UI;

public class EnergyPuzzleButton : MonoBehaviour
{
    Button puzzleButton;
    RectTransform rectTransform;
    Vector3 correctPos;
    int[] possibleRotations = new int[4] {90, 180, -90, -180};
    private void Awake()
    {
        puzzleButton = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        puzzleButton.onClick.AddListener(RotateButton);
        correctPos = rectTransform.eulerAngles;
    }

    void OnEnable()
    {
        rectTransform.Rotate(new Vector3(0, 0, possibleRotations[Random.Range(0, possibleRotations.Length)]));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RotateButton()
    {
        rectTransform.Rotate(new Vector3(0, 0, -90));
        if (rectTransform.eulerAngles == correctPos)
        {
            Debug.Log("CORRETECUMME");
        }
    }
}
