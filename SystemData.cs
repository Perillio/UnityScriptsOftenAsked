using UnityEngine;
using UnityEngine.Profiling;

//Gives info about the System and the FPS of the program. Can be used for anything. Also gets a unique system identifier from system.

public class SystemData : MonoBehaviour
{
    [Header("CPU: ")]
    public string processorName = "";
    public string processorCores = "";
    public string processorFrequency = "";
    [Header("GPU: ")]
    public string gpu = "";
    public string gpuUsing = "";
    public string gpuMemory = "";
    public string maxTextureSize = "";
    public int framesPerSecond = 0;
    public string deviceID = "";
    [Header("Systemdata: ")]
    public string systemOS = "";
    public string systemUUID = "";
    public string deviceModel = "";
    public string deviceType = "";
    public string ram = "";
    public string batteryStatus = "";
    public string batteryLevel = "";
    public string systemDate = "";

    private float timer = 1f;
    private int fpsCount = 0;

    private void Start()
    {
        ram = SystemInfo.systemMemorySize.ToString() + " MB";
        gpu = SystemInfo.graphicsDeviceName;
        gpuUsing = SystemInfo.graphicsDeviceVersion;
        systemOS = SystemInfo.operatingSystem.ToString();
        systemUUID = SystemInfo.deviceUniqueIdentifier.ToString();
        deviceModel = SystemInfo.deviceModel;
        processorCores = SystemInfo.processorCount.ToString();
        processorName = SystemInfo.processorType.ToString();
        maxTextureSize = SystemInfo.maxTextureSize.ToString() + " x "+ SystemInfo.maxTextureSize.ToString();
        deviceType = SystemInfo.deviceType.ToString();
        deviceID = SystemInfo.graphicsDeviceID.ToString();
    }
    void Update()
    {
        fpsCount++;
        FPSCounter();
        processorFrequency = SystemInfo.processorFrequency.ToString() + " MhZ";
        batteryStatus = SystemInfo.batteryStatus.ToString();
        batteryLevel = SystemInfo.batteryLevel.ToString();
        gpuMemory = SystemInfo.graphicsMemorySize.ToString();
        systemDate = System.DateTime.Now.ToString();
    }
    void FPSCounter()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            framesPerSecond = fpsCount;
            fpsCount = 0;
            timer = 1f;
        }
    }
}
