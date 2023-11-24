using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ImageLoader : MonoBehaviour
{
    private object[] textures; //array of textures
    public Renderer thisrenderer; //render of the background
    
    public int index; //index of textures array
    public Texture texture; //texture selected

    void Start()
    {

        textures = Resources.LoadAll("Textures", typeof(Texture)); // loads all textures from textures folder
        index = Random.Range(0, textures.Length); //randomly picks one of the textures
        texture = (Texture)textures[index]; //typecast as Texture

        thisrenderer.material.mainTexture = texture; //loads the texture onto the background

    }

    void Update()
    {
        
        index = Random.Range(0, textures.Length); //randomly picks one of the textures
        texture = (Texture)textures[index]; //typecast as Texture

        thisrenderer.material.mainTexture = texture; //loads the texture onto the background

    }
}
