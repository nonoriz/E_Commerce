using System.Text.Json.Serialization;

namespace E_Commerce.Application.Common
{
    public sealed record Error(string Code, string Description ,ErrorType ErrorType=ErrorType.Failure )
    {
        public static Error Failure(string Code = "General.Failure", string Description = "General Failure Has Occured")
            => new(Code, Description, ErrorType.Failure);
        public static Error Validation(string Code = "General.Validation", string Description = "General Validation Error Has Occured")
            => new(Code, Description, ErrorType.Validation);
        public static Error NotFound(string Code = "General.NotFound", string Description = "Resource NotFound")
            => new(Code, Description, ErrorType.NotFound);
        public static Error Conflict(string Code = "General.Conflict", string Description = "General Conflict Has Occured")
            => new(Code, Description, ErrorType.Conflict);
        public static Error Unauthorized(string Code = "General.Unauthorized", string Description = "Access Is Denied Due To Bad Authorzation")
            => new(Code, Description, ErrorType.Unauthorized);
        public static Error Forbidden(string Code = "General.Forbidden", string Description = "This Operation Is Forbidden")
            => new(Code, Description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string Code = "General.InvalidCredentials", string Description = "Provided Credentials Are Invalid")
            => new(Code, Description, ErrorType.InvalidCredentials);

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure=0,
        Validation=1,
        NotFound=2,
        Conflict=3,
        Unauthorized=4,
        Forbidden=5,
        InvalidCredentials=6,
    }
}