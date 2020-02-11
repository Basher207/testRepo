using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracingMaster : MonoBehaviour {

    public ComputeShader rayTracingShader;

    [HideInInspector] public RenderTexture target;
    [HideInInspector] public Camera camera;

    public Texture skyBox;

    private void Awake() {
        camera = GetComponent<Camera>();
        rayTracingShader.SetTexture(0, "_SkyboxTexture", skyBox);
    }
    public void SetShaderParameteres() {
        rayTracingShader.SetMatrix("_CameraToWorld", camera.cameraToWorldMatrix);
        rayTracingShader.SetMatrix("_CameraInverseProjection", camera.projectionMatrix.inverse);
    }
    public void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Render(destination);
    }
    public void Render(RenderTexture destination) {
        InitRenderTexture();
        SetShaderParameteres();

        rayTracingShader.SetTexture(0, "Result", target);
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        rayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        Graphics.Blit(target, destination);
    }
    private void InitRenderTexture() {
        if (target == null || target.width != Screen.width || target.height != Screen.height) {
            // Release render texture if we already have one
            if (target != null)
                target.Release();
            // Get a render target for Ray Tracing
            target = new RenderTexture(Screen.width, Screen.height, 0,
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            target.enableRandomWrite = true;
            target.Create();
        }

    }
}
