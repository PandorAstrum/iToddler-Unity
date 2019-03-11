/// <summary>
/// Snap scrolling and content instantiator
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using PandorAstrum.Utils;
using PandorAstrum.Save;

namespace PandorAstrum.UI
{
    public class UISnapScrolling : MonoBehaviour {
    #region public variable ==============================================
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
        public GameObject[] instPans;
    #endregion ===========================================================

    #region private variable =============================================
        private ContentFiller contentFiller;
        private int panCount;
        private Vector2[] pansPos;
        private Vector2[] pansScale;
        private RectTransform contentRect;
        private Vector2 contentVector;
        private int selectedPanID;
        private bool isScrolling;
        private UIManager uIManager;
        private bool init;
    #endregion ===========================================================

    #region properties ===================================================
        public int SelectedPanID { get {return selectedPanID; } }
    #endregion ===========================================================

    #region main methods =================================================
        private void Start () {
            uIManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
            // init = false;
        }
        private void FixedUpdate() {
            if (init) {
                if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
                    scrollRect.inertia = false;
                float nearestPos = float.MaxValue;
                for (int i = 0; i < panCount; i++) {
                    float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                    if (distance < nearestPos) {
                        nearestPos = distance;
                        selectedPanID = i;
                    }
                    if (instPans[i] != null) {
                        float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                        pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                        pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                        instPans[i].transform.localScale = pansScale[i];
                    }
                    
                }
                float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
                if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
                if (isScrolling || scrollVelocity > 400) return;
                contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
                contentRect.anchoredPosition = contentVector;
            }     
        }
    #endregion ===========================================================

    #region custom methods ===============================================
        private void Initializer(int _panCount) {
            contentRect = GetComponent<RectTransform>();
            instPans = new GameObject[_panCount];
            pansPos = new Vector2[_panCount];
            pansScale = new Vector2[_panCount];
            for (int i = 0; i < _panCount; i++) {
                instPans[i] = Instantiate(panPrefab, transform, false);
                if (i == 0) continue;
                instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                    instPans[i].transform.localPosition.y);
                pansPos[i] = -instPans[i].transform.localPosition;
            }
        }
        public void AlbumSetup(List<AlbumData> _datas) {
            panCount = _datas.Count;
            Initializer(panCount);
            for (int i = 0; i < panCount; i++) {
                contentFiller = instPans[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText) {
                    contentFiller.nameText.text = _datas[i].albumName;
                }
                if (contentFiller.button){
                    contentFiller.button.onClick.AddListener(delegate {uIManager.GoToAlbum(this); });
                }
            }
            contentRect.anchoredPosition = new Vector2(0f, 0f);
            init = true;
        }
        
        public void ClearAll () {
            init = false;
            for (int i = 0; i < instPans.Length; i++) {
                DestroyImmediate(instPans[i]);
            }
        }
        public void UserSetup(List<UserData> _datas) {
            panCount = _datas.Count;
            Initializer(panCount);
            for (int i = 0; i < panCount; i++) {
                contentFiller = instPans[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText) {
                    contentFiller.nameText.text = _datas[i].userName;
                }
                if (contentFiller.button){
                    contentFiller.button.onClick.AddListener(delegate {uIManager.GoToProfile(this); });
                }
            }
            contentRect.anchoredPosition = new Vector2(0f, 0f);
            init = true;
        }
        public void ExpressionSetup(List<Expressions> _datas) {
            panCount = _datas.Count;
            Initializer(panCount);
            for (int i = 0; i < panCount; i++) {
                contentFiller = instPans[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText)
                    contentFiller.nameText.text = _datas[i].ExpressionName;
                if (contentFiller.contentImage)
                    contentFiller.contentImage.sprite = _datas[i].ImageContent;
            }
            
            contentRect.anchoredPosition = new Vector2(0f, 0f);
            init = true;
        }
        public void GameSetup(List<Games> _datas) {
            panCount = _datas.Count;
            Initializer(panCount);
            for (int i = 0; i < panCount; i++) {
                contentFiller = instPans[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText) {
                    contentFiller.nameText.text = _datas[i].gameName;
                }
                if (contentFiller.contentImage){
                    contentFiller.contentImage.sprite = _datas[i].gameImage;
                }
                if (contentFiller.contentImage){
                    contentFiller.boardImage.sprite = _datas[i].gameHolderImage;
                }
            }
            contentRect.anchoredPosition = new Vector2(0f, 0f);
            init = true;
        }
        public void TrainingSetup(AlbumData _data, int albumID) {
            panCount = _data.imagePath.Count;
            Initializer(panCount);
            for (int i = 0; i < panCount; i++) {
                contentFiller = instPans[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText)
                    contentFiller.nameText.text = _data.imageName[i];
                if (albumID == 0 || albumID == 1) {
                    // load from resources
                    var texture = Resources.Load<Texture2D>( "defaultalbum/" +
                                                                 _data.imagePath[i]);
                    if (contentFiller.trainingImage)
                        contentFiller.trainingImage.texture = texture;

                } else {
                    //load from library
                }
            }
            contentRect.anchoredPosition = new Vector2(0f, 0f);
            init = true;
        }

        public void Scrolling(bool scroll) {
            isScrolling = scroll;
            if (scroll) scrollRect.inertia = true;
        }
        public void NextScroll() {
            if (SelectedPanID == panCount-1) { 
                return;
            } else {
                contentRect.anchoredPosition -= new Vector2(984.4f, 0);
            }
        }
        public void PreviousScroll() {
            if (SelectedPanID == 0) { 
                return;
            } else {
                contentRect.anchoredPosition += new Vector2(984.4f, 0);
            }
        }
    #endregion ===========================================================

    }
}
