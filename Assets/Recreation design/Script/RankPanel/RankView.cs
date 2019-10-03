using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankView : MonoBehaviour {
    public Button Btn_Clear { get; set; }
    public Button Btn_Return { get; set; }

    public Text Txt_Curr { get; set; }
    public Text Txt_Max { get; set; }
    public Text Txt_Count { get; set; }

    // Use this for initialization
    void Awake() {
        Btn_Clear = transform.Find("Img_Bg/Btn_Clear").GetComponent<Button>();
        Txt_Curr = transform.Find("Img_Bg/Txt_Name").GetComponent<Text>();
        Txt_Max = transform.Find("Img_Bg/Txt_Max/Text").GetComponent<Text>();
        Txt_Count = transform.Find("Img_Bg/Txt_Count/Text").GetComponent<Text>();
        Btn_Return = GetComponent<Button>();
    }
    void Start()
    {
        SetScoreText();
    }
    public void TextClearData() {
        Txt_Max.text = 0.ToString();
        Txt_Count.text = 0.ToString();
        GameManager.instance.Score = 0;
        GameManager.instance.highScore = 0;
        GameManager.instance.numbersGame = 0;
    }

    public void SetScoreText() {
        Txt_Max.text = GameManager.instance.highScore.ToString();
        Txt_Count.text = GameManager.instance.numbersGame.ToString();
    }
    
}
