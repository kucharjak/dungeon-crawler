using System.CodeDom;
using System.Linq;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Exceptions;
using Godot;

namespace DungeonCrawler.Extensions
{
    public static class NodeExtension
    {
        public static TType GetChildNode<TType>(this Node node, string childName) 
            where TType : Node
        {
            var childNode = node.GetChildNodeOrNull<TType>(childName);
            if (childNode is null)
            {
                LoggingComponent.Logger.Error($"Could not find node with name '{childName}' for parent '{node.GetPath()}'");   
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
                LoggingComponent.Logger.Error($"Could not find node of type '{typeof(TType).Name}' for parent '{node.GetPath()}'");
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