using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Jobs;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using UnityEngine.UI;



public class TextureLoader : MonoBehaviour
{
    #region Singleton
    public static TextureLoader instance;
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] public RenderTexture fpSrc;
    [SerializeField] public RenderTexture tpSrc;

    [SerializeField] public RawImage taskPageDst;
    [SerializeField] public RawImage monitorPageDst;

    [SerializeField] private byte[] frameData;

    [SerializeField] private RawImage streamTarget;

    private int _width;
    private int _height;

    private Dictionary<int, byte[]> streamFrames;
    private Dictionary<int, int> streamChunksCount;
    private Dictionary<int, float> framesLifetime;

    public float frameTimeOut = 1f;

    private void Start()
    {
        // StartCoroutine(UpdateRawImage());
        streamFrames = new Dictionary<int, byte[]>();
        streamChunksCount = new Dictionary<int, int>();
        framesLifetime = new Dictionary<int, float>();

        // GetComponent<NetworkMessageHandler>().streamUpdated += UpdateRender;
        EventManager.Instance.StreamUpdated += UpdateRender;
    }

    private void OnDestroy()
    {
        // TransmissionManager.instance.streamUpdated -= UpdateRender;
        EventManager.Instance.StreamUpdated -= UpdateRender;
    }

    /// <summary>
    /// 
    /// </summary>
    void UpdateRender(StreamDataHeader header, byte[] chunk)
    {
        if (!streamFrames.ContainsKey(header.id))
        {
            byte[] frame = new byte[header.totalSize];
            streamFrames.Add(header.id, frame);
            streamChunksCount.Add(header.id, 0);

            framesLifetime[header.id] = 0f;
        }

        try
        {
            Buffer.BlockCopy(chunk, 0, streamFrames[header.id], header.offset, header.size);
            streamChunksCount[header.id] += 1;
        }
        catch (Exception e)
        {
            //  Block of code to handle errors
        }

        // This frame is completed
        if (streamChunksCount[header.id] == header.totalCount)
        {
            Texture2D receivedTex = new Texture2D(header.width, header.height);
            receivedTex.LoadImage(streamFrames[header.id]);

            streamTarget.texture = receivedTex;

            streamFrames.Remove(header.id);
            streamChunksCount.Remove(header.id);
        }

        framesLifetime[header.id] += Time.deltaTime;
        if (framesLifetime[header.id] >= frameTimeOut)
        {
            streamFrames.Remove(header.id);
            streamChunksCount.Remove(header.id);
            framesLifetime.Remove(header.id);
        }


        // Texture2D receivedTex = new Texture2D(header.width, header.height);
        // receivedTex.LoadImage(chunk);
        // 
        // streamTarget.texture = receivedTex;

        // Debug.Log($"chunk {metaData.index}, data length = {chunk.Length}, offset = {metaData.offset}");
        // Buffer.BlockCopy(chunk, 0, frameData, metaData.offset, chunk.Length);

    }

}
