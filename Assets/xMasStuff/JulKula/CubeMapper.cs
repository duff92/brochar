using UnityEngine;
using System.Collections;


public class CubeMapper : MonoBehaviour
{
    /*
	 * Creates a cubemap from a camera and feeds it to a material
	 */


    public int cubeMapRes = 128;
    public bool createMipMaps = false;
	public GameObject go;

    RenderTexture renderTex;



    public RenderTexture GetRenderTexture()
    {

        if (renderTex != null)
        {
            return renderTex;
        }


        gameObject.AddComponent<Camera>();
		gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
   

        renderTex = new RenderTexture(cubeMapRes, cubeMapRes, 16);
        renderTex.hideFlags = HideFlags.HideAndDontSave;
        renderTex.autoGenerateMips = createMipMaps;
        renderTex.isCubemap = true;

        GetComponent<Camera>().RenderToCubemap(renderTex);
        return renderTex;
    }

    public RenderTexture UpdateRenderTexture()
    {
        GetComponent<Camera>().RenderToCubemap(renderTex);

        return renderTex;
    }


}