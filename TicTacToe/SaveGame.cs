using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class SaveGame
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public int TotalGamesPlayed
        {
            get
            {
                return Wins + Losses + Draws;
            }
        }

        public float WinRatio
        {
            get
            {
                if (TotalGamesPlayed == 0)
                {
                    return 0;
                }

                return (float)Wins / TotalGamesPlayed;
            }
        }

        public SaveGame()
        {
            try
            {
                string[] stats = File.ReadAllText("savegame").Split(',');
                Wins = int.Parse(stats[0]);
                Losses = int.Parse(stats[1]);
                Draws = int.Parse(stats[2]);
            }

            catch (Exception e)
            {
                Wins = 0;
                Losses = 0;
                Draws = 0;
            }
        }

        public void SaveToDisk()
        {
            string data = String.Format("{0},{1},{2}", Wins, Losses, Draws);
            File.WriteAllText("savegame", data);
        }
    }
}
