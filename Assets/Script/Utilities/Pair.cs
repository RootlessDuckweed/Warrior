using System;

//自定义键值对
[Serializable]
public class Pair<T1,T2>
{
    public T1 key;
    public T2 value;
}