using System;
using System.Linq;
using Logic.interfaces;

namespace WebView.json
{
    public class JsonCreator
    {
        public static JsonModel CreateFromBaseOpeator(IBaseMathOperator baseOperator)
        {
            var json = new JsonModel();
            var text = new TextModel();
            text.title = baseOperator.MathSymbol();
            json.text = text;
            if (baseOperator.GetChilds() !=  null)
            {
                json.children = baseOperator.GetChilds().Where(x => x != null).Select(CreateFromBaseOpeator).ToList();
            }
            return json;
        }
    }
}