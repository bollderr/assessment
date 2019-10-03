using UnityEngine;
using System.Collections;

public class RunCtrl : MonoBehaviour {
    public bool isPause { get; set; }

    public RunView view { get; set; }
    Camera mainCamera { get; set; }

    private void Awake()
    {
        GameManager.instance.isOver = false;
        isPause = false;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    // Use this for initialization
    void Start () {
        view = gameObject.AddComponent<RunView>();
        view.SetScoreText(0, GameManager.instance.highScore);

        StartCoroutine(doZoomIn());


        view.Btn_Pause.onClick.AddListener(delegate () {
            isPause = true;
            GameManager.instance.currentShape.isPause = true;
            GameManager.instance.CreatePanel(PanelType.PasuePanel);
            AudioManager.instance.PlayCursor();
        });
        

        view.Btn_Left.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Left;
        });
        view.Btn_Right.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Right;
        });
        view.Btn_Down.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Down;
        });
        view.Btn_Rotate.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Up;
        });
    }

    /// <summary>
    /// Zoom
    /// </summary>
    /// <returns></returns>
    IEnumerator doZoomIn()
    {
        bool isdo = false;
        yield return null;
        while (!isdo)
        {
            if (Camera.main.orthographicSize < 10f)
            {
                isdo = true;
            }
            Camera.main.orthographicSize -= 0.1f;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void Update()
    {
        if (isPause) return;
        while (GameManager.instance.currentShape == null)
        {
            SquareType st = (SquareType)Random.Range(0, 7);
            //Test block creation
            GameManager.instance.CreateSquare(st);
        }
    }

}
