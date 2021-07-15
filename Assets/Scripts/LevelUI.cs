using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Text taskText;
    
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject blackoutScreen;
    [SerializeField] private GameObject loadingScreen;
    
    [SerializeField] private LevelSpawner ls;
    [SerializeField] private GameSession gs;

    private IFade fadedLoadingScreen, fadedBlackoutScreen;
    
    private void Awake()
    {
        ls.LevelSpawnFinish += ShowTask;
        gs.OnSessionOver += GameOver;
    }

    private void Start()
    {
        fadedLoadingScreen = loadingScreen.GetComponent<IFade>();
        fadedBlackoutScreen = blackoutScreen.GetComponent<IFade>();
        StartSession();
    }

    private void StartSession()
    {
        taskText.GetComponent<IFade>().FadeOut(1, 0.5f);
    }
    
    public void RestartBegin()
    {
        fadedLoadingScreen.FadeIn(0f);
        loadingScreen.SetActive(true);
        fadedLoadingScreen.FadeOut(1f, 0.5f);
        Invoke(nameof(RestartLevel), 0.5f);
    }

    private void RestartLevel()
    {
        taskText.GetComponent<IFade>().FadeIn(0);
        blackoutScreen.SetActive(false);
        restartButton.SetActive(false);
        StartSession();
        gs.StartSession();
        fadedLoadingScreen.FadeIn( 0.5f);
        loadingScreen.SetActive(false);
    }
    
    private void ShowTask(Entity entity)
    {
        taskText.text = "Find " + entity.Name;
    }

    private void GameOver()
    {
        fadedBlackoutScreen.FadeIn(0f);
        blackoutScreen.SetActive(true);
        fadedBlackoutScreen.FadeOut(0.5f, 1f);
        restartButton.SetActive(true);
    }
}
