using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// PanelType
public enum PanelType {
    StartPanel, RunPanel, SetPanel, RankPanel, GameOverPanel,PasuePanel, AuthorPanel
}

// BlockType
public enum SquareType
{
    Square_1, Square_2, Square_3, Square_4, Square_5, Square_6, Square_7
}

public class GameManager : MonoBehaviour {
    public StartControl Control_start { get; set; }
    public RunControl Control_run { get; set; }
    public SetControl Control_set { get; set; }
    public RankControl Control_rank { get; set; }
    public OverControl Control_over { get; set; }
    public PasueControl Control_pasue { get; set; }
    public AuthorControl Control_author { get; set; }
    public GameView view { get; set; }

    public int Score { get; set; }
    public bool Sound { get; set; }
    public int highScore { get; set; }
    public int numbersGame { get; set; }
    public bool isOver { get; set; }
    public SquareControl currentShape { get; set; }
    public static GameManager instance { get; set; }
    public const int NORMAL_ROWS = 12;
    public const int MAX_ROWS = 15;
    public const int MAX_COLUMNS = 10;

    Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];
    private bool isUpData;
    Color[] colors;
    void Awake()
    {
        Init();
        isOver = false;
        instance = this;
        view = gameObject.AddComponent<GameView>();
        gameObject.AddComponent<AudioManager>();
        
    }
    // Use this for initialization
    void Start () {
        if (Control_start == null)
        {
            CreatePanel(PanelType.StartPanel);
        }
        DataModel.LoadData();
        AudioManager.instance.SetAudioSource(!Sound);
    }

    void Update()
    {
        if (!isOver)
        {
            if (IsGameOver())
            {
                //Game end, Pause
                Control_run.isPause = true;
                isOver = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isOver)
        {
            Application.Quit();
        }
    }

    public void Init()
    {
        GameObject tranMap = Resources.Load<GameObject>("Prefab/Other/Map");
        Transform map = Instantiate(tranMap) .transform;
        map.name = "Map";

        GameObject Colors = Resources.Load<GameObject>("Prefab/Other/Colors");
        GameObject tranColor = Instantiate(Colors, map) as GameObject;
        tranColor.transform.name = "Colors";
        tranColor.transform.position = Vector3.one;
        colors = new Color[tranColor.transform.childCount];
        for (int i = 0; i < tranColor.transform.childCount; i++)
        {
            colors[i] = tranColor.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
        }
    }

    // Create Panel
    public GameObject CreatePanel(PanelType type) {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Panel/" + type);
        if (prefab != null)
        {
            GameObject clone = Instantiate(prefab,transform,false) as GameObject;
            clone.name = type.ToString();
            switch (type)
            {
                case PanelType.StartPanel:
                    Control_start = clone.AddComponent<StartControl>();
                    break;
                case PanelType.RunPanel:
                    Control_run = clone.AddComponent<RunControl>();
                    break;
                case PanelType.SetPanel:
                    Control_set = clone.AddComponent<SetControl>();
                    break;
                case PanelType.RankPanel:
                    Control_rank = clone.AddComponent<RankControl>();
                    break;
                case PanelType.GameOverPanel:
                    Control_over = clone.AddComponent<OverControl>();
                    break;
                case PanelType.PasuePanel:
                    Control_pasue = clone.AddComponent<PasueControl>();
                    break;
                case PanelType.AuthorPanel:
                    Control_author = clone.AddComponent<AuthorControl>();
                    break;
            }
            return clone;
        }
        return null;
    }

    // Creat Block
    public GameObject CreateSquare(SquareType type) {
        int indexColor = Random.Range(0, colors.Length);
        GameObject prefab = Resources.Load<GameObject>("Prefab/Square/" + type);
        if (prefab != null)
        {
            GameObject clone = Instantiate(prefab, view.CreatePoint) as GameObject;
            clone.name = type.ToString();

            clone.transform.position = view.CreatePoint.transform.position;
            currentShape = clone.AddComponent<SquareControl>();
            currentShape.SetColor(colors[indexColor]);
            return clone;
        }
        return null;
    }

    // Check if the block's place is valid
    public bool IsValidMapPosition(Transform t)
    {
        // Traversing blocks
        foreach (Transform child in t)
        {
            //Skip tag which is not Block
            if (child.tag != "Block") continue;
            Vector2 pos = Round(child.position);
            //Check if is in the Map
            if (IsInsideMap(pos) == false) return false;
            //Check if is in the Array
            if (map[(int)pos.x, (int)pos.y] != null) return false;
        }
        return true;
    }

    // If is in the Map
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }

    // Place block
     public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = Round(child.position);
            map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }

    // Check Map if need delete row
    private bool CheckMap()
    {
        int count = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = CheckIsRowFull(i);
            if (isFull)
            {
                count++;
                DeleteRow(i);
                MoveDownRowsAbove(i + 1);
                i--;
            }
        }
        Debug.Log(count);
        if (count > 0)
        {
            Score += (count * 100);
            if (Score > highScore)
            {
                highScore = Score;
            }
            isUpData = true;
            return true;
        }
        else return false;
    }

    // If the row is full
    bool CheckIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] == null) return false;
        }
        return true;
    }

    // DeleteRow
    void DeleteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    // MoveDownRowsAbove
    void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }

    // MoveDownRow
    void MoveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    // Block fall down
    public void FallDown()
    {
        currentShape = null;
        if (isUpData)
        {
            Control_run.view.SetScoreText(Score, highScore);
        }
        foreach (Transform t in view.CreatePoint)
        {
            if (t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
    }

    // V3 change to V2
    public static Vector2 Round(Vector3 v)
    {
        //RoundToInt：Returns rounded to the nearest integer.
        int x = Mathf.RoundToInt(v.x);
        int y = Mathf.RoundToInt(v.y);
        return new Vector2(x, y);
    }


    // Whether game is over
    public bool IsGameOver()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j, i] != null)
                {
                    isOver = true;
                    Control_run.isPause = true;
                    //Loading GameOverPanel
                    CreatePanel(PanelType.GameOverPanel);
                    Control_run.view.SetScoreText(0, highScore);
                    
                    Debug.Log(123 + "<---------->");
                    numbersGame++;
                    DataModel.SaveData();
                    return true;
                }
            }
        }
        return false;
    }
}
