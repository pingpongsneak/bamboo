// Bamboo (c) by Tangram
//
// Bamboo is licensed under a
// Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License.
//
// You should have received a copy of the license along with this
// work. If not, see <http://creativecommons.org/licenses/by-nc-nd/4.0/>.

using System;
using System.Threading.Tasks;

using McMaster.Extensions.CommandLineUtils;

namespace CLi.ApplicationLayer.Commands.Wallet
{
    [CommandDescriptor("version", "Running version")]
    public class WalletVersionCommand : Command
    {
        public WalletVersionCommand(IServiceProvider serviceProvider)
            : base(typeof(WalletVersionCommand), serviceProvider)
        {
        }

        public override Task Execute()
        {
            _console.WriteLine($"{BAMWallet.Helper.Util.GetAssemblyVersion()}");
            return Task.CompletedTask;
        }
    }
}