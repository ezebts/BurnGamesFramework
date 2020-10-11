using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BurnGames.DependencyInjection;
using BurnGames.IO.GestureRecognition;

namespace BurnGames.IO
{

    /// <summary>
    /// 
    /// A generic crossplatform input system with some useful
    /// features.
    /// 
    /// </summary>
    public interface IGameInput
    {

        int HorizontalInput { get; }

        int VerticalInput { get; }

        Gestures Gestures { get; }

        bool Debug { get; set; }

        void UpdateInputSystem();

    }

    public interface IGameInputFactory
    {
        IGameInput GetGameInputInstance();
    }

    public class GameInputContext
    {

        readonly static Dictionary<string, IGameInputFactory> implementations = new Dictionary<string, IGameInputFactory>();

        private static string ProviderKey(string providerKey)
        {
            return providerKey.ToLower().Trim();
        }

        public static void AddGameInputProvider(string providerKey, IGameInputFactory provider)
        {
            implementations[ProviderKey(providerKey)] = provider;
        }

        /// <summary>
        /// Gets new IGameInput instance
        /// </summary>
        /// <param name="providerKey">Key to look provider</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>IGameInput instance</returns>
        public IGameInput GetGameInputInstance(string providerKey)
        {

            var provider = ProviderKey(providerKey);

            if (implementations.ContainsKey(provider))
            {
                return implementations[provider].GetGameInputInstance();
            }
            else
            {
                throw new ArgumentException($"Input System '{ provider.ToString() }' isn't supported yet", nameof(providerKey));
            }

        }

    }

}
