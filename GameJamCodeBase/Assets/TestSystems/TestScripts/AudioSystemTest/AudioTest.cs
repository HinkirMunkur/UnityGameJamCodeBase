using UnityEngine;
using TMPro;
using Munkur;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private Clickable _customSoundClick;
    [SerializeField] private Clickable _importSoundClick;
    [SerializeField] private Clickable _musicIncrease;
    [SerializeField] private Clickable _musicDecrease;
    [SerializeField] private Clickable _soundEffectIncrease;
    [SerializeField] private Clickable _soundEffectDecrease;

    [SerializeField] private TextMeshPro _musicVolumeText;
    [SerializeField] private TextMeshPro _soundEffectVolumeText;

    private int _musicVolume;
    private int _soundEffectVolume;
    
    private void Start()
    {
        _customSoundClick.OnClicked += PlayCustomSound;
        _importSoundClick.OnClicked += PlayImportSound;    
        _soundEffectIncrease.OnClicked += IncreaseSoundEffectVolume;
        _soundEffectDecrease.OnClicked += DecreaseSoundEffectVolume;
        _musicIncrease.OnClicked += IncreaseMusicVolume;
        _musicDecrease.OnClicked += DecreaseMusicVolume;

        _musicVolume = 5;
        _soundEffectVolume = 5;

        _musicVolumeText.text = _musicVolume.ToString();
        _soundEffectVolumeText.text = _soundEffectVolume.ToString();

        AudioManager.Instance.PlayMusic("TestMusic");
        AudioManager.Instance.PlaySoundEffect("ImportSoundEffect");

        AudioManager.Instance.PlayCustomSoundEffect("C4-D4-E4-C4-E4-C4-E4", "400-200-400-400-400-400-400");
    }

    private void OnDestroy() 
    {
        _customSoundClick.OnClicked -= PlayCustomSound;
        _importSoundClick.OnClicked -= PlayImportSound;  
        _soundEffectIncrease.OnClicked -= IncreaseSoundEffectVolume;
        _soundEffectDecrease.OnClicked -= DecreaseSoundEffectVolume;
        _musicIncrease.OnClicked -= IncreaseMusicVolume;
        _musicDecrease.OnClicked -= DecreaseMusicVolume; 
    }

    private void PlayCustomSound() 
    {
        AudioManager.Instance.PlayCustomSoundEffect("C4-D4-E4");
    }

    private void PlayImportSound()
    {
        AudioManager.Instance.PlaySoundEffect("ImportSoundEffect");
    }

    private void IncreaseMusicVolume() 
    {
        if (_musicVolume == 10)
        {
            return;
        }

        AudioManager.Instance.MuteMusic(false);

        _musicVolume++;
        _musicVolumeText.text = _musicVolume.ToString();
        AudioManager.Instance.SetMusicVolume(_musicVolume - 15);
    }

    private void DecreaseMusicVolume() 
    {
        if (_musicVolume == 0) 
        {
            return;
        }
        _musicVolume--;
        _musicVolumeText.text = _musicVolume.ToString();

        if (_musicVolume == 0)
        {
            AudioManager.Instance.MuteMusic(true);
        }

        AudioManager.Instance.SetMusicVolume(_musicVolume - 15);
    }

    private void IncreaseSoundEffectVolume() 
    {
        if (_soundEffectVolume == 10)
        {
            return;
        }

        AudioManager.Instance.MuteSoundEffect(false);

        _soundEffectVolume++;
        _soundEffectVolumeText.text = _soundEffectVolume.ToString();
        AudioManager.Instance.SetSoundEffectVolume(_soundEffectVolume - 15);
    }

    private void DecreaseSoundEffectVolume() 
    {
        if (_soundEffectVolume == 0) 
        {
            return;
        }
        _soundEffectVolume--;
        _soundEffectVolumeText.text = _soundEffectVolume.ToString();

        if (_soundEffectVolume == 0)
        {
            AudioManager.Instance.MuteSoundEffect(true);
        }
        else
        {
            AudioManager.Instance.MuteSoundEffect(false);
        }

        AudioManager.Instance.SetSoundEffectVolume(_soundEffectVolume - 15);
    }
}
