using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController Instance { get; private set; }
    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;

    private Animator manimater;
    public Transform firePoint;
    [SerializeField]
    private GameObject pfmagicspell;
    [SerializeField]
    private Transform SpellParent;

    public LayerMask hitEnemy;
    //public GameObject useingWp;
    public LayerMask hitGround;
    


    bool changeMainWp;
    private AnimatorStateInfo animStateInfo;
    
    public Transform weapon;


    private PathFindingGrid m_PutTrapTerrain;
    //public Transform t_Gridtrans;
    private PlacedObject placedObject;
    

    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir= PlacedObjectTypeSO.Dir.Down;
    private int HitCount = 0;

    private float force=0;
    private float minforce=2;
    private float maxforce=7;

    public GameObject SkillCharge;
    public GameObject TrapCharge;



    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        m_PutTrapTerrain = null;
    }
    public void Init(PathFindingGrid grid)
    {
        m_PutTrapTerrain = grid;
    }
    void Start()
    {
        manimater = GetComponent<Animator>();
        placedObjectTypeSO = null;
        changeMainWp = true;
        animStateInfo = manimater.GetCurrentAnimatorStateInfo(1);
        
        SkillCharge.SetActive(false);
        TrapCharge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!manimater.GetCurrentAnimatorStateInfo(1).IsName("AtkIde") && manimater.GetCurrentAnimatorStateInfo(1).normalizedTime > 1f)
        {
            // 每次設置完參數之後，都應該在下一幀開始時將參數設置清空，避免連續切換  
            manimater.SetInteger("ActionID", 0);
            HitCount = 0;
        }

        
        //useingWp = GetComponent<SitchWeapon>().GetCurrectWp();

        //if (useingWp.CompareTag("Weapon1"))
        //{
        //    manimater.SetInteger("WpState", 0);
        //    NormalAtkMotin();
        //    SkillAtkMotin();
        //}
        //  else if(useingWp.CompareTag("Trap1"))
        //{
        //    manimater.SetInteger("WpState", 1);
        //    PutTrap(useingWp);
        //}//把這個直接換成另一套
        NumSwitchWpController();
        if (changeMainWp)
        {
            SkillChargeSet();
            manimater.SetInteger("WpState", 0);
            if (manimater.GetCurrentAnimatorStateInfo(1).IsName("AtkIde"))
            {
                weapon.gameObject.SetActive(true);

            }
            
            if (Input.GetMouseButton(0))
            { 
                NormalAtkMotin();
          
            }
            SkillAtkMotin();
            Vatk();
        }
        else
        {
            manimater.SetInteger("WpState", 1);
            if (manimater.GetCurrentAnimatorStateInfo(1).IsName("PutTrap"))
            {
                weapon.gameObject.SetActive(false);


            }
            PutTrapChargeSet();
            PutTrap(placedObjectTypeSO);
        }

    }
    void NumSwitchWpController()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            
            changeMainWp = true;
            DeselectObjectType();
            TrapCharge.SetActive(false);

        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            
            changeMainWp = false;
            placedObjectTypeSO = placedObjectTypeSOList[0];
            RefreshSelectedObjectType();

        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            
            changeMainWp = false;
            placedObjectTypeSO = placedObjectTypeSOList[1];
            RefreshSelectedObjectType();
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            
            changeMainWp = false;
            placedObjectTypeSO = placedObjectTypeSOList[2];
            RefreshSelectedObjectType();
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            
            changeMainWp = false;
            placedObjectTypeSO = placedObjectTypeSOList[3];
            RefreshSelectedObjectType();
        }


    }
    void NormalAtkMotin()
    {
        if (manimater.GetCurrentAnimatorStateInfo(1).IsName("AtkIde") && HitCount == 0 && manimater.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.4f)
        {
            // 在待命狀態下，按下攻擊鍵，進入攻擊1狀態，並記錄連擊數爲1 
            manimater.SetInteger("ActionID", 1);
            HitCount = 1;
        }
        if (manimater.GetCurrentAnimatorStateInfo(1).IsName("Attack01") && HitCount == 1 && manimater.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
        {
            // 在攻擊1狀態下，按下攻擊鍵，記錄連擊數爲2（切換狀態在Update()中）  
            manimater.SetInteger("ActionID", 2);
            HitCount = 2;
        }

        //if (Input.GetMouseButton(0))
        //{
        //    manimater.SetTrigger("NormalAtk");

        //}

    }
    public void NorAtk()
    {

        
        //Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));
        //GameObject goMagicSpell = GameObject.Instantiate(pfmagicspell, firePoint.position, Quaternion.identity,SpellParent);
        Transform CameraTrans = Camera.main.transform;
        
        RaycastHit rh;
        GameObject goMagicSpell = GameObject.Instantiate(pfmagicspell);
        MagicSpell magicspellsp = goMagicSpell.GetComponent<MagicSpell>();
        if (Physics.Raycast(CameraTrans.position, CameraTrans.forward,out rh,hitEnemy))//Physics.Raycast(r, out rh, 1000.0f /*hitEnemy*/)
        {

            //Vector3 vAtkDir = rh.point - firePoint.position;
            //GameObject goMagicSpell = GameObject.Instantiate(pfmagicspell);

            //magicspellsp.target = rh.point;
            //magicspellsp.hit = true;
            magicspellsp.MagicNorAttack(firePoint.position, rh.point);//use

            //goMagicSpell.GetComponent<MagicSpell>().MagicNorAttack(firePoint.position, vAtkDir);
        }
        else
        {
            //magicspellsp.target = CameraTrans.position+CameraTrans.forward * 25.0f;
            //magicspellsp.hit = true;

            //Vector3 vTarget=Camera.main.transform.forward * 1000.0f;

            Vector3 vTarget = CameraTrans.forward * 1000.0f;
            magicspellsp.MagicNorAttack(firePoint.position, vTarget);//use

            //Vector3 vAtkDir = vTarget - firePoint.position;
            //Vector3 sDir = new Vector3(Screen.width / 2, Screen.height / 2);
            //Vector3 vAtDir = sDir - firePoint.position;
            //GameObject goMagicSpell = GameObject.Instantiate(pfmagicspell);

            //goMagicSpell.GetComponent<MagicSpell>().MagicNorAttack(firePoint.position, vAtkDir);
            Debug.Log("hhhhhhhhh");
        }
    }//靠動畫的事件觸發
    void Vatk()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            manimater.SetTrigger("Vatk");
        }
    }
    void SkillAtkMotin()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    manimater.SetTrigger("SkillAtk");

        //}
        if (Input.GetMouseButton(1))
        {
            if (force < maxforce)
            {
                force += Time.deltaTime * 10f;
            }
            else
            {
                force = maxforce;
            }
            manimater.SetFloat("IsForce", force);
        }
        else
        {
            if (force > 0.0f)
            {
                if(force < maxforce)
                {
                    manimater.SetTrigger("SmallSkill");
                    force = 0.0f;
                }
                else
                {
                    manimater.SetTrigger("BigSkill");
                    force = 0.0f;
                }
                

            }
            
            manimater.SetFloat("IsForce", -1);

        }
        

    }
    void SkillChargeSet()
    {
        if (manimater.GetCurrentAnimatorStateInfo(0).IsName("Force"))
        {
            SkillCharge.SetActive(true);
        }
        else
        {
            SkillCharge.SetActive(false);

        }
    }
    void PutTrapChargeSet()
    {
       
        if (manimater.GetCurrentAnimatorStateInfo(1).IsName("PutTrap"))
        {
            TrapCharge.SetActive(true);
        }
        else
        {
            TrapCharge.SetActive(false);

        }

    }
    void PutTrap(PlacedObjectTypeSO placedObjectTypeSO)
    {
        Vector3 inGridPoint;
        Vector3 mouseHit;//需要把她轉換成網格裡的向量xz就好
        Ray rt = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));//目前螢幕正中射到世界的點
        RaycastHit rtHit;
        if(Physics.Raycast(rt,out rtHit,999f, hitGround))
        {
            mouseHit = rtHit.point;

        }
        else
        {
            mouseHit = Vector3.zero;
        }
        int index=m_PutTrapTerrain.GetCellIndex(mouseHit);
        inGridPoint = m_PutTrapTerrain.GetCellPosition(index);
        inGridPoint.y = mouseHit.y;
        if (Input.GetMouseButtonDown(0))
        {
            manimater.SetTrigger("PutTrap");
            //List<Vector2Int> gridPostionList=GetGridPositionList(VintBack(index), Dir.Down);//現在有放的人存進去
            List<Vector2Int> gridPostionList = placedObjectTypeSO.GetGridPositionList(VintBack(index), dir);
            bool canBuild = true;
            foreach(Vector2Int gridPostion in gridPostionList)
            {   //能把xz帶入確認狀態
                if(m_PutTrapTerrain.GetTrapCellAllState(gridPostion.x, gridPostion.y) != 0)//只有沒東西才能過去
                {
                    canBuild = false;
                    break;
                }

            }
            if (canBuild)//CanBuild(index)++CostforBuild
            {
                Vector2Int rotationOffect = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placeObjectWorldPosition = inGridPoint + new Vector3(rotationOffect.x, 0, rotationOffect.y) * m_PutTrapTerrain.CellSize;
                PlacedObject placedObject= PlacedObject.Create(placeObjectWorldPosition, VintBack(index), dir, placedObjectTypeSO);
                //GameObject buildtrapforms=Instantiate(PlacedObjectTypeSO.prefeb, inGridPoint, Quaternion.Euler(0,placedObjectTypeSO.GetRotationAngle(dir), 0)));


                foreach (Vector2Int gridPostion in gridPostionList)//把每一個加進去陷阱的xz都設定1//每有一個陷阱就塗黑
                {
                    m_PutTrapTerrain.SetTrapCellAllState(gridPostion.x, gridPostion.y, 1);
                    SetPlacedObject(placedObject, index);

                    //變成要把每個[xz]都傳過去塗黑
                }
                OnObjectPlaced?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log("Stop");
            }
          
        }
        if (Input.GetMouseButtonDown(1))
        {
                Vector3 preinGridPoint;
                Vector3 premouseHit;//需要把她轉換成網格裡的向量xz就好
                Ray prert = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));//目前螢幕正中射到世界的點
                RaycastHit prertHit;
                if (Physics.Raycast(prert, out prertHit, 999f, hitGround))
                {
                    premouseHit = prertHit.point;

                }
                else
                {
                    premouseHit = Vector3.zero;
                }
                int preindex = m_PutTrapTerrain.GetCellIndex(premouseHit);
                preinGridPoint = m_PutTrapTerrain.GetCellPosition(preindex);
                preinGridPoint.y = premouseHit.y;
                PlacedObject placedObject = GetPlacedObject();
                if (placedObject != null)
                {
                    placedObject.DestroySelf();

                List<Vector2Int> gridPostionList = placedObject. GetGridPositionList();
                
                foreach (Vector2Int gridPostion in gridPostionList)//把每一個加進去陷阱的xz都設定1//每有一個陷阱就塗黑
                    {
                        m_PutTrapTerrain.SetTrapCellAllState(gridPostion.x, gridPostion.y, 0);
                        ClearPlacedObject();//preindex

                        //變成要把每個[xz]都傳過去塗黑
                    }
                }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            Debug.Log(""+dir);

        }
    }
    private void DeselectObjectType()
    {
        placedObjectTypeSO = null; RefreshSelectedObjectType();
    }
    private void RefreshSelectedObjectType()
    {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SetPlacedObject(PlacedObject placedObject, int index)
    {
        this.placedObject = placedObject;
        m_PutTrapTerrain.SetTrapCellState(index,1);
    }
    public void ClearPlacedObject()//int index
    {
        placedObject = null;
        //m_PutTrapTerrain.SetTrapCellState(index, 0);
    }
    public bool CanBuild(int index)
    {
        //return t_Gridtrans == null;
        //return m_PutTrapTerrain.GetTrapCellState(index)==0;
        return placedObject == null;
        
    }
    public Vector2Int VintBack(int index)
    {
        int col = m_PutTrapTerrain.GetColumn(index);
        int row = m_PutTrapTerrain.GetRow(index);
        return new Vector2Int(col,row);
    }
    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }
    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 inGridPoint;
        Vector3 mouseHit;//需要把她轉換成網格裡的向量xz就好
        Ray rt = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));//目前螢幕正中射到世界的點
        RaycastHit rtHit;
        if (Physics.Raycast(rt, out rtHit, 999f, hitGround))
        {
            mouseHit = rtHit.point;

        }
        else
        {
            mouseHit = Vector3.zero;
        }
        int index = m_PutTrapTerrain.GetCellIndex(mouseHit);
        inGridPoint = m_PutTrapTerrain.GetCellPosition(index);
        inGridPoint.y = mouseHit.y;
        Vector3 mousePosition = inGridPoint;
        

        if (placedObjectTypeSO != null)
        {
            Vector2Int rotationOffect = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = inGridPoint + new Vector3(rotationOffect.x, 0, rotationOffect.y) * m_PutTrapTerrain.CellSize;
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (placedObjectTypeSO != null)
        {
            return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }
    public PlacedObjectTypeSO GetPlacedObjectTypeSO()
    {
        return placedObjectTypeSO;
    }
    //public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    //{
    //    List<Vector2Int> gridPositionList = new List<Vector2Int>();
    //    switch (dir)
    //    {
    //        default:
    //        case Dir.Down:
    //        case Dir.Up:
    //            for (int x = 0; x < trap1width; x++)
    //            {
    //                for (int y = 0; y < trap1height; y++)
    //                {
    //                    gridPositionList.Add(offset + new Vector2Int(x, y));
    //                }
    //            }
    //            break;
    //        case Dir.Left:
    //        case Dir.Right:
    //            for (int x = 0; x < trap1height; x++)
    //            {
    //                for (int y = 0; y < trap1width; y++)
    //                {
    //                    gridPositionList.Add(offset + new Vector2Int(x, y));
    //                }
    //            }
    //            break;
    //    }
    //    return gridPositionList;
    //}//幫我把塗黑的地方擴大傳進底層的表
}
