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
      
      3.Finally, by inheriting the class that contains the transition functions we inherited from the abstract State class, we can fill in what needs to be done when the desired states are executed or when transitioning to other states.
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





- ### Transition System

  The aim is to record various scene transitions and enable their utilization in a desired manner on your projeckts.


- ### Camera System

  This system allows you to create different camera systems which each system have any number of virtual cameras also allowed transition between cameras in-system any time.


- ### Input System

  This system consists of two different classes where one for touch and the other for mouse click. Both have three actions for an input where first object can be touched/clicked, second dragging/moving and the third is    released.


- ### Executable System // task ve priority system


- ### Button Activity

  This system allows you to assign to keys specific functions or tasks.


- ### Database

  This system allows you to store your data in JSON format which allowes you to create your own structures.


- ### Animation System

  This system ease you to create animations and transitions between them without the overhead of using Unity's Animator.


