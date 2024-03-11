using Application.Interface;
using System.Net;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Infrasturcture.shared.Models;

namespace Infrasturcture.shared.Services
{
    public class PictureHandler : IPictureHandler
    {

        public string ConvertBase64ToImage(string ImageName, string folderPath)
        {
            try
            {
                string ProfileImage = "";
                if (!string.IsNullOrEmpty(ImageName))
                {
                    string webRootPath = "";

                    //get root path
                    webRootPath = Directory.GetCurrentDirectory();

                    //getting data from base64 url
                    var base64Data = Regex.Match(ImageName, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

                    //getting extension of the image
                    string extension = Regex.Match(ImageName, @"data:image/(?<type>.+?),(?<data>.+)").Groups["type"].Value.Split(';')[0];

                    extension = "." + extension;
                    //folderPath= "\Images\photos\"
                    if (!Directory.Exists(webRootPath + folderPath))
                    {
                        Directory.CreateDirectory(webRootPath + folderPath);
                    }
                    string picName = Guid.NewGuid().ToString();
                    List<ImageModel> obj = new List<ImageModel>();

                    ImageModel img = new ImageModel();

                    img.Base64 = base64Data;
                    img.ImageUrl = webRootPath + folderPath + picName + extension;

                    obj.Add(img);

                    SaveImageAndThumb(obj);

                    ProfileImage = picName + extension;
                    //ProfileImage = base64Data;


                }
                else if (string.IsNullOrEmpty(ImageName))
                {
                    ProfileImage = string.Empty;

                }
                return ProfileImage;
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
        public static void SaveImageAndThumb(dynamic obj)
        {
            foreach (var item in obj)
            {
                string imageURL = item.ImageUrl;
                byte[] bytes = Convert.FromBase64String(item.Base64);

                System.Drawing.Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    try
                    {
                        image = System.Drawing.Image.FromStream(ms);
                        image.Save(imageURL);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception();
                    }
                }
            }
        }
    }
}
