using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnGames.Utils
{

    public interface ITicker
    {

        void Start();

        void DoTick();

        void DoFixedTick();

        void End();

    }

}
