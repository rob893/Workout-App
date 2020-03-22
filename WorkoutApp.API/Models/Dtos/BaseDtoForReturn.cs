using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Models.Dtos
{
    public abstract class BaseDtoForReturn : IIdentifiable
    {
        public int Id { get; set; }
        public string Url { get => $"{baseUrl}/{controllerUrl}/{Id}"; }
        public string DetailedUrl { get => $"{Url}/detailed"; }

        protected readonly string baseUrl;
        protected readonly string controllerUrl;


        public BaseDtoForReturn(string baseUrl, string controllerUrl)
        {
            this.baseUrl = baseUrl;
            this.controllerUrl = controllerUrl;
        }
    }
}