using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject mainScreen;
    public GameObject saveScreen;
    public GameObject scoresScreen;
    public GameObject refreshScreen;

    public Sprite emptyCart;

    public Text slot1Desc;
    public Text slot2Desc;
    public Text slot3Desc;
    public Image slot1Image;
    public Image slot2Image;
    public Image slot3Image;

    public Transform scoreParent;
    public GameObject scorePF;

    private static AudioSource music = null;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        if (!music)
        {
            AudioSource mas = AudioManager.one.PlaySound("SpaceJazz", "music");
            mas.loop = true;
            music = mas;
        }

        LoadSaveSlots();

        if (SaveManager.wonGame)
        {
            ShowScreen("scores");
        }

    }

    private void LoadSaveSlots()
    {
        string s1 = PlayerPrefs.GetString("SaveData1", "");
        string s2 = PlayerPrefs.GetString("SaveData2", "");
        string s3 = PlayerPrefs.GetString("SaveData3", "");
        if (s1 == "")
        {
            slot1Image.sprite = emptyCart;
            slot1Desc.text = "New Game";
        }
        else
        {
            string[] sData = s1.Split('\n');
            slot1Desc.text = TimeString(float.Parse(sData[2]));
        }
        if (s2 == "")
        {
            slot2Image.sprite = emptyCart;
            slot2Desc.text = "New Game";
        }
        else
        {
            string[] sData = s2.Split('\n');
            slot2Desc.text = TimeString(float.Parse(sData[2]));
        }
        if (s3 == "")
        {
            slot3Image.sprite = emptyCart;
            slot3Desc.text = "New Game";
        }
        else
        {
            string[] sData = s3.Split('\n');
            slot3Desc.text = TimeString(float.Parse(sData[2]));
        }
    }

    public void ShowScreen(string screen)
    {
        mainScreen.SetActive(false);
        saveScreen.SetActive(false);
        scoresScreen.SetActive(false);

        switch (screen)
        {
            case "main":
                mainScreen.SetActive(true);
                break;
            case "save":
                saveScreen.SetActive(true);
                break;
            case "scores":
                scoresScreen.SetActive(true);
                LoadMyScores();
                break;
        }
    }

    public void PlayGame(int slot)
    {
        SaveManager.saveSlot = slot;
        SaveManager.saveToLoad = PlayerPrefs.GetString("SaveData" + slot, "");
        SaveManager.wonGame = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void DeleteSlot(int slot)
    {
        PlayerPrefs.SetString("SaveData" + slot, "");
        LoadSaveSlots();
    }

    private void LoadMyScores()
    {
        for (int i = 0; i < scoreParent.childCount; i++)
        {
            Destroy(scoreParent.GetChild(i));
        }
        string scores = PlayerPrefs.GetString("Times");
        string[] sScores = scores.Split('\n');
        if (PlayerPrefs.GetFloat("BestTime", Mathf.Infinity) != Mathf.Infinity)
        {
            GameObject best = Instantiate(scorePF, scoreParent);
            best.transform.GetChild(0).GetComponent<Text>().text = "Best Time: " + TimeString(PlayerPrefs.GetFloat("BestTime"));
        }
        foreach (string score in sScores)
        {
            if (score == "") continue;
            GameObject go = Instantiate(scorePF, scoreParent);
            go.transform.GetChild(0).GetComponent<Text>().text = TimeString(float.Parse(score));
        }
    }
    private void LoadGlobalScores()
    {

    }

    public void QuitGame()
    {
        refreshScreen.SetActive(true);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string TimeString(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt(time / 60) - (hours * 60);
        float seconds = time - (minutes * 60) - (hours * 3600);
        if (hours > 0)
            return $"{hours:00}:{minutes:00}:{seconds:00.00}";
        else
            return $"{minutes:00}:{seconds:00.00}";
    }
}
