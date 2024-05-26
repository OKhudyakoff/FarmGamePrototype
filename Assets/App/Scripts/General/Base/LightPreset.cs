using UnityEngine;

[CreateAssetMenu(menuName = "LightPresets/LightPreset", fileName = "LightPreset")]
public class LightPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalLightColor;
    public Gradient FogColor;
}
