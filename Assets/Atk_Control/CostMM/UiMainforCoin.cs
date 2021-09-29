using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMainforCoin : MonoBehaviour
{
    static private UiMainforCoin mInstance;
    static public UiMainforCoin Instance() { return mInstance; }
    Canvas cv;
    RectTransform rt;
    //public Object floatTextPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        mInstance = this;
        cv = GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();
    }
    public Canvas GetCanvas()
    {
        return cv;
    }
    public RectTransform GetRectTransform()
    {
        return rt;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnFloatingText(Transform target, string sText)
    {
        GameObject pfgo = Resources.Load<GameObject>("FloatingCoin");
        GameObject go = Instantiate(pfgo) as GameObject;
        
        FloatingCoin ft = go.GetComponent<FloatingCoin>();
        ft.SetupText(target, sText, 1.0f);
        go.transform.SetParent(this.transform);
    }

}
