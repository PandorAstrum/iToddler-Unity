using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PandorAstrum.UI
{
    public class PA_UISnapScrolling : MonoBehaviour {
        
        [Header("Main Properties")]
        [Range(0, 500)]
        public int panOffset;
        [Range(0f, 20f)]
        public float snapSpeed;
        [Range(0f, 10f)]
        public float scaleOffset;
        [Range(1f, 20f)]
        public float scaleSpeed;
        [Header("Fill up Info")]
        public GameObject panPrefab;
        public ScrollRect scrollRect;
        public PandorAstrum.UI.PA_UISystem uISystem;
        private int panCount = 6;
        private GameObject[] instPans;
        private Vector2[] pansPos;
        private Vector2[] pansScale;
 
        private RectTransform contentRect;
        private Vector2 contentVector;
 
        private int selectedPanID;
        private bool isScrolling;
 
        private void Start () {
            // PandorAstrum.Utility.Displayer _displayer = GetComponent<PandorAstrum.Utility.Displayer>();
            // _displayer.GetScriptableObject();
            // panCount = _displayer._g.Length;
            
            // _displayer.Set(10);
            // int s = _displayer.GetAs<int>();

            contentRect = GetComponent<RectTransform>();
            instPans = new GameObject[panCount];
            pansPos = new Vector2[panCount];
            pansScale = new Vector2[panCount];

            for (int i = 0; i < panCount; i++) {

                instPans[i] = Instantiate(panPrefab, transform, false);

                // Text nameText = instPans[i].GetComponentInChildren<Text>();
                // Image expressionImage = instPans[i].transform.Find("placeholder_image").GetComponent<Image>();
                // nameText.text = _displayer.expressions[i].name;
                // expressionImage.sprite = _displayer.expressions[i].expressionImage;


                if (i == 0) continue;
                instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                    instPans[i].transform.localPosition.y);
                pansPos[i] = -instPans[i].transform.localPosition;
            }
            contentRect.anchoredPosition = new Vector2(0f, 0f);
        }
 
        private void FixedUpdate()
        {
            if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
                scrollRect.inertia = false;
            float nearestPos = float.MaxValue;
            for (int i = 0; i < panCount; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedPanID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                instPans[i].transform.localScale = pansScale[i];
            }
            float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
            if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
            if (isScrolling || scrollVelocity > 400) return;
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
 
        public void Scrolling(bool scroll)
        {
            isScrolling = scroll;
            if (scroll) scrollRect.inertia = true;
        }

        public void NextScroll(){
            Debug.Log("first" + contentRect.anchoredPosition);
            if (selectedPanID == panCount-1) { 
                return;
            } else {
                contentRect.anchoredPosition -= new Vector2(984.4f, 0);
                Debug.Log("After" + contentRect.anchoredPosition);
            }
        
        }
        public void PreviousScroll(){
            if (selectedPanID == 0) { 
                return;
            } else {
                contentRect.anchoredPosition += new Vector2(984.4f, 0);
            }
        }

        public void GetSelection(){
            // get all the game screen in a list
            if (uISystem){
                // GameObject[] gamescreens = ;
            }
            
            // uISystem.SwitchScreen()
        }

    }
}
