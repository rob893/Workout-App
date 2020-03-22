namespace WorkoutApp.API.Models.Dtos
{
    public abstract class DtoWithUrls
    {
        protected readonly string baseUrl;


        public DtoWithUrls(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
    }
}