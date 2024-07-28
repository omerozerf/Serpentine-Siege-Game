using UnityEngine;

namespace _Controllers
{
    public class ChangeMaterialsColorController : MonoBehaviour
    {
        public MeshRenderer[] meshRenderers; // 5 MeshRenderer nesnesi burada olacak
        public Color[] colors; // Her bir MeshRenderer için farklı bir renk burada olacak

        
        private void Start()
        {
            if (meshRenderers.Length != colors.Length)
            {
                Debug.LogError("MeshRenderer sayısı ile renk sayısı eşleşmiyor!");
                return;
            }

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // Her bir MeshRenderer'ın materyalini al
                Material material = meshRenderers[i].material;
                // Materyalin rengini belirlenen renk ile değiştir
                material.color = colors[i];
            }
        }
    }
}