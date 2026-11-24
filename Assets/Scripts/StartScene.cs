using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnPlayButton()
    {
        // Load game scene
        SceneManager.LoadScene("GameScene");
    }

    public void OnMuteButton()
    {
        // Mute only the background music, not sound effects
        SoundManager.Instance?.ToggleMusic();
    }
}
