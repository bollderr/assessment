using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PasueView : MonoBehaviour {

    public Button Home { get; set; }
    public Button Start { get; set; }
    public Text Txt_Curr { get; set; }
    // Use this for initialization
    void Awake()
    {
        Home = transform.Find("Img_Bg/Btn_Home").GetComponent<Button>();
        Start = transform.Find("Img_Bg/Btn_Start").GetComponent<Button>();
        Txt_Curr = transform.Find("Img_Bg/Txt_Count").GetComponent<Text>();
    }

}
