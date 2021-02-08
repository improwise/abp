﻿namespace Volo.CmsKit
{
    public static class CmsKitErrorCodes
    {
        public static class Tags
        {
            public const string TagAlreadyExist = "CmsKit:Tag:0001";
            public const string EntityNotTaggable = "CmsKit:Tag:0002";
        }

        public const string ContentAlreadyExist = "CmsKit:0002";

        public static class Pages
        {
            public const string UrlAlreadyExist = "CmsKit:Page:0001";
        }

        public static class Blogs
        {
            public const string UrlSlugAlreadyExist = "CmsKit:BlogPost:0001";
        }
        
        public static class MediaDescriptors
        {
            public const string InvalidName = "CmsKit:Medias:0001";
        }
    }
}
