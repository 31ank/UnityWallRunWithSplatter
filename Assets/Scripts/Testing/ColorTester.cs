using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTester : MonoBehaviour
{
    public RenderTexture renderTexture;

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
            Color colorOrg = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));

            Texture2D tex = toTexture2D(renderTexture);

            Color color = tex.GetPixel(64, 64);
            Debug.Log("Hit color: " + color + " is same: " + isSameColor(color, colorOrg) + " " + colorOrg);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 2f, color);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 2f, Color.red);
            Debug.Log("Did not Hit");
        }
    }

    private bool isSameColor(Color color, Color color2)
    {
        if (Math.Abs(color.r - color2.r) > 0.1f || Math.Abs(color.b - color2.b) > 0.1f || Math.Abs(color.g - color2.g) > 0.1f)
            return false;
        return true;
    }

    /// <summary>
    /// Source https://stackoverflow.com/questions/44264468/convert-rendertexture-to-texture2d
    /// </summary>
    /// <param name="rTex"></param>
    /// <returns></returns>
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
