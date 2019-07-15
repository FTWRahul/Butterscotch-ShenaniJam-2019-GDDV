using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking.Match;

namespace Assets.Scripts
{
    public static class AviliableMatchesList
    {
        public static event Action<List<MatchInfoSnapshot>> OnAvaliableMatchesChanged = delegate { };

        private static List<MatchInfoSnapshot> matches = new List<MatchInfoSnapshot>();

        internal static void HandleNewMatchList(List<MatchInfoSnapshot> matchList)
        {
            matches = matchList;
            OnAvaliableMatchesChanged(matches);
        }
    }
}
