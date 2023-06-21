using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Helper
{
    /// <summary>
    /// The delegate function for a state in the state machine.
    /// </summary>
    public delegate void FsmState();

    /// <summary>
    /// The very simple finite state machine. It just provides a convenient
    /// way to execute different function on different state.
    /// The interface design of this state machine should look alike
    /// the more-complex Fsm class so that developers can upgrade easily.
    /// </summary>
    public class SimpleFsm
    {
        /// <summary>
        /// The current state
        /// </summary>
        public FsmState State;

        /// <summary>
        /// Initializes the state machine with the specified state.
        /// </summary>
        /// <param name="state">State.</param>
        public SimpleFsm(FsmState state = null)
        {
            this.State = state;
        }

        /// <summary>
        /// Executes the specified state in the next game frame.
        /// </summary>
        /// <param name="state">State.</param>
        public SimpleFsm On(FsmState state)
        {
            this.State = state;
            return this;
        }


        /// <summary>
        /// Turns off the state machine.
        /// </summary>
        public SimpleFsm Off()
        {
            State = null;
            return this;
        }


        /// <summary>
        /// Updates the state machine. This should be called in the MonoBehavior.Update (),
        /// MonoBehavior.LateUpdate (), or MonoBehavior.FixedUpdate () function.
        /// </summary>
        public void Update()
        {
            if (State != null)
                State();
        }
    }


    /// <summary>
    /// The old-school finite state machine that we are using for many games
    /// by now. I just want to copy it hear just in case we miss it.
    /// </summary>
    public class Fsm
    {
        /// <summary>
        /// The total sleep time without calling to any state program.
        /// </summary>
        private float sleepTime;

        /// <summary>
        /// The delay time to ring the alarm.
        /// </summary>
        private float alarmTime;

        /// <summary>
        /// True determines that the alarm has been ringed
        /// </summary>
        public bool IsAlarmed;

        /// <summary>
        /// The current state machine state
        /// </summary>
        public FsmState State
        {
            get { return execProg; }
            set
            {
                execProg = value;
                exitProg = null;
            }
        }

        /// <summary>
        /// The current state program that is executed every game frame.
        /// </summary>
        public FsmState execProg;

        /// <summary>
        /// The program that will be invoked when the fsm transition out from the current state.
        /// </summary>
        private FsmState exitProg;

        /// <summary>
        /// The previous state program, before transition the current state.
        /// </summary>
        public FsmState prevExecProg;

        /// <summary>
        /// The exit program of the previous state.
        /// </summary>
        public FsmState prevExitProg;

        /// <summary>
        /// It is common to need counters while using state machine.
        /// These int variables are free for developers to use.
        /// </summary>
        public int i, j;

        /// Initializes a new instance of the state machine, given the specified state program.
        /// </summary>
        /// <param name="prog">The state program to execute on the first Update.</param>
        public Fsm(FsmState prog = null)
        {
            execProg = prog;
        }

        /// <summary>
        /// Sets the current state for the state machine, given the specified program to run
        /// on the state machine update.
        /// </summary>
        /// <returns>Itself for chainability.</returns>
        /// <param name="prog">The state program to execute.</param>
        public Fsm On(FsmState prog, FsmState exitProg = null)
        {
            this.execProg = prog;
            this.exitProg = exitProg;
            return this;
        }

        /// <summary>
        /// Sets the current state of the state machine to none and makes sure no state program
        /// is executed on Update.
        /// </summary>
        /// <returns>The off.</returns>
        public Fsm Off()
        {
            execProg = null;
            return this;
        }

        /// <summary>
        /// Determines if there is an alarm setup.
        /// </summary>
        public bool IsAlarmOn
        {
            get
            {
                return alarmTime > 0;
            }
        }

        /// <summary>
        /// Determines if the alarm is currently off.
        /// </summary>
        public bool IsAlarmOff
        {
            get
            {
                return alarmTime <= 0;
            }
        }

        /// <summary>
        /// Turns on the internal alarm
        /// </summary>
        /// <returns>Itself for chainability.</returns>
        /// <param name="alarmTime">The time to ring the alarm.</param>
        public Fsm AlarmOn(float alarmTime)
        {
            this.alarmTime = alarmTime;
            return this;
        }


        /// <summary>
        /// Turns off the internal alarm
        /// </summary>
        /// <returns>The off.</returns>
        public Fsm AlarmOff()
        {
            alarmTime = 0;
            return this;
        }

        /// <summary>
        /// Sleeps the state machine for the specified sleeping time.
        /// </summary>
        /// <returns>Itself for chainability</returns>
        /// <param name="sleepTime">The sleep time.</param>
        public Fsm SleepOn(float sleepTime)
        {
            this.sleepTime = sleepTime;
            return this;
        }

        /// <summary>
        /// Wakes up the state machine.
        /// </summary>
        /// <returns>The off.</returns>
        public Fsm SleepOff()
        {
            sleepTime = 0;
            return this;
        }

        /// <summary>
        /// Updates the state machine. This should be called in the
        /// MonoBehavior.Update (), MonoBehavior.LateUpdate (), or
        /// MonoBehavior.FixedUpdate () function.
        /// </summary>
        public void Update()
        {
            Update(Time.deltaTime);
        }


        /// <summary>
        /// Updates the state machine. This should be called in the
        /// MonoBehavior.Update (), MonoBehavior.LateUpdate (), or
        /// MonoBehavior.FixedUpdate () function.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Update(float deltaTime)
        {
            if (sleepTime > 0)
            {
                sleepTime -= deltaTime;
                if (sleepTime > 0)
                    return;

                // This is the remaining delta time after sleeping
                deltaTime += sleepTime;
                sleepTime = 0;
            }

            if (alarmTime > 0)
                alarmTime -= deltaTime;

            IsAlarmed = alarmTime <= 0;

			// State changed, run the exit state before running the current state.
			if (execProg != prevExecProg)
				prevExitProg?.Invoke();

			prevExecProg = execProg;
			prevExitProg = exitProg;

			// Executes the current state program.
			execProg?.Invoke();
        }
    }
}
