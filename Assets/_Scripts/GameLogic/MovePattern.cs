using System.Collections.Generic;

namespace GameLogic
{
    public class MovePattern
    {
        /*
         * В чём идея: тут будут записан порядок ходов, которые собирается сделать юнит.
         * Значения в List-e упорядочены.
         */
        public List<List<Coordinate>> moveSequences;

        public MovePattern(List<List<Coordinate>> moveSequences)
        {
            this.moveSequences = moveSequences;
        }
    }
}