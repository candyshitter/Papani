using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public static string LevelToLoad;
    
    [SerializeField] private string _levelName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => LevelToLoad = _levelName);
    }
}
