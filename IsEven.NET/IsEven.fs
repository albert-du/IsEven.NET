module IsEven

open System
open System.IO
open System.Net
open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization

#if NETSTANDARD2_0 
ServicePointManager.FindServicePoint(Uri "https://api.isevenapi.xyz/api/iseven").ConnectionLeaseTimeout <- 60 * 1000
#endif

let maxRetries = 3

type RetryHandler() = 
#if NETSTANDARD2_0
    inherit DelegatingHandler(new HttpClientHandler())
#else
    inherit DelegatingHandler(new SocketsHttpHandler())
#endif

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
#if !NETSTANDARD2_0
    member private _.Send'(request, cancellationToken) = base.Send(request, cancellationToken)    
    override this.Send(request, cancellationToken) = 
        let rec loop attempts = 
            if attempts >= maxRetries then
                null
            else
                let resp = this.Send'(request, cancellationToken)
                if resp.IsSuccessStatusCode then
                    resp
                else 
                    loop (attempts + 1)
        loop 0
#endif
    
let httpClient = new HttpClient(new RetryHandler())

type EvenResult = 
    { [<JsonPropertyName "iseven">]
      Even: bool 
      [<JsonPropertyName "ad">]
      Ad: string }

[<CompiledName "IsEvenAsync">]
let isEvenAsync number = task {
    if number > 999999 || number < 0 then 
        invalidArg (nameof number) "Number out of range. Upgrade to isEven API Premium or Enterprise."
    
    let! stream = httpClient.GetStreamAsync $"https://api.isevenapi.xyz/api/iseven/{number}"
    return! JsonSerializer.DeserializeAsync<EvenResult> stream
}

[<CompiledName "IsEven">]
let isEven number =
    if number > 999999 || number < 0 then 
        invalidArg (nameof number) "Number out of range. Upgrade to isEven API Premium or Enterprise."
#if NETSTANDARD2_0
    let task = task { return! httpClient.GetStreamAsync $"https://api.isevenapi.xyz/api/iseven/{number}" }
    task.Wait()
    JsonSerializer.Deserialize<EvenResult> task.Result 
#else
    let response =
        new HttpRequestMessage(HttpMethod.Get, $"https://api.isevenapi.xyz/api/iseven/{number}")
        |> httpClient.Send

    use reader = new StreamReader(response.Content.ReadAsStream())

    reader.ReadToEnd()
    |> JsonSerializer.Deserialize<EvenResult>
#endif
    