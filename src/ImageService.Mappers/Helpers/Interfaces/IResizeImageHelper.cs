using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Helpers
{
    [AutoInject]
    public interface IResizeImageHelper
    {
        string Resize(string inputBase64, string extention);
    }
}
