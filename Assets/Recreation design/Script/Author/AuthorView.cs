using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AuthorView : MonoBehaviour {
    public Button Btn_Return { get; set; }
    // Use this for initialization
    void Awake () {
        Btn_Return = GetComponent<Button>();
    }	
}
