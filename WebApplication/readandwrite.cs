
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class readandwrite
    {
        public string ConvertCsvFileToJsonObject(string path)
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
                csv.Add(line.Split(','));

            var properties = lines[0].Split(',');

            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }

            return JsonConvert.SerializeObject(listObjResult);
        }
        public List<ShipDetails> LoadJson()
        {
            using (StreamReader r = new StreamReader("shipsDetail.json"))
            {
                string json = r.ReadToEnd();
                List<ShipDetails> sD = JsonConvert.DeserializeObject<List<ShipDetails>>(json);
                return sD;
            }
        }
       

    }

    public class WriteData
    {
        public void writeDataToJson(ShipDetails _data, bool update)
        {

            var filePath = @"shipsDetail.json";
            // Read existing json data
            var jsonData = File.ReadAllText(filePath);
            // De-serialize to object or create new list
            var shipList = JsonConvert.DeserializeObject<List<ShipDetails>>(jsonData)
                                  ?? new List<ShipDetails>();
            if (update)
            {
                for (var i = 0; i < shipList.Count; i++)
                {
                    if (shipList[i].id == _data.id)
                    {
                        shipList[i] = _data;
                    }
                }
            }
            else
            {
                shipList.Add(_data);
            }

            jsonData = JsonConvert.SerializeObject(shipList);
            File.WriteAllText(filePath, jsonData);
        }

        public PortDetail getNearestPort(ShipDetails sd, List<PortDetail> pd)
        {
            Dictionary<double,PortDetail> allportsWithDistances  = new Dictionary<double, PortDetail>();
            List<double> alldistances = new List<double>();
            for (var i = 0; i < pd.Count; i++)
            {
                var distance = DistanceTo(sd.latitude, sd.longitude, pd[i].latitude, pd[i].longitude, 'K');
                alldistances.Add(distance);
                allportsWithDistances.Add(distance,pd[i]);
            }

            alldistances = alldistances.OrderBy(x => x).ToList();
            return allportsWithDistances[alldistances[0]];
        }

        public string GetTime(ShipDetails sd , PortDetail nearestpd)
        {
            var distance = DistanceTo(sd.latitude, sd.longitude, nearestpd.latitude, nearestpd.longitude, 'K');
            double time = distance / sd.velocity;
            return TimeSpan.FromDays(time).ToString(@"dd\:hh\:mm");
        }
        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit )
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }
    }
}
