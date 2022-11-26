using BTP_API.ViewModels;

namespace BTP_API.Ultils
{
    public class UploadFile
    {
        /*public string UploadBookImage(BookVM bookVM, IWebHostEnvironment environment)
        {
            string fileName;
            if (bookVM != null)
            {
                string uploadDir = Path.Combine(environment.WebRootPath, "BookImage");
                fileName = bookVM.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    bookVM.Image.CopyTo(fileStream);
                }

                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                fileName = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray);
            }
            else
            {
                fileName = "empty";
            }
            return fileName;
        }*/

       /* public string UploadPostImage(PostVM postVM, IWebHostEnvironment environment)
        {
            string fileName;
            if (postVM != null)
            {
                //byte[] bytes = Convert.FromBase64String(postVM.Image.FileName);
                //fileName = Convert.ToBase64String(bytes);

                //using (var fileStream = new FileStream(postVM.Image.FileName, FileMode.OpenOrCreate)) ;
                //byte[] bytes = new byte[postVM.Image.Length];
                //fileName = Convert.ToBase64String(bytes);

                string uploadDir = Path.Combine(environment.WebRootPath, "PostImage");
                fileName = postVM.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    postVM.Image.CopyTo(fileStream);
                }
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                fileName = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray);
            }
            else
            {
                fileName = "empty";
            }
            return fileName;
        }*/

        /*public string UploadUserImage(IFormFile Avatar, IWebHostEnvironment environment)
        {
            string fileName;
            if (Avatar != null)
            {
                string uploadDir = Path.Combine(environment.WebRootPath, "UserImage");
                fileName = Avatar.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Avatar.CopyTo(fileStream);
                }

                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                fileName = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray);
            }
            else
            {
                fileName = "empty";
            }
            return fileName;
        }*/
    }
}
