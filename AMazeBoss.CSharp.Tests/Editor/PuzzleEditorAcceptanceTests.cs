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
        protected readonly PuzzleLayout _sut;
        protected readonly TilePos Node1Position = new TilePos(0, 0);
        protected readonly TilePos Node2Position = new TilePos(1, 0);
        protected readonly TilePos Node3Position = new TilePos(0, 1);
        protected readonly TilePos Node4Position = new TilePos(1, 1);
        protected int CallsToNodeAdded;
        protected int CallsToNodeRemoved;
        protected int CallsToConnectionRemoved;
        protected int CallsToConnectionAdded;

        public PuzzleEditorAcceptanceTests()
        {
            _sut = new PuzzleLayout();
        }

        public PuzzleEditorAcceptanceTests<T> ConnectingBetween(TilePos position1, TilePos position2)
        {
            var command = new AddNodeConnectionCommand(_sut, new NodeConnection(position1, position2));
            command.Execute();
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ConnectionBetween(TilePos position1, TilePos position2)
        {
            ConnectingBetween(position1, position2);
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ShouldHaveNodeAt(TilePos position, Action<Node> nodeAction)
        {
            _sut.Nodes.ContainsKey(position).Should().BeTrue("there should be node at " + position);
            nodeAction(_sut.Nodes[position]);
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ShouldHaveNodeCountOf(int count)
        {
            _sut.Nodes.Count.Should().Be(count);
            return this;
        }

        public void ShouldHaveTotalConnectionCountOf(int connectionCount)
        {
            _sut.Nodes.Values.SelectMany(x => x.Connections).Count().Should().Be(connectionCount);
        }

        public void RemovingConnectionBetween(TilePos position1, TilePos position2)
        {
            var command = new RemoveNodeConnectionCommand(_sut, new NodeConnection(position1, position2));
            command.Execute();
        }

        public PuzzleEditorAcceptanceTests<T> ListeningToEvents()
        {
            _sut.NodeAdded += node => CallsToNodeAdded++;
            _sut.ConnectionAdded += connection => CallsToConnectionAdded++;
            _sut.NodeRemoved += node => CallsToNodeRemoved++;
            _sut.ConnectionRemoved += node => CallsToConnectionRemoved++;
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ShouldHaveCalledNodeAdded(int callCount)
        {
            CallsToNodeAdded.Should().Be(callCount, "event should be called for each added node");
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ShouldHaveCalledConnectionAdded(int callCount)
        {
            CallsToConnectionAdded.Should().Be(callCount, "event should be called for each added connection");
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ShouldHaveCalledNodeRemoved(int callCount)
        {
            CallsToNodeRemoved.Should().Be(callCount, "event should be called for each removed node");
            return this;
        }

        public PuzzleEditorAcceptanceTests<T> ShouldHaveCalledConnectionRemoved(int callCount)
        {
            CallsToConnectionRemoved.Should().Be(callCount, "event should be called for each removed connection");
            return this;
        }
    }
}