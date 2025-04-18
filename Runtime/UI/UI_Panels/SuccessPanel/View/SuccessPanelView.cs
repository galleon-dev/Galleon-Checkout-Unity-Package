using System.Threading.Tasks;
using UnityEngine;

namespace Galleon.Checkout.UI
{
    public class SuccessPanelView : View
    {
        //////////////////////////////////////////////////////////////////////////// View Result
        
        public      ViewResult Result = ViewResult.None;
        public enum ViewResult
        {
            None,
            Back,
            Confirm,
        }
        
        //////////////////////////////////////////////////////////////////////////// View Flow

        public bool IsCompleted = false;

        public Step View()
        =>
            new Step(name   : $"view_success_panel"
                    ,action : async (s) =>
                    {
                        IsCompleted = false;
                        
                        this.gameObject.SetActive(true);
                        
                        while (!IsCompleted)
                            await Task.Yield();
                        
                        this.gameObject.SetActive(false);
                    });
        
        //////////////////////////////////////////////////////////////////////////// UI Events
        
        public void OnConfirmSuccessButtonClick()
        {
            IsCompleted = true;
            Result      = ViewResult.Confirm;
        }
    }
}
