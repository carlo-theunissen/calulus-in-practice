using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logic.Calculators;
using Newtonsoft.Json;

namespace WebView.json
{
    public static class JSONHelper
    {
        public static string ToJSON(IList<Point> obj)
        {
            return JsonConvert.SerializeObject(obj.Select(x => new [] {x.X, x.Y}).ToList());
        }

    }
}