using UnityEngine;

public class PlayerAwerenessController : MonoBehaviour
{
    public boolean AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance { get; private set; }

    private Transform _player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ennemyToplayerVector = _player.position - transform.position;
        DirectionToPlayer = ennemyToplayerVector.normalized;
        if (ennemyToplayerVector.magnitude < _playerAwarenessDistance)
        {
            AwareOfPlayer = true;


        }
        else
        {
            AwareOfPlayer = false;
        }

    }
}
