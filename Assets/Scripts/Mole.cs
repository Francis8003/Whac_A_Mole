using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace WhacA_mole
{
    public enum MoleState
    {
        Hitten,   //击中
        Escap,    //逃跑
        Stay,     //停留
        Waiting,  //等待
        Showing   //出现
    }

    public class Mole : MonoBehaviour
    {
        //当前地鼠的状态
        private MoleState moleState = MoleState.Waiting;
        //控制是否显示
        private bool b_Showing = false;
        //是否在逃跑
        private bool b_Escape = true;
        /// <summary>
        /// 初始化老鼠
        /// </summary>
        public void Init()
        {
            if (b_Showing == false)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                moleState = MoleState.Showing;
                if (moleState != MoleState.Hitten)
                {
                    StartCoroutine("BurrowEscape");
                }
            }
            b_Showing = true;
            b_Escape = false;
        }


        void Update()
        {
            if (moleState == MoleState.Showing)
            {
                if (gameObject.transform.position.y <= 0.9f)
                {
                    gameObject.transform.Translate(Vector3.up * 5f * Time.deltaTime);
                }
            }
            else if (moleState == MoleState.Escap)
            {
                if (gameObject.transform.position.y > -0.9f)
                {
                    gameObject.transform.Translate(Vector3.down * 5f * Time.deltaTime);
                    if (gameObject.transform.position.y <= -0.9f)
                    {
                        gameObject.GetComponent<MeshRenderer>().enabled = false;
                        moleState = MoleState.Stay;
                    }
                }
            }
            else if (moleState == MoleState.Hitten)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -0.9f, gameObject.transform.position.z);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                moleState = MoleState.Stay;
                b_Showing = false;
            }
        }

        public void GetHitten()
        {
            moleState = MoleState.Hitten;
            GameManager.score += 1;
        }


        public void Escape()
        {
            moleState = MoleState.Escap;
            b_Escape = true;
        }

        IEnumerator BurrowEscape()
        {
            yield return new WaitForSeconds(3f);
            Escape();
        }
    }
}