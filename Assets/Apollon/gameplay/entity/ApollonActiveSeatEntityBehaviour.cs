﻿// using directives
using System;
using System.Text;
using System.Collections;
using System.Threading;

// avoid namespace pollution
namespace Labsim.apollon.gameplay.entity
{

    public class ApollonActiveSeatEntityBehaviour
        : UnityEngine.MonoBehaviour
    {

        #region properties/members

        [UnityEngine.SerializeField]
        private UnityEngine.GameObject m_subject = null;
        protected ref UnityEngine.GameObject Subject => ref this.m_subject;

        [UnityEngine.SerializeField]
        private UnityEngine.GameObject m_motionSystem = null;
        protected ref UnityEngine.GameObject MotionSystem => ref this.m_motionSystem;

        [UnityEngine.SerializeField]
        private UnityEngine.GameObject m_virtualMotionSystem = null;
        protected ref UnityEngine.GameObject VirtualMotionSystem => ref this.m_virtualMotionSystem;

        [UnityEngine.SerializeField]
        private UnityEngine.GameObject m_entity = null;
        protected ref UnityEngine.GameObject Entity => ref this.m_entity;
        
        public ApollonActiveSeatEntityBridge Bridge { get; set; }
        private bool m_bHasInitialized = false;

        #endregion

        #region controllers implemantion

        internal class IdleController
            : UnityEngine.MonoBehaviour
        {
            private ApollonActiveSeatEntityBehaviour _parent = null;

            private void Awake()
            {

                // disable by default & set name
                this.enabled = false;

            } /* Awake() */

            private void OnEnable() 
            {
                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.IdleController.OnEnable() : begin"
                );

                // preliminary
                if ((this._parent = this.GetComponentInParent<ApollonActiveSeatEntityBehaviour>()) == null)
                {

                    // log
                    UnityEngine.Debug.LogError(
                        "<color=Red>Error: </color> ApollonActiveSeatEntityBehaviour.IdleController.OnEnable() : failed to get parent/rigidbody reference ! Self disabling..."
                    );

                    // disable
                    this.gameObject.SetActive(false);
                    this.enabled = false;

                    // return
                    return;

                } /* if() */

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.IdleController.OnEnable() : end"
                );
                
            } /* OnEnable() */
            
        } /* internal class IdleController */

        internal class VisualOnlyController
            : UnityEngine.MonoBehaviour
        {
            private ApollonActiveSeatEntityBehaviour _parent = null;

            private void Awake()
            {

                // disable by default & set name
                this.enabled = false;

            } /* Awake() */

            private void OnEnable() 
            {
                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.VisualOnlyController.OnEnable() : begin"
                );

                // preliminary
                if ((this._parent = this.GetComponentInParent<ApollonActiveSeatEntityBehaviour>()) == null)
                {

                    // log
                    UnityEngine.Debug.LogError(
                        "<color=Red>Error: </color> ApollonActiveSeatEntityBehaviour.VisualOnlyController.OnEnable() : failed to get parent/rigidbody reference ! Self disabling..."
                    );

                    // disable
                    this.gameObject.SetActive(false);
                    this.enabled = false;

                    // return
                    return;

                } /* if() */

                // move subject reference to current VIRTUAL motion platform
                this._parent.Subject.transform.parent = this._parent.VirtualMotionSystem.transform;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.VisualOnlyController.OnEnable() : end"
                );
                
            } /* OnEnable() */
            
        } /* internal class VisualOnlyController */

        internal class VestibularOnlyController
            : UnityEngine.MonoBehaviour
        {
            private ApollonActiveSeatEntityBehaviour _parent = null;

            private void Awake()
            {

                // disable by default & set name
                this.enabled = false;

            } /* Awake() */

            private void OnEnable() 
            {
                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.VestibularOnlyController.OnEnable() : begin"
                );

                // preliminary
                if ((this._parent = this.GetComponentInParent<ApollonActiveSeatEntityBehaviour>()) == null)
                {

                    // log
                    UnityEngine.Debug.LogError(
                        "<color=Red>Error: </color> ApollonActiveSeatEntityBehaviour.VestibularOnlyController.OnEnable() : failed to get parent/rigidbody reference ! Self disabling..."
                    );

                    // disable
                    this.gameObject.SetActive(false);
                    this.enabled = false;

                    // return
                    return;

                } /* if() */

                // move subject reference to current Entity 
                this._parent.Subject.transform.parent = this._parent.Entity.transform;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.VestibularOnlyController.OnEnable() : end"
                );
                
            } /* OnEnable() */
            
        } /* internal class VestibularOnlyController */

        internal class VisuoVestibularController
            : UnityEngine.MonoBehaviour
        {
            private ApollonActiveSeatEntityBehaviour _parent = null;

            private void Awake()
            {

                // disable by default & set name
                this.enabled = false;

            } /* Awake() */

            private void OnEnable() 
            {
                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.VisuoVestibularController.OnEnable() : begin"
                );

                // preliminary
                if ((this._parent = this.GetComponentInParent<ApollonActiveSeatEntityBehaviour>()) == null)
                {

                    // log
                    UnityEngine.Debug.LogError(
                        "<color=Red>Error: </color> ApollonActiveSeatEntityBehaviour.VisuoVestibularController.OnEnable() : failed to get parent/rigidbody reference ! Self disabling..."
                    );

                    // disable
                    this.gameObject.SetActive(false);
                    this.enabled = false;

                    // return
                    return;

                } /* if() */

                // move subject reference to current Entity 
                this._parent.Subject.transform.parent = this._parent.Entity.transform;

                // log
                UnityEngine.Debug.Log(
                    "<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.VisuoVestibularController.OnEnable() : end"
                );
                
            } /* OnEnable() */
            
        } /* internal class VisuoVestibularController */

        #endregion

        // Init
        private void Initialize()
        {

            // log
            UnityEngine.Debug.Log("<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.Initialize() : begin");
            
            // instantiate state controller components
            var idle = this.gameObject.AddComponent<IdleController>();
            var visualOnly = this.gameObject.AddComponent<VisualOnlyController>();
            var vestibular_only = this.gameObject.AddComponent<VestibularOnlyController>();
            var visuo_vestibular_only = this.gameObject.AddComponent<VisuoVestibularController>();
            
            UnityEngine.Debug.Log("<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.Initialize() : state controller added as gameObject's component, mark as initialized");

            // switch state
            this.m_bHasInitialized = true;

            // log
            UnityEngine.Debug.Log("<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.Initialize() : end");

        } /* Initialize() */

        #region MonoBehaviour implementation

        void Awake()
        {

            // behaviour inactive by default & gameobject inactive
            this.enabled = false;
            this.gameObject.SetActive(false);

        } /* Awake() */

        void OnEnable()
        {

            // initialize iff. required
            if (!this.m_bHasInitialized)
            {

                // log
                UnityEngine.Debug.Log("<color=Blue>Info: </color> ApollonActiveSeatEntityBehaviour.OnEnable() : initialize recquired");

                // call
                this.Initialize();

            } /* if() */
                        
        } /* OnEnable()*/

        void OnDisable()
        {
            
            // skip it hasn't been initialized 
            if (!this.m_bHasInitialized)
            {
                return;
            }

        } /* OnDisable() */

        #endregion

    } /* public class ApollonActiveSeatEntityBehaviour */

} /* } Labsim.apollon.gameplay.entity */
