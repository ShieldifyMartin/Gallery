﻿namespace GalleryServer.Data
{
    public class Validation
    {
        public class Post
        {
            public const int MaxDescriptionLength = 2000;
            public const int MaxLocationLength = 40;
        }

        public class User
        {
            public const int MaxNameLength = 40;
            public const int MaxBiographyLength = 150;
        }
    }
}
