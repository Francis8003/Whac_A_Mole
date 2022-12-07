using UnityEngine;
using UnityEngine.UI;

namespace WhacA_mole
{
    public enum GameState
    {
        Launch, //����
        Start,  //��ʼ
        Pause,  //��ͣ
        Playing,//��Ϸ 
        Over    //����
    }



    public class GameManager : MonoBehaviour
    {
        //public static GameManager Instance;

        //Ĭ�ϵ���Ϸ״̬��Ϊ����״̬
        public static GameState gameState = GameState.Launch;
        //�÷ֵ��ı��ؼ�
        public Text text_Score;
        //�÷ֵ���ֵ
        public static int score;
        //��ʱ���ؼ�����һ��slider����ʾ
        public Slider slid_Time;
        //��Ϸ����ʱ��
        public static float playTime;
        //��Ϸ��ʱ��
        private float totalTime;
        //���ֵ��󶴿ڵ����
        public int burrowIndex = -1;
        //��Ϸֹͣ��ʱ��
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
            //��ʱ����ʱ
            playTime -= Time.deltaTime;
            slid_Time.value = playTime / totalTime;
            //print("gameTime ==" + slid_Time.value);
            //��ʱ��������Ϸ����
            if (slid_Time.value <= 0)
            {
                gameState = GameState.Over;
                btn_EndWin.SetActive(true);
                btn_EndWin.GetComponent<GameOver>().Init("GameOver");
            }

            //������
            if (Input.GetMouseButtonDown(0))//����������¼�
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Mole")
                    {
                        // print("����С����");
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