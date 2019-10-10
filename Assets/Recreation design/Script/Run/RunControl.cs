using UnityEngine;
using System.Collections;

public class RunControl : MonoBehaviour {
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


        view.Pause.onClick.AddListener(delegate () {
            isPause = true;
            GameManager.instance.currentShape.isPause = true;
            GameManager.instance.CreatePanel(PanelType.PasuePanel);
            AudioManager.instance.PlayCursor();
        });
        

        view.Left.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Left;
        });
        view.Right.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Right;
        });
        view.Down.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Down;
        });
        view.Rotate.onClick.AddListener(delegate () {
            GameManager.instance.currentShape.dirType = DirectionType.Up;
        });
    }

    // Zoom
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
