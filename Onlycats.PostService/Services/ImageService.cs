namespace Onlycats.PostService.Services
{
    using System;
    using System.IO;
    using Mono.Unix;
    using Mono.Unix.Native;

    public class ImageService
    {
        public string SaveImage(int userId, Stream imageStream, string imageName, string postId)
        {
            var date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var directoryPath = Path.Combine("images", userId.ToString(), date, postId);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                SetDirectoryPermissions(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, imageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                imageStream.CopyTo(fileStream);
            }

            return filePath;
        }

        private void SetDirectoryPermissions(string path)
        {
            var unixFileInfo = new UnixFileInfo(path);
            unixFileInfo.FileAccessPermissions = FileAccessPermissions.UserRead | FileAccessPermissions.UserWrite | FileAccessPermissions.UserExecute |
                                                 FileAccessPermissions.GroupRead | FileAccessPermissions.GroupWrite | FileAccessPermissions.GroupExecute |
                                                 FileAccessPermissions.OtherRead | FileAccessPermissions.OtherWrite | FileAccessPermissions.OtherExecute;
        }
    }

}
