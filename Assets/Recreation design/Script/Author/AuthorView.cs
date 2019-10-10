using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AuthorView : MonoBehaviour {
    public Button Return { get; set; }
    // Use this for initialization
    void Awake () {
        Return = GetComponent<Button>();
    }	
}
