using UnityEngine;

public class DialogueData : SingletonnPersistent<DialogueData>
{

    private const string currDialogueIndex = "CURR_DIALOGUE_INDEX";
    
    private const string savedDialogueIndex = "SAVED_DIALOGUE_INDEX";
    
    private int initialDialogueIndex = 0;

    public override void Awake()
    {
        base.Awake();
        
        PlayerPrefs.SetInt(currDialogueIndex, PlayerPrefs.GetInt(currDialogueIndex, initialDialogueIndex));
        PlayerPrefs.SetInt(savedDialogueIndex, PlayerPrefs.GetInt(savedDialogueIndex, initialDialogueIndex));
    }

    private void OnDestroy()
    {
        ResetData();
    }

    public int GetCurrDialogueIndex()
    {
        return PlayerPrefs.GetInt(currDialogueIndex, initialDialogueIndex);
    }
    
    public void SetCurrDialogueIndex(int currIndex)
    {
        PlayerPrefs.SetInt(currDialogueIndex, currIndex);
    }
    
    public int GetSavedDialogueIndex()
    {
        return PlayerPrefs.GetInt(savedDialogueIndex, initialDialogueIndex);
    }
    
    public void SetSavedDialogueIndex(int savedIndex)
    {
        PlayerPrefs.SetInt(savedDialogueIndex, savedIndex);
    }

    public void ResetData()
    {
        SetCurrDialogueIndex(0);
    }


}
