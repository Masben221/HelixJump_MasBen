using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class Floor : MonoBehaviour
    {
        [SerializeField] private List<Segment> defaultSegments;

        public void AddEmptySegment(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                defaultSegments[i].SetEmpty();
            }

            for (int i = amount; i >= 0; i--)
            {
                defaultSegments.RemoveAt(i);
            }
        }

        public void AddRandomTrapSegment(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int index = Random.Range(0, defaultSegments.Count);
                int typeTrap = Random.Range(0, 3);
                if (typeTrap == 0) defaultSegments[index].SetSpike();
                if (typeTrap == 1) defaultSegments[index].SetPiston();
                if (typeTrap == 2) defaultSegments[index].SetFan();
                defaultSegments.RemoveAt(index);
            }
        }

        public void SetRandomRotation()
        {
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }

        public void SetFinishAllSegment()
        {
            for (int i = 0; i < defaultSegments.Count; i++)
            {
                defaultSegments[i].SetFinish();
            }
        }
        public void AddDefaultSegment()
        {
            for (int i = 0; i < defaultSegments.Count; i++)
            {
                defaultSegments[i].SetDefault();
            }
        }
    }

}
