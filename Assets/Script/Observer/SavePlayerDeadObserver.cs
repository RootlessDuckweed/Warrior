using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Windows;
using File = System.IO.File;

namespace Script.Observer
{
    public static class SavePlayerDeadObserver
    {   
        [SerializeField]
        private static List<Vector3> _playerDeadPos=new List<Vector3>(); //保存的数据
        private static List<Vector3> _playerDeadPosFromJson;//从保存的json文件中读取的数据

        //添加观察者
        public static void AddPlayerDeadPos(Vector3 pos)
        {
            _playerDeadPos.Add(pos);
        }
        
        //移除观察者
        public static void RemovePlayerDeadPos(Vector3 pos)
        {
            _playerDeadPos.Remove(pos);
        }

        //清空保存的数据
        public static void RemoveAll()
        {
            _playerDeadPos.Clear();
            if (File.Exists(Application.streamingAssetsPath + "/savePlayerDeadPosJson.json"))
            {
                File.Delete(Application.streamingAssetsPath + "/savePlayerDeadPosJson.json");
            }
        }

        //序列化保存的数据
        public static void SerializePlayerDeadPos()
        {
            var settings = new JsonSerializerSettings();
            // 注册 Vector3Converter
            settings.Converters.Add(new ListOfVector3Converter());
            // 使用这个设置进行序列化
            var saveJson=JsonConvert.SerializeObject(_playerDeadPos, settings);
            //Debug.Log(saveJson);
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }
            File.WriteAllText(Application.streamingAssetsPath + "/savePlayerDeadPosJson.json", saveJson); //保存文件
        }

        //反序列化保存的数据
        public static void DeserializePlayerDeadPos()
        {
            if (!File.Exists(Application.streamingAssetsPath + "/savePlayerDeadPosJson.json"))
            {
                return;
            }
            var settings = new JsonSerializerSettings();
            // 注册 Vector3Converter
            settings.Converters.Add(new ListOfVector3Converter());

            // 使用这个设置进行序列化
            
            var jsonData = File.ReadAllText(Application.streamingAssetsPath + "/savePlayerDeadPosJson.json");
            _playerDeadPosFromJson= JsonConvert.DeserializeObject<List<Vector3>>(jsonData, settings);
            _playerDeadPos = _playerDeadPosFromJson;

        }
        
        //根据已经读取json反序列化的观察者 实例化保存的角色遗体位置 
        public static void InstantiateSavedPlayerDeadInMap()
        {
            if (_playerDeadPosFromJson != null)
            {
                foreach (var pos in _playerDeadPosFromJson)
                {
                    var handle = Addressables.LoadAssetAsync<GameObject>("Player");
                    handle.Completed += ((obj) =>
                    {
                        var playerAsset = obj.Result;
                        var playerObj= GameObject.Instantiate(playerAsset);
                        var player= playerObj.gameObject.transform.Find("Player-Main").GetComponent<PlayerController>();
                        player.isDead = true;
                        player.transform.position = pos;
                        player.input.GamePlay.Disable();
                        player.gameObject.tag = "PlayerDead";
                        player.gameObject.layer = 8;
                        player.rb.mass = 1f;
                        player.transform.GetChild(4).gameObject.SetActive(false);
                        player.transform.GetChild(6).gameObject.SetActive(false);
                        Debug.Log("is Generated PlayerDead");
                    });
                }
            }
        }
        
    }
}