using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System;
public class SceneController : MonoBehaviour
{
    public ImageSynthesis synth; //creates imagesynthesis object to save photos
    public GameObject[] prefabs; //array containing objects of interest i.e. ID Cards, Feet

    private int frameref = 5000;
    private int framecount = 5000; //tracks frame number

    private GameObject current; //current object of interest being used in data collection

    private Vector3[] pts = new Vector3[4]; //points of cube object
    public Vector3 newPos; //new position
    public Vector3 center; //center position
    public Vector3 extents; //radi 
    public Camera cam; //camera for converting between world space and screen space 


    public Vector3 min; //min coordinates of bounding box
    public Vector3 max; //max coordinates of bounding box
    public System.IO.StreamWriter file;

    public string filename; 
    
    void Start() //called on first frame
    {

        cam = Camera.main; //default camera perspective
        GameObject prefab = prefabs[0]; //Selects which object to generate photos of
        current = Instantiate(prefab); //instantiate selected game object
        file = new System.IO.StreamWriter(@"C:\Users\Alvar\Downloads\Unity-Technologies-ml-imagesynthesis-2b2bce9c0fc3\ImageSynth\Captures\Uber\" + "id_dataset.csv");
        GenerateRandom(); //randomizes position, 

    }

    // Update is called once per frame
    void Update()
    {
        
        framecount++;
        GenerateRandom();
       
    }


    void GenerateRandom()
    {
 
        //Position
        float newx = UnityEngine.Random.Range(-3.0f, 3.0f); //Random X position between two values in world space
        float newy = UnityEngine.Random.Range(0.01f, 0.01f); //Random Y position between two values in world space
        float newz = UnityEngine.Random.Range(-1.0f, 1.0f); //Random Z position between two values in world space
        Vector3 newPos = new Vector3(newx, newy, newz); //Create new position vector

        //Size 
        float dimx = 6.61f; //default scale x 6.61
        float dimz = 4.13f; //default scale z   4.13 
        float scaler = UnityEngine.Random.Range(0.3f, 1.00f); //Create random scale for object 
        Vector3 newScale = new Vector3(dimx * scaler, dimz * scaler, 0.01f); //Scales objects dimensions
      

        //Rotation
        float yrot = UnityEngine.Random.Range(80.0f, 100.0f); //Rotation 
        Quaternion rotation = Quaternion.Euler(270.0f, yrot, 0); //Create new object rotation

        current.transform.localScale = newScale; //Implements scale
        current.transform.position = newPos; //Implements new position
        current.transform.rotation = rotation; //Implements scale

        //Find bounds
        Renderer rend = current.GetComponent<Renderer>();
        center = rend.bounds.center; //finds center of current object in terms of world space
        extents = rend.bounds.extents; //finds extents (radi from center in all directions) of current object in terms of world space

        float cx = center.x; 
        float cz = center.z;

        float ex = extents.x; 
        float ez = extents.z;

        //Transform bounds vertices to 2d pixel coordinates bottom-left of the screen is (0,0)
        pts[0] = cam.WorldToScreenPoint(new Vector3(cx + ex, 0, cz + ez)); //cy + ey
        pts[1] = cam.WorldToScreenPoint(new Vector3(cx + ex, 0, cz - ez)); //cy + ey
        pts[2] = cam.WorldToScreenPoint(new Vector3(cx - ex, 0, cz + ez));
        pts[3] = cam.WorldToScreenPoint(new Vector3(cx - ex, 0, cz - ez));



        //Transform so that (0,0) is top left of the screen
        for (int i = 0; i < pts.Length; i++)
        {
            pts[i].y = Screen.height - pts[i].y;
        }

        //Calculate the min and max positions
        min = pts[0];
        max = pts[0];
        for (int i = 1; i < pts.Length; i++)
        {
            min = Vector3.Min(min, pts[i]);
            max = Vector3.Max(max, pts[i]);
        }

  
        //Use image synthesis object to save photos 
        if (framecount < frameref + 1000) //sets a cap on how many photos to generate
        {
            filename = $"image_{framecount.ToString().PadLeft(4, '0')}"; //creates file name in format image_xxxxx.jpg
            synth.Save(filename, 640, 360, "Captures/Uber"); //saves image to Train folder // 1920 1080

            createCsv(); //generates corresponding CSV file


        }


    }


    void createCsv() //Write to csv file for TFLite model creation 
    {
        
       if(framecount < frameref + 100)
            file.WriteLine("TEST" + ","  + "C:/Users/Alvar/Downloads/Unity-Technologies-ml-imagesynthesis-2b2bce9c0fc3/ImageSynth/Captures/uber/" + filename + ".jpg" + "," + "TAMU_ID" + "," + ((Decimal)(min.x / 1920)).ToString() + "," + ((Decimal)(min.y / 1080)).ToString() + "," + "," + "," + ((Decimal)(max.x / 1920)).ToString() + "," + ((Decimal)(max.y / 1080)).ToString());
       if(framecount < frameref + 200 && framecount > frameref + 99)
            file.WriteLine("VALIDATE" + ","  + "C:/Users/Alvar/Downloads/Unity-Technologies-ml-imagesynthesis-2b2bce9c0fc3/ImageSynth/Captures/uber/" + filename + ".jpg" + "," + "TAMU_ID" + "," + ((Decimal)(min.x / 1920)).ToString() + "," + ((Decimal)(min.y / 1080)).ToString() + "," + "," + "," + ((Decimal)(max.x / 1920)).ToString() + "," + ((Decimal)(max.y / 1080)).ToString());
       if(framecount < frameref + 1000 && framecount > frameref + 199)
            file.WriteLine("TRAIN" + "," + "C:/Users/Alvar/Downloads/Unity-Technologies-ml-imagesynthesis-2b2bce9c0fc3/ImageSynth/Captures/uber/" + filename + ".jpg" + "," + "TAMU_ID" + "," + ((Decimal)(min.x / 1920)).ToString() + "," + ((Decimal)(min.y / 1080)).ToString() + "," + "," + "," + ((Decimal)(max.x / 1920)).ToString() + "," + ((Decimal)(max.y / 1080)).ToString());
        

    }

    


       

    void OnGUI()
    {
        Rect r = Rect.MinMaxRect(min.x, min.y, max.x, max.y); //create visual for bounding box while generating images

        //see bounding box coordinates and frame count 
        Debug.Log("xMin " + r.xMin + "      xMax:" + r.xMax + "      yMin:" + r.yMin + "      yMax:" + r.yMax + "        Frame:" + framecount);
        
        //Render the box
        GUI.Box(r, "ID Card");

    } 
}
