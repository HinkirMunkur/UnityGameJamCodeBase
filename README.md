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
  
- ### Transition System

  The aim is to record various scene transitions and enable their utilization in a desired manner on your projeckts.
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
    
    ![CycleSceneTransition](https://github.com/BoraKaraaa/UnityGameJamCodeBase/assets/72511237/456167dc-aa4b-427b-99f2-34aa8f45f415)

- ### Camera System

  This system allows you to create different camera systems which each system have any number of virtual cameras also allowed transition between cameras in-system any time.


- ### Dialogue System

  Unity TMP Dialogue System that provides creating simple dialogues with custom text effects.
  
  Broad Description of Dialogue System: https://github.com/BoraKaraaa/UnityDialogueSystemTMP
  
  ![Untitled video - Made with Clipchamp (2)](https://github.com/BoraKaraaa/UnityGameJamCodeBase/assets/72511237/7c6d3c7e-4345-438a-ada2-4bfcfeeee81e)

  
- ### Input System

  This system consists of two different classes where one for touch and the other for mouse click. Both have three actions for an input where first object can be touched/clicked, second dragging/moving and the third is    released.


- ### Executable System // task ve priority system


- ### Button Activity

  This system allows you to assign to keys specific functions or tasks.


- ### Animation System

  This system ease you to create animations and transitions between them without the overhead of using Unity's Animator.


