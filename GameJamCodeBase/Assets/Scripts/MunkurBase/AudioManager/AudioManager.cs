/*
 * This is an all purpose Audio Manager that can play music and sound effect separately.
 * Author: Mehmet Feyyaz Küçük
 * 
 * TO-DO LIST:
 * - Play music and sound effect using (one)->two audio sources (DONE) (IMPROVED)
 * - Keep playing the music where it left off across scenes (DONE)
 * - Produce very basic sound effects using code (DONE)
 * - Use an audio mixer to control the volume of all musics in the game (DONE)
 * - Change the volume of a music in the runtime over a period of time or immediately (DONE)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Munkur 
{
    public class AudioManager : SingletonnPersistent<AudioManager>
    {
        [SerializeField]
        private Music[] _musicList;
        private Dictionary<string, Music> _musicDict = new Dictionary<string, Music>();

        [SerializeField]
        private SoundEffect[] _soundEffectList;
        private Dictionary<string, SoundEffect> _soundEffectDict = new Dictionary<string, SoundEffect>();

        [SerializeField]
        private AudioSource _musicAudioSource;

        [SerializeField]
        private int _soundEffectAudioSourceCount;
        private List<AudioSource> _soundEffectAudioSources = new List<AudioSource>();

        [SerializeField]
        private AudioSource _customSoundEffectAudioSource;

        private Dictionary<string, int> _notes;
        private const float SEMITONE = 1.05946f;

        [SerializeField]
        private AudioClip _blip;

        private bool _isMusicPlaying;
        public bool IsMusicPlaying => _isMusicPlaying;

        public void Start()
        {
            // Create music and sound effect dictionaries.
            for (int i = 0; i < _musicList.Length; i++) 
            {
                _musicDict.Add(_musicList[i].audioName, _musicList[i]);
            }

            for (int i = 0; i < _soundEffectList.Length; i++) 
            {
                _soundEffectDict.Add(_soundEffectList[i].audioName, _soundEffectList[i]);
            }

            // Generate sound effect audio sources.
            GenerateSoundEffectAudioSources();

            // Set music and sound effect volumes.
            SetMusicVolume(-10);
            SetSoundEffectVolume(-10);

            // Declarations
            _isMusicPlaying = false;

            // Add notes to the dictionary
            _notes = new Dictionary<string, int>
            {
                ["E3"] = -12,
                ["F3"] = -11,
                ["G3"] = -9,
                ["A3"] = -7,
                ["B3"] = -5,
                ["C4"] = -4,
                ["D4"] = -2,
                ["E4"] = 0,
                ["F4"] = 1,
                ["G4"] = 3,
                ["A4"] = 5,
                ["B4"] = 7,
                ["C5"] = 8,
                ["D5"] = 10,
                ["E5"] = 12,
                ["F5"] = 13
            };
        }

        
        /**
        * <summary>
        * This function is used to populate the _soundEffectAudioSources list.
        * </summary>
        */
        private void GenerateSoundEffectAudioSources() 
        {
            for (int i = 0; i < _soundEffectAudioSourceCount; i++) 
            {
                AudioSource audioSourceToAdd = gameObject.AddComponent<AudioSource>();
                _soundEffectAudioSources.Add(audioSourceToAdd);
                audioSourceToAdd.playOnAwake = false;
                audioSourceToAdd.outputAudioMixerGroup = _customSoundEffectAudioSource.outputAudioMixerGroup;
            }
        }

        /**
        * <summary>
        * This function should only be used when playing sound effect.
        * <paramref name="audioName"/> is the name of the sound effect to be played.
        * </summary>
        */
        public void PlaySoundEffect(string audioName)
        {
            SoundEffect soundEffectToPlay = _soundEffectDict[audioName];

            if (!soundEffectToPlay) 
            {
                Debug.LogError("Sound effect could not be found");
                return;
            }

            if (!soundEffectToPlay.isLooping) 
            {
                _customSoundEffectAudioSource.PlayOneShot(soundEffectToPlay.audioClip, soundEffectToPlay.volume);
            }

            // Find a free sound effect audio source.
            bool isThereSpace = false;

            foreach (AudioSource soundEffectAudioSource in _soundEffectAudioSources) 
            {
                if (!soundEffectAudioSource.clip) 
                {
                    isThereSpace = true;

                    soundEffectAudioSource.clip = soundEffectToPlay.audioClip;
                    soundEffectAudioSource.volume = soundEffectToPlay.volume;
                    soundEffectAudioSource.loop = soundEffectToPlay.isLooping;

                    soundEffectAudioSource.Play();

                    break;
                } 
            }

            if (!isThereSpace) 
            {
                Debug.LogError("There is no more space for looping sound effects!");
            }
        }

        /**
        * <summary>
        * This function should only be used when stopping sound effect.
        * </summary>
        */
        public void StopSoundEffect(string audioName) 
        {
            SoundEffect soundEffectToStop = _soundEffectDict[audioName];

            if (!soundEffectToStop) 
            {
                Debug.LogError("Sound effect could not be found");
                return;
            }

            // Find the audio source holding the sound effect.
            foreach (AudioSource soundEffectAudioSource in _soundEffectAudioSources) 
            {
                if (soundEffectAudioSource.clip == soundEffectToStop.audioClip) 
                {
                    soundEffectAudioSource.Stop();
                    soundEffectAudioSource.clip = null;

                    break;
                } 
            }
        }

        /**
        * <summary>
        * This function should only be used when playing music.
        * <paramref name="musicName"/> is the name of the music to be played.
        * </summary>
        */
        public void PlayMusic(string musicName)
        {
            Music music = _musicDict[musicName];

            if (!music) 
            {
                Debug.LogError("Music could not be found!");
            }

            _musicAudioSource.clip = music.audioClip;
            _musicAudioSource.volume = music.volume;
            _musicAudioSource.pitch = music.pitch;
            _musicAudioSource.loop = music.isLooping;

            _musicAudioSource.Play();
            _isMusicPlaying = true;
        }

        /**
        * <summary>
        * This function should only be used when stopping music.
        * </summary>
        */
        public void StopMusic()
        {
            _musicAudioSource.Stop();
            _isMusicPlaying = false;
            _musicAudioSource.clip = null;
        }

        /// <summary>
        /// Change the overall volume of musics.
        /// </summary>
        /// <param name="newVolume"></param> is the new volume to be set for musics.
        public void SetMusicVolume(float newVolume)
        {
            _musicAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", newVolume);
        }

        /// <summary>
        /// Change the overall volume of sound effects.
        /// </summary>
        /// <param name="newVolume"></param> is the new volume to be set for sound effects.
        public void SetSoundEffectVolume(float newVolume)
        {
            _customSoundEffectAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("SoundEffectVolume", newVolume);
        }

        /// <summary>
        /// Mute or unmute all musics.
        /// </summary>
        /// <param name="mute"></param> is the flag for mute or unmute.
        public void MuteMusic(bool mute)
        {
            _musicAudioSource.mute = mute;   
        }

        /// <summary>
        /// Mute or unmute all sound effects.
        /// </summary>
        /// <param name="mute"></param> is the flag for mute or unmute.
        public void MuteSoundEffect(bool mute)
        {
            foreach (AudioSource soundEffectAudioSource in _soundEffectAudioSources) 
            {
                soundEffectAudioSource.mute = mute;
            }
            _customSoundEffectAudioSource.mute = mute; 
        }

        /**
        * <summary>
        * Changes the volume of the music audio source. (Not the volume of music scriptable object!!!!!
        * So, when the same music is played, the value before volume change will be assigned to the music audio source.)
        * Also MUST be used with StartCoroutine();
        * <paramref name="newVolume"/> is the desired new volume of the music audio source.
        * <paramref name="duration"/> is the period of time for the volume transition. (0 will change it immediately.)
        * </summary>
        */
        private IEnumerator ChangeMusicVolumeCoroutine(float newVolume, float duration)
        {
            // Duration cannot be negative.
            if (duration < 0)
            {
                Debug.Log("Duration must be positive.");
                yield break;
            }

            // Change volume in a specified period of time.
            // If the duration is 0, then change the volume immediately.
            if (duration == 0)
            {
                _musicAudioSource.volume = newVolume;
                yield break;
            }

            // This interval is the amount of volume change every 1 milliseconds.
            var interval = (newVolume - _musicAudioSource.volume) / (duration * 100);
            for (int i = 0; i < ((int)duration * 100); i++)
            {
                _musicAudioSource.volume += interval;
                yield return new WaitForSeconds(0.01f);
            }
        }

        /**
        * <summary>
        * Changes the volume of the music audio source. (Not the volume of music scriptable object!!!!!
        * So, when the same music is played, the value before volume change will be assigned to the music audio source.)
        * <paramref name="newVolume"/> is the desired new volume of the music audio source.
        * <paramref name="duration"/> is the period of time for the volume transition. (0 will change it immediately.)
        * </summary>
        */
        public void ChangeMusicVolume(float newVolume, float duration = 0)
        {
            StartCoroutine(ChangeMusicVolumeCoroutine(newVolume, duration));
        }

        private IEnumerator PlayCustomSoundEffectCoroutine(string track, float timeBetweenNotes)
        {
            // Convert to all upper letters
            track = track.ToUpper() + "-";
            string currentNote = "";
            
            // For each note in the track...
            for (int i = 0; i < track.Length; i++)
            {
                if (track[i] != '-')
                {
                    currentNote += track[i];
                }
                else
                {
                    _customSoundEffectAudioSource.pitch = Mathf.Pow(SEMITONE, _notes[currentNote]);
                    _customSoundEffectAudioSource.PlayOneShot(_blip);

                    currentNote = "";
                    yield return new WaitForSeconds(timeBetweenNotes / 1000);
                }
            }
        }

        private IEnumerator PlayCustomSoundEffectCoroutine(string track, string timeTrack) 
        {
            // Convert to all upper letters
            track = track.ToUpper() + "-";
            timeTrack = timeTrack + "-";
            string currentNote = "";
            string currentTime = "";

            Queue<float> timeIntervals = new Queue<float>();

            for (int i = 0; i < timeTrack.Length; i++) 
            {
                if (timeTrack[i] != '-')
                {
                    currentTime += timeTrack[i];
                }
                else
                {
                    timeIntervals.Enqueue(float.Parse(currentTime));

                    currentTime = "";
                }
            }
            
            
            float timeBetweenNotes = 0f;


            // For each note in the track...
            for (int i = 0; i < track.Length; i++)
            {

                if (track[i] != '-')
                {
                    currentNote += track[i];
                }
                else
                {
                    timeBetweenNotes = timeIntervals.Dequeue();
                    _customSoundEffectAudioSource.pitch = Mathf.Pow(SEMITONE, _notes[currentNote]);
                    _customSoundEffectAudioSource.PlayOneShot(_blip);

                    currentNote = "";
                    yield return new WaitForSeconds(timeBetweenNotes / 1000);
                }
            }
        }

        /// <summary>
        /// Create a sound effect in code using track.
        /// Example format is the following: "C4-D4-E4"
        /// Letters represent the note, and number represent the octave.
        /// [C: Do, D: Re, E: Mi, F: Fa, G: Sol, A: La, B: Si]
        /// Interval of the notes that can be played: [G3-C5] (G3 being the lowest, C5 being the highest)
        /// Example sound effects:
        ///     - Collectible Pick-up sound: "F4-A5"
        ///     - Door Closing: "B4-G3"
        ///     - Door Opening: "G3-B4"
        /// </summary>
        /// <param name="track"></param> is the set of notes.
        /// <param name="timeBetweenNotes"></param> is the time interval between any two notes in milliseconds.
        public void PlayCustomSoundEffect(string track, float timeBetweenNotes = 200f)
        {
            StartCoroutine(PlayCustomSoundEffectCoroutine(track, timeBetweenNotes));
        }

        public void PlayCustomSoundEffect(string track, string timeTrack) 
        {
            StartCoroutine(PlayCustomSoundEffectCoroutine(track, timeTrack));
        }
    }

}

