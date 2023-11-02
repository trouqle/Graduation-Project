using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Herb.LevelManagement
{
    public class LevelNode : MonoBehaviour
    {
        [SerializeField] string levelName;
        [SerializeField] string levelID;
        public LevelNode source;
        public List<LevelNode> connections = new List<LevelNode>();
        public bool isPlayable;
        public bool isPlayed;
        [SerializeField] GameObject connectionPrefab;

        private void Start()
        {
            PlayerPrefs.SetInt("level_" + "ankara" + "_playable",1);

            var completed = PlayerPrefs.GetInt("level_" + levelID);
            var playable = PlayerPrefs.GetInt("level_" + levelID + "_playable");

            Debug.Log(playable);

            isPlayed = completed == 1 ? true : false;
            isPlayable = playable == 1 ? true : false;

            if (!isPlayable)
            {
                gameObject.SetActive(false);
            }
            if (isPlayed)
            {
                InitConnectionsInstant();

            }
        }

        private void InitConnectionsInstant()
        {
            foreach (var node in connections)
            {
                var line = Instantiate(connectionPrefab, transform.position, Quaternion.identity, transform).GetComponent<LineRenderer>();
                line.SetPosition(0, Vector3.zero);
                line.SetPosition(1, node.transform.position - transform.position);
            }
        }

        public void LevelClearNotify()
        {
            foreach (var node in connections)
            {
                var line = Instantiate(connectionPrefab, transform.position, Quaternion.identity, transform).GetComponent<LineRenderer>();
                line.SetPosition(0, Vector3.zero);
                StartCoroutine(AnimateLine(node, line, node.transform.position - transform.position, 2));

                Debug.Log("level_" + node.levelID + "_playable");
                PlayerPrefs.SetInt("level_" + node.levelID + "_playable", 1);
            }
        }

        public void ActivateNode()
        {
            gameObject.SetActive(true);
            isPlayable = true;

            var scale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce);
        }


        void OnMouseDown()
        {
            LevelClearNotify();
            SaveCompletedLevel();
            //SelectLevel();
            //TODO LEVEL SELECT
        }

        public void SelectLevel()
        {
            var proc = SceneManager.LoadSceneAsync(levelName);
        }


        IEnumerator AnimateLine(LevelNode node, LineRenderer line, Vector3 pos, float animationTime)
        {
            float timeToComplete = animationTime;
            float currentTime = 0;
            var lineStartPos = line.GetPosition(1);
            while (currentTime < timeToComplete)
            {
                line.SetPosition(1, Vector2.Lerp(lineStartPos, pos, currentTime / timeToComplete));

                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            node.ActivateNode();

        }

        public void SaveCompletedLevel()
        {
            PlayerPrefs.SetInt("level_" + levelID, 1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }

}