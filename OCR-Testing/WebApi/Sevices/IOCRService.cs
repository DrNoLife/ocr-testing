namespace WebApi.Sevices;

public interface IOCRService
{
    List<string> ProcessImage(string filePath);
}
