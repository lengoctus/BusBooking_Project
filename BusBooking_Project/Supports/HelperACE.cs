using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Supports
{
    public class HelperACE
    {
        public static Dictionary<int, string> GetDataCity()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            dynamic result = JsonConvert.DeserializeObject<dynamic>(ApiACE.MethodGET($"https://thongtindoanhnghiep.co/api/city"));
            foreach (var item in result.LtsItem)
            {
                int id = Convert.ToInt32(item.ID.Value);
                string name = item.Title.Value;
                list.Add(id, name);
            }
            return list;
        }
        public static Dictionary<int, string> GetDataDistrict(int cityId)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            dynamic result = JsonConvert.DeserializeObject<dynamic>(ApiACE.MethodGET($"https://thongtindoanhnghiep.co/api/city/{cityId}/district"));
            foreach (var item in result)
            {
                int id = Convert.ToInt32(item.ID.Value);
                string name = item.Title.Value;
                list.Add(id, name);
            }
            return list;
        }
        public static Dictionary<int, string> GetDataWard(int districtId)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            dynamic result = JsonConvert.DeserializeObject<dynamic>(ApiACE.MethodGET($"https://thongtindoanhnghiep.co/api/district/{districtId}/ward"));
            foreach (var item in result)
            {
                int id = Convert.ToInt32(item.ID.Value);
                string name = item.Title.Value;
                list.Add(id, name);
            }
            return list;
        }
    }
}
