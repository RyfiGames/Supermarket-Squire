using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager one;

    public bool playing = true;

    public PlayerController player;
    public Tilemap speedTiles;
    public TileBase[] speedTileBase;
    public PercentageBar creamBar;
    public GameObject fakeWall;
    public GameObject listItem;
    public GameObject breadItem;
    public GameObject bolognaItem;
    public GameObject mayoItem;
    public GameObject eggItem;
    public GameObject milkItem;
    public GameObject listButton;
    public GameObject pauseButton;
    public Text timerText;
    public GameObject tutorialScreen;
    public GameObject errorLoading;
    public GameObject listScreen;
    public GameObject[] listCross;
    public GameObject pauseScreen;

    public Transform exitStart;

    private float tutTimer = 5f;
    private Dictionary<string, PlayerTriggerCollider> triggers = new Dictionary<string, PlayerTriggerCollider>();

    // Save Variables
    private bool fakeWallDown = false;
    private bool listFound = false;
    private bool[] itemFound = new bool[5];
    ///

    private bool loadingSave = true;
    private float saveTimer = 10f;
    private float speedRunTimer = 0f;

    void Awake()
    {
        one = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        LoadSave();
    }

    public void LoadSave()
    {
        string saveData = SaveManager.saveToLoad;

        if (saveData == "")
        {
            loadingSave = false;
            return;
        }

        try
        {
            string[] sData = saveData.Split('\n');
            float px = float.Parse(sData[0]);
            float py = float.Parse(sData[1]);
            player.transform.position = new Vector3(px, py, 0f);
            speedRunTimer = float.Parse(sData[2]);
            if (bool.Parse(sData[3]))
            {
                ActivateTrigger("opening");
            }
            if (bool.Parse(sData[4]))
            {
                ActivateTrigger("list");
            }
            if (bool.Parse(sData[5]))
            {
                ActivateTrigger("bread");
            }
            if (bool.Parse(sData[6]))
            {
                ActivateTrigger("bologna");
            }
            if (bool.Parse(sData[7]))
            {
                ActivateTrigger("mayo");
            }
            if (bool.Parse(sData[8]))
            {
                ActivateTrigger("eggs");
            }
            if (bool.Parse(sData[9]))
            {
                ActivateTrigger("milk");
            }
        }
        catch
        {
            errorLoading.SetActive(true);
        }

        loadingSave = false;
    }

    public void SaveGame()
    {
        string saveData = "";
        saveData += player.transform.position.x + "\n";
        saveData += player.transform.position.y + "\n";
        saveData += speedRunTimer + "\n";
        saveData += fakeWallDown + "\n";
        saveData += listFound + "\n";
        saveData += itemFound[0] + "\n";
        saveData += itemFound[1] + "\n";
        saveData += itemFound[2] + "\n";
        saveData += itemFound[3] + "\n";
        saveData += itemFound[4] + "\n";
        PlayerPrefs.SetString("SaveData" + SaveManager.saveSlot, saveData);
    }

    public Vector2 GetTileDirection(Vector2 collPos)
    {
        Vector3Int tileSpacePos = speedTiles.WorldToCell(collPos);
        TileBase tb = speedTiles.GetTile(tileSpacePos);
        if (!tb) { return Vector2.zero; }
        string tileName = ((AnimatedTile)tb).name;
        switch (tileName)
        {
            case "Speed Tile Up":
                return Vector2.up;
            case "Speed Tile Left":
                return Vector2.left;
            case "Speed Tile Down":
                return Vector2.down;
            case "Speed Tile Right":
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    public void RotateSpeedTiles(Vector2 low, Vector2 high)
    {
        Vector3Int lowInt = speedTiles.WorldToCell(low);
        Vector3Int highInt = speedTiles.WorldToCell(high);
        for (int x = lowInt.x; x <= highInt.x; x++)
        {
            for (int y = lowInt.y; y <= highInt.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tb = speedTiles.GetTile(position);
                if (!tb) { continue; }
                string tileName = ((AnimatedTile)tb).name;
                switch (tileName)
                {
                    case "Speed Tile Up":
                        speedTiles.SetTile(position, speedTileBase[2]);
                        break;
                    case "Speed Tile Left":
                        speedTiles.SetTile(position, speedTileBase[3]);
                        break;
                    case "Speed Tile Down":
                        speedTiles.SetTile(position, speedTileBase[0]);
                        break;
                    case "Speed Tile Right":
                        speedTiles.SetTile(position, speedTileBase[1]);
                        break;
                }
            }
        }
    }

    public void RegisterTrigger(PlayerTriggerCollider trigger)
    {
        triggers.Add(trigger.triggerID, trigger);
    }

    public void ActivateTrigger(string id)
    {
        triggers[id].activated = true;

        switch (id)
        {
            case "opening":
                fakeWall.SetActive(false);
                fakeWallDown = true;
                break;
            case "list":
                listItem.SetActive(false);
                OpenList(true);
                listFound = true;
                break;
            case "bread":
                breadItem.SetActive(false);
                FindItem(0);
                OpenList(true);
                RotateSpeedTiles(new Vector2(-4, 70), new Vector2(14, 144));
                break;
            case "bologna":
                bolognaItem.SetActive(false);
                FindItem(1);
                OpenList(true);
                RotateSpeedTiles(new Vector2(48, 60), new Vector2(92, 87));
                break;
            case "mayo":
                mayoItem.SetActive(false);
                FindItem(2);
                OpenList(true);
                RotateSpeedTiles(new Vector2(-16, 173), new Vector2(-16, 231));
                RotateSpeedTiles(new Vector2(-16, 172), new Vector2(38, 172));
                break;
            case "eggs":
                eggItem.SetActive(false);
                FindItem(3);
                OpenList(true);
                break;
            case "milk":
                milkItem.SetActive(false);
                FindItem(4);
                OpenList(true);
                RotateSpeedTiles(new Vector2(56, 181), new Vector2(66, 231));
                break;
            case "exit":
                if (itemFound[0] && itemFound[1] && itemFound[2] && itemFound[3] && itemFound[4])
                {
                    EndGame();
                }
                else
                {
                    OpenList(true);
                    player.transform.position = exitStart.position;
                }
                break;
        }
    }

    public void OpenList(bool open)
    {
        if (loadingSave)
        {
            listButton.SetActive(true);
            return;
        }
        listScreen.SetActive(open);
        listButton.SetActive(!open);
        PauseGame(open);
    }

    public void FindItem(int item)
    {
        listCross[item].SetActive(true);
        itemFound[item] = true;
    }

    public void PauseGame(bool pause)
    {
        if (!listScreen.activeInHierarchy) pauseScreen.SetActive(pause);
        playing = !pause;
        pauseButton.SetActive(!pause);
        Time.timeScale = pause ? 0f : 1f;
    }

    public void EndGame()
    {
        if (PlayerPrefs.GetFloat("BestTime", Mathf.Infinity) > speedRunTimer)
        {
            PlayerPrefs.SetFloat("BestTime", speedRunTimer);
        }
        PlayerPrefs.SetString("Times", PlayerPrefs.GetString("Times") + speedRunTimer + "\n");
        SaveManager.wonGame = true;
        PlayerPrefs.SetString("SaveData" + SaveManager.saveSlot, "");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void SaveQuit()
    {
        SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (tutTimer > 0f)
        {
            tutTimer -= Time.deltaTime;
            if (tutTimer <= 0f)
            {
                tutorialScreen.SetActive(false);
            }
        }
        if (saveTimer > 0f)
        {
            saveTimer -= Time.deltaTime;
            if (saveTimer <= 0f)
            {
                saveTimer = 10f;
                SaveGame();
            }
        }
        if (playing)
        {
            speedRunTimer += Time.deltaTime;
            timerText.text = TimeString(speedRunTimer);
        }
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
