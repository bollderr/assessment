using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunView : MonoBehaviour {

    public Button Btn_Pause { get; set; }
    public Button Btn_Left { get; set; }
    public Button Btn_Right { get; set; }
    public Button Btn_Rotate { get; set; }
    public Button Btn_Down { get; set; }
    public Text Txt_Curr { get; set; }
    public Text Txt_Max { get; set; }
    // Use this for initialization
    void Awake() {
        Btn_Pause = transform.Find("Page_Top/Btn_Pause").GetComponent<Button>();
        Txt_Curr = transform.Find("Page_Top/Txt_Curr/Text").GetComponent<Text>();
        Txt_Max = transform.Find("Page_Top/Txt_Max/Text").GetComponent<Text>();

        Btn_Left = transform.Find("ButtonGroup/Btn_Left").GetComponent<Button>();
        Btn_Right = transform.Find("ButtonGroup/Btn_Right").GetComponent<Button>();
        Btn_Rotate = transform.Find("ButtonGroup/Btn_Rotate").GetComponent<Button>();
        Btn_Down = transform.Find("ButtonGroup/Btn_Down").GetComponent<Button>();
    }

    public void SetScoreText(int curr,int max) {
        Txt_Curr.text = curr.ToString();
        Txt_Max.text = max.ToString();
    }
    public void SetScoreText(string curr, string max)
    {
        Txt_Curr.text = curr;
        Txt_Max.text = max;
    }
}
