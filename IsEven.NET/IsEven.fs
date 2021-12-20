module IsEven

open System.IO
open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization

let maxRetries = 3

type RetryHandler() = 
    inherit DelegatingHandler(new SocketsHttpHandler())

    // Expose protected method.
    member private _.SendAsync'(request, cancellationToken) = base.SendAsync(request, cancellationToken)

    override this.SendAsync(request, cancellationToken) = 
        let rec loop attempts = task {
            if attempts >= maxRetries then 
                return null
            else
                let! resp = this.SendAsync'(request, cancellationToken)
                if resp.IsSuccessStatusCode then 
                    return resp
                else 
                    return! loop (attempts + 1)
        }
        loop 0

let httpClient = new HttpClient(new RetryHandler())

type EvenResult = 
    { [<JsonPropertyName "iseven">]
      Even: bool 
      [<JsonPropertyName "ad">]
      Ad: string }

[<CompiledName "IsEven">]
let isEven number =
    if number > 999999 || number < 0 then 
        invalidArg (nameof number) "Number out of range. Upgrade to isEven API Premium or Enterprise."
    
    let response =
        new HttpRequestMessage(HttpMethod.Get, $"https://api.isevenapi.xyz/api/iseven/{number}")
        |> httpClient.Send

    use reader = new StreamReader(response.Content.ReadAsStream())

    reader.ReadToEnd()
    |> JsonSerializer.Deserialize<EvenResult>

[<CompiledName "IsEvenAsync">]
let isEvenAsync number = task {
    if number > 999999 || number < 0 then 
        invalidArg (nameof number) "Number out of range. Upgrade to isEven API Premium or Enterprise."
    
    let! stream = httpClient.GetStreamAsync $"https://api.isevenapi.xyz/api/iseven/{number}"
    return! JsonSerializer.DeserializeAsync<EvenResult> stream
}
    