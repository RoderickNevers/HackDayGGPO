using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private const float GROUND_DISTANCE = 0.2f;
    private const float GRAVITY = -9.81f;

    //[SerializeField] private ReplayManager _ReplayManager;
    [SerializeField] private int _PlayerSpeed = 20;
    [SerializeField] private int _JumpHeight = 8;
    [SerializeField] private LayerMask _Ground;

    private int PlayerIndex;
    private CharacterController _Controller;
    private Transform _GroundChecker;
    private Vector3 _MoveDirection;
    private Vector3 _JumpVelocity;
    private bool _IsReplaying;
    private bool _GroundedPlayer;

    public ReplayManager ReplayManager { get; set; }
    public GGPOGameState GameState { get; set; }

    public Vector3 MoveDirection 
    {
        get => _MoveDirection;
        set => _MoveDirection = value;
    }

    public bool Jump { get; set; }

    void Start()
    {
        _Controller = GetComponent<CharacterController>();
        _GroundChecker = transform.GetChild(0);
    }

    public void Setup(int index)
    {
        PlayerIndex = index;
        ReplayManager.OnStartedReplaying += () => { _IsReplaying = true; };
        ReplayManager.OnStoppedReplaying += () => { _IsReplaying = false; };
    }

    // Physics
    private void FixedUpdate()
    {
        //CharacterPhysics();
    }

    // Gameplay
    void Update()
    {
        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition()
    {
        Player player = GameState.GetPlayer(PlayerIndex);
        transform.position = player.position;
    }

    private void CharacterPhysics()
    {
        //_GroundedPlayer = Physics.CheckSphere(_GroundChecker.position, GROUND_DISTANCE, _Ground, QueryTriggerInteraction.Ignore);

        //// Horizontal
        //if (MoveDirection != Vector3.zero)
        //{
        //    gameObject.transform.forward = _MoveDirection;
        //}

        //_Controller.Move(_MoveDirection * Time.deltaTime * _PlayerSpeed);

        //// Vertical
        //if (_GroundedPlayer && _MoveDirection.y < 0)
        //{
        //    _MoveDirection.y = 0f;
        //}

        ////if (Jump)
        ////{
        ////    _JumpVelocity.y = _JumpHeight;
        ////}

        //_JumpVelocity.y += GRAVITY * Time.deltaTime;
        //_Controller.Move(_JumpVelocity * Time.deltaTime);
    }
}