using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Editor.Undo;
using Assets.LevelEditorUnity;
using FluentAssertions;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class PuzzleEditorAcceptanceTests<T> : AcceptanceTests<T> where T : PuzzleEditorAcceptanceTests<T>
    {
        protected readonly PuzzleLayout Sut;
        protected readonly TilePos Node1Position = new TilePos(0, 0);
        protected readonly TilePos Node2Position = new TilePos(1, 0);
        protected readonly TilePos Node3Position = new TilePos(0, 1);
        protected readonly TilePos Node4Position = new TilePos(1, 1);
        protected int CallsToLayoutChanged;
        protected int CallsToNodeAdded;
        protected int CallsToNodeRemoved;
        protected int CallsToConnectionRemoved;
        protected int CallsToConnectionAdded;
        protected ICommand LastCommand;

        public PuzzleEditorAcceptanceTests()
        {
            Sut = new PuzzleLayout();
        }

        public PuzzleEditorAcceptanceTests<T> ConnectingBetween(TilePos position1, TilePos position2)
        {
            LastCommand = new AddNodeConnectionCommand(Sut, new NodeConnection(position1, position2));
            LastCommand.Execute();
            return this;
        }

        public T ConnectionBetween(TilePos position1, TilePos position2)
        {
            ConnectingBetween(position1, position2);
            return This;
        }

        public T ShouldHaveNodeAt(TilePos position, Action<Node> nodeAction)
        {
            Sut.Nodes.ContainsKey(position).Should().BeTrue("there should be node at " + position);
            nodeAction(Sut.Nodes[position]);
            return This;
        }

        public T ShouldHaveNodeCountOf(int count)
        {
            Sut.Nodes.Count.Should().Be(count);
            return This;
        }

        public void ShouldHaveTotalConnectionCountOf(int connectionCount)
        {
            Sut.Nodes.Values.SelectMany(x => x.Connections).Count().Should().Be(connectionCount);
        }

        public T RemovingConnectionBetween(TilePos position1, TilePos position2)
        {
            LastCommand = new RemoveNodeConnectionCommand(Sut, new NodeConnection(position1, position2));
            LastCommand.Execute();
            return This;
        }

        public T ListeningToEvents()
        {
            Sut.LayoutChanged += () => CallsToLayoutChanged++;
            Sut.NodeAdded += node => CallsToNodeAdded++;
            Sut.ConnectionAdded += connection => CallsToConnectionAdded++;
            Sut.NodeRemoved += node => CallsToNodeRemoved++;
            Sut.ConnectionRemoved += node => CallsToConnectionRemoved++;
            return This;
        }

        public T ShouldHaveCalledLayoutChanged(int callCount)
        {
            CallsToLayoutChanged.Should().Be(callCount, "event should be called each layout change");
            return This;
        }

        public T ShouldHaveCalledNodeAdded(int callCount)
        {
            CallsToNodeAdded.Should().Be(callCount, "event should be called for each added node");
            return This;
        }

        public T ShouldHaveCalledConnectionAdded(int callCount)
        {
            CallsToConnectionAdded.Should().Be(callCount, "event should be called for each added connection");
            return This;
        }

        public T ShouldHaveCalledNodeRemoved(int callCount)
        {
            CallsToNodeRemoved.Should().Be(callCount, "event should be called for each removed node");
            return This;
        }

        public T ShouldHaveCalledConnectionRemoved(int callCount)
        {
            CallsToConnectionRemoved.Should().Be(callCount, "event should be called for each removed connection");
            return This;
        }
    }
}