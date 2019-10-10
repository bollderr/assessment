using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverView : MonoBehaviour {
    public Button Home { get; set; }
    public Button ReStart { get; set; }
    public Text Txt_Curr { get; set; }
    // Use this for initialization
    void Awake () {
        Home = transform.Find("Img_Bg/Btn_Home").GetComponent<Button>();
        ReStart = transform.Find("Img_Bg/Btn_ReStart").GetComponent<Button>();
        Txt_Curr = transform.Find("Img_Bg/Txt_Count").GetComponent<Text>();
    }

    public void SetScoreText(int score) {
        Txt_Curr.text = score.ToString();
    }
}
