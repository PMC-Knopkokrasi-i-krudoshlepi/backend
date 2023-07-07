namespace DPOBackend.Models.UserModels;

public class GroupRegistrationResponseModel
{
    public GroupRegistrationResponseModel(UserModel user)
    {
        Name = user.Name;
        Code = user.Code;
    }

    public string Name { get; set; }
    public string Code { get; set; }
}