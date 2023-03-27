using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextureDropdown : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private TMP_Dropdown textureDropdown;
    [SerializeField] private List<Texture> textures;

    private Texture currentSelectedTexture;

    private void Start()
    {
        textureDropdown.onValueChanged.AddListener(delegate { ChangeTexture(textureDropdown); });
        currentSelectedTexture = textures[0];
    }

    private void ChangeTexture(TMP_Dropdown dropdown)
    {
        
        SetTexture(prefab, currentSelectedTexture);
    }

    private void SetTexture(GameObject obj, Texture texture)
    {
        Renderer renderer = obj.transform.GetChild(0).GetComponent<MeshRenderer>();
        Material newMaterial = new Material(renderer.sharedMaterial);
        newMaterial.mainTexture = texture;
        renderer.material = newMaterial;
    }
}
