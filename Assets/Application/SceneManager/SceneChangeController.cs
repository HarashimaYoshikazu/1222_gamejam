using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    private static SceneChangeController Instance = default;
    private static bool loadNow = false;
    [SerializeField] bool _isFadeIn = true;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        if(_isFadeIn)
        {
            FadeController.StartFadeIn();
        }
    }

    /// <summary>
    /// �w��V�[���Ɉڍs����
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        if (loadNow)
        {
            return;
        }
        loadNow = true;
        FadeController.StartFadeOut(() => Load(sceneName));
    }  
    private static void Load(string sceneName)
    {
        loadNow = false;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// �A�v���P�[�V�������I��������
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
