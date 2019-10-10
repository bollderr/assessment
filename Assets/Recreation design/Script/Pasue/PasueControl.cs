using UnityEngine;
using System.Collections;

public class PasueControl : MonoBehaviour {
    public PasueView view { get; set; }

    // Use this for initialization
    void Start () {
        view = gameObject.AddComponent<PasueView>();
        view.Txt_Curr.text = GameManager.instance.Score.ToString();
        //Start button
        view.Start.onClick.AddListener(delegate () {
            GameManager.instance.isOver = false;
            GameManager.instance.Control_run.isPause = false;
            GameManager.instance.currentShape.isPause = false;
            Destroy(gameObject);
            AudioManager.instance.PlayCursor();
        });

        view.Home.onClick.AddListener(delegate () {
            DestroyAll();
            GameManager.instance.isOver = true;
            AudioManager.instance.PlayCursor();
            GameManager.instance.CreatePanel(PanelType.StartPanel);
        });
    }
    void DestroyAll() {
        for (int i = 1; i < GameManager.instance.transform.childCount; i++)
        {
            Destroy(GameManager.instance.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < GameManager.instance.view.CreatePoint.childCount; i++)
        {
            Destroy(GameManager.instance.view.CreatePoint.GetChild(i).gameObject);
        }
    }
}
