using UnityEngine;
using UnityEngine.UI;

public class EnergyPuzzleButton : MonoBehaviour
{
    Button puzzleButton;
    RectTransform rectTransform;
    public Vector3 correctPos;
    int[] possibleRotations = new int[4] { 90, 180, -90, -180 };
    public bool isCorrect;
    [SerializeField]
    PuzzleBrain brain;
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
        //Debug.Log(rectTransform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RotateButton()
    {
        rectTransform.Rotate(new Vector3(0, 0, -90));
        //Debug.Log(rectTransform.eulerAngles.z);
        if (rectTransform.eulerAngles.z < correctPos.z + 0.1 && rectTransform.eulerAngles.z > correctPos.z - 0.1)
        {
            isCorrect = true;
            brain.CheckCompletion();
            Debug.Log("CORRETECUMME" + isCorrect);
        }else
        {
            isCorrect = false;
        }
    }
}
