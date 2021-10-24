using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public static GameManager one;

    public bool playing = true;

    public Tilemap speedTiles;
    public TileBase[] speedTileBase;
    public PercentageBar creamBar;
    public GameObject fakeWall;
    public GameObject listItem;
    public GameObject breadItem;
    public GameObject bolognaItem;
    public GameObject listButton;
    public GameObject pauseButton;
    public GameObject listScreen;
    public GameObject[] listCross;

    // Save Variables
    private bool fakeWallDown = false;
    private bool listFound = false;
    private bool[] itemFound = new bool[5];

    void Awake()
    {
        one = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
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

    public void ActivateTrigger(string id)
    {
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
        }
    }

    public void OpenList(bool open)
    {
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
        playing = !pause;
        pauseButton.SetActive(!pause);
        Time.timeScale = pause ? 0f : 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
