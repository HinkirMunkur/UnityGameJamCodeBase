using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private Clickable _customSoundClick;
    [SerializeField] private Clickable _importSoundClick;
    
    private void Start()
    {
        _customSoundClick.OnClicked += PlayCustomSound;
        _importSoundClick.OnClicked += PlayImportSound;    

        AudioManager.Instance.PlayMusic("TestMusic");
    }

    private void OnDestroy() 
    {
        _customSoundClick.OnClicked -= PlayCustomSound;
        _importSoundClick.OnClicked -= PlayImportSound;   
    }

    private void PlayCustomSound() 
    {
        AudioManager.Instance.PlayCustomSoundEffect("C4-D4-E4");
    }

    private void PlayImportSound()
    {
        AudioManager.Instance.PlaySoundEffect("ImportSoundEffect");
    }
}
