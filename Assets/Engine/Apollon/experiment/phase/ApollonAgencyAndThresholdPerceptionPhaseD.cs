﻿using System.Linq;

// avoid namespace pollution
namespace Labsim.apollon.experiment.phase
{
    
    //
    // End phase - FSM state
    //
    public sealed class ApollonAgencyAndThresholdPerceptionPhaseD
        : ApollonAbstractExperimentState<profile.ApollonAgencyAndThresholdPerceptionProfile>
    {
        public ApollonAgencyAndThresholdPerceptionPhaseD(profile.ApollonAgencyAndThresholdPerceptionProfile fsm)
            : base(fsm)
        {
        }

        public async override System.Threading.Tasks.Task OnEntry()
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseD.OnEntry() : begin"
            );
                
            // get bridge
            gameplay.entity.ApollonActiveSeatEntityBridge seat_bridge
                = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity
                ) as gameplay.entity.ApollonActiveSeatEntityBridge;

            // check
            if (seat_bridge == null)
            {

                // log
                UnityEngine.Debug.LogError(
                    "<color=Red>Error: </color> ApollonAgencyAndThresholdPerceptionPhaseD.OnEntry() : Could not find corresponding gameplay bridge !"
                );

                // fail
                return;

            } /* if() */

            // stop movement
            seat_bridge.Dispatcher.RaiseStop();
                
            // fade out from black for vestibular-only scenario
            if (this.FSM.CurrentSettings.scenario_type == profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VestibularOnly)
            {

                // run it asynchronously
                System.Threading.Tasks.Task.Run(() => this.FSM.DoFadeOut(this.FSM.CurrentSettings.phase_D_duration));

            } /* if() */

            // inactivate HOTAS
            gameplay.ApollonGameplayManager.Instance.setInactive(gameplay.ApollonGameplayManager.GameplayIDType.HOTASWarthogThrottleSensor);

            // show red cross
            frontend.ApollonFrontendManager.Instance.setActive(frontend.ApollonFrontendManager.FrontendIDType.RedCrossGUI);

            // wait a certain amout of time
            await this.FSM.DoSleep(this.FSM.CurrentSettings.phase_D_duration / 2.0f);

            // show red frame
            frontend.ApollonFrontendManager.Instance.setActive(frontend.ApollonFrontendManager.FrontendIDType.RedFrameGUI);

            // wait a certain amout of time
            await this.FSM.DoSleep(this.FSM.CurrentSettings.phase_D_duration / 2.0f);

            // hide red cross & frame
            frontend.ApollonFrontendManager.Instance.setInactive(frontend.ApollonFrontendManager.FrontendIDType.RedCrossGUI);
            frontend.ApollonFrontendManager.Instance.setInactive(frontend.ApollonFrontendManager.FrontendIDType.RedFrameGUI);

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseD.OnEntry() : end"
            );

        } /* OnEntry() */

        public async override System.Threading.Tasks.Task OnExit()
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseD.OnExit() : begin"
            );

            // get bridge
            gameplay.entity.ApollonActiveSeatEntityBridge seat_bridge
                = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity
                ) as gameplay.entity.ApollonActiveSeatEntityBridge;

            // check
            if (seat_bridge == null)
            {

                // log
                UnityEngine.Debug.LogError(
                    "<color=Red>Error: </color> ApollonAgencyAndThresholdPerceptionPhaseD.OnExit() : Could not find corresponding gameplay bridge !"
                );

                // fail
                return;

            } /* if() */

            // finally reset
            seat_bridge.Dispatcher.RaiseReset();

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseD.OnExit() : end"
            );

        } /* OnExit() */

    } /* public sealed class ApollonAgencyAndThresholdPerceptionPhaseD */

} /* } Labsim.apollon.experiment.phase */
