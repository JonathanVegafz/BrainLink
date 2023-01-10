using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CamaraVTX : MonoBehaviour
{
    [SerializeField] float param = 0.5f;

    [SerializeField] TextAsset imageAsset;

    Texture2D tex2D;

    Material material;
    Camera cam;

    [SerializeField]
    Vector4 offset = Vector4.zero;

    float lit;

    private void Awake()
    {
        material = new Material(Shader.Find("Jona/Iris"));
        tex2D = new Texture2D(2, 2);
        tex2D.LoadImage(imageAsset.bytes);

        tex2D.wrapMode = TextureWrapMode.Clamp;

        material.SetTexture("_tex", tex2D);
        cam = GetComponent<Camera>();   
        DOTween.Init();
        //DOTween.To(() => param, (x) => param = x, -5.1f, 1f).Delay();
        DOTween.To(() => param, (x) => param = x, 0, 1f);
    }


    public void CambioEscena()
    {
        DOTween.To(() => param, (x) => param = x, -5.1f, 1f).OnComplete(()=>SceneManager.LoadScene(2));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        lit = param < -5f ? 0 : 1;
        material.SetFloat("_lit", lit);

        material.SetFloat("_aspect", cam.aspect);

        material.SetVector("_offset", offset * param * param);
        material.SetFloat("_param", param * param);
        Graphics.Blit(source, destination, material);
    }
}
