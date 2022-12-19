using System.Text;
using Newtonsoft.Json;

class GFG
{
    // Main Method without any
    // access modifier
    static async Task Main()
    {
        int current_hour = DateTime.Now.Hour;
        int current_minute = DateTime.Now.Minute;

        using HttpClient client = new HttpClient();
        string uri = @"http://localhost:8989";

        //get
        string get_endpoint = @"/test_get_method";
        string get_result = await client.GetStringAsync($"{uri}{get_endpoint}?hour={current_hour}&minute={current_minute}");

        //post
        string post_endpoint = @"/test_post_method";
        var post_body = new { hour = current_hour, minute = current_minute, requestId = get_result };
        StringContent post_body_json = new StringContent(JsonConvert.SerializeObject(post_body), Encoding.UTF8, "application/json");
        HttpResponseMessage post_result = await client.PostAsync(uri + post_endpoint, post_body_json);

        //put
        string put_endpoint = @"/test_put_method";
        var put_body = new { hour = (current_hour + 21) % 24, minute = (current_minute + 13) % 60};
        StringContent put_body_json = new StringContent(JsonConvert.SerializeObject(put_body), Encoding.UTF8, "application/json");
        HttpResponseMessage put_result = await client.PutAsync(uri + put_endpoint + $"?id={post_result.Content.ReadAsStringAsync().Result}", put_body_json);

        //delete
        string delete_endpoint = @"/test_delete_method";
        HttpResponseMessage delete_result = await client.DeleteAsync(uri + delete_endpoint + $"?id={put_result.Content.ReadAsStringAsync().Result}");
    }
}