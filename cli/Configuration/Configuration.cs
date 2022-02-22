// Bamboo (c) by Tangram
//
// Bamboo is licensed under a
// Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License.
//
// You should have received a copy of the license along with this
// work. If not, see <http://creativecommons.org/licenses/by-nc-nd/4.0/>.

using System;
using System.IO;
using BAMWallet.HD;
using Cli.UI;

namespace Cli.Configuration
{
    public class Configuration
    {
        private IUserInterface _userInterface;

        public Configuration(IUserInterface userInterface)
        {
            _userInterface = userInterface;

            var networkConfiguration = new Network(userInterface);
            if (!networkConfiguration.Do())
            {
                Cancel();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Environment       : " + networkConfiguration.Configuration.Environment);
            Console.WriteLine("Wallet API port   : " + networkConfiguration.Configuration.WalletPort);
            Console.WriteLine("Node API address  : " + networkConfiguration.Configuration.NodeIPAddress);
            Console.WriteLine("Node API port     : " + networkConfiguration.Configuration.NodePort);
            Console.WriteLine("Node Public Key   : " + networkConfiguration.Configuration.NodePubKey);
            Console.WriteLine();

            var configTemplate = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "Templates", Constant.AppSettingsFile));
            var config = configTemplate
                .Replace("<ENVIRONMENT>", networkConfiguration.Configuration.Environment)
                .Replace("<WALLET_ENDPOINT_BIND>",
                    $"http://{networkConfiguration.Configuration.WalletIPAddress}:{networkConfiguration.Configuration.WalletPort.ToString()}")
                .Replace("<NODE_ENDPOINT>",
                    $"{networkConfiguration.Configuration.NodeIPAddress}:{networkConfiguration.Configuration.NodePort}")
                .Replace("<NODE_PUBKEY>", $"{networkConfiguration.Configuration.NodePubKey}");

            var configFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constant.AppSettingsFile);
            File.WriteAllText(configFileName, config);

            Console.WriteLine($"Configuration written to {configFileName}");
            Console.WriteLine();
        }

        private void Cancel()
        {
            var section = new UserInterfaceSection(
                "Cancel configuration",
                "Configuration cancelled",
                null);

            _userInterface.Do(section);
        }
    }
}