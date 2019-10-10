using UnityEngine;
using System.Collections;

public class RankControl : MonoBehaviour {
    public RankView view { get; set; }
    // Use this for initialization
    void Start () {
        view = gameObject.AddComponent<RankView>();

        view.Clear.onClick.AddListener(delegate () {
            view.TextClearData();
            DataModel.SaveData();
        });

        //Return button
        view.Return.onClick.AddListener(delegate () {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update () {
	
	}
}
