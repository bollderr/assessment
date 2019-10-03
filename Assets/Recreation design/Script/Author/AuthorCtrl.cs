using UnityEngine;
using System.Collections;

public class AuthorCtrl : MonoBehaviour {
    public AuthorView view { get; set; }
    // Use this for initialization
    void Start () {
        view = gameObject.AddComponent<AuthorView>();
        view.Btn_Return.onClick.AddListener(delegate() {
            Destroy(gameObject);
        });
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
