using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHSS
{
    public class urun
    {
        public string BarkodNumarasi { get; set; }
        public int StokKodu { get; set; }
        public string Adi { get; set; }
        public string Birimi { get; set; }
        public string Grubu { get; set; }
        public float Adet { get; set; }
        public float AlisFiyati { get; set; }
        public float SatisFiyati { get; set; }
        public int KdvYuzde { get; set; }
        public int OtvYuzde { get; set; }
        public bool KdvDahilmi { get; set; }
        public bool OtvDahilmi { get; set; }
        public int IndirimOrani { get; set; }
        public bool HizliSatis { get; set; }
    }
}
