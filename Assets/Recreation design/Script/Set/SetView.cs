using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetView : MonoBehaviour {
    public Button Btn_Forum { get; set; }
    public Button Btn_WebSite { get; set; }
    public Button Btn_Collect { get; set; }
    public Button Btn_Sound { get; set; }
    public Button Return { get; set; }

    public GameObject mask { get; set; }
    // Use this for initialization
    void Awake() {
        Btn_Forum = transform.Find("Img_Bg/Btn_Forum").GetComponent<Button>();
        Btn_WebSite = transform.Find("Img_Bg/Btn_WebSite").GetComponent<Button>();
        Btn_Collect = transform.Find("Img_Bg/Btn_Collect").GetComponent<Button>();
        Btn_Sound = transform.Find("Img_Bg/Btn_Sound").GetComponent<Button>();
        mask = Btn_Sound.transform.Find("Mask").gameObject;
        Return = GetComponent<Button>();
    }
    public void SetSoundMask(bool isActive)
    {
        mask.SetActive(isActive);
    }
    public void SetSoundMask() {
        mask.SetActive(!mask.activeSelf);
        GameManager.instance.Sound = mask.activeSelf;
        AudioManager.instance.SetAudioSource(!mask.activeSelf);
    }
}
