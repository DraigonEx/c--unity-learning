using Assets.Scripts.model.GameBoard.types;
using System.Collections;

namespace Assets.Scripts.model.GameBoard
{
    interface ICard : IEnumerable
    {
        float sequenceNumber { get; }
        Biomes Biome { get; set; }
    }
}
