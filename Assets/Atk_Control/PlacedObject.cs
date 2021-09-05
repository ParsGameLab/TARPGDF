using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    private GameObject gotrap;
    private Vector2Int origin;
    private WeaponController.Dir dir;
    private WeaponController wp;
    private GameObject m_WeaponController;
    // Start is called before the first frame update
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, WeaponController.Dir dir, GameObject gotrap)
    {
        Transform placedObjectTransform = Instantiate(gotrap.transform, worldPosition, Quaternion.identity);

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
        placedObject.Setup(gotrap, origin, dir);

        return placedObject;
    }
    private void Setup(GameObject gotrap, Vector2Int origin, WeaponController.Dir dir)
    {
        this.gotrap = gotrap;
        this.origin = origin;
        this.dir = dir;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public List<Vector2Int> GetGridPositionList()
    {
        m_WeaponController = GameObject.Find("SdUnitychan");
        wp = m_WeaponController.GetComponent(typeof(WeaponController)) as WeaponController;
        return wp.GetGridPositionList(origin, dir);
    }





}
