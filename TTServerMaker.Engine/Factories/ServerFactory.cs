// <copyright file="ServerFactory.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Factories;
/// <summary>
/// Creates server objects.
/// </summary>
internal class ServerFactory
{
    /*
    /// <summary>
    /// Creates a new server instance with the appropriate server based on the server settings.
    /// </summary>
    /// <param name="serverSettings">The server settings contatining the type of the new server.</param>
    /// <returns>New server instance.</returns>
    public static ServerBase CreateNewServerInstance(ServerSettings serverSettings)
    {
        Type serverType = default;
        switch (serverSettings.ServerType)
        {
            case ServerType.Vanilla:
                serverType = typeof(VanillaServer);
                break;
            case ServerType.Forge:
                serverType = typeof(ForgeServer);
                break;
        }

        ServerBase newServer = Activator.CreateInstance(serverType, serverSettings) as ServerBase;
        return newServer;
    }

    /// <summary>
    /// Creates a brand new server folder.
    /// </summary>
    /// <param name="serverName">The name of the server.</param>
    /// <param name="typeString">The string representation of the server type.</param>
    /// <returns>Returns the server settings stored in the new folder.</returns> // TODO weird?
    internal static async Task<ServerSettings> CreateNewServerFolderAsync(string serverName, string typeString)
    {
        string folderName = MakeStringFoldernameFriendly(serverName);

        // Making sure the folder doesn't exist yet
        string newDir = AppSettings.GeneralSettings.ServerFoldersPath + folderName;

        while (Directory.Exists(newDir))
        {
            newDir += "v2";
        }

        // Creating directory
        Directory.CreateDirectory(newDir);

        ServerSettings basicInfo = new ServerSettings
        {
            ServerFolderPath = newDir,
            Name = serverName,
            DateCreated = DateTime.Now,
            DateLastLoaded = DateTime.Now,
        };

        basicInfo.ServerImagePath = basicInfo.ServerImagePath;

        // Saving the new basic server info to file
        await basicInfo.SaveChangesAsync();

        return basicInfo;
    }

    /// <summary>
    /// Makes any string foldername friendly by replacing/removing forbidden characters. (e.g: "#Árvíz néző" => "ArvizNezo").
    /// </summary>
    /// <param name="folderName">The string to convert.</param>
    /// <returns>Directory name friendly string.</returns>
    private static string MakeStringFoldernameFriendly(string folderName)
    {
        // Capitalizing letters after spaces, for better readability
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        folderName = textInfo.ToTitleCase(folderName);

        // Removing forbidden characters from the server name
        List<char> forbiddenCharacters = new List<char>(Path.GetInvalidPathChars());
        forbiddenCharacters.Add(' ');
        forbiddenCharacters.Add('\\');

        foreach (char c in forbiddenCharacters)
        {
            folderName = folderName.Replace(c.ToString(), string.Empty);
        }

        // Credit goes to Julien Roncaglia
        // https://stackoverflow.com/a/5459738/2154120
        folderName = Regex.Replace(folderName, "[éèëêð]", "e");
        folderName = Regex.Replace(folderName, "[ÉÈËÊ]", "E");
        folderName = Regex.Replace(folderName, "[àâä]", "a");
        folderName = Regex.Replace(folderName, "[ÀÁÂÃÄÅ]", "A");
        folderName = Regex.Replace(folderName, "[àáâãäå]", "a");
        folderName = Regex.Replace(folderName, "[ÙÚÛÜ]", "U");
        folderName = Regex.Replace(folderName, "[ùúûüµ]", "u");
        folderName = Regex.Replace(folderName, "[òóôõöø]", "o");
        folderName = Regex.Replace(folderName, "[ÒÓÔÕÖØ]", "O");
        folderName = Regex.Replace(folderName, "[ìíîï]", "i");
        folderName = Regex.Replace(folderName, "[ÌÍÎÏ]", "I");
        folderName = Regex.Replace(folderName, "[š]", "s");
        folderName = Regex.Replace(folderName, "[Š]", "S");
        folderName = Regex.Replace(folderName, "[ñ]", "n");
        folderName = Regex.Replace(folderName, "[Ñ]", "N");
        folderName = Regex.Replace(folderName, "[ç]", "c");
        folderName = Regex.Replace(folderName, "[Ç]", "C");
        folderName = Regex.Replace(folderName, "[ÿ]", "y");
        folderName = Regex.Replace(folderName, "[Ÿ]", "Y");
        folderName = Regex.Replace(folderName, "[ž]", "z");
        folderName = Regex.Replace(folderName, "[Ž]", "Z");
        folderName = Regex.Replace(folderName, "[Ð]", "D");
        folderName = Regex.Replace(folderName, "[œ]", "oe");
        folderName = Regex.Replace(folderName, "[Œ]", "Oe");

        return folderName;
    }
    */
}