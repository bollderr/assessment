using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunView : MonoBehaviour {

    public Button Pause { get; set; }
    public Button Left { get; set; }
    public Button Right { get; set; }
    public Button Rotate { get; set; }
    public Button Down { get; set; }
    public Text Txt_Curr { get; set; }
    public Text Txt_Max { get; set; }
    // Use this for initialization
    void Awake() {
        Pause = transform.Find("Page_Top/Btn_Pause").GetComponent<Button>();
        Txt_Curr = transform.Find("Page_Top/Txt_Curr/Text").GetComponent<Text>();
        Txt_Max = transform.Find("Page_Top/Txt_Max/Text").GetComponent<Text>();

        Left = transform.Find("ButtonGroup/Btn_Left").GetComponent<Button>();
        Right = transform.Find("ButtonGroup/Btn_Right").GetComponent<Button>();
        Rotate = transform.Find("ButtonGroup/Btn_Rotate").GetComponent<Button>();
        Down = transform.Find("ButtonGroup/Btn_Down").GetComponent<Button>();
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
