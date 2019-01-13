namespace WebView.Models
{
    public class CalculateViewModel
    {
        public string Original;
        public string NiceFormat;
        public string DerivativePointJson;
        public string DerivativeNiceFormat;
        public string TaylorPoloynoomNiceFormat;
        public double TaylorPoloynoomAround;
        public string McClairenPoloynoomNiceFormat;

        public double IntegralStart;
        public double IntegralEnd;
        public double IntegralSum;

        public string GausJordon;

        public bool GausJordonFault { get; set; }
        public object Request { get; set; }
        public string JsonData { get; set; }
    }
}