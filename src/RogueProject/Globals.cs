using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueProject
{
    public static class Globals
    {
        public static int COL_GRID = 50;
        public static int ROW_GRID = 25;
        public static int DEFAULT_WIDTH = 1920;
        public static int DEFAULT_HEIGHT = 1080;
        public static int DEFAULT_WIDTH_SCALE = 1;
        public static int DEFAULT_HEIGHT_SCALE = 1;
        public static int NUMBER_OF_ROOM = 10;

        public static float m_CurrentWidthScale = DEFAULT_WIDTH_SCALE;
        public static float m_CurrentHeightScale = DEFAULT_HEIGHT_SCALE;
        public static float m_CurrentWidth = DEFAULT_WIDTH;
        public static float m_CurrentHeight = DEFAULT_HEIGHT;
        public static List<string> m_Message = new List<string>();

    }
}
