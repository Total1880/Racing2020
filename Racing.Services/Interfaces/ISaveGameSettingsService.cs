namespace Racing.Services.Interfaces
{
    public interface ISaveGameSettingsService
    {
        bool SaveGameSettings(int playerTeamId);
        int GetPlayerTeamId();
    }
}
