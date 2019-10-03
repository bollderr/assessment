using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PasueView : MonoBehaviour {

    public Button Btn_Home { get; set; }
    public Button Btn_Start { get; set; }
    public Text Txt_Curr { get; set; }
    // Use this for initialization
    void Awake()
    {
        Btn_Home = transform.Find("Img_Bg/Btn_Home").GetComponent<Button>();
        Btn_Start = transform.Find("Img_Bg/Btn_Start").GetComponent<Button>();
        Txt_Curr = transform.Find("Img_Bg/Txt_Count").GetComponent<Text>();
    }

}
