using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearScanner : MonoBehaviour
{
    public float ScanRange;
    public LayerMask TargetLayer;
    public RaycastHit2D[] Targets;
    public Transform NearsTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // 시작점, 원 반지름, 탐색 방향, 탐색 거리, 탐색 대상 레이어 
        Targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, TargetLayer);
        NearsTarget = GetNearest();
    }

    private Transform GetNearest()
    {
        Transform ret = null;
        float dif = ScanRange + 100;
        Vector3 myPos = transform.position;

        foreach (RaycastHit2D target in Targets)
        {
            Vector3 targetPos = target.transform.position;
            float curDif = Vector3.Distance(myPos, targetPos);
            if (curDif < dif)
            {
                dif = curDif;
                ret = target.transform;
            }
        }
        return ret;
    }
}
