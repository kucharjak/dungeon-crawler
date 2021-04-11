using System;
using System.CodeDom;
using System.Linq;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Exceptions;
using Godot;

namespace DungeonCrawler.Extensions
{
    public static class NodeExtension
    {
        public static void ChangeValueAndEmitSignal<TValue>(this Node node, string signalName, ref TValue changedValueRef, TValue newValue)
            where TValue : struct
        {
            var oldValue = changedValueRef;

            changedValueRef = newValue;
            
            if (!newValue.Equals(oldValue))
                node.EmitSignal(signalName, oldValue, newValue);
        }
        
        public static TType GetParentNodeRecurse<TType>(this Node node, string parentName) 
            where TType : Node
        {
            Node parent = null;
            while ((parent = node.GetParent()) != null)
            {
                if (parent.Name.Equals(parentName))
                    return (TType) parent;
            }
            
            LoggingComponent.Logger.Error($"Could not find parent node with name '{parentName}' for node '{node.GetPath()}'");

            return null;
        }
        
        public static TType GetParentNodeRecurse<TType>(this Node node)
            where TType : Node
        {
            var searchedForType = typeof(TType);
            
            Node parent = null;
            while ((parent = node.GetParent()) != null)
            {
                var parentType = parent.GetType();
                if (parentType == searchedForType || parentType.IsSubclassOf(searchedForType))
                    return (TType) parent;
            }
            
            LoggingComponent.Logger.Error($"Could not find parent node ot type '{nameof(TType)}' for node '{node.GetPath()}'");

            return null;
        }
        
        public static TType GetChildNode<TType>(this Node node, string childName) 
            where TType : Node
        {
            var childNode = node.GetChildNodeOrNull<TType>(childName);
            if (childNode is null)
            {
                LoggingComponent.Logger.Error($"Could not find child node with name '{childName}' for node '{node.GetPath()}'");   
            }

            return childNode;
        }
        
        public static TType GetChildNodeOrNull<TType>(this Node node, string childName) 
            where TType : Node
        {
            return node.GetNodeOrNull<TType>(childName);
        }
        
        public static TType GetChildNode<TType>(this Node node) 
            where TType : Node
        {
            var childNode = node.GetChildNodeOrNull<TType>();
            if (childNode is null)
            {
                // LoggingComponent.Logger.Error($"Could not find child node of type '{typeof(TType).Name}' for node '{node.GetPath()}'");
                throw new MissingChildNodeException($"Could not find child node of type '{typeof(TType).Name}' for node '{node.GetPath()}'");
            }

            return childNode;
        }
        
        public static TType GetChildNodeOrNull<TType>(this Node node) 
            where TType : Node
        {
            var children = node.GetChildren().Cast<Node>();
            var childNode = children.FirstOrDefault(chl => chl.GetType() == typeof(TType));

            return childNode as TType;
        }
    }
}