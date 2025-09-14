using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayButtonBehaviour : MonoBehaviour 
{
    [SerializeField] private TMP_InputField mapHeightSizeInputField;
    [SerializeField] private TMP_InputField mapWidthSizeInputField;
    [SerializeField] private TMP_InputField goldInputField;
    [SerializeField] private TMP_InputField potionsInputField;
    [SerializeField] private TMP_Text errorsMessegesText;
    [SerializeField] private GameObject canvas;

    public static int mapHeightSize;
    public static int mapWidthSize;
    public static int goldSize;
    public static int potionsSize;

    void Awake()
    {
        //DontDestroyOnLoad(canvas); // keeps canvas and its containings when loading a new scene and they dont get destroyded
    }

    public void GoToMainScene()
    {
        if (mapHeightSizeInputField.text.Equals("") || goldInputField.text.Equals("") || potionsInputField.text.Equals("") || mapWidthSizeInputField.text.Equals("")) // checks if given input is an integer between 100-200. If true keeps a var for mapHeightSize and loads new scene. If false shows error
        {
            errorsMessegesText.text = "Please fill all the fields";
        }
        else if (!IsDigitsOnly(mapHeightSizeInputField.text) || !IsDigitsOnly(goldInputField.text) || !IsDigitsOnly(potionsInputField.text) || !IsDigitsOnly(mapWidthSizeInputField.text))
        {
            errorsMessegesText.text = "Please enter only a positive integer";
        }
        else if (int.Parse(mapHeightSizeInputField.text) > 200 || int.Parse(mapHeightSizeInputField.text) < 100 || int.Parse(mapWidthSizeInputField.text) > 200 || int.Parse(mapWidthSizeInputField.text) < 100)
        {
            errorsMessegesText.text = "Please enter an integer between 100 and 200 for Map field";
        }
        else if (int.Parse(goldInputField.text) <= 0 || int.Parse(potionsInputField.text) <= 0)
        {
            errorsMessegesText.text = "Please enter a positive integer for Gold and Energy Potions field";
        }
        else // right condition
        {
            mapHeightSize = int.Parse(mapHeightSizeInputField.text);
            mapWidthSize = int.Parse(mapWidthSizeInputField.text);
            goldSize = int.Parse(goldInputField.text);
            potionsSize = int.Parse(potionsInputField.text);
            canvas.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    private bool IsDigitsOnly(string str) // checks if given input is only integers
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}


