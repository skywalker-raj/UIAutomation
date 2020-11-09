using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
/*
-----------------------------------------------------------------------------------------------------------------------------------------
Author: Ganesh Gyawali
Date: 05-11-2020
Description : Json file update used to update json file of zAnalytics UI Automation Testing. 
Main purpose is make client related information dynamic.
-----------------------------------------------------------------------------------------------------------------------------------------
*/
namespace JsonFileModifier
{
    public  class Program
    {
        #region private property
        private static string FileName;
        #endregion
        static void Main(string[] args)
        {
            try
            {
                //Get file path from command
                foreach (var item in args)
                {
                    if (item.StartsWith("path="))
                    {
                        FileName = item.TrimStart("path=".ToCharArray()).Trim();
                    }
                }

                var Object = JsonToObject();
                ChangeJsonValue(args, Object);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        #region private Method
        // chnage value by key from command
        private static void ChangeJsonValue(string[] args, dynamic obj)
        {

            foreach (var item in args)
            {
                if (item.Contains("=") && !item.StartsWith("path="))
                {
                    string key = item.Split('=')[0].Trim();
                    string value = item.Split('=')[1].Trim();
                    obj[key] = value;
                }
            }
            WriteJsonFile(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        // update existing json file.
        private static void WriteJsonFile(string jsonString)
        {
            File.WriteAllText(FileName, jsonString);
        }
        //  Read data from json file and convert it to object
        private static dynamic JsonToObject()
        {
            string json = File.ReadAllText(FileName);
            return JsonConvert.DeserializeObject(json);
        }
       
        #endregion
    }
}

