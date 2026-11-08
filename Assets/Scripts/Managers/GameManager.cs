using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent gameStarted;
    
    public static GameManager Instance;

    [SerializeField] GameObject m_player;

    private int pausers;
    public enum GameState
    {
        waiting,
        started,
        interacting
    }

    private GameState gameState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            pausers = 0;
        }
    }

    public void StartGame()
    {
        gameStarted.Invoke();
        gameState = GameState.started;
    }
    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }
    public GameObject GetPlayer()
    {
        return m_player;
    }
    public void RequestPause()
    {
        if(pausers >= 0)
        {
            m_player.GetComponent<BoatMover>().EnableControlls(false);
        }
        pausers++;
    }
    public void RequestUnpause()
    {
        pausers--;
        if(pausers <= 0)
        {
            pausers = 0;
            m_player.GetComponent<BoatMover>().EnableControlls(true);
        }
    }
}
