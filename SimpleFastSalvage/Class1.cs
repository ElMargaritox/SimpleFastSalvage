using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace SimpleFastSalvage
{
    public class Class1 : RocketPlugin<Config>
    {
        public List<GruposVip> ConfigGrupos;
        public List<UnturnedPlayer> personas = new List<UnturnedPlayer>();
        public Class1 Instance;
        protected override void Load()
        {
            base.Load();
            Instance = this;

            ConfigGrupos = Configuration.Instance.GruposVip;

            Logger.LogWarning("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Logger.Log("Plugin Creado Por @Margarita", ConsoleColor.Green);
            Logger.Log($"Version Del Plugin: {Assembly.GetName().Version}", ConsoleColor.Cyan);
            Logger.Log($"Nombre Del Plugin: {Assembly.GetName().Name}", ConsoleColor.Cyan);
            Logger.LogWarning("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Logger.Log("Este Plugin Es Propiedad De EGN - Libre De Uso");


            Logger.Log("Todos Los Permisos:");
            foreach (var item in ConfigGrupos)
            {
                Logger.LogWarning(item.Permiso + " " + item.Interval.ToString());
            }

            Logger.Log("Plugin Cargado Correctamente", ConsoleColor.Cyan);

            U.Events.OnPlayerConnected += PlayerConnected;
            U.Events.OnPlayerDisconnected += PlayerDisconnected;


        }

        private void PlayerDisconnected(UnturnedPlayer player)
        {
            personas.Remove(player);
        }

        private void PlayerConnected(UnturnedPlayer player)
        {
            personas.Add(player);
            foreach (GruposVip item in Configuration.Instance.GruposVip)
            {
                if (player.HasPermission(item.Permiso))
                {
                    if(Configuration.Instance.mostrar_mensaje_bienvenida_vip == true) {

                        player.Player.interact.sendSalvageTimeOverride(item.Interval);
                        UnturnedChat.Say(player, Translate("bienvenida", item.Interval.ToString()));return;
                                                     
                    }
                    else
                    {
                        player.Player.interact.sendSalvageTimeOverride(item.Interval); return;
                    }
                }
                else
                {
                    if(Configuration.Instance.only_vip == true)
                    {
                        return;
                    }

                    player.Player.interact.sendSalvageTimeOverride(1);
                }
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                     { "bienvenida", "¡Gracias Por Comprar! Tu Interval De Reduccion Es De: {0}"}
                };
            }
        }

        protected override void Unload()
        {

            foreach (UnturnedPlayer player in personas)
            {
                player.Player.interact.sendSalvageTimeOverride(10);
            }
            
            ConfigGrupos = null;
            personas = null;

            U.Events.OnPlayerConnected -= PlayerConnected;
            U.Events.OnPlayerDisconnected -= PlayerDisconnected;
            base.Unload();
            Logger.Log("Plugin Desactivado Correctamente");
        }
    }
}
