using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Presentation
{
    public class MenuItem : MonoBehaviour
    {

        public GameObject newPlant;

        public void ChangePlant()
        {
            var p = PlanterManager.Instance;
            p.SelectedPlant = newPlant;

        }

    }
}
