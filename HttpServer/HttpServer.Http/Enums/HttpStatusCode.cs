namespace HttpServer.Http.Enums
{
    public enum HttpStatusCode
    {
        Continue = 100,
        Ok = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        ServerError = 500,
        NotImplemented = 501,
        LoopDetected = 508
    }
}