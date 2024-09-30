using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelPallete
{
    public Color AxisColor;
    public Color BallColor;
    public Color DefaultSegmentColor;
    public Color SpikeSegmentColor;
    public Color FinishSegmentColor;
    public Color FanSegmentColor;
    public Color PistonSegmentColor;
    public Color BackgroundColor;
    public Color CameraBackgroundColor;
}
public class LevelColors : MonoBehaviour
{
    [SerializeField] private LevelPallete[] pallette;

    [SerializeField] private Material m_AxisMaterial;
    [SerializeField] private Material m_BallMaterial;
    [SerializeField] private Material m_DefaultMaterial;
    [SerializeField] private Material m_SpikeMaterial;
    [SerializeField] private Material m_FinishMaterial;
    [SerializeField] private Material m_FanMaterial;
    [SerializeField] private Material m_PistonMaterial;
    [SerializeField] private Image m_BackgroundImage;
    [SerializeField] private new Camera camera;

    public void Start()
    {
        int index = Random.Range(0, pallette.Length);

        m_AxisMaterial.color = pallette[index].AxisColor;
        m_BallMaterial.color = pallette[index].BallColor;
        m_DefaultMaterial.color = pallette[index].DefaultSegmentColor;
        m_SpikeMaterial.color = pallette[index].SpikeSegmentColor;
        m_FinishMaterial.color = pallette[index].FinishSegmentColor;
        m_FanMaterial.color = pallette[index].FanSegmentColor;
        m_PistonMaterial.color = pallette[index].PistonSegmentColor;
        m_BackgroundImage.color = pallette[index].BackgroundColor;
        camera.backgroundColor = pallette[index].CameraBackgroundColor;
    }
}
