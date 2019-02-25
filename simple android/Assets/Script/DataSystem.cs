using UnityEngine;

public class DataSystem : MonoBehaviour{

    public static bool isCreated = false;
    public static int gainedMoney = 0;
    public static int highestScore = 0;

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
            if (PlayerPrefs.HasKey("isSaved"))
            {
                Debug.Log("Saved Data exist");
                LoadInfo();
            }
            else {
                Debug.Log("Saved Data don't exist");
                SaveInfo();
            }
            isCreated = true;
        }
    }

    public void SetCharactor(GameObject player) {
        player.GetComponent<MeshFilter>().mesh = charactors[indexCharactor].charactorMesh;
        player.GetComponent<MeshRenderer>().material = charactors[indexCharactor].charactorMat;
    }

    public void LoadInfo() {
        gainedMoney = PlayerPrefs.GetInt("gainedMoney");
        for (int i = 0; i < charactors.Length; i++) {
            charactors[i].isGained = PlayerPrefs.GetInt("charactors" + i) == 1;
        }
        highestScore = PlayerPrefs.GetInt("highScore");

        GameObject.Find("System").GetComponent<lobbySystem>().ShowMoney();
    }

    public void SaveInfo() {
        PlayerPrefs.SetInt("isSaved", 1);
        PlayerPrefs.SetInt("gainedMoney", gainedMoney);
        for (int i = 0; i < charactors.Length; i++) {
            PlayerPrefs.SetInt("charactors" + i, charactors[i].isGained ? 1 : 0);
        }
        PlayerPrefs.SetInt("highScore", highestScore);

        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        int nowTime = (int)span.TotalSeconds;

        PlayerPrefs.SetInt("saveTime", nowTime);
    }
}