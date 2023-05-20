using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MaidEscape.Util
{
    /// <summary>
    /// �̱��� ���̽� Ŭ����
    /// </summary>
    /// <typeparam name="T"> �̱����� ������� �ϴ� �Ļ� Ŭ���� �̸� </typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static object syncObject = new object();        // �̱��� �ν��Ͻ��� ã�ų� ���� �� �ٸ� �����忡�� ��� ������ �Ǵ��� ��ü

        private static T instance;

        /// <summary>
        /// �ܺο��� �ν��Ͻ��� �����ϱ� ���� ������Ƽ
        /// </summary>
        public static T Instance
        {
            get
            {
                // �ν��Ͻ��� ���ٸ�
                if (instance == null)
                {
                    // �ٸ� �����忡�� ��� ������ �Ǵ�
                    lock (syncObject)
                    {
                        // �ִٸ� �� ��ü�� Ÿ���� �����´�
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            // ���ٸ� ���ο� ��ü�� ���� �־��ش�
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
                // ���� �̱��� ��ü�� ���ٸ� ������ ��ü�� �־��ش�
                instance = this as T;
            }
            else
            {
                // ������ ��ü ���� ��ü�� ����
                Destroy(instance);
            }
        }

        private void OnDestroy()
        {
            if (instance != this)
            {
                // �ٸ� �̱��� ��ü�� �����ϸ� �ȵȴ�
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