using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartView : MonoBehaviour {

    public Button Btn_Start { get; set; }
    public Button Btn_ReStart { get; set; }
    public Button Btn_Set { get; set; }
    public Button Btn_Rank { get; set; }
    public Text Txt_Title { get; set; }

    // Use this for initialization
    void Awake() {
        Btn_Start = transform.Find("ButtonGroup/Btn_Start").GetComponent<Button>();
        Btn_ReStart = transform.Find("ButtonGroup/Btn_ReStart").GetComponent<Button>();
        Btn_Set = transform.Find("ButtonGroup/Btn_Set").GetComponent<Button>();
        Btn_Rank = transform.Find("ButtonGroup/Btn_Rank").GetComponent<Button>();
        Txt_Title = transform.Find("Txt_Title").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {        
	}
}
