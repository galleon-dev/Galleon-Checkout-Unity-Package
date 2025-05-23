using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Galleon.Checkout
{
    public class ExplorerItem : VisualElement
    {
        ///// Members
        
        public object Target;
        
        public VisualElement ChildrenHolder;
        public VisualElement InspectorHolder;
        
        public ButtonFoldout ButtonFoldout;
        
        ///// Properties
        
        public Explorer Explorer
        {
            get
            {
                return Explorer.Instance;
                
                // Search up the visual element hierarchy to find an Explorer
                VisualElement current = this;
                while (current != null)
                {
                    if (current is Explorer e)
                    {
                        return e;
                    }
                    current = current.parent;
                }
                return null;
            }
        }
        
        ///// Lifecycle
        
        public ExplorerItem(object target)
        {
            // Item Name
            
            this.Target = target;
            
            // General
            InitializeSelf();

            // Nested Inspectors
            InitializeNestedInspectors();

            // Populate 
            PopulateChildren();
        }
        
        public void InitializeSelf()
        {
            // Name
            
            string entityDisplayName = Target.GetType().Name;
            
            if (Target is IEntity e)
                entityDisplayName = e.Node.DisplayName;
            
            this.ButtonFoldout      = new ButtonFoldout(); this.Add(this.ButtonFoldout);            
            this.ButtonFoldout.Text = entityDisplayName;
            if (Target is IEntity en)
            {
                en.Node.ExplorerItem = this;
            }
            
            ChildrenHolder          = new VisualElement();
            this.ButtonFoldout.Add(ChildrenHolder);
            
            InspectorHolder         = new VisualElement();
            
            // On Button Click
            
            ButtonFoldout.Button.clicked += () =>
                                            {
                                                Explorer.SetSelectedEntity(Target as IEntity);
                                                this.RefrehseChildren();
                                            };

        }
        
        public void InitializeNestedInspectors()
        {
            
            try
            {
                // Check if the target type has a custom Inspector
                var targetType = Target.GetType();
                
                // Look for inspector types directly in the target type as nested classes first
                var inspectorTypes = targetType.GetNestedTypes()
                                               .Where(t => t.IsSubclassOf(typeof(Inspector)) 
                                                        && !t.IsAbstract                    
                                                      //&& t.BaseType.IsGenericType         
                                                      //&& t.BaseType.GetGenericArguments()[0] == targetType)
                                                      )
                                               .ToList();
                
                if (inspectorTypes.Any())
                {
                    // Create Inspector Instance
                    var inspectorType = inspectorTypes.First();
                    var inspector     = Activator.CreateInstance(inspectorType, Target) as Inspector;
                    
                    // Assign
                    if (Target is IEntity entity)
                    {
                        entity.Node.Inspector = inspector;
                        this.InspectorHolder.Clear();
                        this.InspectorHolder.Add(inspector);
                    }
                    
                    // Show
                    // if (inspector != null)
                    // {
                    //     inspector.Target = Target;
                    //     
                    //     VisualElement parentPanel = this.ButtonFoldout;
                    //     if (Explorer?.Mode == Explorer.DisplayMode.Horizontal)
                    //     {
                    //         parentPanel = this.InspectorHolder; //Explorer.InspectorScrollView;
                    //         parentPanel.Clear();
                    //     }
                    //     
                    //     parentPanel.Add(inspector);
                    // }
                }

            }
            catch (Exception ex)
            {
                Add(new HelpBox($"error with nested inspectors : \n{ex.ToString()}", HelpBoxMessageType.Error));
            }
            
        }
        
        public void PopulateChildren()
        {
            
            // Predefined Child Entities
            if (Target is IEntity e)
            {
                foreach (var child in e.Node.Children)
                {
                    var childItem = new ExplorerItem(child);
                    
                    #if UNITY_EDITOR
                    string header = child.Node.editorExtras.HeaderAttributeText;
                    if (header != null)
                        this.ButtonFoldout.Add(new Label(header));
                    #endif
                    
                    this.ChildrenHolder.Add(childItem);
                }
            }
        }
        
        public void RefrehseChildren()
        {
            if (this.ButtonFoldout.Foldout.value == false)
                return;
            
            this.ChildrenHolder.Clear();
            
            PopulateChildren();
        }
    }
}
