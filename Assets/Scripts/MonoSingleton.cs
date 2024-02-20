using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Inst
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null) // �� ��ü�� ������Ʈ ���� ���� ���
                {
                    Debug.Log(typeof(T).Name + " ����");

                    Instantiate(Resources.Load<T>(Path.Combine("MonoSingletons", typeof(T).Name)));

                    instance = (T)FindObjectOfType(typeof(T));
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (transform.parent != null && transform.root != null) // �� ������Ʈ�� �θ� ������Ʈ�� �����ϴ� ���
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}