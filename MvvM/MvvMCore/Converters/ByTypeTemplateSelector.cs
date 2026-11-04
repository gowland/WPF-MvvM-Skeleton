using System.Windows;
using System.Windows.Controls;

namespace MvvMCore.Converters;

/*
 * Usage example:
 *
   <ui:ByTypeTemplateSelector x:Key="StateSelector" DefaultTemplate="{StaticResource NoStateTemplate}">
      <ui:ByTypeTemplateSelector.Options>
          <x:Array Type="{x:Type ui:TemplateSelectorByTypeOption}">
            <ui:TemplateSelectorByTypeOption Type="{x:Type exceptionState:PolarionExceptionState}" Template="{StaticResource PolarionStateTemplate}"/>
            <ui:TemplateSelectorByTypeOption Type="{x:Type exceptionState:UserStoryExceptionState}" Template="{StaticResource UserStoryStateTemplate}"/>
            <ui:TemplateSelectorByTypeOption Type="{x:Type exceptionState:EnhancedExceptionDialogExceptionState}" Template="{StaticResource EhnancedExceptionDialogStateTemplate}"/>
          </x:Array>
      </ui:ByTypeTemplateSelector.Options>
    </ui:ByTypeTemplateSelector>
 */
public class ByTypeTemplateSelector : DataTemplateSelector
{
    public DataTemplate? DefaultTemplate { get; set; }
    public IEnumerable<TemplateSelectorByTypeOption> Options { get; set; } = [];

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        var itemType = item?.GetType();
        var matchingTemplate = Options.FirstOrDefault(option => option.Type == itemType)?.Template;

        return matchingTemplate ?? DefaultTemplate;
    }
}
