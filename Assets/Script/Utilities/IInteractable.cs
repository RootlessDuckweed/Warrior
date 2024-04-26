using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RootlessDuckweed:可互动类 接口
public interface IInteractable 
{
    bool RepeatInteraction(); //是否可重复互动，返回 bool
    void TriggerAction(); //互动逻辑

    void DeserializeFromJsonData(string json) { } // 从json文件中反序列保存的信息 并执行相关操作

    string GetNeedToCovertJsonData() { return null; } // 获取需要保存的json数据从本类中
}
