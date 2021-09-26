using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour, EntityInterface
{
    public static int CurrentID;
    public int ID;
    public WayPath Path;
    public float Radius = 0.5f;
    public float HardRadius = 0.5f;
    public float HardRadiusRatio = 0.5f;
    public Transform HardTransform;
    public List<Boid> Neighbors;
    //public Team Team;
    private Vector3 m_position;
    public Vector3 Position
    {
        get { return m_position; }
        set
        {
            m_position = value;
            transform.position = new Vector3(value.x, value.y, value.z);
        }
    }
    public Vector2 NextPosition
    {
        get { return new Vector2(m_position.x, m_position.z) + Direction * Speed * Time.fixedDeltaTime; }
    }
    public Vector2 VeryNextPosition
    {
        get { return new Vector2(m_position.x, m_position.z) + Direction * Speed; }
    }
    public Vector2 Forward;
    private Vector2 m_direction;
    public Vector2 Direction
    {
        get { return m_direction; }
        set
        {
            m_direction = value;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(m_direction.x, 0, m_direction.y)), 2 * Time.fixedDeltaTime);//拿到總合力去移動或是算下個位子
        }
    }
    public float Speed = 2f;

    void Start()
    {
        ID = CurrentID++;
        HardRadius = Radius * HardRadiusRatio;
        float scale = Radius / 0.5f;
        transform.eulerAngles = scale * Vector3.one;
        //Forward = new Vector3(Random.Range(-1, 1),0, Random.Range(-1, 1)).normalized;
        Forward = gameObject.transform.forward;
        Direction = Forward;
    }

    private int stuckFrames = 0;

    // Update is called once per frame
    void Update()
    {
        
    }
}
