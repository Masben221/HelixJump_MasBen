using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelPallete
{
    public Color AxisColor;
    public Color BallColor;
    public Color DefaultSegmentColor;
    public Color TrapSegmentColor;
    public Color FinishSegmentColor;
    public Color BackgroundColor;
    public Color CameraBackgroundColor;
}
public class LevelColors : MonoBehaviour
{
    [SerializeField] private LevelPallete[] pallette;

    [SerializeField] private Material axisMaterial;
    [SerializeField] private Material ballMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material trapMaterial;
    [SerializeField] private Material finishMaterial;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private new Camera camera;

    public void Start()
    {
        int index = Random.Range(0, pallette.Length);

        axisMaterial.color = pallette[index].AxisColor;
        ballMaterial.color = pallette[index].BallColor;
        defaultMaterial.color = pallette[index].DefaultSegmentColor;
        trapMaterial.color = pallette[index].TrapSegmentColor;
        finishMaterial.color = pallette[index].FinishSegmentColor;
        backgroundImage.color = pallette[index].BackgroundColor;
        camera.backgroundColor = pallette[index].CameraBackgroundColor;
    }
}
