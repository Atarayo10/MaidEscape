using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MaidEscape.Util
{
    /// <summary>
    /// 싱글톤 베이스 클래스
    /// </summary>
    /// <typeparam name="T"> 싱글톤을 만들고자 하는 파생 클래스 이름 </typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static object syncObject = new object();        // 싱글톤 인스턴스를 찾거나 과정 중 다른 스레드에서 사용 중인지 판달할 객체

        private static T instance;

        /// <summary>
        /// 외부에서 인스턴스에 접근하기 위한 프로퍼티
        /// </summary>
        public static T Instance
        {
            get
            {
                // 인스턴스가 없다면
                if (instance == null)
                {
                    // 다른 스레드에서 사용 중인지 판단
                    lock (syncObject)
                    {
                        // 있다면 그 객체의 타입을 가져온다
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            // 없다면 새로운 객체를 만들어서 넣어준다
                            GameObject obj = new GameObject();
                            obj.name = typeof(T).Name;
                            instance = obj.AddComponent<T>();
                        }
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                // 현재 싱글톤 객체가 없다면 생성된 객체를 넣어준다
                instance = this as T;
            }
            else
            {
                // 이전의 객체 사용된 객체를 삭제
                Destroy(instance);
            }
        }

        private void OnDestroy()
        {
            if (instance != this)
            {
                // 다른 싱글톤 객체를 제거하면 안된다
                return;
            }

            instance = null;
        }

        public static bool HasInstance()
        {
            return instance ? true : false;
        }
    }
}