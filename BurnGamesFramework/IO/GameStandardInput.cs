using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace BurnGames.IO
{

    public class GameStandardInput : BaseGameInput
    {

        public override void UpdateInputSystem()
        {

            float x = Input.GetAxis("Horizontal");

            // normalized input
            HorizontalInput = (x > 0) ? 1 : (x < 0) ? -1 : 0;

            VerticalInput = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;

        }

    }

    public class GameStandardInputFactory : IGameInputFactory
    {

        public static GameStandardInputFactory Instance { get; } = new GameStandardInputFactory();

        public IGameInput GetGameInputInstance()
        {
            return new GameStandardInput();
        }

        GameStandardInputFactory()
        {
            GameInputContext.AddGameInputProvider("standard", this);
        }

    }

}
