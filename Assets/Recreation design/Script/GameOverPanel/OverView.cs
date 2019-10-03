using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverView : MonoBehaviour {
    public Button Btn_Home { get; set; }
    public Button Btn_ReStart { get; set; }
    public Text Txt_Curr { get; set; }
    // Use this for initialization
    void Awake () {
        Btn_Home = transform.Find("Img_Bg/Btn_Home").GetComponent<Button>();
        Btn_ReStart = transform.Find("Img_Bg/Btn_ReStart").GetComponent<Button>();
        Txt_Curr = transform.Find("Img_Bg/Txt_Count").GetComponent<Text>();
    }

    public void SetScoreText(int score) {
        Txt_Curr.text = score.ToString();
    }
}
