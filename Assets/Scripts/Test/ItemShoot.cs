using ItemRelated;
using UnityEngine;

public class ItemShoot : ItemActivator
{    
    public override void Use()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        if(Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))    
        {
            //spawn a cube at hit.point
            Transform hitCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            hitCube.localScale = Vector3.one * 0.1f;
            hitCube.position = hit.point;
            Debug.Log(hit.collider.name);
        }
    }
}
