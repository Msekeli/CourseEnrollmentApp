namespace CourseEnrollmentApp.Client.Services
{
    public class ToastService
    {
        public event Action<string, string>? OnShow;

        public void ShowSuccess(string message)
            => OnShow?.Invoke("success", message);

        public void ShowError(string message)
            => OnShow?.Invoke("danger", message);
    }
}
