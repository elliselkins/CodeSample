public enum DataEventType { updateGameData, loadGameData, loadedGameData, deleteBoardRecords, startedBoardsChanged, loadUserProfile, userProfilesLoaded, activeUserProfileSet, resetProfile };

public class DataEvent : EventBase
{
    public DataEventType eventType;

    public string boardId;

    public GameData gameData;

    public int profileIdx;

    public bool changeBoard;
}
