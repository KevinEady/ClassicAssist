using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using static ClassicAssist.Data.Filters.RepeatedMessagesFilter;

namespace ClassicAssist.UI.Views.Filters
{
  public class RepeatedMessagesFilterConfigureWindow : Window
  {

    public RepeatedMessagesFilterConfigureWindow()
    {

    }
    public RepeatedMessagesFilterConfigureWindow(MessageFilterOptions options)
    {
      this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
