namespace GalleryServer
{

    public class AppSettings
    {
        public string Secret { get; set; }

        public string CloudName { get; set; }

        public string APIKey { get; set; }

        public string APISecret { get; set; }

        public int CountPostsPerPage { get; set; }
    }
}
