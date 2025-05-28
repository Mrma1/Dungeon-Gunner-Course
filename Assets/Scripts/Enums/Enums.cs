public enum Orientation
{
    North,
    East,
    South,
    West,
    None
}

public enum GameState
{
    GameStarted,
    PlayingLevel,
    EngagingEnemies,    //吸引敌人
    BossStage,
    EngagingBoss,
    LevelCompleted,
    GameWon,
    GameLost,
    GamePaused,
    DungeonOverviewMap,
    RestartGame
}