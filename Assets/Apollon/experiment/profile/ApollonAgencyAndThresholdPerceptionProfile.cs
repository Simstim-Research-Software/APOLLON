﻿using System.Linq;

// avoid namespace pollution
namespace Labsim.apollon.experiment.profile
{

    public class ApollonAgencyAndThresholdPerceptionProfile 
        : ApollonAbstractExperimentFiniteStateMachine< ApollonAgencyAndThresholdPerceptionProfile >
    {

        // Ctor
        public ApollonAgencyAndThresholdPerceptionProfile()
            : base()
        {
            // default profile
            this.m_profileID = ApollonExperimentManager.ProfileIDType.AgencyAndThresholdPerception;
        }

        #region settings/result
        
        public class Settings
        {
            public enum ScenarioIDType
            {

                [System.ComponentModel.Description("Undefined")]
                Undefined = 0,

                [System.ComponentModel.Description("Visual-Only")]
                VisualOnly,

                [System.ComponentModel.Description("Vestibular-Only")]
                VestibularOnly,

                [System.ComponentModel.Description("Visuo-Vestibular")]
                VisuoVestibular

            } /* enum */

            public bool bIsActive;
            public bool bIsTryCatch;

            public ScenarioIDType scenario_type;

            public float
                phase_A_duration,
                phase_B_begin_stim_timeout_lower_bound,
                phase_B_begin_stim_timeout_upper_bound,
                phase_C_max_stim_duration,
                phase_C_max_stim_angle,
                phase_C_angular_acceleration,
                phase_C_angular_saturation_speed,
                phase_D_duration;

        } /* class Settings */

        public class Results
        {

            public bool
                user_response;

            public int
                user_command;

            public float
                user_stim_unity_timestamp,
                user_perception_unity_timestamp;

            public string
                user_stim_host_timestamp,
                user_perception_host_timestamp;

            public UnityEngine.AudioClip
                user_clip;

        } /* class Results */

        // properties
        public Settings CurrentSettings { get; } = new Settings();
        public Results CurrentResults { get; set; } = new Results();

        // fast hack
        public uint
            positiveConditionCount = 0,
            negativeConditionCount = 0;

        #endregion
        
        #region abstract implementation

        public override void onUpdate(object sender, ApollonEngine.EngineEventArgs arg)
        {

            // base call
            base.onUpdate(sender, arg);

        } /* onUpdate() */

        public async override void onExperimentSessionBegin(object sender, ApollonEngine.EngineExperimentEventArgs arg)
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentSessionBegin() : begin"
            );

            // activate all motion system command/sensor
            gameplay.ApollonGameplayManager.Instance.setActive(
                gameplay.ApollonGameplayManager.GameplayIDType.MotionSystemCommand
            );
            gameplay.ApollonGameplayManager.Instance.setActive(
                gameplay.ApollonGameplayManager.GameplayIDType.MotionSystemSensor
            );
            gameplay.ApollonGameplayManager.Instance.setActive(
                gameplay.ApollonGameplayManager.GameplayIDType.VirtualMotionSystemCommand
            );

            // fade in
            await this.DoFadeIn(2500.0f, false);

            // deactivate default DB & activate room setup
            var we_behaviour
                 = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.WorldElement
                ).Behaviour as gameplay.element.ApollonWorldElementBehaviour;
            we_behaviour.References["DBTag_Default"].SetActive(false);
            we_behaviour.References["DBTag_Room"].SetActive(true);
            we_behaviour.References["DBTag_ExoFrontend"].SetActive(true);

            // base call
            base.onExperimentSessionBegin(sender, arg);

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentSessionBegin() : end"
            );

        } /* onExperimentSessionBegin() */

        public override async void onExperimentSessionEnd(object sender, ApollonEngine.EngineExperimentEventArgs arg)
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentSessionEnd() : begin"
            );

            // base call
            base.onExperimentSessionEnd(sender, arg);

            // deactivate all motion system command/sensor
            gameplay.ApollonGameplayManager.Instance.setInactive(
                gameplay.ApollonGameplayManager.GameplayIDType.MotionSystemSensor
            );
            gameplay.ApollonGameplayManager.Instance.setInactive(
                gameplay.ApollonGameplayManager.GameplayIDType.MotionSystemCommand
            );
            gameplay.ApollonGameplayManager.Instance.setInactive(
                gameplay.ApollonGameplayManager.GameplayIDType.VirtualMotionSystemCommand
            );

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentSessionEnd() : end"
            );


        } /* onExperimentSessionEnd() */

        public override async void onExperimentTrialBegin(object sender, ApollonEngine.EngineExperimentEventArgs arg)
        {
            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentTrialBegin() : begin"
            );

            // // send event to motion system backend
            // (
            //     backend.ApollonBackendManager.Instance.GetValidHandle(
            //         backend.ApollonBackendManager.HandleIDType.ApollonMotionSystemPS6TM550Handle
            //     ) as backend.handle.ApollonMotionSystemPS6TM550Handle
            // ).BeginTrial();
        
            // local
            int currentIdx = ApollonExperimentManager.Instance.Session.currentTrialNum - 1;

            // activate the active seat entity
            gameplay.ApollonGameplayManager.Instance.setActive(gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity);

            // inactivate all visual cues through LINQ request
            var we_behaviour
                 = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.WorldElement
                ).Behaviour as gameplay.element.ApollonWorldElementBehaviour;
            foreach (var vc_ref in we_behaviour.References.Where(kvp => kvp.Key.Contains("VCTag_")).Select(kvp => kvp.Value))
            {
                vc_ref.SetActive(false);
            }

            // current scenario
            switch (arg.Trial.settings.GetString("scenario_name"))
            {

                case "visual-only":
                {
                    we_behaviour.References["VCTag_Fan"].SetActive(true);
                    this.CurrentSettings.scenario_type = Settings.ScenarioIDType.VisualOnly;
                    // transit to corresponding entity state
                    (
                        gameplay.ApollonGameplayManager.Instance.getBridge(
                            gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity
                        ) as gameplay.entity.ApollonActiveSeatEntityBridge
                    ).Dispatcher.RaiseVisualOnly();
                    break;
                }
                case "vestibular-only":
                {
                    we_behaviour.References["VCTag_Spot"].SetActive(true);
                    this.CurrentSettings.scenario_type = Settings.ScenarioIDType.VestibularOnly;
                    // transit to corresponding entity state
                    (
                        gameplay.ApollonGameplayManager.Instance.getBridge(
                            gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity
                        ) as gameplay.entity.ApollonActiveSeatEntityBridge
                    ).Dispatcher.RaiseVestibularOnly();
                    break;
                }
                case "visuo-vestibular":
                {
                    we_behaviour.References["VCTag_Fan"].SetActive(true);
                    this.CurrentSettings.scenario_type = Settings.ScenarioIDType.VisuoVestibular;
                    // transit to corresponding entity state
                    (
                        gameplay.ApollonGameplayManager.Instance.getBridge(
                            gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity
                        ) as gameplay.entity.ApollonActiveSeatEntityBridge
                    ).Dispatcher.RaiseVisuoVestibular();
                    break;
                }
                default:
                {
                    this.CurrentSettings.scenario_type = Settings.ScenarioIDType.Undefined;
                    break;
                }

            } /* switch() */

            // extract trial settings
            this.CurrentSettings.bIsTryCatch                            = arg.Trial.settings.GetBool("is_catch_try_condition");
            this.CurrentSettings.bIsActive                              = arg.Trial.settings.GetBool("is_active_condition");
            this.CurrentSettings.phase_A_duration                       = arg.Trial.settings.GetFloat("phase_A_duration_ms");
            this.CurrentSettings.phase_B_begin_stim_timeout_lower_bound = arg.Trial.settings.GetFloatList("phase_B_begin_stim_timeout_ms")[0];
            this.CurrentSettings.phase_B_begin_stim_timeout_upper_bound = arg.Trial.settings.GetFloatList("phase_B_begin_stim_timeout_ms")[1];
            this.CurrentSettings.phase_C_max_stim_duration              = arg.Trial.settings.GetFloat("phase_C_max_stim_duration_ms");
            this.CurrentSettings.phase_C_max_stim_angle                 = arg.Trial.settings.GetFloat("phase_C_max_stim_angle_deg");
            this.CurrentSettings.phase_C_angular_acceleration           = arg.Trial.settings.GetFloat("phase_C_angular_acceleration_deg_per_s2");
            this.CurrentSettings.phase_C_angular_saturation_speed       
                = arg.Trial.settings.GetFloat("phase_C_angular_saturation_speed_deg_per_s") == 0.0f 
                    ? (this.CurrentSettings.phase_C_angular_acceleration * (this.CurrentSettings.phase_C_max_stim_duration / 1000.0f))
                    : arg.Trial.settings.GetFloat("phase_C_angular_saturation_speed_deg_per_s");
            this.CurrentSettings.phase_D_duration                       = arg.Trial.settings.GetFloat("phase_D_duration_ms");
            
            // log the
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentTrialBegin() : found current settings with pattern["
                + arg.Trial.settings.GetString("current_pattern")
                + "]"
                + "\n - bIsTryCatch : " + this.CurrentSettings.bIsTryCatch
                + "\n - bIsActive : " + this.CurrentSettings.bIsActive
                + "\n - scenario_name : " + ApollonEngine.GetEnumDescription(this.CurrentSettings.scenario_type)
                + "\n - phase_A_duration : " + this.CurrentSettings.phase_A_duration
                + "\n - phase_B_begin_stim_timeout_lower_bound : " + this.CurrentSettings.phase_B_begin_stim_timeout_lower_bound
                + "\n - phase_B_begin_stim_timeout_upper_bound : " + this.CurrentSettings.phase_B_begin_stim_timeout_upper_bound
                + "\n - phase_C_max_stim_duration : " + this.CurrentSettings.phase_C_max_stim_duration
                + "\n - phase_C_max_stim_angle : " + this.CurrentSettings.phase_C_max_stim_angle
                + "\n - phase_C_angular_acceleration : " + this.CurrentSettings.phase_C_angular_acceleration
                + "\n - phase_C_angular_saturation_speed : " + this.CurrentSettings.phase_C_angular_saturation_speed
                + "\n - phase_D_duration : " + this.CurrentSettings.phase_D_duration
            );

            // write the randomized scenario/pattern as result for convenience
            arg.Trial.result["scenario"] = ApollonEngine.GetEnumDescription(this.CurrentSettings.scenario_type);
            arg.Trial.result["pattern"] = arg.Trial.settings.GetString("current_pattern");
           
            // activate world element & contriol system
            gameplay.ApollonGameplayManager.Instance.setActive(gameplay.ApollonGameplayManager.GameplayIDType.WorldElement);
            gameplay.ApollonGameplayManager.Instance.setActive(gameplay.ApollonGameplayManager.GameplayIDType.AgencyAndThresholdPerceptionControl);

            // base call
            base.onExperimentTrialBegin(sender, arg);

            // fade out
            await this.DoFadeOut(this._trial_fade_out_duration, false);

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentTrialBegin() : end " + UnityEngine.Time.fixedTime
            );

            // build protocol
            await this.DoRunProtocol(
                async () => { await this.SetState( new phase.ApollonAgencyAndThresholdPerceptionPhase0(this) ); },
                async () => { await this.SetState( new phase.ApollonAgencyAndThresholdPerceptionPhaseA(this) ); },
                async () => { await this.SetState( new phase.ApollonAgencyAndThresholdPerceptionPhaseB(this) ); },
                async () => { await this.SetState( new phase.ApollonAgencyAndThresholdPerceptionPhaseC(this) ); },
                async () => { await this.SetState( new phase.ApollonAgencyAndThresholdPerceptionPhaseD(this) ); },
                async () => { await this.SetState( null ); }
            );
            
        } /* onExperimentTrialBegin() */

        public override async void onExperimentTrialEnd(object sender, ApollonEngine.EngineExperimentEventArgs arg)
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentTrialEnd() : begin"
            );
            
            // write result
            ApollonExperimentManager.Instance.Trial.result["user_stim_host_timestamp"] = this.CurrentResults.user_stim_host_timestamp;
            ApollonExperimentManager.Instance.Trial.result["user_stim_unity_timestamp"] = this.CurrentResults.user_stim_unity_timestamp;
            ApollonExperimentManager.Instance.Trial.result["user_response"] = this.CurrentResults.user_response;
            ApollonExperimentManager.Instance.Trial.result["user_perception_host_timestamp"] = this.CurrentResults.user_perception_host_timestamp;
            ApollonExperimentManager.Instance.Trial.result["user_perception_unity_timestamp"] = this.CurrentResults.user_perception_unity_timestamp;

            // fade in
            await this.DoFadeIn(this._trial_fade_in_duration, false);

            // // send event to motion system backend
            // (
            //     backend.ApollonBackendManager.Instance.GetValidHandle(
            //         backend.ApollonBackendManager.HandleIDType.ApollonMotionSystemPS6TM550Handle
            //     ) as backend.handle.ApollonMotionSystemPS6TM550Handle
            // ).EndTrial();

            // get active seat bridge
            gameplay.entity.ApollonActiveSeatEntityBridge seat_bridge
                = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.ActiveSeatEntity
                ) as gameplay.entity.ApollonActiveSeatEntityBridge;

            // check
            if (seat_bridge == null)
            {

                // log
                UnityEngine.Debug.LogError(
                    "<color=Red>Error: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentTrialEnd() : Could not find corresponding gameplay bridge !"
                );

                // fail
                return;

            } /* if() */

            // get back to idle state
            seat_bridge.Dispatcher.RaiseIdle();

            // base call
            base.onExperimentTrialEnd(sender, arg);
            
            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionProfile.onExperimentTrialEnd() : end"
            );

        } /* onExperimentTrialEnd() */

        #endregion

    } /* class ApollonAgencyAndTBWExperimentProfile */

} /* } Labsim.apollon.experiment.profile */