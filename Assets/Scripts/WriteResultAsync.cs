using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Leap.Unity.Examples;
using System.IO;



public class WriteResultAsync : MonoBehaviour {

    // Use this for initialization
    int executionCount;
    string filename;
    void Start()
    {
        filename = "execution" + gameObject.name + ".txt";
        if (!File.Exists(filename))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine("0");
            }
        }


        // Open the file to read from.
        using (StreamReader sr = File.OpenText(filename))
        {
            string firstline = sr.ReadLine();
            executionCount = int.Parse(firstline);
            
        }
    }


    void OnApplicationQuit()
    {

        if (gameObject.name == "Mockup(server)" && LayoutController.thisisServer)
        {
            using (StreamWriter sw = new StreamWriter("Result_" + executionCount.ToString() + "_" + gameObject.name + ".txt"))
            {

                List<GameObject> interiorList = gameObject.GetComponent<AttachObjectManager>().getInteriorList();
                if (interiorList != null)
                {
                    foreach (GameObject o in interiorList)
                    {
                        sw.WriteLine(o.name + "-" + o.GetComponent<AnchorableBehaviour>().anchor.name);
                    }

                    sw.WriteLine("=========================================");
                }
                List<GameObject> exteriorList = gameObject.GetComponent<AttachObjectManager>().getExteriorList();
                if (exteriorList != null)
                {
                    foreach (GameObject o in exteriorList)
                    {
                        sw.WriteLine(o.name + " " + o.GetComponent<AnchorableBehaviour>().anchor.name);
                    }
                    sw.WriteLine("=========================================");
                }

                sw.WriteLine(LayoutController.changeOwnViewCount);
                sw.WriteLine(LayoutController.changeTheirViewCount);
                sw.WriteLine(LayoutController.refreshButtonCount);
                sw.WriteLine(TransformTool.culmalativeRotation);
                sw.Close();

            }
        }
        else if (gameObject.name == "Mockup(client)" && !LayoutController.thisisServer)
        {
            using (StreamWriter sw = new StreamWriter("Result_" + executionCount.ToString() + "_" + gameObject.name + ".txt"))
            {

                List<GameObject> interiorList = gameObject.GetComponent<AttachObjectManager>().getInteriorList();
                if (interiorList != null)
                {
                    foreach (GameObject o in interiorList)
                    {
                        sw.WriteLine(o.name + "-" + o.GetComponent<AnchorableBehaviour>().anchor.name);
                    }

                    sw.WriteLine("=========================================");
                }
                List<GameObject> exteriorList = gameObject.GetComponent<AttachObjectManager>().getExteriorList();
                if (exteriorList != null)
                {
                    foreach (GameObject o in exteriorList)
                    {
                        sw.WriteLine(o.name + " " + o.GetComponent<AnchorableBehaviour>().anchor.name);
                    }
                    sw.WriteLine("=========================================");
                }

                sw.WriteLine(LayoutController.changeOwnViewCount);
                sw.WriteLine(LayoutController.changeTheirViewCount);
                sw.WriteLine(LayoutController.refreshButtonCount);
                sw.WriteLine(TransformTool.culmalativeRotation);
                sw.Close();

            }
        }

        if (gameObject.name == "Mockup(server)" && LayoutController.thisisServer)
        {
            using (StreamWriter sw = new StreamWriter("Order" + executionCount.ToString() + "_" + gameObject.name + ".txt"))
            {
                List<string> interiorList = gameObject.GetComponent<AttachObjectManager>().getOrderList();
                List<string> interiorAnchorList = gameObject.GetComponent<AttachObjectManager>().getOrderAnchorList();
                if (interiorList != null)
                {
                    for (int i = 0; i < interiorList.Count; i++)
                    {
                        sw.WriteLine(interiorList[i] + "-" + interiorAnchorList[i]);
                    }
                }
                sw.Close();
            }

        }
        else if (gameObject.name == "Mockup(client)" && !LayoutController.thisisServer)
        {
            using (StreamWriter sw = new StreamWriter("Order" + executionCount.ToString() + "_" + gameObject.name + ".txt"))
            {
                List<string> interiorList = gameObject.GetComponent<AttachObjectManager>().getOrderList();
                List<string> interiorAnchorList = gameObject.GetComponent<AttachObjectManager>().getOrderAnchorList();
                if (interiorList != null)
                {
                    for (int i = 0; i < interiorList.Count; i++)
                    {
                        sw.WriteLine(interiorList[i] + "-" + interiorAnchorList[i]);
                    }
                }
                sw.Close();
            }
      
        }

        

        using (StreamWriter sw = new StreamWriter(filename))
        {
            executionCount++;
            sw.WriteLine(executionCount.ToString());
        }
    }
	
}
