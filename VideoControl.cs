using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoControlScript : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Get the Video Player
    public GameObject panel1;
    public GameObject panel2;
    public Button playPauseButton; // bound with the controller button
    private bool isPlaying = false; // test if the video is playing

    void Start()
    {
        // listen the click event
        playPauseButton.onClick.AddListener(TogglePlayPause);
    }

    void TogglePlayPause()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play(); // play the video
        }
    }
}
