namespace Onlycats.UserService.Services
{
    using System.IO;
    using Mono.Unix;

    public class ImageService
    {
        public string SaveImage(string username, Stream imageStream, string imageName)
        {
            var directoryPath = Path.Combine("profile_pic", username);

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
