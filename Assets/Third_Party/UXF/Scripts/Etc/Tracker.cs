using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace UXF
{
    /// <summary>
    /// Create a new class that inherits from this component to create custom tracking behaviour on a frame-by-frame basis.
    /// </summary>
    public abstract class Tracker : MonoBehaviour
    { 
        
        /// <summary>
        /// Name of the object used in saving
        /// </summary>
        public string objectName;

        /// <summary>
        /// Description of the type of measurement this tracker will perform.
        /// </summary>
        [HideInInspector]
        [Tooltip("Description of the type of measurement this tracker will perform.")]
        public string measurementDescriptor;

        /// <summary>
        /// Custom column headers for tracked objects
        /// </summary>
        [HideInInspector]
        [Tooltip("Custom column headers for each measurement.")]
        public string[] customHeader = new string[] { };
   
        /// <summary>
        /// The header used when saving the filename string within our behavioural data.
        /// </summary>
        public string filenameHeader
        {
            get
            {
                Debug.AssertFormat(measurementDescriptor.Length > 0, "No measurement descriptor has been specified for this Tracker!");
                return string.Join("_", new string[]{ objectName, measurementDescriptor, "filename" });
            }
        }

        private bool recording;

        List<string[]> data = new List<string[]>();
        
        /// <summary>
        /// The header that will go at the top of the output file associated with this tracker
        /// </summary>
        /// <returns></returns>
        public string[] header
        { 
            get
            {
                var newHeader = new string[customHeader.Length + 2];
                newHeader[0] = "host_time";
                newHeader[1] = "unity_time";
                customHeader.CopyTo(newHeader, 2);
                return newHeader;
            }
        } 

        // called when component is added
        void Reset()
        {
            objectName = gameObject.name.Replace(" ", "_").ToLower();
            SetupDescriptorAndHeader();
        }

        //// called by unity just before rendering the frame
        //void LateUpdate()
        //{
        //    RecordRow();
        //}
        
        void FixedUpdate()
        {
            RecordRow();
        }

        /// <summary>
        /// Records a new row of data at current time.
        /// </summary>
        public void RecordRow()
        {
            if (recording)
            {
                string[] values = GetCurrentValues();

                if (values.Length != customHeader.Length)
                    throw new System.InvalidOperationException(string.Format("GetCurrentValues provided {0} values but expected the same as the number of headers! {1}", values.Length, customHeader.Length));

                string[] row = new string[values.Length + 2];

                row[0] = (
                    // from ref point
                    new System.DateTime(UXF.FileIOManager._hr_refpoint.Ticks).AddMilliseconds(
                        // then add elapsed ticks to ns to ms
                        UXF.FileIOManager._hr_timer.ElapsedTicks
                        * ((1000L * 1000L * 1000L) / System.Diagnostics.Stopwatch.Frequency)
                        / 1000000.0
                    )
                ).ToString("HH:mm:ss.fffffff");
                row[1] = Time.time.ToString();
                values.CopyTo(row, 2);

                data.Add(row);
            }
        }


        /// <summary>
        /// Begins recording.
        /// </summary>
        public void StartRecording()
        {
            data.Clear();
            recording = true;
        }

        /// <summary>
        /// Pauses recording.
        /// </summary>
        public void PauseRecording()
        {
            recording = false;
        }

        /// <summary>
        /// Stops recording.
        /// </summary>
        public void StopRecording()
        {
            recording = false;
        }

        /// <summary>
        /// Returns a copy of the data collected by this tracker.
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetDataCopy()
        {
            return data.Clone();
        }

        /// <summary>
        /// Acquire values for this frame and store them in an array.
        /// </summary>
        /// <returns></returns>
        protected abstract string[] GetCurrentValues();

        /// <summary>
        /// Override this method and define your own descriptor and header.
        /// </summary>
        protected abstract void SetupDescriptorAndHeader();

    }
}