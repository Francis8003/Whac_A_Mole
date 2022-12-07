using UnityEngine;
using UnityEngine.UI;

namespace WhacA_mole
{
    public enum GameState
    {
        Launch, //加载
        Start,  //开始
        Pause,  //暂停
        Playing,//游戏 
        Over    //结束
    }



    public class GameManager : MonoBehaviour
    {
        //public static GameManager Instance;

        //默认的游戏状态，为启动状态
        public static GameState gameState = GameState.Launch;
        //得分的文本控件
        public Text text_Score;
        //得分的数值
        public static int score;
        //计时器控件，用一个slider来表示
        public Slider slid_Time;
        //游戏进行时长
        public static float playTime;
        //游戏总时长
        private float totalTime;
        //出现地鼠洞口的序号
        public int burrowIndex = -1;
        //游戏停止的时间
        private float stopTime;


        public GameObject btn_Start;
        public GameObject btn_EndWin;
        public GameObject[] go_Moles;


        private void Awake()
        {
            //if (Instance == null)
            //{
            //    Instance = this;
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}


            playTime = 30.0f;
            totalTime = playTime;
            stopTime = 0.7f;
        }

        private void Start()
        {
            gameState = GameState.Start;
        }

        private void Update()
        {
            if (gameState == GameState.Start || gameState == GameState.Over)
            {
                return;
            }
            //计时器计时
            playTime -= Time.deltaTime;
            slid_Time.value = playTime / totalTime;
            //print("gameTime ==" + slid_Time.value);
            //计时结束，游戏结束
            if (slid_Time.value <= 0)
            {
                gameState = GameState.Over;
                btn_EndWin.SetActive(true);
                btn_EndWin.GetComponent<GameOver>().Init("GameOver");
            }

            //打老鼠
            if (Input.GetMouseButtonDown(0))//鼠标左键点击事件
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Mole")
                    {
                        // print("打中小老鼠");
                        hit.collider.gameObject.GetComponent<Mole>().GetHitten();
                    }
                }
            }

            if (gameState == GameState.Playing)
            {
                stopTime -= Time.deltaTime;
                if (stopTime <= 0)
                {
                    ShowBurrow();
                    stopTime = 0.7f;
                }
            }

            text_Score.text = score.ToString();

            Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.red);
        }

        public void ShowBurrow()
        {
            int random = Random.Range(0, 9);
            if (random != burrowIndex)
            {
                burrowIndex = go_Moles[random].GetComponent<Burrow>().InitBurrow();
            }
        }

        public void OnButStartClick()
        {
            btn_Start.SetActive(false);
            gameState = GameState.Playing;
        }
    }
}