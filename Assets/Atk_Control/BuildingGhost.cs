using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private Transform visual;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private ITrapCustomer trapBuyer;
    public GameObject NoCoinUI;
    private bool CanBuy;
    private LayerMask hitGround;
    private PathFindingGrid m_PutTrapTerrain;
    public GameObject CantPutUI;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    bool isbackzero;
    private void Start() {
        trapBuyer = GameObject.FindWithTag("Player").GetComponent<ITrapCustomer>();

        RefreshVisual();
        //CanCoinBuyTrap();GetComponent<ITrapCustomer>();
        hitGround = WeaponController.Instance.hitGround;
        isbackzero = false;



        WeaponController.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        //WeaponController.Instance.OnSelectedChanged += Instance_OnSelectedChanged_CanBuyTrap;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }
    //private void Instance_OnSelectedChanged_CanBuyTrap(object sender, System.EventArgs e)
    //{
    //    CanCoinBuyTrap();
    //}

    private void LateUpdate() {
        //CanCoinBuyTrap();
        ////if(不能買不夠錢){Destroy}else{}
        //if (CanBuy)
        //{

        //}
        //else
        //{

        //    NoCoinUI.SetActive(true);
        //}

        RefreshVisual();//?問一下怎摸省

        Vector3 targetPosition = WeaponController.Instance.GetMouseWorldSnappedPosition();
        //targetPosition.y = 1f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, WeaponController.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }
    //private void CanCoinBuyTrap()
    //{
    //    PlacedObjectTypeSO placedObjectTypeSO = WeaponController.Instance.GetPlacedObjectTypeSO();
    //    if (placedObjectTypeSO != null&&trapBuyer.CanBuyTrap(placedObjectTypeSO.GetCost)) //我可以買的話就顯示
    //    {
    //        CanBuy = true;
    //    }
    //    else
    //    {
    //        CanBuy = false;
    //    }//還要再加一個禁止
    //}



    private void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
            
        }
        
        PlacedObjectTypeSO placedObjectTypeSO = WeaponController.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) 
        {
            if (CanPutTrapVisually(placedObjectTypeSO))
            {
                CantPutUI.SetActive(false);
                if (trapBuyer.CanBuyTrap(placedObjectTypeSO.GetCost)) //我可以買的話就顯示
                {
                    NoCoinUI.SetActive(false);
                    visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
                    visual.parent = transform;
                    visual.localPosition = Vector3.zero;
                    visual.localEulerAngles = Vector3.zero;
                    SetLayerRecursive(visual.gameObject, 11);
                }
                else
                {
                    if (!isbackzero)
                    {
                        visual = Instantiate(placedObjectTypeSO.redvisual, Vector3.zero, Quaternion.identity);
                        visual.parent = transform;
                        visual.localPosition = Vector3.zero;
                        visual.localEulerAngles = Vector3.zero;
                        SetLayerRecursive(visual.gameObject, 11);
                    }
                    
                    NoCoinUI.SetActive(true);
                }


            }
            else
            {
                if (!isbackzero)
                {
                    visual = Instantiate(placedObjectTypeSO.redvisual, Vector3.zero, Quaternion.identity);
                    visual.parent = transform;
                    visual.localPosition = Vector3.zero;
                    visual.localEulerAngles = Vector3.zero;
                    SetLayerRecursive(visual.gameObject, 11);
                }
                CantPutUI.SetActive(true);
            }
            

        }
        else
        {
            CantPutUI.SetActive(false);
            NoCoinUI.SetActive(false);
        }
        
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }
    public bool CanPutTrapVisually(PlacedObjectTypeSO placedObjectTypeSO)
    {
        m_PutTrapTerrain = WeaponController.Instance.GetGrid();
        Vector3 inGridPoint;
        Vector3 mouseHit;//需要把她轉換成網格裡的向量xz就好
        Ray rt = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));//目前螢幕正中射到世界的點
        RaycastHit rtHit;
        if (Physics.Raycast(rt, out rtHit, 999f, hitGround))
        {
            mouseHit = rtHit.point;
            isbackzero = false;

        }
        else
        {
            mouseHit = Vector3.zero + new Vector3(0, -999f, 0);
            isbackzero = true;
        }
        //Physics.Raycast(rt, out rtHit, 999f, hitGround);
        //mouseHit = rtHit.point;
        int index = m_PutTrapTerrain.GetCellIndex(mouseHit);
        inGridPoint = m_PutTrapTerrain.GetCellPosition(index);
        inGridPoint.y = mouseHit.y;
        Vector2Int getV = WeaponController.Instance.VintBack(index);
        List<Vector2Int> gridPostionList = placedObjectTypeSO.GetGridPositionList(getV, dir);
        bool canBuild = true;
        foreach (Vector2Int gridPostion in gridPostionList)
        {   //能把xz帶入確認狀態
            if (m_PutTrapTerrain.GetTrapCellAllState(gridPostion.x, gridPostion.y) != 0)//只有沒東西才能過去
            {
                canBuild = false;
                break;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            Debug.Log("" + dir);

        }
        if (canBuild)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}

