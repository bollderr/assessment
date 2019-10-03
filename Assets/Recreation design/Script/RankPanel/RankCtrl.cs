using UnityEngine;
using System.Collections;

public class RankCtrl : MonoBehaviour {
    public RankView view { get; set; }
    // Use this for initialization
    void Start () {
        view = gameObject.AddComponent<RankView>();

        view.Btn_Clear.onClick.AddListener(delegate () {
            view.TextClearData();
            DataModel.SaveData();
        });

        //Return button
        view.Btn_Return.onClick.AddListener(delegate () {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update () {
	
	}
}
