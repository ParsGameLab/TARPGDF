using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private Transform visual;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private ITrapCustomer trapBuyer;
    public GameObject NoCoinUI;
    private bool CanBuy;

    private void Start() {
        trapBuyer = GameObject.FindWithTag("Player").GetComponent<ITrapCustomer>();

        RefreshVisual();
        //CanCoinBuyTrap();GetComponent<ITrapCustomer>();

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
        
        //RefreshVisual();//?問一下怎摸省
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
                NoCoinUI.SetActive(true);
            }

        }
        else
        {
            NoCoinUI.SetActive(false);
        }
        
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

}

