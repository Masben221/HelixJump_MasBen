using UnityEngine;

namespace HelixJump
{
    public class DestroyLevel : BallEvent
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private BallTrail ballTrail;        

        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Empty)
            {
                Destroy(levelGenerator.Floors[levelGenerator.Floors.Count - 1].gameObject);
                levelGenerator.Floors.Remove(levelGenerator.Floors[levelGenerator.Floors.Count - 1]);

                Destroy(levelGenerator.DestroyFloors[levelGenerator.DestroyFloors.Count - 1].gameObject, 1.0f);
                levelGenerator.DestroyFloors[levelGenerator.DestroyFloors.Count - 1].gameObject.SetActive(true);
                levelGenerator.DestroyFloors.Remove(levelGenerator.DestroyFloors[levelGenerator.DestroyFloors.Count - 1]);                

                if (type != SegmentType.Default && type != SegmentType.Spike)// тупое условие
                {
                    for (int i = 0; i < ballTrail.Blots.Count; i++)
                    {
                        ballTrail.Blots[i].gameObject.SetActive(false);
                        //Destroy(ballTrail.Blots[i].gameObject);
                    }
                }
            }
        }
    }

}
