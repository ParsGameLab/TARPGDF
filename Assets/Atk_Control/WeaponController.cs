using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;
        }
    }

    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
    }
    private Animator manimater;
    public Transform firePoint;
    public GameObject magicspell;
    public LayerMask hitEnemy;
    public GameObject useingWp;
    public LayerMask hitGround;


    private PathFindingGrid m_PutTrapTerrain;
    //public Transform t_Gridtrans;
    private PlacedObject placedObject;
    private int trap1width=4;
    private int trap1height=4;



    // Start is called before the first frame update
    private void Awake()
    {
        m_PutTrapTerrain = null;
    }
    public void Init(PathFindingGrid grid)
    {
        m_PutTrapTerrain = grid;
    }
    void Start()
    {
        manimater = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        useingWp = GetComponent<SitchWeapon>().GetCurrectWp();
        
        if (useingWp.CompareTag("Weapon1"))
        {
            manimater.SetInteger("WpState", 0);
            NormalAtkMotin();
            SkillAtkMotin();
            

        }else if(useingWp.CompareTag("Trap1"))
        {
            manimater.SetInteger("WpState", 1);
            PutTrap(useingWp);
        }
        
    }
    void NormalAtkMotin()
    {

        if (Input.GetMouseButton(0))
        {
            manimater.SetTrigger("NormalAtk");

        }

    }
    public void NorAtk()
    {

        RaycastHit rh;
        Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));
        if (Physics.Raycast(r, out rh, 40.0f, hitEnemy))
        {

            Vector3 vAtkDir = rh.point - firePoint.position;
            GameObject goMagicSpell = GameObject.Instantiate(magicspell);
            goMagicSpell.GetComponent<MagicSpell>().MagicNorAttack(firePoint.position, vAtkDir);
        }
        else
        {

            //Vector3 vTarget=Camera.main.transform.forward * 1000.0f;
            Vector3 vTarget = Camera.main.transform.forward * 1000.0f;
            Vector3 vAtkDir = vTarget - firePoint.position;
            //Vector3 sDir = new Vector3(Screen.width / 2, Screen.height / 2);
            //Vector3 vAtDir = sDir - firePoint.position;
            GameObject goMagicSpell = GameObject.Instantiate(magicspell);
            goMagicSpell.GetComponent<MagicSpell>().MagicNorAttack(firePoint.position, vAtkDir);
            Debug.Log("hhhhhhhhh");
        }
    }//�a�ʵe���ƥ�Ĳ�o
    void SkillAtkMotin()
    {
        if (Input.GetMouseButton(1))
        {
            manimater.SetTrigger("SkillAtk");

        }

    }
    void PutTrap(GameObject gotrap)
    {
        Vector3 inGridPoint;
        Vector3 mouseHit;//�ݭn��o�ഫ������̪��V�qxz�N�n
        Ray rt = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));//�ثe�ù������g��@�ɪ��I
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
            List<Vector2Int> gridPostionList=GetGridPositionList(VintBack(index), Dir.Down);//�{�b���񪺤H�s�i�h
            bool canBuild = true;
            foreach(Vector2Int gridPostion in gridPostionList)
            {   //���xz�a�J�T�{���A
                if(m_PutTrapTerrain.GetTrapCellAllState(gridPostion.x, gridPostion.y) != 0)//�u���S�F��~��L�h
                {
                    canBuild = false;
                    break;
                }

            }
            if (canBuild)//CanBuild(index)
            {
                PlacedObject placedObject= PlacedObject.Create(inGridPoint, VintBack(index), Dir.Down, gotrap);
                //GameObject buildtrapforms=Instantiate(gotrap, inGridPoint, Quaternion.identity);


                foreach(Vector2Int gridPostion in gridPostionList)//��C�@�ӥ[�i�h������xz���]�w1//�C���@�ӳ����N���
                {
                    m_PutTrapTerrain.SetTrapCellAllState(gridPostion.x, gridPostion.y, 1);
                    SetPlacedObject(placedObject, index);

                    //�ܦ��n��C��[xz]���ǹL�h���
                }
                

            }
            else
            {
                Debug.Log("Stop");
            }
            


        }
       if (Input.GetMouseButtonDown(1))
            {
                Vector3 preinGridPoint;
                Vector3 premouseHit;//�ݭn��o�ഫ������̪��V�qxz�N�n
                Ray prert = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1.0f));//�ثe�ù������g��@�ɪ��I
                RaycastHit prertHit;
                if (Physics.Raycast(prert, out prertHit, 999f, hitGround))
                {
                    premouseHit = prertHit.point;

                }
                else
                {
                    premouseHit = Vector3.zero;
                }
                int preindex = m_PutTrapTerrain.GetCellIndex(mouseHit);
                preinGridPoint = m_PutTrapTerrain.GetCellPosition(preindex);
                preinGridPoint.y = premouseHit.y;
                PlacedObject placedObject = GetPlacedObject();
                if (placedObject != null)
                {
                    placedObject.DestroySelf();

                    List<Vector2Int> gridPostionList = placedObject. GetGridPositionList();
                    foreach (Vector2Int gridPostion in gridPostionList)//��C�@�ӥ[�i�h������xz���]�w1//�C���@�ӳ����N���
                    {
                        m_PutTrapTerrain.SetTrapCellAllState(gridPostion.x, gridPostion.y, 0);
                        ClearPlacedObject(preindex);

                        //�ܦ��n��C��[xz]���ǹL�h���
                    }
                }

       }
    }
    public void SetPlacedObject(PlacedObject placedObject, int index)
    {
        this.placedObject = placedObject;
        m_PutTrapTerrain.SetTrapCellState(index,1);
    }
    public void ClearPlacedObject(int index)
    {
        placedObject = null;
        m_PutTrapTerrain.SetTrapCellState(index, 0);
    }
    public bool CanBuild(int index)
    {
        //return t_Gridtrans == null;
        return m_PutTrapTerrain.GetTrapCellState(index)==0;
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
    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < trap1width; x++)
                {
                    for (int y = 0; y < trap1height; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < trap1height; x++)
                {
                    for (int y = 0; y < trap1width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
        }
        return gridPositionList;
    }//���ڧ��ª��a���X�j�Ƕi���h����
}
