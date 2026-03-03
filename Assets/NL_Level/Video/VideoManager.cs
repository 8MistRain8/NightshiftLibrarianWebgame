using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;

    private VideoTrigger currentTrigger;

    void Awake()
    {
        Instance = this;
    }

    public void RequestPlay(VideoTrigger trigger)
    {
        if (currentTrigger != null && currentTrigger != trigger)
        {
            currentTrigger.StopVideo();
        }

        currentTrigger = trigger;
        currentTrigger.PlayVideo();
    }
}