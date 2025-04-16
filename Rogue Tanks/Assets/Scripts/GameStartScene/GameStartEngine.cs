using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartEngine : MonoBehaviour
{
    public Image TankImage;
    public Text OnePlayer;
    public Text TwoPlayers;
    public Text LevelSelection;
    public Text Construction;
    private int selection = 1;

    void Start()
    {
        TankImage.rectTransform.position = new Vector3(TankImage.rectTransform.position.x, OnePlayer.rectTransform.position.y, 0);
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selection -= 1;
            if (selection == 0)
                selection = 4;
            PlaceSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selection += 1;
            if (selection == 5)
                selection = 1;
            PlaceSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
            LoadStage();

        if (selection == 3 && LevelSelector.Instance != null)
        {
            bool updated = false;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LevelSelector.Instance.DecreaseLevel();
                updated = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                LevelSelector.Instance.IncreaseLevel();
                updated = true;
            }

            if(updated)
            {
                UpdateLevelText();
            }
        }
    }

    private void LoadStage()
    {
        if (selection == 1)
        {
            SceneManager.LoadGameScene();
        }
        else if(selection == 3)
        {
            LevelSelector levelSelector = LevelSelector.Instance;
            if(levelSelector != null)
            {
                levelSelector.UseSelector = true;
            }
            SceneManager.LoadGameScene();
        }
    }

    private void PlaceSelection()
    {
        if (selection == 1)
            SetTank(OnePlayer.rectTransform.position.y);
        else if (selection == 2)
            SetTank(TwoPlayers.rectTransform.position.y);
        else if (selection == 3)
            SetTank(LevelSelection.rectTransform.position.y);
        else if (selection == 4)
            SetTank(Construction.rectTransform.position.y);
    }

    private void SetTank(float pos)
    {
        TankImage.rectTransform.position = new Vector3(TankImage.rectTransform.position.x, pos, 0);
    }

    private void UpdateLevelText()
    {
        LevelSelector levelSelector = LevelSelector.Instance;
        LevelSelection.text = $"LEVEL SELECT: {(levelSelector != null ? levelSelector.Level : 0)}";
    }
}
