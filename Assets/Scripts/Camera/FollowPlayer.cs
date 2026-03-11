using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private readonly float xMax = 24.2f, xMin = -23.9f, yMin = -41.3f, yMax = 29.3f;
    [SerializeField] Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        Vector3 playerP = player.position;
        playerP.z = transform.position.z;
        if(playerP.x>xMax) playerP.x = xMax;
        if(playerP.x<xMin) playerP.x = xMin;
        if(playerP.y>yMax) playerP.y = yMax;
        if(playerP.y<yMin) playerP.y = yMin;
        transform.position = playerP;
        
    }
}
