using Assets;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class LayoutEvents : PuzzleEditorAcceptanceTests<LayoutEvents>
    {
        [Fact]
        public void CallsLayoutChangedEvents()
        {
            Given
                .ListeningToEvents();

            When
                .ConnectingBetween(Node1Position, Node2Position)
                .RemovingConnectionBetween(Node1Position, Node2Position)
                .ObjectAt("object", Node1Position)
                .SettingProperty(Node1Position, "Key", "Value");

            Then
                .ShouldHaveCalledLayoutChanged(4)
                .ShouldHaveCalledNodeAdded(2)
                .ShouldHaveCalledConnectionAdded(1)
                .ShouldHaveCalledNodeRemoved(2)
                .ShouldHaveCalledConnectionRemoved(1);
        }
    }
}