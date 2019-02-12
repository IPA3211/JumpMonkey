using UnityEngine;

public class DataSystem : MonoBehaviour{

    public static bool isCreated = false;

    [System.Serializable]
    public class CharactorSystem
    {
        public bool isGained = false;
        public Mesh charactorMesh;
        public Material charactorMat;
    }

    public CharactorSystem[] charactors;
    public int indexCharactor;

    private void Start()
    {
        if (!isCreated) 
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
        }
    }

    public void SetCharactor(GameObject player) {
        player.GetComponent<MeshFilter>().mesh = charactors[indexCharactor].charactorMesh;
        player.GetComponent<MeshRenderer>().material = charactors[indexCharactor].charactorMat;
    }
}
