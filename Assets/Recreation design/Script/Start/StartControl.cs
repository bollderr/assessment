using UnityEngine;
using System.Collections;

public class StartControl : MonoBehaviour {
    public StartView view { get; set; }
    Camera mainCamera { get; set; }
    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start () {
        view = gameObject.AddComponent<StartView>();
        //Start button
        view.Start.onClick.AddListener(delegate() {
            Destroy(gameObject);
            GameManager.instance.CreatePanel(PanelType.RunPanel);
            AudioManager.instance.PlayCursor();
        });
        //Restart button
        view.ReStart.onClick.AddListener(delegate () {
            Destroy(gameObject);
            GameManager.instance.CreatePanel(PanelType.RunPanel);
            AudioManager.instance.PlayCursor();
        });
        //Setting button
        view.Btn_Set.onClick.AddListener(delegate () {            
            GameManager.instance.CreatePanel(PanelType.SetPanel);
            AudioManager.instance.PlayCursor();
        });
        //Rank button
        view.Btn_Rank.onClick.AddListener(delegate () {
            GameManager.instance.CreatePanel(PanelType.RankPanel);
            AudioManager.instance.PlayCursor();
        });
        StartCoroutine(doZoomOut());
    }
    //zoom out
    IEnumerator doZoomOut()
    {
        bool isdo = false;
        yield return null;
        while (!isdo)
        {
            if (mainCamera.orthographicSize > 12.5f)
            {
                isdo = true;
            }
            mainCamera.orthographicSize += 0.1f;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
