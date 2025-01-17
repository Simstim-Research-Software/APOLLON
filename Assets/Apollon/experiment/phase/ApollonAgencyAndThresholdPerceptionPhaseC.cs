using System.Linq;
using System.Threading.Tasks;

// avoid namespace pollution
namespace Labsim.apollon.experiment.phase
{

    //
    // Acceleration phase - FSM state
    //
    public sealed class ApollonAgencyAndThresholdPerceptionPhaseC
        : ApollonAbstractExperimentState<profile.ApollonAgencyAndThresholdPerceptionProfile>
    {
        public ApollonAgencyAndThresholdPerceptionPhaseC(profile.ApollonAgencyAndThresholdPerceptionProfile fsm)
            : base(fsm)
        {
        }

        public async override System.Threading.Tasks.Task OnEntry()
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : begin with pattern ["
                + UXF.Session.instance.CurrentTrial.settings.GetString("current_pattern")
                + "] & phase_C_linear_acceleration_target [" 
                + System.String.Join(",", this.FSM.CurrentSettings.phase_C_linear_acceleration_target) 
                + "]"
            );

            // get bridges
            var control_bridge
                = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.AgencyAndThresholdPerceptionControl
                ) as gameplay.control.ApollonAgencyAndThresholdPerceptionControlBridge;

            var motion_system_bridge
                = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.MotionSystemCommand
                ) as gameplay.device.command.ApollonMotionSystemCommandBridge;

            var virtual_motion_system_bridge
                = gameplay.ApollonGameplayManager.Instance.getBridge(
                    gameplay.ApollonGameplayManager.GameplayIDType.VirtualMotionSystemCommand
                ) as gameplay.device.command.ApollonVirtualMotionSystemCommandBridge;

            // current scenario
            bool bHasRealMotion = false; 
            switch (this.FSM.CurrentSettings.scenario_type)
            {

                default:
                case profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VisualOnly:
                {   
                    bHasRealMotion = false;
                    break;
                }
                case profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VestibularOnly:
                case profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VisuoVestibular:
                {
                    bHasRealMotion = true;
                    break;
                }

            } /* switch() */

            // check
            if (control_bridge == null || motion_system_bridge == null || virtual_motion_system_bridge == null)
            {

                // log
                UnityEngine.Debug.LogError(
                    "<color=Red>Error: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : Could not find corresponding gameplay bridge !"
                );

                // fail
                return;

            } /* if() */

            // filtering
            foreach(var (saturation_item, index) in this.FSM.CurrentSettings.phase_C_angular_velocity_saturation_threshold.Select((e,idx) => (e,idx)))
            {
                if(saturation_item == 0.0f)
                {
                    this.FSM.CurrentSettings.phase_C_angular_velocity_saturation_threshold[index] 
                        = (
                            this.FSM.CurrentSettings.phase_C_angular_acceleration_target[index] 
                            * ( this.FSM.CurrentSettings.phase_C_stim_duration / 1000.0f )
                        );
                }
            }
            foreach(var (saturation_item, index) in this.FSM.CurrentSettings.phase_C_linear_velocity_saturation_threshold.Select((e,idx) => (e,idx)))
            {
                if(saturation_item == 0.0f )
                {
                    this.FSM.CurrentSettings.phase_C_linear_velocity_saturation_threshold[index] 
                        = (
                            this.FSM.CurrentSettings.phase_C_linear_acceleration_target[index] 
                            * ( this.FSM.CurrentSettings.phase_C_stim_duration / 1000.0f )
                        );
                }
            }

            // synchronisation mechanism (TCS + local function)
            var sync_detection_point = new System.Threading.Tasks.TaskCompletionSource<(bool, float, string)>();
            var sync_idle_point = new System.Threading.Tasks.TaskCompletionSource<bool>();

            if(!bHasRealMotion) 
            {            

                void sync_user_response_local_function(object sender, gameplay.control.ApollonAgencyAndThresholdPerceptionControlDispatcher.EventArgs e)
                    => sync_detection_point?.TrySetResult((true, UnityEngine.Time.time, System.DateTime.Now.ToString("HH:mm:ss.ffffff")));
                void sync_end_stim_local_function(object sender, gameplay.device.command.ApollonVirtualMotionSystemCommandDispatcher.EventArgs e)
                    => sync_idle_point?.TrySetResult(true);

                // register our synchronisation function
                control_bridge.Dispatcher.UserResponseTriggeredEvent += sync_user_response_local_function;
                virtual_motion_system_bridge.Dispatcher.IdleEvent += sync_end_stim_local_function;

                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : begin stim"
                );

                // log stim begin timestamp
                this.FSM.CurrentResults.user_stim_unity_timestamp = UnityEngine.Time.time;
                this.FSM.CurrentResults.user_stim_host_timestamp = System.DateTime.Now.ToString("HH:mm:ss.ffffff");

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : beginning "
                    + (this.FSM.CurrentSettings.bIsTryCatch ? "fake" : "real")
                    + " stim, result [user_stim_unity_timestamp:"
                    + this.FSM.CurrentResults.user_stim_unity_timestamp
                    + ",user_stim_host_timestamp:"
                    + this.FSM.CurrentResults.user_stim_host_timestamp
                    + "]"
                );
                    
                // check if it's a try/catch condition & begin stim
                if(this.FSM.CurrentSettings.bIsTryCatch)
                {

                    virtual_motion_system_bridge.Dispatcher.RaiseAccelerate(
                        this.FSM.CurrentSettings.phase_C_angular_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_stim_duration,
                        (
                            this.FSM.CurrentSettings.scenario_type == profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VisualOnly 
                            ? true 
                            : false
                        )
                    );

                }
                else
                {

                    virtual_motion_system_bridge.Dispatcher.RaiseAccelerate(
                        this.FSM.CurrentSettings.phase_C_angular_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_stim_duration,
                        (
                            this.FSM.CurrentSettings.scenario_type == profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VisualOnly 
                            ? true 
                            : false
                        )
                    );

                } /* if() */

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : begin "
                    + (this.FSM.CurrentSettings.bIsTryCatch ? "fake" : "real")
                    + " stim, result [user_stim_unity_timestamp:"
                    + this.FSM.CurrentResults.user_stim_unity_timestamp
                    + ",user_stim_host_timestamp:"
                    + this.FSM.CurrentResults.user_stim_host_timestamp
                    + "]"
                );

                var phase_running_task 
                    // wait for idle state
                    = System.Threading.Tasks.Task.Factory.StartNew(
                        async () => 
                        { 

                            UnityEngine.Debug.Log(
                                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : waiting for idle state"
                            );
                            await sync_idle_point.Task; 
                        } 
                    // then sleep remaining idle time & raise end
                    ).Unwrap().ContinueWith( 
                        async antecedant => 
                        { 

                            UnityEngine.Debug.Log(
                                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : waiting [" 
                                + (this.FSM.CurrentSettings.phase_C_total_duration - ( 2.0f * this.FSM.CurrentSettings.phase_C_stim_duration ))
                                + " ms] for remaining phase total time"
                            );
                            await this.FSM.DoSleep(this.FSM.CurrentSettings.phase_C_total_duration - ( 2.0f * this.FSM.CurrentSettings.phase_C_stim_duration ));
                        
                        }
                    ).Unwrap().ContinueWith(
                        antecedent => 
                        {

                            if(!sync_detection_point.Task.IsCompleted) 
                            {
                                
                                UnityEngine.Debug.Log(
                                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : user hasn't responded, injecting default result"
                                );
                                
                                sync_detection_point?.TrySetResult((false, -1.0f, "-1.0"));

                            } else {
                                
                                UnityEngine.Debug.Log(
                                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : user has responded, keep result"
                                );
                            
                            } /* if() */

                        }
                    );

                // wait for detection synchronisation point indefinitely & reset it once hit
                (
                    this.FSM.CurrentResults.user_response_C, 
                    this.FSM.CurrentResults.user_perception_C_unity_timestamp,
                    this.FSM.CurrentResults.user_perception_C_host_timestamp
                ) = await sync_detection_point.Task;

                // unregister our control synchronisation function
                control_bridge.Dispatcher.UserResponseTriggeredEvent -= sync_user_response_local_function;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : waiting for phase end"
                );

                // wait for phase task completion
                await phase_running_task;

                // unregister our motion synchronisation function
                virtual_motion_system_bridge.Dispatcher.IdleEvent -= sync_end_stim_local_function;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : end phase, result [user_response_C:"
                    + this.FSM.CurrentResults.user_response_C
                    + ",user_perception_C_unity_timestamp:"
                    + this.FSM.CurrentResults.user_perception_C_unity_timestamp
                    + ",user_perception_C_host_timestamp:"
                    + this.FSM.CurrentResults.user_perception_C_host_timestamp
                    + "]"
                );

            }
            else
            {

                void sync_user_response_local_function(object sender, gameplay.control.ApollonAgencyAndThresholdPerceptionControlDispatcher.EventArgs e)
                    => sync_detection_point?.TrySetResult((true, UnityEngine.Time.time, System.DateTime.Now.ToString("HH:mm:ss.ffffff")));
                void sync_end_stim_local_function(object sender, gameplay.device.command.ApollonMotionSystemCommandDispatcher.EventArgs e)
                    => sync_idle_point?.TrySetResult(true);

                // register our synchronisation function
                control_bridge.Dispatcher.UserResponseTriggeredEvent += sync_user_response_local_function;
                motion_system_bridge.Dispatcher.IdleEvent += sync_end_stim_local_function;

                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : begin stim"
                );

                // log stim begin timestamp
                this.FSM.CurrentResults.user_stim_unity_timestamp = UnityEngine.Time.time;
                this.FSM.CurrentResults.user_stim_host_timestamp = System.DateTime.Now.ToString("HH:mm:ss.ffffff");
                
                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : beginning "
                    + (this.FSM.CurrentSettings.bIsTryCatch ? "fake" : "real")
                    + " stim, result [user_stim_unity_timestamp:"
                    + this.FSM.CurrentResults.user_stim_unity_timestamp
                    + ",user_stim_host_timestamp:"
                    + this.FSM.CurrentResults.user_stim_host_timestamp
                    + "]"
                );

                // check if it's a try/catch condition & begin stim
                if(this.FSM.CurrentSettings.bIsTryCatch)
                {

                    motion_system_bridge.Dispatcher.RaiseAccelerate(
                        this.FSM.CurrentSettings.phase_C_angular_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                x.IsMandatory ? x.Value : 0.0f
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_stim_duration,
                        (
                            this.FSM.CurrentSettings.scenario_type == profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VisualOnly 
                            ? true 
                            : false
                        )
                    );

                }
                else
                {

                    motion_system_bridge.Dispatcher.RaiseAccelerate(
                        this.FSM.CurrentSettings.phase_C_angular_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_angular_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_angular_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_acceleration_target.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_velocity_saturation_threshold.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_linear_displacement_limiter.Select(
                            (e, idx) 
                                => new { 
                                    Value = e, 
                                    IsMandatory = this.FSM.CurrentSettings.phase_C_linear_mandatory_axis[idx] 
                                }
                        ).Select(
                            x => (
                                (x.IsMandatory || (this.FSM.CurrentResults.user_command == 0.0f))
                                    ? x.Value 
                                    : (x.Value * UnityEngine.Mathf.Abs(this.FSM.CurrentResults.user_command))
                            )
                        ).ToArray(),
                        this.FSM.CurrentSettings.phase_C_stim_duration,
                        (
                            this.FSM.CurrentSettings.scenario_type == profile.ApollonAgencyAndThresholdPerceptionProfile.Settings.ScenarioIDType.VisualOnly 
                            ? true 
                            : false
                        )
                    );

                } /* if() */

                var phase_running_task 
                    // wait for idle state
                    = System.Threading.Tasks.Task.Factory.StartNew(
                        async () => 
                        { 

                            UnityEngine.Debug.Log(
                                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : waiting for idle state"
                            );
                            await sync_idle_point.Task; 
                        } 
                    // then sleep remaining idle time & raise end
                    ).Unwrap().ContinueWith( 
                        async antecedant => 
                        { 

                            UnityEngine.Debug.Log(
                                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : waiting [" 
                                + (this.FSM.CurrentSettings.phase_C_total_duration - ( 2.0f * this.FSM.CurrentSettings.phase_C_stim_duration ))
                                + " ms] for remaining phase total time"
                            );
                            await this.FSM.DoSleep(this.FSM.CurrentSettings.phase_C_total_duration - ( 2.0f * this.FSM.CurrentSettings.phase_C_stim_duration ));
                        
                        }
                    ).Unwrap().ContinueWith(
                        antecedent => 
                        {

                            if(!sync_detection_point.Task.IsCompleted) 
                            {
                                
                                UnityEngine.Debug.Log(
                                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : user hasn't responded, injecting default result"
                                );
                                
                                sync_detection_point?.TrySetResult((false, -1.0f, "-1.0"));

                            } else {
                                
                                UnityEngine.Debug.Log(
                                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : user has responded, keep result"
                                );
                            
                            } /* if() */

                        }
                    );

                // wait for detection synchronisation point indefinitely & reset it once hit
                (
                    this.FSM.CurrentResults.user_response_C, 
                    this.FSM.CurrentResults.user_perception_C_unity_timestamp,
                    this.FSM.CurrentResults.user_perception_C_host_timestamp
                ) = await sync_detection_point.Task;
                
                // unregister our control synchronisation function
                control_bridge.Dispatcher.UserResponseTriggeredEvent -= sync_user_response_local_function;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : waiting for phase end"
                );

                // wait for phase task completion
                await phase_running_task;

                // unregister our motion synchronisation function
                motion_system_bridge.Dispatcher.IdleEvent -= sync_end_stim_local_function;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : end phase, result [user_response_C:"
                    + this.FSM.CurrentResults.user_response_C
                    + ",user_perception_C_unity_timestamp:"
                    + this.FSM.CurrentResults.user_perception_C_unity_timestamp
                    + ",user_perception_C_host_timestamp:"
                    + this.FSM.CurrentResults.user_perception_C_host_timestamp
                    + "]"
                );

            } /* if() */

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnEntry() : end"
            );

        } /* OnEntry() */

        public async override System.Threading.Tasks.Task OnExit()
        {

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnExit() : begin"
            );

            // log
            UnityEngine.Debug.Log(
                "<color=Blue>Info: </color> ApollonAgencyAndThresholdPerceptionPhaseC.OnExit() : end"
            );

        } /* OnExit() */

    } /* public sealed class ApollonAgencyAndThresholdPerceptionPhaseC */

} /* } Labsim.apollon.experiment.phase */
