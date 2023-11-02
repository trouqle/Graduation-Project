using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.LevelManagement
{
    public class LevelSelectorManager : Singleton<LevelSelectorManager>
    {
        [SerializeField] LayerMask levelMask;
        Camera cam;
        LevelNode selectedLevel;
        protected override void Awake()
        {
            base.Awake();
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LookForLevel();
            }
        }

        public void LookForLevel()
        {
            Debug.Log("Looking for Level");
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, 99, levelMask);
            if (hit.collider != null)
            {
                selectedLevel = hit.collider.GetComponent<LevelNode>();
            }

        }
    }
}
