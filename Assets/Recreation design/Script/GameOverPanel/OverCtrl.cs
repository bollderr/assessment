using UnityEngine;
using System.Collections;

public class OverControl : MonoBehaviour {
    public OverView view { get; set; }
    // Use this for initialization
    void Start () {
        view = gameObject.AddComponent<OverView>();

        view.SetScoreText(GameManager.instance.Score);
        
        AudioManager.instance.PlayGameOver();


        //Restart
        view.ReStart.onClick.AddListener(delegate() {
            ClearSquare();
            GameManager.instance.isOver = false;
            //Cancle Pause
            GameManager.instance.Control_run.isPause = false;
        });

        //Return to Home page
        view.Home.onClick.AddListener(delegate() {
            ClearSquare();
            GameManager.instance.isOver = true;
            Destroy(GameManager.instance.Control_run.gameObject);
            GameManager.instance.CreatePanel(PanelType.StartPanel);
        });
    }

    // Update is called once per frame
    void ClearSquare () {
        //Clear blocks
        for (int i = 0; i < GameManager.instance.view.CreatePoint.childCount; i++)
        {
            Destroy(GameManager.instance.view.CreatePoint.GetChild(i).gameObject);
        }
        //Clear Score
        GameManager.instance.Score = 0;
        GameManager.instance.Control_run.view.Txt_Curr.text = 0.ToString();
        //Self Destroy
        Destroy(gameObject);
    }
}
