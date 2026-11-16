using UnityEngine;

public class Capsule : MonoBehaviour
{
    [Header("* * * RayCast Settings * * *")]

    [SerializeField] private float _raycastDistance;    // 判定の距離設定

    [SerializeField] private LayerMask _layerMask;      // 判定のレイヤー設定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 一番最初に接触した物のデータを取得する
        bool isHit = Physics.Raycast(
            origin: transform.position,      // 光線の始点
            direction: Vector3.down,         // 光線の方向
            hitInfo: out RaycastHit hit,     // 衝突したオブジェクトの情報
            maxDistance: _raycastDistance,   // 光線の長さ(距離)
            layerMask: _layerMask            // 衝突レイヤー
            );

        // 光線の可視化
        Debug.DrawRay(transform.position,
            Vector3.down * _raycastDistance,
            Color.red);

        if(isHit)
        {
            // 光線に反応がある場合
            Debug.Log($"衝突情報 ---- " +
                $"\nNome = { hit.transform.name }"+
                $"\nPoint(衝突位置) = { hit.point }" + 
                $"\nNormal(法線) = {hit.normal}" + 
                $"\nDistance(距離) = {hit.distance}"
                );

            // 法線を可視化
            Debug.DrawRay(hit.point,hit.normal, Color.yellow);
        }

        // === ひたすら前進
        Vector3 velocty = transform.position;   // カプセルの進行方向

        // "進行方向"と"法線"の外積 = 進行方向に右向きのベクトル
        Vector3 right = Vector3.Cross(velocty, hit.normal);

        // "右向きのベクトル"と"法線"の外積 = スロープのベクトル
        //Vector3 slope = Vector3.Cross(right, hit.normal);

        // 直接坂道のベクトルを取得する
        Vector3 slope = Vector3.ProjectOnPlane(velocty,hit.normal);

        // 坂道のベクトルを可視化
        Debug.DrawRay(hit.point,slope.normalized,Color.green);

        transform.position += (slope.normalized * -1) * 30.0f * Time.deltaTime;

        
    }



}
