using System.Text.Json;

namespace Backend.BusinessMethod
{
    static public class StaticControllerMethod
    {
        static public string SerealizationToJSON<T>(IList<T> lst)
        {
            return JsonSerializer.Serialize(lst);
        }
    }
}
