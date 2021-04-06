using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;

namespace SimpleFastSalvage
{
    public class Config : IRocketPluginConfiguration
    {
        public float interval;
        public bool only_vip;
        public bool mostrar_mensaje_bienvenida_vip;
        //public string grupo_vip;

        public List<GruposVip> GruposVip;

        public void LoadDefaults()
        {
            interval = 1;
            mostrar_mensaje_bienvenida_vip = true;


            GruposVip = new List<GruposVip>
            {
                new GruposVip { Permiso = "Vip.Basico", Interval = 8 },
                new GruposVip { Permiso = "Vip.Normal", Interval = 5 },
                new GruposVip { Permiso = "Vip.Pro", Interval = 3 }
            };

        }


    }


    public class GruposVip
    {
        public GruposVip() { }
        public string Permiso;
        public float Interval;
    }
}
