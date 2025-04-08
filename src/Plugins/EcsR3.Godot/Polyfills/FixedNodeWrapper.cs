using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.DependencyInjection.Injection;

namespace EcsR3.Godot.Plugins.EcsR3.Godot.Polyfills;

internal readonly struct FixedNodeWrapper : INodeWrapper
{
    object INodeWrapper.RawNode => Node;

    public readonly Node Node;

    public FixedNodeWrapper(Node node)
    {
        this.Node = node;
    }

    public IEnumerable<INodeWrapper> GetChildren()
    {
        return Node.GetChildren(true).Select<Node, INodeWrapper>(x => new FixedNodeWrapper(x));
    }
}