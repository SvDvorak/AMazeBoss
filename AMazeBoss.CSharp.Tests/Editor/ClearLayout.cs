using Assets.Editor.Undo;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class ClearLayout : PuzzleEditorAcceptanceTests<ClearLayout>
    {
        [Fact]
        public void RemovesAllNodesAndCallsEventsWhenClearing()
        {
            Given
                .ConnectionBetween(Node1Position, Node2Position)
                .ListeningToEvents();

            When
                .ClearingLayout();

            Then
                .ShouldHaveNodeCountOf(0)
                .ShouldHaveCalledNodeRemoved(2)
                .ShouldHaveCalledConnectionRemoved(1);
        }

        public ClearLayout ClearingLayout()
        {
            new ClearLayoutCommand(_sut).Execute();
            return this;
        }
    }
}