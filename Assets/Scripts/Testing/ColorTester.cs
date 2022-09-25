using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2f, layerMask))
        {
            // https://forum.unity.com/threads/trying-to-get-color-of-a-pixel-on-texture-with-raycasting.608431/
            Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
            //Texture2D texture2D = renderer.material.mainTexture as Texture2D;
            Texture2D texture2D = renderer.sharedMaterial.GetTexture("Texture2D_41271c3c5f484ca2a435c65087a81705") as Texture2D;
            Vector2 pCoord = hit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;
            
            Vector2 tiling = renderer.material.mainTextureScale;
            Color color = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));
            Debug.Log("Hit color: " + color);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 2f, color);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 2f, Color.red);
            Debug.Log("Did not Hit");
        }
    }
}
