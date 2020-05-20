using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnGames.IO
{

    public enum InputSystemType
    {

        Standard,
        Mobile

    }

    public class GameInputContext
    {

        public IGameInput GameInput { get; private set; }

        public void ChangeGameInputSystem(IGameInput newGameInput)
        {
            GameInput = newGameInput;
        }

        public virtual GameInputContext LoadDefaultInputFor(InputSystemType type)
        {

            if (type == InputSystemType.Mobile)
            {
                GameInput = new GameMobileInput();
            }

            else if (type == InputSystemType.Standard)
            {
                GameInput = new GameStandardInput();
            }

            else
            {
                throw new NotImplementedException($"Input System '{ type.ToString() }' isn't supported yet");
            }

            return this;

        }

        public GameInputContext() { }

        public GameInputContext(IGameInput gameInput)
        {
            GameInput = gameInput;
        }

    }

}
