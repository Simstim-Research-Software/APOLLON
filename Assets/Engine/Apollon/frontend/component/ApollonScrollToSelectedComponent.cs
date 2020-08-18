﻿
// avoid namespace pollution
namespace Labsim.apollon.frontend.component
{

    [UnityEngine.RequireComponent(typeof(UnityEngine.UI.ScrollRect))]
    public class ApollonScrollToSelectedComponent : UnityEngine.MonoBehaviour {

        // public fields
        [UnityEngine.SerializeField, UnityEngine.Range(1.0f, 20.0f)]
        public float scrollSpeed = 10.0f;

        // priavet member
        private UnityEngine.UI.ScrollRect m_scrollRect;
        private UnityEngine.RectTransform m_rectTransform;
        private UnityEngine.RectTransform m_contentRectTransform;
        private UnityEngine.RectTransform m_selectedRectTransform;

        // awake
        void Awake()
        {

            // get components
            this.m_scrollRect = this.GetComponent<UnityEngine.UI.ScrollRect>();
            this.m_rectTransform = this.GetComponent<UnityEngine.RectTransform>();
            this.m_contentRectTransform = this.m_scrollRect.content;

        } /* Awale() */

	    // Use this for initialization
	    void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {

            // update scroll postion
            this.UpdateScrollToSelected();

	    }

        void UpdateScrollToSelected()
        {

            // grab the selected from the EventSystem
            UnityEngine.GameObject selected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            // check
            if (selected == null)
            {
                return;
            }
            if (selected.transform.parent != this.m_contentRectTransform.transform)
            {
                return;
            }

            this.m_selectedRectTransform = selected.GetComponent<UnityEngine.RectTransform>();

            // math calculus :)
            UnityEngine.Vector3 selectedDifference = this.m_rectTransform.localPosition - this.m_selectedRectTransform.localPosition;
            float contentHeightDifference = (this.m_contentRectTransform.rect.height - this.m_rectTransform.rect.height);

            float selectedPosition = this.m_contentRectTransform.rect.height - selectedDifference.y;
            float currentScrollRectPosition = this.m_scrollRect.normalizedPosition.y * contentHeightDifference;
            float above = currentScrollRectPosition - (this.m_selectedRectTransform.rect.height / 2) + this.m_rectTransform.rect.height;
            float below = currentScrollRectPosition + (this.m_selectedRectTransform.rect.height / 2);

            // check bounds
            float step;
            if(selectedPosition > above)
            {
                step = selectedPosition - above;
            }
            else if(selectedPosition < below)
            {
                step = selectedPosition - below;
            }
            else 
            {
                // escape
                return;
            }
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;

            // finally update normalized position
            this.m_scrollRect.normalizedPosition =
                UnityEngine.Vector2.Lerp(
                    this.m_scrollRect.normalizedPosition,
                    new UnityEngine.Vector2(0.0f, newNormalizedY),
                    this.scrollSpeed * UnityEngine.Time.deltaTime
                );

        } /* UpdateScrollToSelected() */

    } /* public class ApollonScrollToSelectedComponent */

} /* } Labsim.apollon.frontend.component */
