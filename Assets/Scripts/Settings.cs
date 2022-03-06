// Murat Sancak

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.SystemInfo;

public class Settings:MonoBehaviour
{
    [SerializeField]
    private Button
        pB, // pB: Pitch Button.
        polyB, // Poly Button.
        vB; // vB: Volume Button.

    [SerializeField]
    private Image polyH; // polyH: Poly Handle.

    [SerializeField]
    private RectTransform[] rTs = new RectTransform[3]; // rTs: Rect Transforms.

    [SerializeField]
    private Slider
        pS, // pS: Pitch Slider.
        polyS, // polyS: Poly Slider.
        vS; // vS: Volume Slider.

    [SerializeField]
    private Sprite
        g, // g: Gold.
        s; // s: Silver.

    [SerializeField]
    private TextMeshProUGUI
        pTMPUGUI, // pTMPUGUI: Pitch Text (TMP).
        polyTMPUGUI, // polyTMPUGUI: Poly Text (TMP).
        vTMPUGUI; // vTMPUGUI: Volume Text (TMP).

    private int f; // f: For.

    private readonly Vector2 b = Menu.b; // b: Banner.

    private readonly Vector2[]
        v2s0 = new Vector2[3], // v2s0: Vector2's 0.
        v2s1 = new Vector2[3]; // v2s1: Vector2's 1.

    private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

    // Murat Sancak

    private Vector2[] V2s => !Monetization.IBL||IAP.HR(0) ? v2s0 : v2s1; // V2s: Vector2's.

    // Murat Sancak

    private void Awake()
    {
        for(f=0;f<rTs.Length;f++)
        {
            v2s0[f]=b+rTs[f].anchoredPosition;
            v2s1[f]=rTs[f].anchoredPosition;
        }

        Reload();
    }

    private void Start() => StartCoroutine(Enumerator());

    // Murat Sancak

    private System.Collections.IEnumerator Enumerator()
    {
        while(true)
        {
            for(f=0;f<rTs.Length;f++)
                rTs[f].anchoredPosition=V2s[f];

            yield return wFS;
        }
    }

    // Murat Sancak

    public void Load(int s) => Scene.Load(s); // s: Scene.

    public void Mail() => Application.OpenURL
        (
            System.String.Concat
            (
                "mailto:murasanca@pm.me?subject=Dice&body=%0A%0A%0A%0A%0AStatic Properties%0A%0A%0ABattery Level: ",batteryLevel,
                "%0ABattery Status: ",batteryStatus,

                "%0A%0AConstant Buffer Offset Alignment: ",constantBufferOffsetAlignment,

                "%0A%0ACopy Texture Support: ",copyTextureSupport,

                "%0A%0ADevice Model: ",deviceModel,
                "%0ADevice Name: ",deviceName,
                "%0ADevice Type: ",deviceType,
                "%0ADevice Unique Identifier: ",deviceUniqueIdentifier,

                "%0A%0AGraphics Device ID: ",graphicsDeviceID,
                "%0AGraphics Device Name: ",graphicsDeviceName,
                "%0AGraphics Device Type: ",graphicsDeviceType,
                "%0AGraphics Device Vendor: ",graphicsDeviceVendor,
                "%0AGraphics Device Vendor ID: ",graphicsDeviceVendorID,
                "%0AGraphics Device Version: ",graphicsDeviceVersion,
                "%0AGraphics Memory Size: ",graphicsMemorySize,
                "%0AGraphics Multi Threaded: ",graphicsMultiThreaded,
                "%0AGraphics Shader Level: ",graphicsShaderLevel,
                "%0AGraphics UV Starts At Top: ",graphicsUVStartsAtTop,

                "%0A%0AHas Dynamic Uniform Array Indexing In Fragment Shaders: ",hasDynamicUniformArrayIndexingInFragmentShaders,
                "%0AHas Hidden Surface Removal On GPU: ",hasHiddenSurfaceRemovalOnGPU,
                "%0AHas Mip Max Level: ",hasMipMaxLevel,

                "%0A%0AHDR Display Support Flags: ",hdrDisplaySupportFlags,

                "%0A%0AMax Compute Buffer Inputs Compute: ",maxComputeBufferInputsCompute,
                "%0AMax Compute Buffer Inputs Domain: ",maxComputeBufferInputsDomain,
                "%0AMax Compute Buffer Inputs Fragment: ",maxComputeBufferInputsFragment,
                "%0AMax Compute Buffer Inputs Geometry: ",maxComputeBufferInputsGeometry,
                "%0AMax Compute Buffer Inputs Hull: ",maxComputeBufferInputsHull,
                "%0AMax Compute Buffer Inputs Vertex: ",maxComputeBufferInputsVertex,
                "%0AMax Compute Work Group Size: ",maxComputeWorkGroupSize,
                "%0AMax Compute Work Group Size X: ",maxComputeWorkGroupSizeX,
                "%0AMax Compute Work Group Size Y: ",maxComputeWorkGroupSizeY,
                "%0AMax Compute Work Group Size Z: ",maxComputeWorkGroupSizeZ,
                "%0AMax Cubemap Size: ",maxCubemapSize,
                "%0AMax Texture Size: ",maxTextureSize,

                "%0A%0ANPOT Support: ",npotSupport,

                "%0A%0AOperating System: ",operatingSystem,
                "%0AOperating System Family: ",operatingSystemFamily,

                "%0A%0AProcessor Count: ",processorCount,
                "%0AProcessor Frequency: ",processorFrequency,
                "%0AProcessor Type: ",processorType,

                "%0A%0ARendering Threading Mode: ",renderingThreadingMode,

                "%0A%0ASupported Random Write Target Count: ",supportedRandomWriteTargetCount,
                "%0ASupported Render Target Count: ",supportedRenderTargetCount,

                "%0A%0ASupports 2D Array Textures: ",supports2DArrayTextures,
                "%0ASupports 32 Bits Index Buffer: ",supports32bitsIndexBuffer,
                "%0ASupports 3D Render Textures: ",supports3DRenderTextures,
                "%0ASupports 3D Textures: ",supports3DTextures,
                "%0ASupports Accelerometer: ",supportsAccelerometer,
                "%0ASupports Async Compute: ",supportsAsyncCompute,
                "%0ASupports Async GPU Readback: ",supportsAsyncGPUReadback,
                "%0ASupports Audio: ",supportsAudio,
                "%0ASupports Compressed 3D Textures: ",supportsCompressed3DTextures,
                "%0ASupports Compute Shaders: ",supportsComputeShaders,
                "%0ASupports Conservative Raster: ",supportsConservativeRaster,
                "%0ASupports Cubemap Array Textures: ",supportsCubemapArrayTextures,
                "%0ASupports Geometry Shaders: ",supportsGeometryShaders,
                "%0ASupports GPU Recorder: ",supportsGpuRecorder,
                "%0ASupports Graphics Fence: ",supportsGraphicsFence,
                "%0ASupports Gyroscope: ",supportsGyroscope,
                "%0ASupports Hardware Quad Topology: ",supportsHardwareQuadTopology,
                "%0ASupports Instancing: ",supportsInstancing,
                "%0ASupports Location Service: ",supportsLocationService,
                "%0ASupports Mip Streaming: ",supportsMipStreaming,
                "%0ASupports Motion Vectors: ",supportsMotionVectors,
                "%0ASupports Multisample Auto Resolve: ",supportsMultisampleAutoResolve,
                "%0ASupports Multisampled 2D Array Textures: ",supportsMultisampled2DArrayTextures,
                "%0ASupports Multisampled Textures: ",supportsMultisampledTextures,
                "%0ASupports Multiview: ",supportsMultiview,
                "%0ASupports Raw Shadow Depth Sampling: ",supportsRawShadowDepthSampling,
                "%0ASupports Ray Tracing: ",supportsRayTracing,
                "%0ASupports Render Target Array Index From Vertex Shader: ",supportsRenderTargetArrayIndexFromVertexShader,
                "%0ASupports Separated Render Targets Blend: ",supportsSeparatedRenderTargetsBlend,
                "%0ASupports Set Constant Buffer: ",supportsSetConstantBuffer,
                "%0ASupports Shadows: ",supportsShadows,
                "%0ASupports Sparse Textures: ",supportsSparseTextures,
                "%0ASupports Store And Resolve Action: ",supportsStoreAndResolveAction,
                "%0ASupports Tessellation Shaders: ",supportsTessellationShaders,
                "%0ASupports Texture Wrap Mirror Once: ",supportsTextureWrapMirrorOnce,
                "%0ASupports Vibration: ",supportsVibration,

                "%0A%0ASystem Memory Size: ",systemMemorySize,

                "%0A%0AUnsupported Identifier: ",unsupportedIdentifier,

                "%0A%0AUses Load Store Actions: ",usesLoadStoreActions,
                "%0AUses Reversed Z Buffer: ",usesReversedZBuffer,

                "%0A%0A%0A"
            )
        );

    public void PB() => pS.value=1; // PB: Pitch Button.

    public void PV() // PV: Pitch Value.
    {
        Preferences.P=Sound.P=pS.value;

        pB.interactable=Preferences.P is not 1;
        pTMPUGUI.text=Preferences.P.ToString("F2");
    }

    public void PolyB() => polyS.value=1; // B: Button.

    public void PolyV() // V: Value.
    {
        Preferences.Poly=(int)polyS.value;

        polyB.interactable=Preferences.Poly is 0;
        polyH.sprite=Preferences.Poly is 1 ? g : s;
        polyTMPUGUI.text=Preferences.Poly.ToString();
    }

    public void Reload()
    {
        PB();
        PolyB();
        VB();
    }

    public void VB() => vS.value=.64f; // VB: Volume Button.

    public void VV() // VV: Volume Value.
    {
        Preferences.V=Sound.V=vS.value;

        vB.interactable=Preferences.V is not .64f;
        vTMPUGUI.text=Preferences.V.ToString("F2");
    }
}

// Murat Sancak