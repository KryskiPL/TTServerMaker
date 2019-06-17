using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using ServerEngine.Models.Servers;

namespace ServerEngine.Models
{
    public class Properties : BaseNotificationClass
    {
        private const string Filename = "server.properties";

        private Dictionary<string, string> _properties;
        private readonly ServerBase server;

        // TODO server.property overriding


        public string this[string propertyName]
        {
            get
            {
                return (_properties.ContainsKey(propertyName) ? _properties[propertyName] : "");
            }
            set
            {
                _properties[propertyName] = value;
                OnPropertyChanged(Binding.IndexerName); // TODO <-- nem tom működik-e
            }
        }

        private string PropertiesFilePath
        {
            get
            {
                return server.FolderPath + Filename;
            }
        }

        public Properties(ServerBase server)
        {
            this.server = server;
        }

        /// <summary>
        /// Reads the default server.properties file (from the resources folder), and sets the properties to default
        /// </summary>
        public async void SetToDefault()
        {
           var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ServerEngine.Resources.defaultServerProperties.txt"; 

            //string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("defaultServerProperties.txt"));

            _properties = new Dictionary<string, string>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string row = await reader.ReadLineAsync();
                    string[] spl = row.Split('=');

                    if (spl.Length > 1)
                        _properties.Add(spl[0], spl[1]);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Reads the .properties file
        /// </summary>
        /// <param name="Path"></param>
        public void LoadFromFile()
        {
            // If the file does not exist yet, we load the default values to the properties, and save the file.
            if (!File.Exists(PropertiesFilePath))
            {
                SetToDefault();
                SaveToFile();
                return;
            }

            _properties = new Dictionary<string, string>();

            using (StreamReader olv = new StreamReader(PropertiesFilePath))
            {
                while(!olv.EndOfStream)
                {
                    string[] splitLine = olv.ReadLine().Split(new[] { '=' }, 2);

                    if (splitLine.Length > 1)
                        _properties.Add(splitLine[0], splitLine[1]);
                }

                olv.Close();
            }
        }


        /// <summary>
        /// Saves to properties to the server.properties file
        /// </summary>
        public async void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(PropertiesFilePath))
            {

                foreach (KeyValuePair<string, string> property in _properties)
                {
                    // Removing invalid characters from the property value
                    string value = property.Value.Replace("=", "");

                    await writer.WriteLineAsync($"{property.Key}={value}");
                }

                writer.Close();
            }
        }

        /// <summary>
        /// Returns the description of the given property. Returns null if not supported.
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <returns></returns>
        public static string GetDescriptionByName(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "allow-flight":
                    return
                        "Allows users to use flight on your server while in Survival mode, if they have a mod that provides flight installed.\r\n" +
                        "With allow-flight enabled, griefers will possibly be more common, because it will make their work easier. In Creative mode " +
                        "this has no effect.";
                case "allow-nether":
                    return "Allows players to travel to the Nether.";
                case "difficulty":
                    return "Defines the difficulty (such as damage dealt by mobs and the way hunger and poison affects players) of the server.";
                case "enable-command-block":
                    return "Enables command blocks. Duh";
                case "enable-query":
                    return "Enables GameSpy4 protocol server listener. Used to get information about server.";
                case "enable-rcon":
                    return "Enables remote access to the server console.";
                case "force-gamemode":
                    return "Force players to join in the default game mode.\r\n" +
                           "Off - Players will join in the gamemode they left in.\r\n" +
                           "on - Players will always join in the default gamemode.";
                case "gamemode":
                    return "Defines the mode of gameplay.";
                case "generate-structures":
                    return "Defines whether structures (such as villages) will be generated.\r\n" +
                           "Note: Dungeons will still generate if this is set to false.";
                case "generator-settings":
                    return "The settings used to customize world generation. See Superflat and Customized for possible settings and examples.";
                case "hardcore":
                    return "If turned on, server difficulty is ignored and set to hard and players will be set to spectator mode if they die.";
                case "level-name":
                    return "The 'level name' value will be used as the world name and its folder name. You may also copy your saved game into " +
                           "the server folder, and change the name to the same as that folder's to load it instead.";
                case "level-seed":
                    return "Add a seed for your world, as in Singleplayer.";
                case "level-type":
                    return "Determines the type of map that is generated.";
                case "max-build-height":
                    return "The maximum height in which building is allowed. Terrain may still naturally generate above a low height limit.";
                case "max-players":
                    return "The maximum number of players that can play on the server at the same time. Note that if more players are on the " +
                           "server it will use more resources. Note also, op player connections are not supposed to count against the max players.";
                case "max-tick-time":
                    return
                        "The maximum number of milliseconds a single tick may take before the server watchdog stops the server with the message, " +
                        "A single server tick took 60.00 seconds (should be max 0.05); Considering it to be crashed, server will forcibly shutdown. " +
                        "Set it to -1 to disable the feature";
                case "max-world-size":
                    return "This sets the maximum possible size in blocks, expressed as a radius, that the world border can obtain. Setting the " +
                           "world border bigger causes the commands to complete successfully but the actual border will not move past this block " +
                           "limit. Setting the max-world-size higher than the default doesn't appear to do anything.";
                case "network-compression-threshold":
                    return "By default it allows packets that are n-1 bytes big to go normally, but a packet that n bytes or more will be compressed " +
                           "down. So, lower number means more compression but compressing small amounts of bytes might actually end up with a larger " +
                           "result than what went in.\r\n" +
                           "- 1 - disable compression entirely\r\n" +
                           "0 - compress everything";
                case "online-mode":
                    return "Server checks connecting players against Minecraft account database. Only set this to false if your server is not connected " +
                           "to the Internet. Hackers with fake accounts can connect if this is set to false! If minecraft.net is down or inaccessible, " +
                           "no players will be able to connect if this is set to true. Setting this variable to off purposely is called \"cracking\" " +
                           "a server, and servers that are presently with online mode off are called \"cracked\" servers, allowing players with " +
                           "unlicensed copies of Minecraft to join.";
                case "op-permission-level":
                    return "Sets the default permission level for ops when using /op";
                case "player-idle-timeout":
                    return "If non-zero, players are kicked from the server if they are idle for more than that many minutes.";
                case "prevent-proxy-connections":
                    return "If the ISP/AS sent from the server is different from the one from Mojang's authentication server, the player is kicked";
                case "pvp":
                    return "Enable PvP on the server. Players shooting themselves with arrows will only receive damage if PvP is enabled.";
                case "query.port":
                    return "Sets the port for the query server.";
                case "rcon.password":
                    return "Sets the password to rcon";
                case "rcon.port":
                    return "Sets the port to rcon.";
                case "resource-pack":
                    return "Optional URI to a resource pack. The player may choose to use it.";
                case "resource-pack-sha1":
                    return "Optional SHA-1 digest of the resource pack, in lowercase hexadecimal. It's recommended to specify this. " +
                           "This is not yet used to verify the integrity of the resource pack, but improves the effectiveness and " +
                           "reliability of caching.";
                case "server-port":
                    return "Changes the port the server is hosting (listening) on. This port must be forwarded if the server is " +
                           "hosted in a network using NAT (If you have a home router/firewall).";
                case "snooper-enabled":
                    return "Sets whether the server sends snoop data regularly to http://snoop.minecraft.net.";
                case "spawn-animals":
                    return "Determines if animals will be able to spawn.";
                case "spawn-monsters":
                    return "Determines if monsters will be spawned.";
                case "spawn-npcs":
                    return "Determines whether villagers will be spawned.";
                case "spawn-protection":
                    return "Determines the radius of the spawn protection as 2x+1. Setting this to 0 will not disable spawn protection. " +
                           "0 will protect the single block at the spawn point. 1 will protect a 3x3 area centered on the spawn point. 2 " +
                           "will protect 5x5, 3 will protect 7x7, etc. This option is not generated on the first server start and appears " +
                           "when the first player joins. If there are no ops set on the server, the spawn protection will be disabled automatically.";
                case "view-distance":
                    return "Sets the amount of world data the server sends the client, measured in chunks in each direction of the player (radius, " +
                           "not diameter). It determines the server-side viewing distance. (see Render distance)";
                case "white-list":
                    return
                        "With a whitelist enabled, users not on the whitelist will be unable to connect. Intended for private servers, such as those for " +
                        "real-life friends or strangers carefully selected via an application process, for example.";
                case "enforce-whitelist":
                    return "Enforces the whitelist on the server. When this option is enabled, users who are not present on the whitelist(if it's enabled) " +
                           "will be kicked from the server after the server reloads the whitelist file.";
                case "server-ip":
                    return
                        "This is the ip you will be using. If you are not sure what this means, please leave it empty!";
                case "motd":
                    return
                        "This is the message the players see under the server name, when they are in the multiplayer menu inside Minecraft.";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns the dictionary containing the properties
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ToDictionary()
        {
            return _properties;
        }
        
    }
}
