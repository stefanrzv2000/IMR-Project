using System;
using System.Collections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using MediapipeHandTracking;
using UnityEngine.UI;

public class ARFrameProcessor : MonoBehaviour {

    public static float ALPHA = float.NegativeInfinity;
    public static float BETA = float.NegativeInfinity;
    //public ARCameraManager cameraManager;
    //public RawImage source;
    private float imageRatio = float.NegativeInfinity;
    private HandProcessor handProcessor;
    private WebCamTexture webCamTexture;
    RenderTexture rt;
    bool resized = false;

    //private Texture2D image;

    void Start() {
        Debug.Log("SOMETHING STARTED MOVING");
        BETA = (float)Screen.width / Screen.height;
        webCamTexture = new WebCamTexture();
        Debug.Log($"WEBCAMTXT {webCamTexture}");
        webCamTexture.Play();

        handProcessor = new HandProcessor();
        Debug.Log("SOMETHING STARTED MOVING");

    }

    unsafe void convertCPUImage() {

        //XRCameraImage image;
        //if (!cameraManager.TryGetLatestImage(out image)) {
        //    Debug.Log("Cant get image");
        //    return;
        //}

        //var image = (Texture2D)source.texture;
        var image = GetImage();
        //handProcessor.addFrameTexture(image);
        //Debug.Log(image);
        //Debug.Log(image.GetPixel(10, 10));

        //Debug.Log($"Something else {image.width} {image.height}");

        if (float.IsNegativeInfinity(ALPHA))
        {
            ALPHA = (float)image.height / image.width;
        }

        BETA = (float)Screen.width / Screen.height; // -- this is original

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft) imageRatio = -ALPHA * BETA;
        else imageRatio = (float)(BETA / ALPHA);

        //Debug.Log("screen");
        //Debug.Log(Screen.height.ToString() + " x " + Screen.width.ToString());
        /*
         * if landscape: 720 x 1439
         *           or  1080 x 2159
         * else: 1493 x 720
         *    or 2240 x 1080
         * 
         * YES, it ACTUALLY is 1439 and 1493!!
         */


        //Debug.Log("image");
        //Debug.Log(image.height.ToString() + " x " + image.width.ToString());
        /*
         * if landscape: 480 x 640
         * else: 480 x 640
         */

        //Debug.Log("ratio");
        //Debug.Log(ALPHA.ToString() + " - " + BETA.ToString() + " - " + imageRatio.ToString());
        /*
         * 0.75 - 0.48  - 0.64  -- original
         * 0.75 - 2.074 - 2.765 -- swapped beta
         */

        //var conversionParams = new XRCameraImageConversionParams {
        //    // Get the entire image
        //    inputRect = new RectInt(0, 0, image.width, image.height),
        //    // Downsample by 2
        //    outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
        //    // Choose RGBA format
        //    outputFormat = TextureFormat.RGBA32,
        //    // Flip across the vertical axis (mirror image)
        //    transformation = CameraImageTransformation.MirrorY
        //};

        //int size = image.GetConvertedDataSize(conversionParams);

        //var buffer = new NativeArray<byte>(size, Allocator.Temp);
        //image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);
        //image.Dispose();

        //Texture2D m_Texture = new Texture2D(
        //    conversionParams.outputDimensions.x,
        //    conversionParams.outputDimensions.y,
        //    conversionParams.outputFormat,
        //    false);

        //m_Texture.LoadRawTextureData(buffer);
        //m_Texture.Apply();
        //buffer.Dispose();
        //Debug.Log("image");
        //Debug.Log(image);

        

        // pass image for mediapipe
        if (handProcessor != null)
            handProcessor.addFrameTexture(image);
        Destroy(image);
    }

    public void Update()
    {
        //getIMG();
        convertCPUImage();
    }

    public IEnumerator getIMG()
    {
        Debug.Log("WATAFAC");
        yield return new WaitForEndOfFrame();
        convertCPUImage();
        yield return null;
    }

    public IEnumerator process() {
        while (true) {
            Debug.Log("WATAFAC");
            yield return new WaitForEndOfFrame();
            convertCPUImage();
            yield return null;
        }
    }

    Texture2D GetImage()
    {
        var w = webCamTexture.width;
        var h = webCamTexture.height;
        //if (!resized) resized = ((Texture2D)img.texture).Resize(w/2,h/2);

        var pix = webCamTexture.GetPixels();
        //System.Array.Reverse(pix);
        Texture2D tex = new Texture2D(w, h);
        tex.SetPixels(pix);
        tex.Apply();
        //tex.Resize(w / 2, h / 2);
        //tex.Apply();
        tex = Resize(tex, w / 2, h / 2);
        return tex;
    }

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        if (!resized)
        {
            rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            resized = true;
        }

        //var old_rt = RenderTexture.active;
        //Destroy(old_rt);
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        Destroy(texture2D);
        return result;
    }

    public HandProcessor HandProcessor { get => handProcessor;}
    public float ImageRatio { get => imageRatio;}
}