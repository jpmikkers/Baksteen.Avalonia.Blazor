namespace DemoApp.ViewModels;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class DoExitMessage : ValueChangedMessage<int>
{
    public DoExitMessage() : base(0)
    {        
    }
}
