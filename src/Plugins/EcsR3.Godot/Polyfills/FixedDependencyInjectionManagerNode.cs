using System.Linq;
using EcsR3.Godot.Plugins.EcsR3.Godot.Applications;
using Godot;
using Godot.DependencyInjection.Injection;

namespace EcsR3.Godot.Plugins.EcsR3.Godot.Polyfills;

// This works around an issue with the Godot DI framework
public abstract partial class FixedDependencyInjectionManagerNode : Node
{
    private InjectionService _injectionService = null!;

    /// <summary>
    /// Called when the node is ready.
    /// </summary>
    public override void _EnterTree()
    {
        var tree = GetTree();
        (_injectionService, var nodesToInject) = InjectionServiceFactory.Create(new FixedNodeWrapper(tree.Root));

        var unpackedNodes = nodesToInject.Select(x => ((FixedNodeWrapper)x).Node);
        foreach (var node in unpackedNodes)
        {
            _injectionService.InjectDependencies(node);
        }
        tree.NodeAdded += _injectionService.InjectDependencies;
    }
    
}