using UnityEngine;

namespace WitchOS
{
    public class ColorPaletteApplier : MonoBehaviour
    {
        public Material Material;

        void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, Material);
        }
    }
}
