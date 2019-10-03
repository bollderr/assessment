using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameView : MonoBehaviour {
    
    #region GameObject
    public GameObject Square_O { get; set; }
    public GameObject Square_L { get; set; }
    public GameObject Square_L1 { get; set; }
    public GameObject Square_I { get; set; }
    public GameObject Square_S { get; set; }
    public GameObject Square_Z { get; set; }
    public GameObject Square_T { get; set; }
    public Transform CreatePoint { get; set; }
    #endregion


    // Use this for initialization
    void Awake () {
        Square_O = Resources.Load<GameObject>("Prefab/Square_O");
        Square_L = Resources.Load<GameObject>("Prefab/Square_L");
        Square_L1 = Resources.Load<GameObject>("Prefab/Square_L1");
        Square_I = Resources.Load<GameObject>("Prefab/Square_I");
        Square_S = Resources.Load<GameObject>("Prefab/Square_S");
        Square_Z = Resources.Load<GameObject>("Prefab/Square_Z");
        Square_T = Resources.Load<GameObject>("Prefab/Square_T");
        CreatePoint = GameObject.Find("Map").transform.Find("CreatePoint");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
