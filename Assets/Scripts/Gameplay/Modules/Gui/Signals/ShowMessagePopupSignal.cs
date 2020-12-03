namespace Gameplay.Modules.Gui.Signals
{
    public class ShowMessagePopupSignal
    {
        public string Message { get; }
        
        public ShowMessagePopupSignal(string message)
        {
            Message = message;
        }

        
    }
}