using System;

namespace Splatrika.StackClone.Model
{
    public interface ITower
    {
        Block Last { get; }
        Block LastUncutted { get; }
        bool IsFinished { get; }

        delegate void BlockAddedAction(Block block, bool perfect);

        event Action Finished;
        event BlockAddedAction BlockAdded;

        void AddBlock(Block block, out bool perfect, out bool finished);
    }
}
