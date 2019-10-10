using UnityEngine;
using System.Collections;

public class SetControl : MonoBehaviour {
    public SetView view { get; set; }
    // Use this for initialization
    void Start () {	
        view = gameObject.AddComponent<SetView>();

        view.SetSoundMask(GameManager.instance.Sound);

        //Sound button
        view.Btn_Sound.onClick.AddListener(delegate () {
            view.SetSoundMask();
            DataModel.SaveData();
            AudioManager.instance.PlayControl();
        });

        view.Btn_Collect.onClick.AddListener(delegate () {
            GameManager.instance.CreatePanel(PanelType.AuthorPanel);
        });
        view.Btn_Forum.onClick.AddListener(delegate () {
            GameManager.instance.CreatePanel(PanelType.AuthorPanel);
        });
        view.Btn_WebSite.onClick.AddListener(delegate () {
            GameManager.instance.CreatePanel(PanelType.AuthorPanel);
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
