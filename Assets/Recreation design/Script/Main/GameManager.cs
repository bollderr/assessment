using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// PanelType
/// </summary>
public enum PanelType {
    StartPanel, RunPanel, SetPanel, RankPanel, GameOverPanel,PasuePanel, AuthorPanel
}

/// <summary>
/// BlockType
/// </summary>
public enum SquareType
{
    Square_1, Square_2, Square_3, Square_4, Square_5, Square_6, Square_7
}

public class GameManager : MonoBehaviour {
    public StartCtrl ctrl_start { get; set; }
    public RunCtrl ctrl_run { get; set; }
    public SetCtrl ctrl_set { get; set; }
    public RankCtrl ctrl_rank { get; set; }
    public OverCtrl ctrl_over { get; set; }
    public PasueCtrl ctrl_pasue { get; set; }
    public AuthorCtrl ctrl_author { get; set; }
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
        if (ctrl_start == null)
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
                ctrl_run.isPause = true;
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

    /// <summary>
    /// Create Panel
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject CreatePanel(PanelType type) {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Panel/" + type);
        if (prefab != null)
        {
            GameObject clone = Instantiate(prefab,transform,false) as GameObject;
            clone.name = type.ToString();
            switch (type)
            {
                case PanelType.StartPanel:
                    ctrl_start = clone.AddComponent<StartCtrl>();
                    break;
                case PanelType.RunPanel:
                    ctrl_run = clone.AddComponent<RunCtrl>();
                    break;
                case PanelType.SetPanel:
                    ctrl_set = clone.AddComponent<SetCtrl>();
                    break;
                case PanelType.RankPanel:
                    ctrl_rank = clone.AddComponent<RankCtrl>();
                    break;
                case PanelType.GameOverPanel:
                    ctrl_over = clone.AddComponent<OverCtrl>();
                    break;
                case PanelType.PasuePanel:
                    ctrl_pasue = clone.AddComponent<PasueCtrl>();
                    break;
                case PanelType.AuthorPanel:
                    ctrl_author = clone.AddComponent<AuthorCtrl>();
                    break;
            }
            return clone;
        }
        return null;
    }

    /// <summary>
    /// Creat Block
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Check if the block's place is valid
    /// </summary>
    /// <param name="t">Position</param>
    /// <returns></returns>
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

    /// <summary>
    /// If is in the Map
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }

    /// <summary>
    /// Place block
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Check Map if need delete row
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// If the row is full
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    bool CheckIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] == null) return false;
        }
        return true;
    }

    /// <summary>
    /// DeleteRow
    /// </summary>
    /// <param name="row"></param>
    void DeleteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    /// <summary>
    /// MoveDownRowsAbove
    /// </summary>
    /// <param name="row"></param>
    void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }

    /// <summary>
    /// MoveDownRow
    /// </summary>
    /// <param name="row"></param>
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
    /// <summary>
    /// Block fall down
    /// </summary>
    public void FallDown()
    {
        currentShape = null;
        if (isUpData)
        {
            ctrl_run.view.SetScoreText(Score, highScore);
        }
        foreach (Transform t in view.CreatePoint)
        {
            if (t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
    }

    /// <summary>
    /// V3 change to V2
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 Round(Vector3 v)
    {
        //RoundToInt：Returns rounded to the nearest integer.
        int x = Mathf.RoundToInt(v.x);
        int y = Mathf.RoundToInt(v.y);
        return new Vector2(x, y);
    }


    /// <summary>
    /// Whether game is over
    /// </summary>
    /// <returns></returns>
    public bool IsGameOver()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j, i] != null)
                {
                    isOver = true;
                    ctrl_run.isPause = true;
                    //Loading GameOverPanel
                    CreatePanel(PanelType.GameOverPanel);
                    ctrl_run.view.SetScoreText(0, highScore);
                    
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
