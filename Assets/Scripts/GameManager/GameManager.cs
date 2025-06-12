using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;
    //当前地牢等级
    [SerializeField] private int currentDungeonLevelListIndex = 0;

    private Room currentRoom;
    private Room previousRoom;
    private PlayerDetailsSO playerDetails;
    private Player player;

    [HideInInspector] public GameState gameState;

	protected override void Awake()
	{
		base.Awake();

        playerDetails = GameResources.Instance.currentPlayer.playerDetails;

        InstantiatePlayer();
	}

    private void InstantiatePlayer()
    {
        GameObject playerGameObject = Instantiate(playerDetails.playerPrefab);

        player = playerGameObject.GetComponent<Player>();
		player.Initialize(playerDetails);
    }


	// Start is called before the first frame update
	void Start()
    {
        gameState = GameState.GameStarted;
        HandleGameState();

	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
			PlayDungeonLevel(currentDungeonLevelListIndex);
		}
    }

    private void HandleGameState()
    {
        switch (gameState)
        {
            case GameState.GameStarted:
                PlayDungeonLevel(currentDungeonLevelListIndex);

                gameState = GameState.PlayingLevel;
                break;
        }
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void SetCurrentRoom(Room room)
	{
		previousRoom = currentRoom;
		currentRoom = room;
	}

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        bool dungeonBuiltSuccessfully = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSuccessfully)
        {
            Debug.LogError("Could not build dungeon from specified rooms and node graphs");
        }

        player.gameObject.transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f, (currentRoom.lowerBounds.y +
            currentRoom.upperBounds.y) / 2f, 0f);

        player.gameObject.transform.position = HelperUtilities.GetSpawnPositionNearestToPlayer(player.gameObject.transform.position);
    }

    public Room GetCurrentRoom()
	{
		return currentRoom;
	}

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(dungeonLevelList), dungeonLevelList);
    }
#endif

    #endregion
}
