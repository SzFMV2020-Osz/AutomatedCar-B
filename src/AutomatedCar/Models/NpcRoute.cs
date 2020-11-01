using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutomatedCar.Models
{
    public class NpcRoute
    {
        public string Tag { get; private set; }

        public string X { get; private set; }

        public string Y { get; private set; }

        public List<NpcRoute> NpcRoutes { get; private set; }

        public void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                this.NpcRoutes = JsonConvert.DeserializeObject<List<NpcRoute>>(json);
            }
        }
    }
}
