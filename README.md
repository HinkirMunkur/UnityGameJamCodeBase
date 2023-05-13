# UnityGameJamCodeBase

We have collected the codes of the Game Jam games we have developed so far, and improved the code base by adding a few more systems. We hope that you can facilitate and accelerate your game development process by using this code base. By forking this repository, you can easily make your own improvements on top of it.

We have created test scenes and scripts for each of the following systems. If you wish, you can see the sample versions of the systems in these scenes.

```bash
Assets\TestSystems\TestScenes
Assets\TestSystems\TestScripts
```

## Systems

- ### FSM (Finite State Machine) System

  This system can be used in any setting which has finite number of states and any number of transitions among that states.

    - #### Usage:
  
      1.In order to use the abstract and generic StateMachine class on the structure we want, we need to inherit it first. Then, for the state machine structure we will create, we should determine the states and create a specific enumerator for the class we inherited, assigning this enumerator to the generic value of the base class.
      ```C#
      public enum EPlayerState
      {
          IDLE,
          RUN
      }

      public class PlayerStateMachine : StateMachine<EPlayerState>
      {
          [SerializeField] private EPlayerState startEStates;

          protected override void Awake()
          {
              base.Awake();

              stateTransitionDictionary = new Dictionary<EPlayerState, Action>()
              {
                  { EPlayerState.IDLE, GoIdle },
                  { EPlayerState.RUN, GoRun }
              };
          }

          private void Start()
          {
              SetState(startEStates);
          }

          private void GoIdle() => ((PlayerState)currentState).GoIdle(this);
          private void GoRun() => ((PlayerState)currentState).GoRun(this);
      }
      ```
      
      2.To create state transitions, we should write functions specific to each state transition and put them into the abstract state class by inheriting it.
      ```C#
      public abstract class PlayerState : State
      {
          public virtual void GoIdle(IContext<EPlayerState> context) { context.SetState(EPlayerState.IDLE); }
          public virtual void GoRun(IContext<EPlayerState> context) { context.SetState(EPlayerState.RUN); }
      }
      ```
      
      3.In order to define the necessary actions to be taken when certain states are executed or transitions to other states occur, we need to create a new class which inherits the class that contains the transition functions that we have inherited from the abstract State class. This way, we can fill in the required actions specific to each state as needed.
      ```C#
      public class IdleState : PlayerState
      {
          public override void Do()
          {
              // WRITE YOUR IMPLEMENTATION
          }

          public override void GoRun(IContext<EPlayerState> context)
          {
              base.GoRun(context);
              // WRITE YOUR IMPLEMENTATION
          }
      }
      ```
      
- ### Database System

  This system allows you to store your data in JSON format which allowes you to create your own structures. By using this system, you can save your data with as many JSON files as you want, and you can also use your own data structures in JSON.

  - #### Usage:
  
    1.RecordedData is an abstract class that contains the data we want to write to JSON. After inheriting this class, we can write the desired data into it, and we can also assign default values using the constructor.
    ```C#
    [System.Serializable]
    public class PlayerRecordedData : RecordedData
    {
        public string PlayerName;
        public int PlayerHealt;
        public int playerDamage;
        public bool isPlayerDead;

        public PlayerRecordedData() : base()
        {
            PlayerName = "DefaultName";
            PlayerHealt = 100;
            playerDamage = 5;
            isPlayerDead = false;
        }
    }
    ```

    2.The RecordedDataHandler class is an abstract class that performs JSON writing and reading operations through JsonFileHandlers and contains Getter and Setter methods for Recorded Data. By inheriting this class, we should define our newly inherited RecordedData class as a property inside it.
    ```C#
    public class PlayerDataHandler : RecordedDataHandler
    {
        public PlayerDataHandler(string dataFileName, PlayerRecordedData playerRecordedData, bool useEncryption) 
            : base(dataFileName, useEncryption)
        {
            this.playerRecordedData = playerRecordedData;
        }

        private PlayerRecordedData playerRecordedData;

        public PlayerRecordedData PlayerRecordedData
        {
            get
            {
                playerRecordedData.IsLoaded = true;
                return playerRecordedData;
            }
            set
            {
                playerRecordedData.IsDirty = true;
                playerRecordedData = value;
            }
        }
        public override RecordedData GetRecordedData()
        {
            return playerRecordedData;
        }

        public override void SetRecordedData(RecordedData recordedData)
        {
            playerRecordedData = (PlayerRecordedData)recordedData;
        }
    }
    ```

    3.Inside the DatabaseManager, we need to define our inherited RecordedDataHandlers as properties and initialize them within the "InitRecordedDataHandlers()" function.
    (Part of a DatabaseManager Class)
    ```C#
    public sealed class DatabaseManager : SingletonnPersistent<DatabaseManager>
    {
          private PlayerDataHandler playerDataHandler;
          public PlayerDataHandler PlayerDataHandler => playerDataHandler;

          private void InitRecordedDataHandlers()
          {
              playerDataHandler = new PlayerDataHandler("PlayerData.json", new PlayerRecordedData(), useEncryption);
          }
    }
    ```
    4.To optimize the database system, we have added two boolean variables, IsLoaded and IsDirty, to the abstract RecordedData class for JSON reading and writing operations. IsLoaded bool is used to ensure that the data is not loaded again from JSON once it is loaded. IsDirty bool is used to prevent unnecessary writing to JSON when there is no change made to the data.

- ### Audio System

  AudioManager allows us to play musics and sound effects, stop them, change their volume by using its API. Most unique feature of the AudioManager is continuing playing a music across scenes, and producing custom sound effects in the code by providing notes and octaves.
  - ### Usage:
     1.AudioManager is a singleton class and is should be initialized at the start of a game, you can access its API by calling its instance from any scene in the game. The AudioManager can hold all the musics and sound effects in your game in the form of scriptable objects. You must create either a "Music" or a "SoundEffect" scriptable object depending on your needs.

    Music scriptable object properties:
    ```C#
    public AudioClip audioClip;
    public string audioName;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch;

    public bool isLooping = false;

    public float delay = 0;
    ``` 
    SoundEffect scriptable object properties:
    ```C#
    public AudioClip audioClip;
    public string audioName;

    [Range(0f, 1f)]
    public float volume;
    ``` 
    You can modify the values in the scriptable objects to your needs. Then, created "Music" and "SoundEffect" scriptable objects should be referenced by the AudioManager in lists _musicList and _soundEffectList respectively where the AudioManager is initialized. Finally the musics and the sound effects can be played using "PlayMusic" and "PlaySoundEffect" functions using the "audioName" determined in the scriptable objects.
    
    
    ```C#
    public void PlayMusic(string musicName)
    {
        foreach (Music music in _musicList)
        {
            if (musicName.Equals(music.audioName))
            {
                // Assign the necessary information from the scriptable object
                _musicAudioSource.clip = music.audioClip;
                _musicAudioSource.volume = music.volume;
                _musicAudioSource.pitch = music.pitch;
                _musicAudioSource.loop = music.isLooping;

                // Play the audio source
                _musicAudioSource.Play();
                _isMusicPlaying = true;

                break;
            }
        }
    }
    ```
    ```C#
    public void PlaySoundEffect(string audioName)
    {
        foreach (SoundEffect soundEffect in _soundEffectList)
        {
            if (audioName.Equals(soundEffect.audioName))
            {
                // Play the audio source
                _soundEffectAudioSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume);

                break;
            }
        }
    }
    ```
    
    2.The music being played by the AudioManager will not stop when the scene is changed by default. If you want to change music across scenes, "StopMusic" function should be called before scene is changed, the new music should be played using "PlayMusic" function again when the new scene starts. 

    ```C#
    public void StopMusic()
    {
        _musicAudioSource.Stop();
        _isMusicPlaying = false;
        _musicAudioSource.clip = null;
    }
    ```
    
    3.Another feature of the AudioManager is changing the volume of a music over a period of time using the "ChangeMusicVolume". This function takes "newVolume" and "duration" parameters. The volume of the music is changed to "newVolume" over "duration" period of time. However, this is temporary change: when the music played again later, the music will be played with its original volume.

    ```C#
    public void ChangeMusicVolume(float newVolume, float duration = 0)
    {
        StartCoroutine(ChangeMusicVolumeCoroutine(newVolume, duration));
    }
    ```
    ```C#
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
    ```
    
    4.Last unique feature of the AudioManager is producing custom sound effects in code using a track string. Example format is the following: "C4-D4-E4".
         Letters represent the note, and numbers represent the octave:
         [C: Do, D: Re, E: Mi, F: Fa, G: Sol, A: La, B: Si].
         Interval of the notes that can be played are [G3-C5] (G3 being the lowest, C5 being the highest).
         Example sound effects:
         Collectible Pick-up sound: "F4-A5",
         Door Closing: "B4-G3",
         Door Opening: "G3-B4".

    ```C#
    public void PlayCustomSoundEffect(string track, float timeBetweenNotes = .06f)
    {
        StartCoroutine(PlayCustomSoundEffectCoroutine(track, timeBetweenNotes));
    }
    ```
    ```C#
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
            }
            yield return new WaitForSeconds(timeBetweenNotes);
        }
    }
    ```
    
    Here's a "level passed" sound effect produced using this feature:
    

    https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/14707689-2445-4590-b11d-d9c0288e33d0
    

- ### Transition System

  The aim is to record various scene transitions and enable their utilization in a desired manner on your projects.
  - #### Usage:
    1.By inheriting the Transition class and overriding the necessary functions, we can create the transitions that we want. After registering these transitions in the required enum, our system will be ready to use them. Once we have selected the desired transition to use for scene opening or closing via the Transition Manager, we can create a transition reference by clicking the "Create transition references" button to use it.
    ```C#
    public class SquareTransition : Transition
    {
        public override void ExecuteCustomStartTransition(float duration)
        {
            blackBackground.enabled = true;
            blackBackground.transform.DOScale(Vector3.zero, duration);
        }

        public override void ExecuteCustomEndTransition(float duration)
        {
            blackBackground.transform.localScale = Vector3.zero;
            blackBackground.enabled = true;

            blackBackground.transform.DOScale(Vector3.one, duration);
        }
    }
    ```
   
  ![CycleSceneTransition](https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/16fe8277-e027-4269-af55-d240c9be6fc9)


- ### Animation System

  This system ease you to create animations and transitions between them without the overhead of using Unity's Animator.
  - #### Usage:
    1.A class should be created that inherits AnimationController, takes an enum that represents the desired animations as a generic type, and is placed in an object that has an animator component. For the "Set Proper Animations" button inside our created controller class to work, the names of the created animations must include the corresponding enum's name for each animation. For example, if the animation name is "playerIdle", the enum name could be "IDLE". 
    ```C#
    public enum EPlayerAnimation
    {
        IDLE,
        RUN
    }

    public class PlayerAnimationController : AnimationController<EPlayerAnimation>
    {

    }
    ```
   
    2.Then it can be used as "PlayAnimation". After sending the "OnAnimationFinished" action as a parameter to "PlayAnimation()", the callback function will be triggered at the end of the animation playback.
    ```C#
    public void PlayAnimation(EAnimationType animationType, int layer = 0, 
            Action OnAnimationFinished = null)
    {
        if (currentAnimationType.Equals(animationType))
        {
            StopAllCoroutines();
            animator.Rebind();
        }
        else
        {
            currentAnimationType = animationType;
            animator.Play(animationTypeNameDictionary[animationType], layer);
        }

        StartCoroutine(CheckUntilAnimationFinish(animationTypeNameDictionary[animationType], layer,
            OnAnimationFinished));
    }
    ```
  
- ### Executable System 

  The Executable system allows us to break down an action into parts to create a cleaner structure. Each executable class that we create can be executed by the Executer as many times as needed.
  - #### Usage:
    1.For a class to be executed by the Executer, it must implement the IExecutable interface. After implementing it, the "Run()" function should be filled so that the desired task can be performed when executed. The desired task may take a long time to complete, so the "Run()" function may return an ETaskStatus enumerator that can take the values of START, CONTINUE, or FINISH.
    ```C#
    public interface IExecuteable
    {
        public ETaskStatus CurrentETaskStatus
        {
            get;
            set;
        }
        public ETaskStatus Run();
    }

    public enum ETaskStatus
    {
        START,
        CONTINUE,
        FINISH,
    }

    public abstract class Task : MonoBehaviour, IExecuteable
    {
        public ETaskStatus CurrentETaskStatus { get; set; } = ETaskStatus.START;
        public abstract ETaskStatus Run();
    }
    ```
    2.The Executer continuously checks for the completion of an ongoing task based on the ETaskStatus returned by the "Run()" function, which can take values of START, CONTINUE, or FINISH. If there is an unfinished task, the Executer keeps checking until it is completed. Once all tasks are finished, the isExecuteFinished Action provided as a parameter to the "Execute()" function is invoked. This Action can be used to check if all tasks have completed. 
    ```C#
    public sealed class Executer : SingletonnPersistent<Executer>
    {
        private Dictionary<Transform, IExecuteable[]> taskDictionary =  new Dictionary<Transform, IExecuteable[]>();

        public void Execute(Transform taskHolder, Action isExecuteFinished = null)
        {
            List<IExecuteable> continuingTasks = new List<IExecuteable>();

            if (taskDictionary.ContainsKey(taskHolder))
            {
                foreach (var task in taskDictionary[taskHolder])
                {
                    if (task.Run() == ETaskStatus.CONTINUE)
                    {
                          continuingTasks.Add(task);
                    }
                }

                if (continuingTasks.Count > 0)
                {
                    StartCoroutine(CheckUntilTasksFinished(continuingTasks, isExecuteFinished));
                }
                else
                {
                    isExecuteFinished?.Invoke();
                }

                return;
            }

            IExecuteable[] executeableTasks = taskHolder.GetComponents<IExecuteable>();

            taskDictionary.Add(taskHolder, executeableTasks);

            foreach (var task in executeableTasks)
            {
                if (task.Run() == ETaskStatus.CONTINUE)
                {
                    continuingTasks.Add(task);
                }
            }

            if (continuingTasks.Count > 0)
            {
                StartCoroutine(CheckUntilTasksFinished(continuingTasks, isExecuteFinished));
            }
            else
            {
                isExecuteFinished?.Invoke();
            }
        }

        private IEnumerator CheckUntilTasksFinished(List<IExecuteable> continuingTasks, Action isExecuteFinished)
        {
            while (continuingTasks.Count != 0)
            {
                for (int i = continuingTasks.Count-1; i >= 0; i--)
                {
                    if (continuingTasks[i].CurrentETaskStatus == ETaskStatus.FINISH)
                    {
                        continuingTasks.Remove(continuingTasks[i]);
                    }
                }

                yield return null;
            }

            isExecuteFinished?.Invoke();
        }

    }
    ```
    3.The Executer stores the tasks it retrieves from the Transform component in a dictionary to avoid the cost of recreating the component using "GetComponentInChildren" every time the task needs to be executed. However, this approach comes at the cost of using more memory. If the task needs to be executed only once, it can be executed using the "ExecuteTasksOnce()" method in the ExtensionBase class.
    ```C#
    public static class ExtensionBase
    {
        public static void ExecuteTasksOnce(this Transform taskHolder)
        {
            IExecuteable[] executeableTasks = taskHolder.GetComponents<IExecuteable>();

            foreach (var task in executeableTasks)
            {
                task.Run();
            }
        }
    }
    ```
  
- ### Camera System

  This system allows you to create different camera systems which each system have any number of virtual cameras also allowed transition between cameras in-system any time. For example, in a two-player game, we need to use one camera system for each player, and this camera system can contain as many virtual cameras as desired within itself.
  - #### Usage:
    1.The Camera System is an abstract and generic class that allows us to create our own camera system by inheriting it and then defining the types of virtual cameras that will be included in our camera system as an Enum. We need to set this Enum as the generic value of the base class.
    ```C#
    public enum EGameCameraType
    {
        PreGameCamera,
        InGameCamera,
        PostGameCamera
    }

    public class GameCameraSystem : CameraSystem<EGameCameraType>
    {
    }
    ```
    2.We need to inherit the abstract and generic VirtualCamera class to create each virtual camera we want, and we need to provide the Enum we created as the generic value.
    ```C#
    public class InGameVirtualCamera : VirtualCamera<EGameCameraType>
    {

    }
    ```
    3.After setting up the entire system correctly, we can perform camera transitions in any desired class by calling the "SetCamera()" function through the CameraManager instance, and passing the CameraSystem and which VirtualCamera to switch to using the defined Enum values.
    ```C#
    public void SetCamera(ECameraSystem eCameraSystem, Enum eCameraType)
    {
        ICameraTransition cameraTransitionSystem = GetCameraSystem(eCameraSystem);
        cameraTransitionSystem.SetCamera(eCameraType);
    }
    ```
  
- ### Dialogue System

  Unity TMP Dialogue System that provides creating simple dialogues with custom text effects.
  
  Broad Description of Dialogue System: https://github.com/BoraKaraaa/UnityDialogueSystemTMP
  
  ![Untitled video - Made with Clipchamp (2)](https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/5de0b211-7aed-4bd8-809d-910dbcf94fc2)


  
- ### Input System

  This system consists of two different classes where one for touch and the other for mouse click. Both have three actions for an input where first object can be touched/clicked, second dragging/moving and the third is    released.
  - #### Usage:
    1.To use the desired device, either the "MouseInputSystemManager" or "TouchInputSystemManager" should be present in the scene and we should activate the "Enable Input Listener" boolean in these scripts. This way, our system will be able to detect holding, dragging, and releasing actions. We can then create an instance of the "Input System Manager" and connect to these actions from anywhere we want to perform the necessary operations.
    
    TouchInputSystemManager Actions
    ```C#
    public Action<Vector2> OnTouchBegin;
    public Action<Vector2> OnTouchMoved;
    public Action<Vector2> OnTouchEnd;
    ```
    
    MouseInputSystemManager Actions
    ```C#
    public Action<Vector2> OnMouseLeftClicked;
    public Action<Vector2> OnMouseDragged;
    public Action<Vector2> OnMouseReleased;
    ```

- ### Button Activity

  This system allows you to assign to buttons specific function.
  - #### Usage:
    1.By inheriting the Button Activity class, we can create a class which stands in the object that has a button component. After that, we define the function that we want our button to perform when clicked inside the "OnPointerClick()" method, so our button is ready to use.
    ```C#
    [RequireComponent(typeof(Button))]
    public abstract class ButtonActivity : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Button button;

        public void DeactivateButton()
        {
            button.interactable = false;
        }

        public void ActivateButton()
        {
            button.interactable = true;
        }

        public abstract void OnPointerClick(PointerEventData eventData);
    }
    ```
## Editor Tools

You can access these tools by clicking "Tools" bar

![Screenshot 2023-05-13 045718](https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/8a3fa9d9-2f5b-42b3-96dd-72ddaeed70fe)

- ### Level Select Window

  You can open the Level Select Window to see all of your levels in build index. It allows you to switch between these levels while in Edit mode.
  
  ![Screenshot 2023-05-13 050059](https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/34021b1d-2037-4c8b-a274-495ecef90ae6)

## Before Build Project

1. It is necessary to set the EditMode boolean value to false on the ManagerSceneController located in the ManagerScene.

    ![Screenshot 2023-05-13 053959](https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/fc432333-c043-4c7d-a0bf-4162582e1b83)

2. Remove all the Scripting Define Symbols in ProjectSettings/Player/OtherSettings/ScriptCompilation. Click on the "Apply" button to save the changes.

    ![Screenshot 2023-05-13 053439](https://github.com/HinkirMunkur/UnityGameJamCodeBase/assets/72511237/1e59f75e-1c7e-4521-9978-1cd94f18ca9b)

## References

https://github.com/madsbangh/EasyButtons

## Licanse

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

